using System;
namespace LBHAsbestosAPI.Dtos
{
	public class InspectionDto
	{
		public DateTime inspection_date { get; set; }
		public string address { get; set; }
		public string inspector { get; set; }
		public int floor { get; set; }
		public string room { get; set; }
	}
}