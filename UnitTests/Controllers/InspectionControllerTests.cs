using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace UnitTests
{
    public class InspectionControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<AsbestosController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        AsbestosController controller;

        public InspectionControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<AsbestosController>>();

            var fakeResponse = new List<Inspection>()
            {
                new Inspection()
            };

            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            controller = new AsbestosController(fakeAsbestosService.Object, 
                                                    fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var response = await controller.GetInspection("12345678");
            Assert.Equal(200, response.StatusCode);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("123456789")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        public async Task return_400_for_invalid_request(string propertyId)
        {
            var response = await controller.GetInspection(propertyId);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            var fakeEmptyResponse = new List<Inspection>();
            var fakeCustomAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeEmptyResponse));

            var CustomController = new AsbestosController(fakeCustomAsbestosService.Object, fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
            var response = await CustomController.GetInspection("00000000");
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var fakeResponse = new List<Inspection>()
            {
                { new Inspection()
                    {
                        Id = 433,
                        LocationDescription = "Under the bridge"
                    }}
            };

            var fakeCustomAsbestosService = new Mock<IAsbestosService>();
            fakeCustomAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            var customController = new AsbestosController(fakeCustomAsbestosService.Object,
                                                        fakeControllerLogger.Object,
                                                           fakeActionsLogger.Object);

            var response = JObject.FromObject((await customController.GetInspection("12345678")).Value);
            var responseId = response["results"][0]["Id"];
            var responseLocationDescription = response["results"][0]["LocationDescription"];

            Assert.Equal(433, responseId);
            Assert.Equal("Under the bridge", responseLocationDescription);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("123456789")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        public async Task return_error_message_if_inspectionid_is_not_valid(string propertyId)
        {
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
            var response = JObject.FromObject((await controller.GetInspection("12345678")).Value);
            Assert.NotNull(response["results"]);
        }

        [Theory]
        //[InlineData("1")]
        //[InlineData("123456789")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        public async Task response_has_the_valid_format_if_request_unsuccessful(string propertyId)
        {
            var response = JObject.FromObject((await controller.GetInspection(propertyId)).Value);
            Assert.NotNull(response["errors"]);
        }
    }
}