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

namespace UnitTests
{
    public class AsbestosServicesTests
    {
        Mock<ILoggerAdapter<AsbestosService>> fakeLogger;
        Mock<ILoggerAdapter<Psi2000Api>> fakePsiLogger;

        public AsbestosServicesTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosService>>();
            fakePsiLogger = new Mock<ILoggerAdapter<Psi2000Api>>();
        }

        // TODO refractoring the two methods below
        [Fact]
        public async Task can_access_inspection_data_from_response()
        {
            IEnumerable<Inspection> responseData;
            IAsbestosService asbestosService;

            // Case for the test running isolated from the other tests
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
            IAsbestosService asbestosService;

            // Case fof the test running isolated from the other thests
            if (!TestStatus.IsRunningTests)
            {
                var fakeRepository = new Mock<IPsi2000Api>();
                var fakeRoomResponse = new RoomResponse()
                {
                    Data = new Room()
                };

                fakeRoomResponse.Data = new Room()
                {
                    Id = 655655,
                    Description = "Third floor"
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
            Assert.Equal(655655, responseData.Id);
            Assert.Equal("Third floor", responseData.Description);
        }
    }
}
