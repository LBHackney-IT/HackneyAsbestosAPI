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
            baseUri = "api/v1/documents/";
        }
         
        #region photo by propertyId endpoint
        [Fact]
        public async Task return_200_for_valid_photoBypropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "photos?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_photoBypropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "photos?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_photoBypropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "photos?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_photoBypropertyId()
        {
            var result = await client.GetAsync(baseUri + "photos?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_photoBypropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "photos?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_photoBypropertyId_requests(string propertyId)
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

            var result = await client.GetAsync(baseUri + "photos?propertyid=" + propertyId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
        #endregion

        #region mainphoto by propertyid
        [Fact]
        public async Task return_200_for_valid_main_photoBypropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "mainphotos?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_main_photoBypropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "mainphotos?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_main_photoBypropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "mainphotos?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_main_photoBypropertyId()
        {
            var result = await client.GetAsync(baseUri + "mainphotos?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_main_photoBypropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "mainphotos?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_main_photoBypropertyId_requests(string propertyId)
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

            var result = await client.GetAsync(baseUri + "mainphotos?propertyid=" + propertyId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }

        #endregion

        #region report by propertyid
        [Fact]
        public async Task return_200_for_valid_reportBypropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "reports?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_drawingBypropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "reports?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_reportBypropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "reports?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_reportBypropertyId()
        {
            var result = await client.GetAsync(baseUri + "reports?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_reportBypropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "reports?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_reportBypropertyId_requests(string propertyId)
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

            var result = await client.GetAsync(baseUri + "reports?propertyid=" + propertyId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
#endregion

        #region drawing by propertyid
        [Fact]
        public async Task return_200_for_valid_drawingBypropertyId_request()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetAsync(baseUri + "drawings?propertyid=" + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_reportBypropertyId_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + "drawings?propertyid=" + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_drawingBypropertyId_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + "drawings?propertyid=" + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error_in_drawingBypropertyId()
        {
            var result = await client.GetAsync(baseUri + "drawings?propertyid=" + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_drawingBypropertyId_requests()
        {
            var randomId = Fake.GenerateRandomId(8);
            var result = await client.GetStringAsync(baseUri + "drawings?propertyid=" + randomId);
            var expectedStringResult = GetexpectedStringResult();

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_drawingBypropertyId_requests(string propertyId)
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

            var result = await client.GetAsync(baseUri + "drawings?propertyid=" + propertyId);
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
