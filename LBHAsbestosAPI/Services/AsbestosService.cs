using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI.Dtos;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Repository;

namespace LBHAsbestosAPI.Services
{
	public class AsbestosService : IAsbestosService
    {
		private PSIApi _PSIApi;

		public AsbestosService(PSIApi PSIApi)
		{
			_PSIApi = PSIApi;
		}

		public async virtual Task<IQueryable<InspectionDto>> GetInspections(string uh_reference)
		{
			IQueryable<Inspection> inspections = await _PSIApi.GetInspections(uh_reference);
			var inspectionDtos = buildResponse(inspections);
            
			return inspectionDtos;
		}

		   
		public string GetPlans(string uh_reference)
		{
			return $"A URL for the plans of {uh_reference}";
		}

		public IQueryable<InspectionDto> buildResponse(IQueryable<Inspection> inspections)
		{
			List<InspectionDto> inspectionDtos = new List<InspectionDto>();
			foreach (Inspection inspection in inspections)
			{
				InspectionDto inspectionDto = new InspectionDto()
				{
					address = inspection.Comment,
					inspector = inspection.CreatedBy
				};
				inspectionDtos.Add(inspectionDto);
                
			}
			return inspectionDtos.AsQueryable();
		}
	}
}
