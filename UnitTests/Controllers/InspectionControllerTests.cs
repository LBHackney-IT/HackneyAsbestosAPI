﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Moq;
using Newtonsoft.Json.Linq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Controllers
{
    public class InspectionControllerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<AsbestosController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        AsbestosController controller;
        int fakeId;
        string fakeDescription;

        public InspectionControllerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<AsbestosController>>();

            fakeId = Fake.GenerateRandomId(8);
            fakeDescription = Fake.GenerateRandomText();

            var fakeResponse = new List<Inspection>()
            {
                { new Inspection()
                    {
                        Id = fakeId,
                        LocationDescription = fakeDescription
                    }}
            };

            fakeAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeResponse));

            controller = new AsbestosController(fakeAsbestosService.Object, 
                                                    fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var response = await controller.GetInspection(fakeId.ToString());
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_request(string propertyId)
        {
            var response = await controller.GetInspection(propertyId);
            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            var fakeEmptyResponse = new List<Inspection>();
            var fakeCustomAsbestosService = new Mock<IAsbestosService>();
            fakeAsbestosService
                .Setup(m => m.GetInspection(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Inspection>>(fakeEmptyResponse));

            var CustomController = new AsbestosController(fakeCustomAsbestosService.Object, fakeControllerLogger.Object,
                                                    fakeActionsLogger.Object);
            var response = await CustomController.GetInspection(fakeId.ToString());
            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_request_successful()
        {
            var response = JObject.FromObject((await controller.GetInspection(fakeId.ToString())).Value);
            var responseId = response["results"][0]["Id"];
            var responseLocationDescription = response["results"][0]["LocationDescription"];

            Assert.Equal(fakeId, responseId);
            Assert.Equal(fakeDescription, responseLocationDescription);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_error_message_if_inspectionid_is_not_valid(string propertyId)
        {
            var response = JObject.FromObject((await controller.GetInspection(propertyId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid inspection id";
            var expectedDeveloperMessage = "Invalid parameter - inspectionId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }

        [Fact]
        public async Task response_has_the_valid_format_if_request_successful()
        {
            var response = JObject.FromObject((await controller.GetInspection(fakeId.ToString())).Value);
            Assert.NotNull(response["results"]);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task response_has_the_valid_format_if_request_unsuccessful(string propertyId)
        {
            var response = JObject.FromObject((await controller.GetInspection(propertyId)).Value);
            Assert.NotNull(response["errors"]);
        }
    }
}