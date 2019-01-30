using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;
using LBHAsbestosAPI.Services;
using Moq;
using LBHAsbestosAPI.Tests.Helpers;
using Xunit;

namespace LBHAsbestosAPI.Tests.Services
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
            var fakeInspectionResponse = new Response<IEnumerable<Inspection>>()
            {
                Data = new List<Inspection>()
                {
                    new Inspection()
                    {
                        Id = fakeId,
                        LocationDescription = fakeDescription
                    }
                }
            };

            fakeRepository
                .Setup(m => m.GetInspections(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeInspectionResponse)); 

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetInspection(fakeId.ToString());

            Assert.Equal(fakeId, responseData.FirstOrDefault().Id);
            Assert.Equal(fakeDescription, responseData.FirstOrDefault().LocationDescription);
        }

        [Fact]
        public async Task can_access_floor_data_from_response()
        {
            Floor responseData;
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeFloorResponse = new Response<Floor>()
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
            var fakeRoomResponse = new Response<Room>()
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
            var fakeElementResponse = new Response<Element>()
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

        [Fact]
        public async Task can_access_document_data_from_response()
        {
            IEnumerable<Document> responseData;

            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeDocumentResponse = new Response<IEnumerable<Document>>()
            {
                Data = new List<Document>
                {
                    new Document()
                    {
                        Id = fakeId,
                        Description = fakeDescription
                    }
                }
            };

            fakeRepository
                .Setup(m => m.GetDocuments(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(fakeDocumentResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            responseData = await asbestosService.GetPhotoDocuments(fakeId.ToString());

            Assert.Equal(fakeId, responseData.FirstOrDefault().Id);
            Assert.Equal(fakeDescription, responseData.FirstOrDefault().Description);
        }

        [Fact]
        public async Task can_access_todo_data_from_response()
        {
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeTodoResponse = new Response<Todo>()
            {
                Data = new Todo
                {
                    Id = fakeId,
                    Description = fakeDescription
                }

            };

            fakeRepository
                .Setup(m => m.GetTodo(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeTodoResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            var responseData = await asbestosService.GetTodo(fakeId.ToString());

            Assert.Equal(fakeId, responseData.Id);
            Assert.Equal(fakeDescription, responseData.Description);
        }

        [Fact]
        public async Task can_access_todo_data_from_GetTodoByPropertyReference_response()
        {
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeTodoResponse = new Response<IEnumerable<Todo>>()
            {
                Data = new List<Todo>
                {
                    new Todo
                    {
                        Id = fakeId,
                        Description = fakeDescription
                    }
                }
            };

            fakeRepository
                .Setup(m => m.GetTodosByPropertyId(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeTodoResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            var responseData = await asbestosService.GetTodosByPropertyId(fakeId.ToString());

            Assert.Equal(fakeId, responseData.FirstOrDefault().Id);
            Assert.Equal(fakeDescription, responseData.FirstOrDefault().Description);
        }

        [Fact]
        public async Task can_access_sample_data_from_response()
        {
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeTodoResponse = new Response<IEnumerable<Sample>>()
            {
                Data = new List<Sample>
                {
                    new Sample
                    {
                        Id = fakeId,
                        RefferedSample = fakeDescription
                    }
                }
            };

            fakeRepository
                .Setup(m => m.GetSamples(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeTodoResponse));

            asbestosService = new AsbestosService(fakeRepository.Object, fakeLogger.Object);
            var responseData = await asbestosService.GetSamples(fakeId.ToString());

            Assert.Equal(fakeId, responseData.FirstOrDefault().Id);
            Assert.Equal(fakeDescription, responseData.FirstOrDefault().RefferedSample);
        }
    }
}
