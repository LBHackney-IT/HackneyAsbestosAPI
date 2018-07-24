using System;
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
        Mock<ILoggerAdapter<AsbestosController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        readonly AsbestosController controller;
        int fakeId;
        string fakeDescription;

        public ElementControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<AsbestosController>>();

            fakeId = Fake.GenerateRandomId(6);
            fakeDescription = Fake.GenerateRandomText();

            var fakeResponse = new Element()
            {
                Id = fakeId,
                Description = fakeDescription
            };

            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetElement(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            controller = new AsbestosController(fakeAsbestosService.Object,
                                                    fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var response = await controller.GetElement(fakeId.ToString());
            Assert.Equal(200, response.StatusCode);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("A123456")]
        [InlineData("1!23456")]
        public async Task return_400_for_invalid_request(string elementId)
        {
            var response = await controller.GetElement(elementId);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            Element fakeEmptyResponse = null;

            var fakeCustomAsbestosService = new Mock<IAsbestosService>();
            fakeCustomAsbestosService
                .Setup(m => m.GetElement(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeEmptyResponse));

            var customController = new AsbestosController(fakeCustomAsbestosService.Object,
                                                        fakeControllerLogger.Object,
                                                           fakeActionsLogger.Object);
            var response = await customController.GetElement(fakeId.ToString());
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
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
            var response = JObject.FromObject((await controller.GetElement(elementId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid element id";
            var expectedDeveloperMessage = "Invalid parameter - elementId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }

        [Fact]
        public async Task response_has_the_valid_format_if_request_successful()
        {
            var response = JObject.FromObject((await controller.GetElement(fakeId.ToString())).Value);
            Assert.NotNull(response["results"]);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("123A56")]
        [InlineData("1!23456")]
        public async Task response_has_the_valid_format_if_request_unsuccessful(string elementId)
        {
            var response = JObject.FromObject((await controller.GetElement(elementId)).Value);
            Assert.NotNull(response["errors"]);
        }
    }
}

