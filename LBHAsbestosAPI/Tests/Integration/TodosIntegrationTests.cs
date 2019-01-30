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
    public class TodosIntegrationTests
    {
        readonly TestServer server;
        readonly HttpClient client;
        static string baseUri = "api/v1/todos";
        static string parameterUri = "?propertyId=";
        static string triggerExceptionId = "999999";
        static string triggerNotFoundId = "888888";

        public TodosIntegrationTests()
        {
            server = new TestServer(new WebHostBuilder()
                                     .UseStartup<TestStartup>());
            client = server.CreateClient();
        }

        #region get todo by todoId
        [Fact]
        public async Task return_200_for_valid_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + '/' + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_400_for_invalid_request(string todoId)
        {
            var result = await client.GetAsync(baseUri + '/' + todoId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task return_404_if_request_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + '/' + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task return_500_when_internal_server_error()
        {
            var result = await client.GetAsync(baseUri + '/' + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task return_valid_json_for_valid_requests()
        {
            var expectedResult = new Dictionary<string, Todo>()
            {
                { "results", new Todo()
                    {
                        Id = 1987,
                        Description = "Bunker"
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
            var result = await client.GetStringAsync(baseUri + '/' + randomId);

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task return_valid_json_for_invalid_requests(string todoId)
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\"errors\":");
            json.Append("[");
            json.Append("{");
            json.Append("\"userMessage\":\"Please provide a valid todo id\",");
            json.Append("\"developerMessage\":\"Invalid parameter - todoId\"");
            json.Append("}");
            json.Append("]");
            json.Append("}");

            var result = await client.GetAsync(baseUri + '/' + todoId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }

        #endregion

        #region get by propertyId
        [Fact]
        public async Task GetTodosByPropertyId_return_200_for_valid_request()
        {
            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetAsync(baseUri + parameterUri + randomId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GetTodosByPropertyId_return_200_if_request_is_successful_but_no_results()
        {
            var result = await client.GetAsync(baseUri + parameterUri + triggerNotFoundId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task GetTodosByPropertyId_return_400_for_invalid_request(string propertyId)
        {
            var result = await client.GetAsync(baseUri + parameterUri + propertyId);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GetTodosByPropertyId_return_500_when_internal_server_error()
        {
            var result = await client.GetAsync(baseUri + parameterUri + triggerExceptionId);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetTodosByPropertyId_return_valid_json_for_valid_requests()
        {
            var expectedResultContent = new Todo()
            {
                Id = 6123,
                LocationDescription = "Outside"
            };

            var expectedResult = new Dictionary<string, IEnumerable<Todo>>()
            {
                { "results", new List<Todo>()
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

            var randomId = Fake.GenerateRandomId(6);
            var result = await client.GetStringAsync(baseUri + parameterUri + randomId);

            Assert.Equal(expectedStringResult, result);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("abc")]
        [InlineData("A1234567")]
        [InlineData("1!234567")]
        [InlineData("12 456")]
        public async Task GetTodosByPropertyId_return_valid_json_for_invalid_requests(string propertyId)
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
             
            var result = await client.GetAsync(baseUri + parameterUri + propertyId);
            var resultString = await result.Content.ReadAsStringAsync();
            Assert.Equal(json.ToString(), resultString);
        }
        #endregion
    }
}
