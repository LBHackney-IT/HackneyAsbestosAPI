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
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Services
{
    public class AsbestosServicesTests
    {
        Mock<ILoggerAdapter<AsbestosService>> fakeLogger;
        Mock<ILoggerAdapter<Psi2000Api>> fakePsiLogger;
        IAsbestosService asbestosService;
        int fakeId;
        string fakeDescription;

        public AsbestosServicesTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosService>>();
            fakePsiLogger = new Mock<ILoggerAdapter<Psi2000Api>>();

            fakeId = Fake.GenerateRandomId(6);
            fakeDescription = Fake.GenerateRandomText();
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
                Id = fakeId,
                LocationDescription = fakeDescription
            });

            fakeRepository
                .Setup(m => m.GetInspections(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeInspectionResponse)); 

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetInspection(fakeId.ToString());

            Assert.Equal(fakeId, responseData.ElementAt(0).Id);
            Assert.Equal(fakeDescription, responseData.ElementAt(0).LocationDescription);
        }

        [Fact]
        public async Task can_access_floor_data_from_response()
        {
            Floor responseData;
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeFloorResponse = new FloorResponse()
            {
                Data = new Floor()
                {
                    Id = fakeId,
                    Description = fakeDescription
                }
            };

            fakeRepository
                .Setup(m => m.GetFloor(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeFloorResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetFloor(fakeId.ToString());

            Assert.Equal(fakeId, responseData.Id);
            Assert.Equal(fakeDescription, responseData.Description);
        }

        [Fact]
        public async Task can_access_room_data_from_response()
        {
            Room responseData;
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeRoomResponse = new RoomResponse()
            {
                Data = new Room()
                {
                    Id = fakeId,
                    Description = fakeDescription
                }
            };

            fakeRepository
                .Setup(m => m.GetRoom(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeRoomResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetRoom(fakeId.ToString());

            Assert.Equal(fakeId, responseData.Id);
            Assert.Equal(fakeDescription, responseData.Description);
        }

        [Fact]
        public async Task can_access_element_data_from_response()
        {
            Element responseData;
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeElementResponse = new ElementResponse()
            {
                Data = new Element()
                {
                    Id = fakeId,
                    Description = fakeDescription
                }
            };

            fakeRepository
                .Setup(m => m.GetElement(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeElementResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetElement("random string");

            Assert.Equal(fakeId, responseData.Id);
            Assert.Equal(fakeDescription, responseData.Description);
        }
    }
}
