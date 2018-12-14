using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;
using Moq;
using UnitTests.Helpers;
using Xunit;namespace UnitTests.Actions
{
    public class InspectionsActionsTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        string fakeId;

        public InspectionsActionsTests()
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
    }
}
