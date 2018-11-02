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
    public class ReportsActionsTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        string fakeId;

        public ReportsActionsTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeId = Fake.GenerateRandomId(6).ToString();
        }

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
    }
}

