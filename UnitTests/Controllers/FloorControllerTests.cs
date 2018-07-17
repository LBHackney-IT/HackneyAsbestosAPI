using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace UnitTests.Controllers
{
    public class FloorControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<AsbestosController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        readonly AsbestosController controller;

        public FloorControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<AsbestosController>>();

            var fakeResponse = new Floor()
            {
                Id = 4567,
                Description = "Fifth"
            };

            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetFloor(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            controller = new AsbestosController(fakeAsbestosService.Object,
                                                    fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var response = await controller.GetFloor("123456");
            Assert.Equal(200, response.StatusCode);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("A123456")]
        [InlineData("1!23456")]
        public async Task return_400_for_invalid_request(string floorId)
        {
            var response = await controller.GetFloor(floorId);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            Floor fakeEmptyResponse = null;

            var fakeCustomAsbestosService = new Mock<IAsbestosService>();
            fakeCustomAsbestosService
                .Setup(m => m.GetFloor(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeEmptyResponse));

            var customController = new AsbestosController(fakeCustomAsbestosService.Object,
                                                        fakeControllerLogger.Object,
                                                           fakeActionsLogger.Object);
            var response = await customController.GetFloor("000000");
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var response = JObject.FromObject((await controller.GetFloor("123456")).Value);
            var responseId = response["results"]["Id"].Value<int>();
            var responseDescription = response["results"]["Description"];

            Assert.Equal(4567, responseId);
            Assert.Equal("Fifth", responseDescription);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("123A56")]
        [InlineData("1!23456")]
        public async Task return_error_message_if_floorid_is_not_valid(string floorId)
        {
            var response = JObject.FromObject((await controller.GetFloor(floorId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid floor id";
            var expectedDeveloperMessage = "Invalid parameter - floorId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }

        [Fact]
        public async Task response_has_the_valid_format_if_request_successful()
        {
            var response = JObject.FromObject((await controller.GetFloor("123456")).Value);
            Assert.NotNull(response["results"]);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("123A56")]
        [InlineData("1!23456")]
        public async Task response_has_the_valid_format_if_request_unsuccessful(string floorId)
        {
            var response = JObject.FromObject((await controller.GetFloor(floorId)).Value);
            Assert.NotNull(response["errors"]);
        }
    }
}

