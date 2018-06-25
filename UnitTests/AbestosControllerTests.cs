using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests
{
    public class AbestosControllerTests
    {
        [Fact]
        public async Task return_200_for_valid_request()
        {
            var fakeResponse = new List<Inspection>();

            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            AsbestosController controller = new AsbestosController(fakeAsbestosService.Object);

            ObjectResult response = (ObjectResult)await controller.GetInspection("12345678");
            Assert.Equal(200, response.StatusCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("123456789")]
        [InlineData("A2345678")]
        [InlineData("?2345678")]
        public async Task return_400_for_invalid_request(string id)
        {
            var fakeResponse = new List<Inspection>();

            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            AsbestosController controller = new AsbestosController(fakeAsbestosService.Object);

            ObjectResult response = (ObjectResult)await controller.GetInspection(id);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error()
        {
            // TODO
            Assert.True(false);
        }

        [Fact]
        public async Task return_a_json_object_for_valid_request()
        {
            // TODO
            Assert.True(false);
        }
    }
}