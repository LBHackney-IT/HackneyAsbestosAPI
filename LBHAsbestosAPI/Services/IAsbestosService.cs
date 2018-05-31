using System;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI.Dtos;

namespace LBHAsbestosAPI.Services
{
    public interface IAsbestosService
    {
		string GetPlans(string uh_reference);

		Task<IQueryable<InspectionDto>> GetInspections(string uh_reference); 
    }
}
