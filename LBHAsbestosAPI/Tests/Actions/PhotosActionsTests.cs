using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;
using Moq;
using LBHAsbestosAPI.Tests.Helpers;
using Xunit;

namespace LBHAsbestosAPI.Tests.Actions
{
    public class PhotosActionsTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        string fakeId;

        public PhotosActionsTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeId = Fake.GenerateRandomId(6).ToString();
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
        #endregion main photo
    }
}
