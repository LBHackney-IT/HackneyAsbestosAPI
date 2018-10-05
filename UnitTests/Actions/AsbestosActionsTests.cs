using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;
using Moq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Actions
{
    public class AsbestosActionsTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        string fakeId;

        public AsbestosActionsTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeId = Fake.GenerateRandomId(6).ToString();
        }

        [Fact]
        public async Task return_type_should_be_list_of_inspections()
        {
            var fakeResponse = new List<Inspection>()
            {
                new Inspection()
            };

            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetInspection(fakeId);

            Assert.True(response is List<Inspection>);
        }

        [Fact]
        public async Task get_inspection_throws_expected_custom_exeption()
        {
            var fakeEmptyResponse = new List<Inspection>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeEmptyResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingInspectionException>(
                async () => await asbestosAction.GetInspection(fakeId));
        }

        [Fact]
        public async Task return_type_should_be_room()
        {
            var fakeResponse = new Room();
            fakeAsbestosService
                .Setup(m => m.GetRoom(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetRoom(fakeId);

            Assert.True(response is Room);
        }

        [Fact]
        public async Task get_room_throws_expected_custom_exeption()
        {
            Room fakeEmptyResponse = null;
            fakeAsbestosService
                .Setup(m => m.GetRoom(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeEmptyResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingRoomException>(
                async () => await asbestosAction.GetRoom(fakeId));
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

        [Fact]
        public async Task return_type_should_be_element()
        {
            var fakeResponse = new Element();
            fakeAsbestosService
                .Setup(m => m.GetElement(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetElement(fakeId);

            Assert.True(response is Element);
        }

        [Fact]
        public async Task get_element_throws_expected_custom_exeption()
        {
            Element fakeEmptyResponse = null;
            fakeAsbestosService
                .Setup(m => m.GetElement(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeEmptyResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingElementException>(
                async () => await asbestosAction.GetElement(fakeId));
        }

        #region photo
        [Fact]
        public async Task get_photo_return_type_should_be_filecontainer()
        {
            FileContainer fakeResponse = Fake.GenerateFakeFile(It.IsAny<string>());
            fakeAsbestosService
                .Setup(m => m.GetPhoto(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetPhoto(fakeId);

            Assert.True(response is FileContainer);
        }

        [Fact]
        public async Task get_photo_throws_expected_custom_exeption()
        {
            var fakeResponse = new FileContainer();
            fakeAsbestosService
                .Setup(m => m.GetPhoto(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingFileException>(
                async () => await asbestosAction.GetPhoto(fakeId));
        }

        [Fact]
        public async Task get_photo_documents_return_type_should_be_list_of_documents()
        {
            var fakeResponse = Fake.GenerateDocument(123, null);
            fakeAsbestosService
                .Setup(m => m.GetPhotoDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetPhotoDocuments(fakeId);

            Assert.True(response is List<Document>);
        }

        [Fact]
        public async Task get_photo_documents_throws_expected_custom_exeption()
        {
            IEnumerable<Document> fakeResponse = new List<Document>();
            fakeAsbestosService
                .Setup(m => m.GetPhotoDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingDocumentException>(
                async () => await asbestosAction.GetPhotoDocuments(fakeId));
        }
        #endregion photo

        #region main photo
        [Fact]
        public async Task get_main_photo_return_type_should_be_filecontainer()
        {
            var fakeResponse = Fake.GenerateFakeFile(fakeId);
            fakeAsbestosService
                .Setup(m => m.GetMainPhoto(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetMainPhoto(fakeId);

            Assert.True(response is FileContainer);
        }

        [Fact]
        public async Task get_main_photo_throws_expected_custom_exeption()
        {
            var fakeResponse = new FileContainer();
            fakeAsbestosService
                .Setup(m => m.GetMainPhoto(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingFileException>(
                async () => await asbestosAction.GetMainPhoto(fakeId));
        }

        [Fact]
        public async Task get_main_photo_documents_return_type_should_be_list_of_documents()
        {
            var fakeResponse = Fake.GenerateDocument(123, null);
            fakeAsbestosService
                .Setup(m => m.GetMainPhotoDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetMainPhotoDocuments(fakeId);

            Assert.True(response is List<Document>);
        }

        [Fact]
        public async Task get_main_photo_documents_throws_expected_custom_exeption()
        {
            IEnumerable<Document> fakeResponse = new List<Document>();
            fakeAsbestosService
                .Setup(m => m.GetMainPhotoDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingDocumentException>(
                async () => await asbestosAction.GetMainPhotoDocuments(fakeId));
        }
        #endregion main photo

        #region report
        [Fact]
        public async Task get_report_return_type_should_be_filecontainer()
        {
            var fakeResponse = Fake.GenerateFakeFile(fakeId);
            fakeAsbestosService
                .Setup(m => m.GetReport(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetReport(fakeId);

            Assert.True(response is FileContainer);
        }

        [Fact]
        public async Task get_report_throws_expected_custom_exeption()
        {
            var fakeResponse = new FileContainer();
            fakeAsbestosService
                .Setup(m => m.GetReport(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingFileException>(
                async () => await asbestosAction.GetReport(fakeId));
        }

        [Fact]
        public async Task get_report_documents_return_type_should_be_list_of_documents()
        {
            var fakeResponse = Fake.GenerateDocument(123, null);
            fakeAsbestosService
                .Setup(m => m.GetReportDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetReportDocuments(fakeId);

            Assert.True(response is List<Document>);
        }

        [Fact]
        public async Task get_report_documents_throws_expected_custom_exeption()
        {
            IEnumerable<Document> fakeResponse = new List<Document>();
            fakeAsbestosService
                .Setup(m => m.GetReportDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingDocumentException>(
                async () => await asbestosAction.GetReportDocuments(fakeId));
        }
        #endregion report

        #region drawing
        [Fact]
        public async Task get_drawing_return_type_should_be_filecontainer()
        {
            var fakeResponse = Fake.GenerateFakeFile(fakeId);
            fakeAsbestosService
                .Setup(m => m.GetDrawing(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetDrawing(fakeId);

            Assert.True(response is FileContainer);
        }

        [Fact]
        public async Task get_drawing_throws_expected_custom_exeption()
        {
            var fakeResponse = new FileContainer();
            fakeAsbestosService
                .Setup(m => m.GetDrawing(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingFileException>(
                async () => await asbestosAction.GetDrawing(fakeId));
        }

        [Fact]
        public async Task get_drawing_documents_return_type_should_be_list_of_documents()
        {
            var fakeResponse = Fake.GenerateDocument(123, null);
            fakeAsbestosService
                .Setup(m => m.GetDrawingDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetDrawingDocuments(fakeId);

            Assert.True(response is List<Document>);
        }

        [Fact]
        public async Task get_drawing_documents_throws_expected_custom_exeption()
        {
            IEnumerable<Document> fakeResponse = new List<Document>();
            fakeAsbestosService
                .Setup(m => m.GetDrawingDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResponse));
            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);

            await Assert.ThrowsAsync<MissingDocumentException>(
                async () => await asbestosAction.GetDrawingDocuments(fakeId));
        }
        #endregion report
    }
}
