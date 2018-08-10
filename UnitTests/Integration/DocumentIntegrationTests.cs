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
    public class DocumentIntegrationTests
    {
        readonly TestServer server;
        readonly HttpClient client;
        string baseUri;
        static string triggerExceptionId = "999999";
        static string triggerNotFoundId = "888888";

        public DocumentIntegrationTests()
        {
            server = new TestServer(new WebHostBuilder()
                                     .UseStartup<TestStartup>());
            client = server.CreateClient();
            baseUri = "api/v1/document/";
        }
         
        #region photo by inspectionId endpoint
        [Fact]
        public async Task return_200_for_valid_photoByInspectionId_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "photo?" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_photoByInspectionId_request(string inspectionId)
        {
            var result = await client.GetAsync(baseUri + "photo?" + inspectionId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_photoByInspectionId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "photo?" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_photoByInspectionId()
        {
            var result = await client.GetAsync(baseUri + "photo?" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_photoByInspectionId_requests()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetStringAsync(baseUri + "photo?" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_photoByInspectionId_requests(string inspectionId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid inspection id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - inspectionId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "photo?" + inspectionId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
        #endregion

        #region mainphoto by inspectionid
        [Fact]
        public async Task return_200_for_valid_main_photoByInspectionId_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "mainphoto?" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_main_photoByInspectionId_request(string inspectionId)
        {
            var result = await client.GetAsync(baseUri + "mainphoto?" + inspectionId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_main_photoByInspectionId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "mainphoto?" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_main_photoByInspectionId()
        {
            var result = await client.GetAsync(baseUri + "mainphoto?" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_main_photoByInspectionId_requests()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetStringAsync(baseUri + "mainphoto?" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_main_photoByInspectionId_requests(string inspectionId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid inspection id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - inspectionId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "mainphoto?" + inspectionId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }

        #endregion

        #region report by inspectionid
        [Fact]
        public async Task return_200_for_valid_reportByInspectionId_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "report?" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_drawingByInspectionId_request(string inspectionId)
        {
            var result = await client.GetAsync(baseUri + "report?" + inspectionId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_reportByInspectionId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "report?" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_reportByInspectionId()
        {
            var result = await client.GetAsync(baseUri + "report?" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_reportByInspectionId_requests()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetStringAsync(baseUri + "report?" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_reportByInspectionId_requests(string inspectionId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid inspection id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - inspectionId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "report?" + inspectionId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
#endregion

        #region drawing by inspectionid
        [Fact]
        public async Task return_200_for_valid_drawingByInspectionId_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + "drawing?" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_reportByInspectionId_request(string inspectionId)
        {
            var result = await client.GetAsync(baseUri + "drawing?" + inspectionId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_drawingByInspectionId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "drawing?" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_drawingByInspectionId()
        {
            var result = await client.GetAsync(baseUri + "drawing?" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_drawingByInspectionId_requests()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetStringAsync(baseUri + "drawing?" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_drawingByInspectionId_requests(string inspectionId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid inspection id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - inspectionId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "drawing?" + inspectionId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
        #endregion

        private string GetexpectedStringResult()
        {
            var expectedResultContent = new Document()
            {
                Id = 4532,
                Description = "A picture"
            };

            var expectedResult = new Dictionary<string, IEnumerable<Document>>()
            {
                { "results", new List<Document>()
                    {
                        {expectedResultContent}
                    }}
            };

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var expectedStringResult = JsonConvert.SerializeObject(expectedResult, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });

            return expectedStringResult;
        }
    }
}
