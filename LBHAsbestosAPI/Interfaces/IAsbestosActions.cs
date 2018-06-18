using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Actions
{
    public interface IAsbestosActions
    {
		Task<IEnumerable<Inspection>> GetInspection(string propertyId);
    }
}
