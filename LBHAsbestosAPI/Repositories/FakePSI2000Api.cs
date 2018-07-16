using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
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
            if (propertyId.Length == 10)
            {
                throw new Exception();
            }

            var fakeInspectionresponse = new InspectionResponse()
            {
                Success = true
            };

            if (propertyId.Length == 9)
            {
                fakeInspectionresponse.Data = new List<Inspection>();
            }
            else 
            {
                fakeInspectionresponse.Data = new List<Inspection>()
                {
                    new Inspection()
                    {
                        Id = 655,
                        LocationDescription = "A house"
                    }
                };
            }

            return Task.FromResult(fakeInspectionresponse);
        }

        public Task<RoomResponse> GetRoom(string roomId)
        {
            if (roomId.Length == 4)
            {
                throw new Exception();
            }

            var fakeRoomResponse = new RoomResponse()
            {
                Success = true
            };

            if (roomId.Length == 5)
            {
                fakeRoomResponse.Data = null;
            }
            else
            {
                fakeRoomResponse.Data = new Room()
                {
                    Id = 5003,
                    Description = "Ground Floor"
                };
            }

            return Task.FromResult(fakeRoomResponse);
        }

        public Task<bool> Login()
        {
            throw new NotImplementedException();
        }
    }
}
