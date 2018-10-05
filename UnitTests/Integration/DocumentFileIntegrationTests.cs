using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LBHAsbestosAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using LBHAsbestosAPI.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Text;
using UnitTests.Helpers;

namespace UnitTests.Integration
{
    public class DocumentFileIntegrationTests
    {
        readonly TestServer server;
        readonly HttpClient client;
        string baseUri;
        static string triggerExceptionId = "999999"; 
        static string triggerNotFoundId = "888888"; 

        public DocumentFileIntegrationTests()
        {
            server = new TestServer(new WebHostBuilder()
                                     .UseStartup<TestStartup>());
            client = server.CreateClient();
            baseUri = "api/v1/documents/";
        }

        #region photo endpoint
        [Fact]
        public async Task return_file_for_valid_photo_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "photos/" + randomId);
            var fileResult = result.Content.ReadAsByteArrayAsync().Result;

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(54, fileResult.Length);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_photo_request(string photoId)
        {
            var result = await client.GetAsync(baseUri + "photos/" + photoId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_photo_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "photos/" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_photo_request()
        {
            var result = await client.GetAsync(baseUri + "photos/" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }
        #endregion

        #region main photo endpoint
        [Fact]
        public async Task return_file_for_valid_main_photo_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "mainphotos/" + randomId);
            var fileResult = result.Content.ReadAsByteArrayAsync().Result;

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(54, fileResult.Length);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_main_photo_request(string mainPhotoId)
        {
            var result = await client.GetAsync(baseUri + "mainphotos/" + mainPhotoId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_main_photo_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "mainphotos/" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_main_photo_request()
        {
            var result = await client.GetAsync(baseUri + "mainphotos/" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }
        #endregion

        #region drawing endpoint
        [Fact]
        public async Task return_file_for_valid_drawing_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "drawings/" + randomId);
            var fileResult = result.Content.ReadAsByteArrayAsync().Result;

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(54, fileResult.Length);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_drawing_request(string mainDrawingId)
        {
            var result = await client.GetAsync(baseUri + "drawings/" + mainDrawingId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_drawing_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "drawings/" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_drawing_request()
        {
            var result = await client.GetAsync(baseUri + "drawings/" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }
        #endregion

        #region report endpoint
        [Fact]
        public async Task return_file_for_valid_report_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "reports/" + randomId);
            var fileResult = result.Content.ReadAsByteArrayAsync().Result;

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(54, fileResult.Length);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_report_request(string reportId)
        {
            var result = await client.GetAsync(baseUri + "reports/" + reportId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_report_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "reports/" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_report_request()
        {
            var randomBadId = Fake.GenerateRandomId(4);
            var result = await client.GetAsync(baseUri + "reports/" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }
        #endregion
    }
}
