using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Integration
{
    public class AsbestosIntegrationTests
    {
        [Fact]
        public async Task return_200_for_valid_request()
        {
            var fakeResponse = new List<Inspection>();

            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            var controller = new AsbestosController(fakeAsbestosService.Object);

            var response = await controller.GetInspection("12345678");
            Assert.Equal(200, response.StatusCode);
        }

        [Theory]
        [InlineData("123456678")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("1")]
        [InlineData("123456789")]
        [InlineData("ABC")]
        [InlineData("A1234567")]
        [InlineData("?1234567")]
        public async Task return_400_for_invalid_request(string id)
        {
            var fakeResponse = new List<Inspection>();
            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            var controller = new AsbestosController(fakeAsbestosService.Object);
            var response = await controller.GetInspection(id);

            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            var fakeResponse = new List<Inspection>();
            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            var controller = new AsbestosController(fakeAsbestosService.Object);
            var response = await controller.GetInspection("00000000");

            Assert.Equal(404, response.StatusCode);
        }

        // Reference 00000000 returns no results
        [Fact]
        public async Task return_400_if_request_is_successful_but_no_results()
        {
            // TODO
            Assert.True(false);
        }

        [Fact]
        public async Task return_500_when_internal_server_error()
        {
            // TODO
            Assert.True(false);
        }
    }
}
