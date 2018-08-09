using System;
using System.Net;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using Newtonsoft.Json.Linq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Controllers
{
    public class ElementControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<DataController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        DataController controller;
        int fakeId;
        string fakeDescription;

        public ElementControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<DataController>>();
            fakeAsbestosService = new Mock<IAsbestosService>();

            fakeId = Fake.GenerateRandomId(6);
            fakeDescription = Fake.GenerateRandomText();
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var fakeResponse = Fake.GenerateElement(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = await controller.GetElement(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("A123456")]
        [InlineData("1!23456")]
        public async Task return_400_for_invalid_request(string elementId)
        {
            controller = SetupControllerWithSimpleService();
            var response = await controller.GetElement(elementId);

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            Element fakeEmptyResponse = null;
            controller = SetupControllerWithServiceReturningFakeObject(fakeEmptyResponse);
            var response = await controller.GetElement(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var fakeResponse = Fake.GenerateElement(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);

            var response = JObject.FromObject((await controller.GetElement(fakeId.ToString())).Value);
            var responseId = response["results"]["Id"].Value<int>();
            var responseDescription = response["results"]["Description"];

            Assert.Equal(fakeId, responseId);
            Assert.Equal(fakeDescription, responseDescription);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("123A56")]
        [InlineData("1!23456")]
        public async Task return_error_message_if_elementid_is_not_valid(string elementId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetElement(elementId)).Value);

            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();
            var expectedUserMessage = "Please provide a valid element id";
            var expectedDeveloperMessage = "Invalid parameter - elementId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }

        private DataController SetupControllerWithSimpleService()
        {
            return new DataController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }

        private DataController SetupControllerWithServiceReturningFakeObject(Element fakeResponse)
        {
            fakeAsbestosService.Setup(m => m.GetElement(It.IsAny<string>()))
                               .Returns(Task.FromResult(fakeResponse));
            return new DataController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }
    }
}

