using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LBHAsbestosAPI.Entities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LBHAsbestosAPI.Repository
{
    public class PSIApi
    {
		string baseUri;
		public static string token;


        public PSIApi()
        {
			baseUri = Environment.GetEnvironmentVariable("PSI2000_API_DEV");
		}


        public async Task<bool> login ()
        {
			using (var client = new HttpClient())
            {
				client.BaseAddress = new Uri(baseUri);
				var loginDetails = JsonConvert.SerializeObject(new LoginDetails());
				var response = await client.PostAsync($"login", 
	                                      new StringContent(loginDetails,Encoding.UTF8,"application/json"));
				var responseContent = await response.Content.ReadAsStringAsync();
				LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
				if (loginResponse.Success)
				{
					token = response.Headers.GetValues("ASP.NET_SessionId").First();
					return true;
				}
				else 
				{
					return false;
				}
            }
        }


        public async virtual Task<IQueryable<Inspection>> GetInspections(string uh_reference)
		{
			if (String.IsNullOrEmpty(token))
			{
ß				login().GetAwaiter();
			}
			List<Inspection> inspections = new List<Inspection>();
			using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
				var response = await client.GetAsync($"api/inspections?filter=(UPRN=\"{uh_reference}\")");
                var responseContent = await response.Content.ReadAsStringAsync();
				inspections = JsonConvert.DeserializeObject<List<Inspection>>(responseContent);
                
            }

			return inspections.AsQueryable();
		}

		public virtual IQueryable<Floor> GetFloor()
        {
            return new List<Floor>().AsQueryable();
        }

		public virtual IQueryable<RoomDetails> GetRoomDetails()
        {
			return new List<RoomDetails>().AsQueryable();
        }
    }
}
