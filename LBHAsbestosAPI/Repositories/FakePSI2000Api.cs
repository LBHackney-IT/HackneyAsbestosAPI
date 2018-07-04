using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;

namespace LBHAsbestosAPI.Repositories
{
    public class FakePSI2000Api: IPsi2000Api
    {
        public IEnumerable<Element> GetElement(int elementId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Floor> GetFloor(int floorId)
        {
            throw new NotImplementedException();
        }

        public Task<InspectionResponse> GetInspections(string propertyId)
        {

            var fakeInspectionResponse = new InspectionResponse()
            {
                Success = true,
                Data = new List<Inspection>()
                {
                    new Inspection()
                    {
                        Id = 655,
                        LocationDescription = "A house"
                    }
                }
            };

            return Task.FromResult(fakeInspectionResponse);
        }

        public IEnumerable<Room> GetRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login()
        {
            throw new NotImplementedException();
        }
    }
}
