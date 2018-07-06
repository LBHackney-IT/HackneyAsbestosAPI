using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Controllers
{
    public class RoomControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<AsbestosController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        AsbestosController controller;

        public RoomControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<AsbestosController>>();

            // TODO PSI may not return a list of rooms
            var fakeResponse = new List<Room>()
            {
                { new Room() }
            };
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetRoom(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Room>>(fakeResponse));
            
            var controller = new AsbestosController(fakeAsbestosService.Object,
                                                    fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            // TODO investigate what would make a valid id
            var response = await controller.GetRoom("valid id");
            Assert.Equal(200, response.StatusCode);
        }

        // TODO add more theories
        [Theory]
        [InlineData("1")]
        [InlineData("abc")]
        public async Task return_400_for_invalid_request(string roomId)
        {
            var response = await controller.GetRoom(roomId);
            Assert.Equal(400, response.StatusCode);
        }
    }
}
