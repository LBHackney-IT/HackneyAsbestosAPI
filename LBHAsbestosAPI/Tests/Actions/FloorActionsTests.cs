using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using LBHAsbestosAPI.Tests.Helpers;
using Xunit;

namespace LBHAsbestosAPI.Tests.Actions
{
    public class FloorActionsTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        string fakeId;

        public FloorActionsTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeId = Fake.GenerateRandomId(6).ToString();
        }

        [Fact]
        public async Task return_type_should_be_floor()
        {
            var fakeResponse = new Floor();
            fakeAsbestosService
                .Setup(m => m.GetFloor(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetFloor(fakeId);

            Assert.True(response is Floor);
        }

        [Fact]
        public async Task get_floor_throws_expected_custom_exeption()
        {
            Floor fakeEmptyResponse = null;
            fakeAsbestosService
                .Setup(m => m.GetFloor(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeEmptyResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingFloorException>(
                async () => await asbestosAction.GetFloor(fakeId));
        }
    }
}