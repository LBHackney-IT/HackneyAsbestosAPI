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
using LBHAsbestosAPI.Tests.Helpers;
using Xunit;

namespace LBHAsbestosAPI.Tests.Integration
{
    public class SamplesIntegrationTests
    {
        readonly TestServer server;
        readonly HttpClient client;
        static string baseUri = "api/v1/samples?inspectionid=";
        static string triggerExceptionId = "999999";
        static string triggerNoResultsId = "888888";

        public SamplesIntegrationTests()
        {
            server = new TestServer(new WebHostBuilder()
                                     .UseStartup<TestStartup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_request(string inspectionId)
        {
            var result = await client.GetAsync(baseUri + inspectionId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_200_and_empty_list_if_request_successful_but_no_results()
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"results\":");
            json.Append("[");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + triggerNoResultsId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(json.ToString(), resultString);
        }

        [Fact]
        public async Task return_500_when_internal_server_error()
        {
            var result = await client.GetAsync(baseUri + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_requests()
        {
            var expectedResult = new Dictionary<string, IEnumerable<Sample>>
            {
                { "results", new List<Sample>
                    {
                        new Sample
                        {
                            Id = 21234,
                            RefferedSample = "Random sample"
                        }

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

            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetStringAsync(baseUri + randomId);
            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_requests(string inspectionId)
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

            var result = await client.GetAsync(baseUri + inspectionId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
    }
}
