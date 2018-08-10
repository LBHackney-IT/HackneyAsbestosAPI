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
         
        #region photo by propertyId endpoint
        [Fact]
        public async Task return_200_for_valid_photoByPropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "photo?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_photoByPropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "photo?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_photoByPropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "photo?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_photoByPropertyId()
        {
            var result = await client.GetAsync(baseUri + "photo?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_photoByPropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "photo?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_photoByPropertyId_requests(string propertyId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid Property id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - propertyId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "photo?propertyid=" + propertyId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
        #endregion

        #region mainphoto by propertyid
        [Fact]
        public async Task return_200_for_valid_main_photoByPropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "mainphoto?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_main_photoByPropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "mainphoto?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_main_photoByPropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "mainphoto?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_main_photoByPropertyId()
        {
            var result = await client.GetAsync(baseUri + "mainphoto?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_main_photoByPropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "mainphoto?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_main_photoByPropertyId_requests(string propertyId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid property id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - propertyId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "mainphoto?propertyid=" + propertyId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }

        #endregion

        #region report by propertyid
        [Fact]
        public async Task return_200_for_valid_reportByPropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "report?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_drawingByPropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "report?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_reportByPropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "report?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_reportByPropertyId()
        {
            var result = await client.GetAsync(baseUri + "report?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_reportByPropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "report?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_reportByPropertyId_requests(string propertyId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid property id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - propertyId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "report?propertyid=" + propertyId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
        #endregion

        #region drawing by propertyid
        [Fact]
        public async Task return_200_for_valid_drawingByPropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "drawing?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_reportByPropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "drawing?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_drawingByPropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "drawing?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_drawingByPropertyId()
        {
            var result = await client.GetAsync(baseUri + "drawing?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_drawingByPropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "drawing?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_drawingByPropertyId_requests(string propertyId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid property id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - propertyId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + "drawing?propertyid=" + propertyId);
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
