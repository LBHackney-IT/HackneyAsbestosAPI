using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;
using LBHAsbestosAPI.Services;
using LBHAsbestosAPI.Validators;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            AsbestosController controller = new AsbestosController(fakeAsbestosService.Object);

            var response = await controller.GetInspection(id);
            Assert.Equal(400, response.StatusCode);
        }

        // Candidate for moving this to the integration tests - reference 00000000 returns
        // no results from PSI.
        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
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

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var fakeResponse = new List<Inspection>();
            fakeResponse.Add(new Inspection()
            {
                Id = 433,
                LocationDescription = "Under the bridge"
            });

            var fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            AsbestosController controller = new AsbestosController(fakeAsbestosService.Object);
            var response = JObject.FromObject((await controller.GetInspection("12345678")).Value);

            var responseId = response["results"][0]["Id"];
            var responseLocationDescription = response["results"][0]["LocationDescription"];

            Assert.Equal(433, responseId);
            Assert.Equal("Under the bridge", responseLocationDescription);
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
        public async Task return_error_message_if_inspectionid_is_not_valid(string inspectionId)
        {
            var fakeAsbestosService = new Mock<IAsbestosService>();
            var controller = new AsbestosController(fakeAsbestosService.Object);

            var response = JObject.FromObject((await controller.GetInspection(inspectionId)).Value);
            var userMessage = response["errors"]["userMessage"];
            var developerMessage = response["errors"]["developerMessage"];

            var expectedUserMessage = "Please provide a valid inspection number";
            var expectedDeveloperMessage = "Invalid parameter - inspectionId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
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
        public async Task response_has_the_valid_format_if_request_unsuccessful(string inspectionId)
        {
            var fakeAsbestosService = new Mock<IAsbestosService>();
            var controller = new AsbestosController(fakeAsbestosService.Object);
            var response = JObject.FromObject((await controller.GetInspection(inspectionId)).Value);

            Assert.NotNull(response["errors"]);
        }
    }
}