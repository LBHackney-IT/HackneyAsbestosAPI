using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;
using LBHAsbestosAPI.Services;
using Moq;
using Xunit;

namespace UnitTests.Services
{
    public class AsbestosServicesTests
    {
        Mock<ILoggerAdapter<AsbestosService>> fakeLogger;
        Mock<ILoggerAdapter<Psi2000Api>> fakePsiLogger;
        IAsbestosService asbestosService;

        public AsbestosServicesTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosService>>();
            fakePsiLogger = new Mock<ILoggerAdapter<Psi2000Api>>();
            TestStatus.IsRunningTests = false;
        }

        [Fact]
        public async Task can_access_inspection_data_from_response()
        {
            IEnumerable<Inspection> responseData;

            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeInspectionResponse = new InspectionResponse()
            {
                Data = new List<Inspection>()
            };

            fakeInspectionResponse.Data.Add(new Inspection()
            {
                Id = 6555,
                LocationDescription = "A houser"
            });

            fakeRepository
                .Setup(m => m.GetInspections(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeInspectionResponse)); 

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetInspection("random string");

            Assert.Equal(6555, responseData.ElementAt(0).Id);
            Assert.Equal("A houser", responseData.ElementAt(0).LocationDescription);
        }

        [Fact]
        public async Task can_access_floor_data_from_response()
        {
            Floor responseData;
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeFloorResponse = new FloorResponse()
            {
                Data = new Floor()
            };

            fakeFloorResponse.Data = new Floor()
            {
                Id = 34344,
                Description = "Firsto Floor"
            };

            fakeRepository
                .Setup(m => m.GetFloor(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeFloorResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetFloor("random string");

            Assert.Equal(34344, responseData.Id);
            Assert.Equal("Firsto Floor", responseData.Description);
        }

        [Fact]
        public async Task can_access_room_data_from_response()
        {
            Room responseData;
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeRoomResponse = new RoomResponse()
            {
                Data = new Room()
            };

            fakeRoomResponse.Data = new Room()
            {
                Id = 50034,
                Description = "Ground Floor 1"
            };

            fakeRepository
                .Setup(m => m.GetRoom(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeRoomResponse));
            bool test = TestStatus.IsRunningTests;
            TestStatus.IsRunningTests = false;
            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetRoom("random string");

            Assert.Equal(50034, responseData.Id);
            Assert.Equal("Ground Floor 1", responseData.Description);
        }
    }
}
