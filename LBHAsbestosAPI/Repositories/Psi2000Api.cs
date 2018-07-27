using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using Newtonsoft.Json;

namespace LBHAsbestosAPI.Repositories
{
    public class Psi2000Api : IPsi2000Api
    {
        static Cookie cookie;
        static string baseUri = Environment.GetEnvironmentVariable("PSI_TEST_BASE_URI");
        static string loginUri = baseUri + "login";
        static string apiUsername = Environment.GetEnvironmentVariable("PSI_TEST_USERNAME");
        static string apiPassword = Environment.GetEnvironmentVariable("PSI_TEST_PASSWORD");
        static string inspectionUri = baseUri + "api/inspections";
        static string roomUri = baseUri + "api/rooms/";
        static string floorUri = baseUri + "api/floors/";

        ILoggerAdapter<Psi2000Api> _logger;

        public Psi2000Api(ILoggerAdapter<Psi2000Api> logger)
        {
            _logger = logger;
        }

        public async Task<bool> Login()
        {
            _logger.LogInformation("Logging into PSI");
            using (var client = new HttpClient())
            {
                try
                {
                    var httpResponse = await client.PostAsync(
                        loginUri, new StringContent(
                            "{\"UserName\":\"" + apiUsername + 
                            "\",\"Password\":\"" + apiPassword + 
                            "\"}", Encoding.UTF8, "application/json"));
                    
                    var response = JsonConvert.DeserializeObject<Response>(
                                    await httpResponse.Content.ReadAsStringAsync());

                    if (!response.Success)
                    {
                        _logger.LogError(response.ErrorMessage);
                        return false;
                    }

                    string cookieValue = "";
                    var headers = httpResponse.Headers;
                    IEnumerable<string> values;

                    if (headers.TryGetValues("Set-Cookie", out values))
                    {
                        cookieValue = values.First();
                    }

                    var headerValues = cookieValue.Split(";");
                    cookieValue = headerValues[0].Split("=")[1];
                    string cookieKey = headerValues[0].Split("=")[0];

                    cookie = new Cookie(cookieKey, cookieValue);
                    cookie.Expires = DateTime.Now.AddMinutes(19);
                }
                catch (HttpRequestException httpRequestException)
                {
                    _logger.LogError("Login failed");
                    return false;
                }
            }
            _logger.LogInformation("Login successful");
            return true;
        }

        public IEnumerable<Element> GetElement(int elementId)
        {
            return new List<Element>();
        }

        public async Task<InspectionResponse> GetInspections(string propertyId)
        {
            var response = new InspectionResponse();
            var loginAction = await LoginIfCookieIsInvalid();

            if (!loginAction)
            {
                throw new InvalidLoginException();
            }

            var baseAddress = new Uri(inspectionUri + "?filter=(UPRN=\"" + propertyId + "\")");
            var responseData = GetResponseData(baseAddress);

            return JsonConvert.DeserializeObject<InspectionResponse>(responseData);
        }

        public async Task<RoomResponse> GetRoom(string roomId)
        {
            var response = new RoomResponse();
            var loginAction = await LoginIfCookieIsInvalid();

            if (!loginAction)
            {
                throw new InvalidLoginException();
            }

            var baseAddress = new Uri(roomUri + roomId);
            var responseData = GetResponseData(baseAddress);

            return JsonConvert.DeserializeObject<RoomResponse>(responseData);
        }

        public async Task<FloorResponse> GetFloor(string floorId)
        {
            var response = new FloorResponse();
            var loginAction = await LoginIfCookieIsInvalid();

            if (!loginAction)
            {
                throw new InvalidLoginException();
            }

            var baseAddress = new Uri(floorUri + floorId);
            var responseData = GetResponseData(baseAddress);

            return JsonConvert.DeserializeObject<FloorResponse>(responseData);
        }

        private async Task<bool> LoginIfCookieIsInvalid()
        {
            if (cookie == null)
            {
                var logedIn = await Login();
                var result = logedIn;
                if (!logedIn)
                {
                    // login failed
                    return false;
                }
            }
            if (cookie.Expired)
            {
                var logedIn = await Login();
                if (!logedIn)
                {
                    // login failed
                    return false;
                }
            }
            return true;
        }

        private string GetResponseData(Uri baseAddress)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, cookie);

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                HttpResponseMessage responseMessage = client.GetAsync(baseAddress).Result;
                responseMessage.EnsureSuccessStatusCode();

                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                return responseData;
            }
        }
    }

    public class InvalidLoginException : Exception { }
}

