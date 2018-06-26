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
		static string inspectionUri = baseUri + "api/inspections";
		static string apiUsername = Environment.GetEnvironmentVariable("PSI_TEST_USERNAME");
		static string apiPassword = Environment.GetEnvironmentVariable("PSI_TEST_PASSWORD");


		public async Task<bool> Login()
		{
			using (var client = new HttpClient())
			{
				try
				{
					HttpResponseMessage res =  await client.PostAsync(loginUri,
					                                                  new StringContent("{\"UserName\":\"" + apiUsername + "\",\"Password\":\"" + apiPassword
					                                                                    + "\"}"
					                                                                    , Encoding.UTF8, "application/json"));


					string cookieValue = "";
					var headers = res.Headers;
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
                    return false;
                }
			}
			return true;
		}

        public IEnumerable<Element> GetElement(int elementId)
        {
			return new List<Element>();
        }

		public async Task<InspectionResponse> GetInspections(string propertyId)
        {
			InspectionResponse response = new InspectionResponse();
			if (cookie == null)
			{
				var logedIn = Login();
                if (!logedIn.Result)
                {
					// login failed
					return response;
                }
			}
			if (cookie.Expired )
			{
				var logedIn = Login();
				if (!logedIn.Result)
				{
					// login failed
					return response;
                }
			}

			var inspections = new List<Inspection>();
			var baseAddress = new Uri(inspectionUri + "?filter=(UPRN=\"" + propertyId + "\")");
			var cookieContainer = new CookieContainer();
			cookieContainer.Add(baseAddress, cookie);

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
				
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
				HttpResponseMessage responseMessage =  client.GetAsync(baseAddress).Result;
				responseMessage.EnsureSuccessStatusCode();

				var responseData =   responseMessage.Content.ReadAsStringAsync().Result ;
 
				response = JsonConvert.DeserializeObject<InspectionResponse>(responseData); 
            }
			return response;
        }

		public IEnumerable<Room> GetRoom(int roomId)
        {
            return new List<Room>();
        }

        public IEnumerable<Floor> GetFloor(int floorId)
        {
            return new List<Floor>();
        }
	}
}
