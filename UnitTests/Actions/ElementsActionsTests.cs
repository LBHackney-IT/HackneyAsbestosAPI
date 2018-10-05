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
    public class ElementsActionsTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        string fakeId;

        public ElementsActionsTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeId = Fake.GenerateRandomId(6).ToString();
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
    }
}
