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
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Controllers
{
    public class RoomControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<AsbestosController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        readonly AsbestosController controller;
        int fakeId;
        string fakeDescription;

        public RoomControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<AsbestosController>>();

            fakeId = Fake.GenerateRandomId(6);
            fakeDescription = Fake.GenerateRandomText();

            var fakeResponse = new Room()
            {
                Id = fakeId,
                Description = fakeDescription
            };
           
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetRoom(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            controller = new AsbestosController(fakeAsbestosService.Object,
                                                    fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var response = await controller.GetRoom(fakeId.ToString());
            Assert.Equal(200, response.StatusCode);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("A123456")]
        [InlineData("1!23456")]
        public async Task return_400_for_invalid_request(string roomId)
        {
            var response = await controller.GetRoom(roomId);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            Room fakeEmptyResponse = null;

            var fakeCustomAsbestosService = new Mock<IAsbestosService>();
            fakeCustomAsbestosService
                .Setup(m => m.GetRoom(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeEmptyResponse));

            var customController = new AsbestosController(fakeCustomAsbestosService.Object,
                                                        fakeControllerLogger.Object,
                                                           fakeActionsLogger.Object);
            var response = await customController.GetRoom(fakeId.ToString());
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var response = JObject.FromObject((await controller.GetRoom(fakeId.ToString())).Value);
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
        public async Task return_error_message_if_roomid_is_not_valid(string roomId)
        {
            var response = JObject.FromObject((await controller.GetRoom(roomId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid room id";
            var expectedDeveloperMessage = "Invalid parameter - roomId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }

        [Fact]
        public async Task response_has_the_valid_format_if_request_successful()
        {
            var response = JObject.FromObject((await controller.GetRoom(fakeId.ToString())).Value);
            Assert.NotNull(response["results"]);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("123A56")]
        [InlineData("1!23456")]
        public async Task response_has_the_valid_format_if_request_unsuccessful(string roomId)
        {
            var response = JObject.FromObject((await controller.GetRoom(roomId)).Value);
            Assert.NotNull(response["errors"]);
        }
    }
}
