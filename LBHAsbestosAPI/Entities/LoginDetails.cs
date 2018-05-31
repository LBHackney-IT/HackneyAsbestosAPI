using System;
namespace LBHAsbestosAPI.Entities
{
	public class LoginDetails
	{
		public LoginDetails()
		{
			string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			switch (environment)
			{
				case "Development":
					username = Environment.GetEnvironmentVariable("PSI2000_API_DEV_USERNAME");
					password = Environment.GetEnvironmentVariable("PSI2000_API_DEV_PASSWORD");
					break;
				case "Production":
                    username = Environment.GetEnvironmentVariable("PSI2000_API_PROD_USERNAME");
					password = Environment.GetEnvironmentVariable("PSI2000_API_DEV_PASSWORD");
					break;
				default:
                    username = Environment.GetEnvironmentVariable("PSI2000_API_DEV_USERNAME");
					password = Environment.GetEnvironmentVariable("PSI2000_API_DEV_PASSWORD");
					break;
			}
		}
		public string username { get; set; }
		public string password { get; set; }
	}
}