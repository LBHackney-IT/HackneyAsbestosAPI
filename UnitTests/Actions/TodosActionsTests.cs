using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Actions
{
    public class TodosActionsTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        string fakeId;

        public TodosActionsTests()
        {
            fakeLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeId = Fake.GenerateRandomId(6).ToString();
        }

        [Fact]
        public async Task GetTodoByPropertyId_return_type_should_be_list_of_todos()
        {
            var fakeResponse = new List<Todo>
            {
                new Todo()
            };

            fakeAsbestosService
                .Setup(m => m.GetTodosByPropertyId(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Todo>>(fakeResponse));

            var asbestosAction = new AsbestosActions(fakeAsbestosService.Object, fakeLogger.Object);
            var response = await asbestosAction.GetTodosByPropertyId(fakeId);

            Assert.True(response is List<Todo>);
        }
    }
}
