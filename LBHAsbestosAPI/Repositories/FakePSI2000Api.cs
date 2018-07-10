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
            if (propertyId == "31415926")
            {
                throw new Exception();
            }
            if (propertyId == "00000000")
            {
                throw new MissingInspectionException();
            }

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

        public Task<RoomResponse> GetRoom(string roomId)
        {
            if (roomId == "314159")
            {
                throw new Exception();
            }
            if (roomId == "000000")
            {
                throw new MissingRoomException();
            }

            var fakeRoomResponse = new RoomResponse()
            {
                Success = true,
                Data = new Room()
                {
                    Id = 5003,
                    Description = "Ground Floor"
                }
            };

            return Task.FromResult(fakeRoomResponse);
        }

        public Task<bool> Login()
        {
            throw new NotImplementedException();
        }
    }
}
