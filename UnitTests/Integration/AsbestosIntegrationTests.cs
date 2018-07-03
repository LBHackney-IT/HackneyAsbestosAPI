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

namespace UnitTests.Integration
{
    public class AsbestosIntegrationTests
    {
        readonly TestServer _server;
        readonly HttpClient _client;
        string _baseUri;

        public AsbestosIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder()
                                     .UseStartup<TestStartup>());
            _client = _server.CreateClient();
            _baseUri = "api/v1/inspection/";
        }

        [Fact]
        public async Task return_200_for_valid_request()
        {
            var result = await _client.GetAsync(_baseUri + "0000001");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
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
            var result = await _client.GetAsync(_baseUri + id);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_is_successful_but_no_results()
        {
            var result = await _client.GetAsync(_baseUri + "00000000");
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error()
        {
            var result = await _client.GetAsync(_baseUri + "31415926");
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_requests()
        {
            var expectedResultContent = new Inspection()
            {
                Id = 655,
                LocationDescription = "A house"
            };

            var expectedResult = new Dictionary<string, IEnumerable<Inspection>>()
            {
                { "results", new List<Inspection>()
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
            var result = await _client.GetStringAsync(_baseUri + "0000001");

            Assert.Equal(expectedStringResult, result);
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
        public async Task return_valid_json_for_invalid_requests(string inspectionId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"results\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"developerMessage\":\"Invalid parameter - inspectionId\",");
            json.Append("\"userMessage\":\"Please provide a valid inspection number\"");
            json.Append("}");
            json.Append("}");

            var result = await _client.GetStringAsync(_baseUri + inspectionId);

            Assert.Equal(json.ToString(), result);
        }
    }
}
