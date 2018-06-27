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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("1")]
        [InlineData("123456789")]
        [InlineData("ABC")]
        [InlineData("A1234567")]
        [InlineData("?1234567")]
        public async Task data_is_null_if_inspectionid_is_not_valid(string inspectionId)
        {
            var fakeResponse = new List<Inspection>();
            fakeResponse.Add(new Inspection()
            {
                Id = 433,
                LocationDescription = "this Inspection should not be returned"
            });

            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));
            
            AsbestosController controller = new AsbestosController(fakeAsbestosService.Object);
            ObjectResult response = (ObjectResult)await controller.GetInspection(inspectionId);

            Assert.Null(response.Value);
        }

        [Fact]
        public async Task data_is_not_null_if_inspectionid_is_valid()
        {
            var fakeResponse = new List<Inspection>();
            fakeResponse.Add(new Inspection()
            {
                Id = 433,
                LocationDescription = "this Inspection should be returned"
            });

            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            var validInspectionId = "12345678";
            AsbestosController controller = new AsbestosController(fakeAsbestosService.Object);
            ObjectResult response = (ObjectResult)await controller.GetInspection(validInspectionId);

            Assert.NotNull(response.Value);
        }
    }
}