using System;
namespace LBHAsbestosAPI.Entities
{
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

		public bool Success { get; set; }
		public string ErrorMessage { get; set; }
		public string Data { get; set; }
    }
}
