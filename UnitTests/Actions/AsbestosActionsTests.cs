using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using Xunit;

namespace UnitTests
{
    public class AsbestosActionsTests
    {
        [Fact]
        public async Task return_type_list_of_inspections()
        {
            var mockLogger = new Mock<ILoggerAdapter<AsbestosController>>();
            var fakeResponse = new List<Inspection>()
            {
                { new Inspection() }
            };

            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, mockLogger.Object);
            var response = await asbestosAction.GetInspection("Random string");

            Assert.IsType(typeof(List<Inspection>), response);
        }
    }
}
