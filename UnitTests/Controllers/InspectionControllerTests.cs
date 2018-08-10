using System;
using System.Collections.Generic;
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
    public class InspectionControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<DataController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        DataController controller;
        int fakeId;
        string fakeDescription;

        public InspectionControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<DataController>>();
            fakeAsbestosService = new Mock<IAsbestosService>();

            fakeId = Fake.GenerateRandomId(8);
            fakeDescription = Fake.GenerateRandomText();
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var fakeResponse = Fake.GenerateInspection(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = await controller.GetInspection(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = await controller.GetInspection(propertyId);

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            var fakeEmptyResponse = new List<Inspection>();
            controller = SetupControllerWithServiceReturningFakeObject(fakeEmptyResponse);
            var response = await controller.GetInspection(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var fakeResponse = Fake.GenerateInspection(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = JObject.FromObject((await controller.GetInspection(fakeId.ToString())).Value);
            var responseId = response["results"][0]["Id"];
            var responseLocationDescription = response["results"][0]["LocationDescription"];

            Assert.Equal(fakeId, responseId);
            Assert.Equal(fakeDescription, responseLocationDescription);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_error_message_if_propertyid_is_not_valid(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetInspection(propertyId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid inspection id";
            var expectedDeveloperMessage = "Invalid parameter - inspectionId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }

        [Fact]
        public async Task response_has_the_valid_format_if_request_successful()
        {
            var fakeResponse = Fake.GenerateInspection(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = JObject.FromObject((await controller.GetInspection(fakeId.ToString())).Value);
            Assert.NotNull(response["results"]);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task response_has_the_valid_format_if_request_unsuccessful(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetInspection(propertyId)).Value);
            Assert.NotNull(response["errors"]);
        }

        private DataController SetupControllerWithSimpleService()
        {
            return new DataController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }

        private DataController SetupControllerWithServiceReturningFakeObject(IEnumerable<Inspection> fakeResponse)
        {
            fakeAsbestosService.Setup(m => m.GetInspection(It.IsAny<string>()))
                               .Returns(Task.FromResult(fakeResponse));
            return new DataController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }
    }
}