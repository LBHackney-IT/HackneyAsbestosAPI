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
        }

        [Fact]
        public async Task can_access_inspection_data_from_response()
        {
            IEnumerable<Inspection> responseData;

            // Case for the test running isolated from the other tests
            // repository is mocked in this function
            if (!TestStatus.IsRunningTests)
            {
                var fakeRepository = new Mock<IPsi2000Api>();
                var fakeInspectionResponse = new InspectionResponse()
                {
                    Data = new List<Inspection>()
                };

                fakeInspectionResponse.Data.Add(new Inspection()
                {
                    Id = 655,
                    LocationDescription = "A house"
                });

                fakeRepository
                    .Setup(m => m.GetInspections(It.IsAny<string>()))
                    .Returns(Task.FromResult(fakeInspectionResponse)); 

                asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            }
            // Case for the test running in conjunction with the solution tests.
            // The asbestos service uses the fakePsi2000Api repository.
            else
            {
                asbestosService = new AsbestosService(new Psi2000Api(fakePsiLogger.Object), fakeLogger.Object);
            }

            responseData = await asbestosService.GetInspection("random string");

            Assert.Equal(655, responseData.ElementAt(0).Id);
            Assert.Equal("A house", responseData.ElementAt(0).LocationDescription);
        }

        [Fact]
        public async Task can_access_room_data_from_response()
        {
            Room responseData;

            // Case fof the test running isolated from the other tests
            // repository is mocked in this function
            if (!TestStatus.IsRunningTests)
            {
                var fakeRepository = new Mock<IPsi2000Api>();
                var fakeRoomResponse = new RoomResponse()
                {
                    Data = new Room()
                };

                fakeRoomResponse.Data = new Room()
                {
                    Id = 5003,
                    Description = "Ground Floor"
                };

                fakeRepository
                    .Setup(m => m.GetRoom(It.IsAny<string>()))
                    .Returns(Task.FromResult(fakeRoomResponse));

                asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            }
            // Case for the test running in conjunction with the solution tests.
            // The asbestos service uses the fakePsi2000Api repository.
            else
            {
                asbestosService = new AsbestosService(new Psi2000Api(fakePsiLogger.Object), fakeLogger.Object);
            }

            responseData = await asbestosService.GetRoom("random string");
            Assert.Equal(5003, responseData.Id);
            Assert.Equal("Ground Floor", responseData.Description);
        }

        [Fact]
        public async Task can_access_floor_data_from_response()
        {
            Floor responseData;

            // Case fof the test running isolated from the other tests
            // repository is mocked in this function
            if (!TestStatus.IsRunningTests)
            {
                var fakeRepository = new Mock<IPsi2000Api>();
                var fakeFloorResponse = new FloorResponse()
                {
                    Data = new Floor()
                };

                fakeFloorResponse.Data = new Floor()
                {
                    Id = 3434,
                    Description = "First Floor"
                };

                fakeRepository
                    .Setup(m => m.GetFloor(It.IsAny<string>()))
                    .Returns(Task.FromResult(fakeFloorResponse));

                asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            }
            // Case for the test running in conjunction with the solution tests.
            // The asbestos service uses the fakePsi2000Api repository.
            else
            {
                asbestosService = new AsbestosService(new Psi2000Api(fakePsiLogger.Object), fakeLogger.Object);
            }

            responseData = await asbestosService.GetFloor("random string");
            Assert.Equal(3434, responseData.Id);
            Assert.Equal("First Floor", responseData.Description);
        }
    }
}
