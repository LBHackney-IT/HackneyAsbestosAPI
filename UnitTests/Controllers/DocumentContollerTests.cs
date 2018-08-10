using System;
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
    public class DocumentContollerTests
    {
        Mock<ILoggerAdapter<AsbestosActions>> fakeActionsLogger;
        Mock<ILoggerAdapter<DocumentController>> fakeControllerLogger;
        Mock<IAsbestosService> fakeAsbestosService;
        DocumentController controller;
        int fakeId;
        string fakeDescription;

        public DocumentContollerTests()
        {
            fakeActionsLogger = new Mock<ILoggerAdapter<AsbestosActions>>();
            fakeControllerLogger = new Mock<ILoggerAdapter<DocumentController>>();
            fakeAsbestosService = new Mock<IAsbestosService>();

            fakeId = Fake.GenerateRandomId(5);
            fakeDescription = Fake.GenerateRandomText();
        }

        #region photo
        [Fact]
        public async Task return_200_for_valid_photo_request()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = await controller.GetPhotoByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_photo_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = await controller.GetPhotoByInspectionId(propertyId);

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_photo_request_is_successful_but_no_results()
        {
            var fakeEmptyResponse = new List<Document>();
            controller = SetupControllerWithServiceReturningFakeObject(fakeEmptyResponse);
            var response = await controller.GetPhotoByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_photo_request_successful()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = JObject.FromObject((await controller.GetPhotoByInspectionId(fakeId.ToString())).Value);
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
        public async Task return_error_message_if_inspectionid_is_not_valid_for_photo_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetPhotoByInspectionId(propertyId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid inspection id";
            var expectedDeveloperMessage = "Invalid parameter - inspectionId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }
        #endregion

        #region mainphoto
        [Fact]
        public async Task return_200_for_valid_main_photo_request()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = await controller.GetMainPhotoByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_main_photo_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = await controller.GetMainPhotoByInspectionId(propertyId);

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_main_photo_request_is_successful_but_no_results()
        {
            var fakeEmptyResponse = new List<Document>();
            controller = SetupControllerWithServiceReturningFakeObject(fakeEmptyResponse);
            var response = await controller.GetMainPhotoByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_main_photo_request_successful()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = JObject.FromObject((await controller.GetMainPhotoByInspectionId(fakeId.ToString())).Value);
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
        public async Task return_error_message_if_inspectionid_is_not_valid_for_main_photo_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetMainPhotoByInspectionId(propertyId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid inspection id";
            var expectedDeveloperMessage = "Invalid parameter - inspectionId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }
        #endregion

        #region drawing
        [Fact]
        public async Task return_200_for_valid_drawing_request()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = await controller.GetDrawingByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_drawing_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = await controller.GetDrawingByInspectionId(propertyId);

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_drawing_request_is_successful_but_no_results()
        {
            var fakeEmptyResponse = new List<Document>();
            controller = SetupControllerWithServiceReturningFakeObject(fakeEmptyResponse);
            var response = await controller.GetDrawingByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_drawing_request_successful()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = JObject.FromObject((await controller.GetDrawingByInspectionId(fakeId.ToString())).Value);
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
        public async Task return_error_message_if_inspectionid_is_not_valid_for_drawing_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetDrawingByInspectionId(propertyId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid inspection id";
            var expectedDeveloperMessage = "Invalid parameter - inspectionId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }
        #endregion

        #region report
        [Fact]
        public async Task return_200_for_valid_report_request()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = await controller.GetReportByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_report_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = await controller.GetReportByInspectionId(propertyId);

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task return_404_if_report_request_is_successful_but_no_results()
        {
            var fakeEmptyResponse = new List<Document>();
            controller = SetupControllerWithServiceReturningFakeObject(fakeEmptyResponse);
            var response = await controller.GetReportByInspectionId(fakeId.ToString());

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task response_has_valid_content_if_report_request_successful()
        {
            var fakeResponse = Fake.GenerateDocument(fakeId, fakeDescription);
            controller = SetupControllerWithServiceReturningFakeObject(fakeResponse);
            var response = JObject.FromObject((await controller.GetReportByInspectionId(fakeId.ToString())).Value);
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
        public async Task return_error_message_if_inspectionid_is_not_valid_for_report_request(string propertyId)
        {
            controller = SetupControllerWithSimpleService();
            var response = JObject.FromObject((await controller.GetReportByInspectionId(propertyId)).Value);
            var userMessage = response["errors"].First["userMessage"].ToString();
            var developerMessage = response["errors"].First["developerMessage"].ToString();

            var expectedUserMessage = "Please provide a valid inspection id";
            var expectedDeveloperMessage = "Invalid parameter - inspectionId";

            Assert.Equal(expectedUserMessage, userMessage);
            Assert.Equal(expectedDeveloperMessage, developerMessage);
        }
        #endregion

        private DocumentController SetupControllerWithSimpleService()
        {
            return new DocumentController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }

        private DocumentController SetupControllerWithServiceReturningFakeObject(IEnumerable<Document> fakeResponse)
        {
            fakeAsbestosService.Setup(m => m.GetDocument(It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(Task.FromResult(fakeResponse));
            return new DocumentController(fakeAsbestosService.Object, fakeControllerLogger.Object,
                                          fakeActionsLogger.Object);
        }
    }
}
