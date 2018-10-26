using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;
using Newtonsoft.Json;

namespace LBHAsbestosAPI.Repositories
{
    public class Psi2000Api : IPsi2000Api
    {
        static Cookie cookie;
        static string baseUri = Environment.GetEnvironmentVariable("PSI_BASE_URI");
        static string loginUri = baseUri + "login";
        static string apiUsername = Environment.GetEnvironmentVariable("PSI_USERNAME");
        static string apiPassword = Environment.GetEnvironmentVariable("PSI_PASSWORD");
        static string inspectionUri = baseUri + "api/inspections";
        static string roomUri = baseUri + "api/rooms/";
        static string floorUri = baseUri + "api/floors/";
        static string elementUri = baseUri + "api/elements/";
        static string documentUri = baseUri + "api/documents/";
        static string todoUri = baseUri + "api/todos";
        static string sampleUri = baseUri + "api/samples";

        ILoggerAdapter<Psi2000Api> _logger;

        public Psi2000Api(ILoggerAdapter<Psi2000Api> logger)
        {
            _logger = logger;
        }

        public async Task<Response<IEnumerable<Inspection>>> GetInspections(string propertyId)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(inspectionUri + $"?filter=(UPRN=\"{ propertyId }\")");
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<IEnumerable<Inspection>>>(responseData);
        }

        public async Task<Response<Room>> GetRoom(string roomId)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(roomUri + roomId);
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<Room>>(responseData);
        }

        public async Task<Response<Floor>> GetFloor(string floorId)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(floorUri + floorId);
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<Floor>>(responseData);
        }

        public async Task<Response<Element>> GetElement(string elementId)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(elementUri + elementId);
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<Element>>(responseData);
        }

        public async Task<Response<IEnumerable<Todo>>> GetTodosByPropertyId(string propertyId)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(todoUri + $"?filter=(UPRN=\"{ propertyId }\")");
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<IEnumerable<Todo>>>(responseData);
        }

        public async Task<Response<Todo>> GetTodo(string todoId)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(todoUri + '/' + todoId);
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<Todo>>(responseData); 
        }

        public async Task<Response<IEnumerable<Sample>>> GetSamples(string inspectionId)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(sampleUri + $"?filter=(inspectionid={ inspectionId })");
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<IEnumerable<Sample>>>(responseData);
        }
         
        public async Task<Response<IEnumerable<Document>>> GetDocuments(string inspectionId, string fileType)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(documentUri + fileType + $"?filter=(UPRN={ inspectionId })");
            var responseData = GetResponseMessage(baseAddress);

            return JsonConvert.DeserializeObject<Response<IEnumerable<Document>>>(responseData);
        }

        public async Task<FileContainer> GetFile(string fileId, string fileType)
        {
            await EnsureSuccessLogin();

            var baseAddress = new Uri(documentUri + fileType + "/" + fileId);
            return GetResponseStream(baseAddress);
        }

        private string GetResponseMessage(Uri baseAddress)
        {
            using (var handler = new HttpClientHandler { CookieContainer = SetupCookie(baseAddress) })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var responseMessage = client.GetAsync(baseAddress).Result;
                responseMessage.EnsureSuccessStatusCode();

                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                return responseData;
            }
        }

        private FileContainer GetResponseStream(Uri baseAddress)
        {
            using (var handler = new HttpClientHandler { CookieContainer = SetupCookie(baseAddress) })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var response = client.GetAsync(baseAddress);

                try
                {
                    response.Result.EnsureSuccessStatusCode();

                    var file = new FileContainer
                    {
                        ContentType = response.Result.Content.Headers.ContentType.ToString(),
                        Size = response.Result.Content.Headers.ContentLength,
                        Data = response.Result.Content.ReadAsByteArrayAsync().Result
                    };
                    return file;
                }
                catch (HttpRequestException)
                {
                    if (response.Result.StatusCode == HttpStatusCode.NotFound)
                    {
                        return new FileContainer();
                    }
                    throw;
                }
            }
        }

        private CookieContainer SetupCookie(Uri baseAddress)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, cookie);
            return cookieContainer;
        }

        private async Task EnsureSuccessLogin() 
        {
            if (!await LoginIfCookieIsInvalid())
            {
                throw new Psi2000ApiException();
            }
        }

        private async Task<bool> LoginIfCookieIsInvalid()
        {
            if (cookie == null)
            {
                var logedIn = await Login();
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

                    var response = JsonConvert.DeserializeObject<LoginResponse>(
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
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new Psi2000ApiException();
                }
            }
            _logger.LogInformation("Login successful");
            return true;
        }
    }

    public static class FileType
    {
        public const string photo = "photo";
        public const string report = "reports";
        public const string drawing = "maindrawing";
        public const string mainPhoto = "mainphoto";
    }

    public class Psi2000ApiException : Exception { }
}

