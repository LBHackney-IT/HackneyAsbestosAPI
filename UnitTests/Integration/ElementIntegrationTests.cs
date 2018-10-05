using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LBHAsbestosAPI;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Integration
{
    public class ElementIntegrationTests
    {
        readonly TestServer _server;
        readonly HttpClient _client;
        string _baseUri;
        static string triggerExceptionId = "999999"; 
        static string triggerNotFoundId = "888888"; 

        public ElementIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder()
                                     .UseStartup<TestStartup>());
            _client = _server.CreateClient();
            _baseUri = "api/v1/elements/";
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            Random random = new Random();
            var randomId = Fake.GenerateRandomId(6);
            var result = await _client.GetAsync(_baseUri + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_request(string elementId)
        {
            var result = await _client.GetAsync(_baseUri + elementId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_successful_but_no_results()
        {
            var result = await _client.GetAsync(_baseUri + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error()
        {
            var result = await _client.GetAsync(_baseUri + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_requests()
        {
            var expectedResult = new Dictionary<string, Element>()
            {
                { "results", new Element()
                    {
                        Id = 3434,
                        Description = "First Floor"
                    }
                }
            };

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var expectedStringResult = JsonConvert.SerializeObject(expectedResult, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });

            var fakeValidId = Fake.GenerateRandomId(6);
            var result = await _client.GetStringAsync(_baseUri + fakeValidId);

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_requests(string elementId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid element id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - elementId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await _client.GetAsync(_baseUri + elementId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
    }
}
