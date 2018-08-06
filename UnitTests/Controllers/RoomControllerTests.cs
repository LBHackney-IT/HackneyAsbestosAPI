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
    public class RoomControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<AsbestosController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        AsbestosController controller;
        int fakeId;
        string fakeDescription;

        public RoomControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<AsbestosController>>();
            fakeAsbestosService = new Mock<IAsbestosService>();

            fakeId = Fake.GenerateRandomId(6);
            fakeDescription = Fake.GenerateRandomText();
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var fakeResponse = Fake.GenerateRoom(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = await controller.GetRoom(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("A123456")]
        [InlineData("1!23456")]
        public async Task return_400_for_invalid_request(string roomId)
        {
            controller = SetupControllerWithSimpleService();
            var response = await controller.GetRoom(roomId);

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            Room fakeEmptyResponse = null;
            controller = SetupControllerWithServiceReturningFakeObject(fakeEmptyResponse);
            var response = await controller.GetRoom(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var fakeResponse = Fake.GenerateRoom(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
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
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetRoom(roomId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();
            var expectedUserMessage = "Please provide a valid room id";
            var expectedDeveloperMessage = "Invalid parameter - roomId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1234 56")]
        [InlineData("123A56")]
        [InlineData("1!23456")]
        public async Task response_has_the_valid_format_if_request_unsuccessful(string roomId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetRoom(roomId)).Value);
            Assert.NotNull(response["errors"]);
        }

        private AsbestosController SetupControllerWithSimpleService()
        {
            return new AsbestosController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }

        private AsbestosController SetupControllerWithServiceReturningFakeObject(Room fakeResponse)
        {
            fakeAsbestosService.Setup(m => m.GetRoom(It.IsAny<string>()))
                               .Returns(Task.FromResult(fakeResponse));
            return new AsbestosController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }
    }
}
