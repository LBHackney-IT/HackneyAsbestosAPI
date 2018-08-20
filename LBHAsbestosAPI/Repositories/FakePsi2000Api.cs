using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;

namespace LBHAsbestosAPI.Repositories
{
    public class FakePsi2000Api : IPsi2000Api
    {
        static string triggerExceptionId = "999999";
        static string nullResponseId = "888888";

        public Task<Response<IEnumerable<Inspection>>> GetInspections(string propertyId)
        {
            if (propertyId == triggerExceptionId)
            {
                throw new TestExceptionInFakePSI();
            }

            var fakeInspectionresponse = new Response<IEnumerable<Inspection>>()
            {
                Success = true
            };

            if (propertyId == nullResponseId)
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

        public Task<Response<Room>> GetRoom(string roomId)
        {
            if (roomId == triggerExceptionId)
            {
                throw new TestExceptionInFakePSI();
            }

            var fakeRoomResponse = new Response<Room>()
            {
                Success = true
            };

            if (roomId == nullResponseId)
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

        public Task<Response<Floor>> GetFloor(string floorId)
        {
            if (floorId == triggerExceptionId)
            {
                throw new TestExceptionInFakePSI();
            }

            var fakeFloorResponse = new Response<Floor>()
            {
                Success = true
            };

            if (floorId == nullResponseId)
            {
                fakeFloorResponse.Data = null;
            }
            else
            {
                fakeFloorResponse.Data = new Floor()
                {
                    Id = 3434,
                    Description = "First Floor"
                };
            }

            return Task.FromResult(fakeFloorResponse);
        }

        public Task<Response<Element>> GetElement(string elementId)
        {
            if (elementId == triggerExceptionId)
            {
                throw new TestExceptionInFakePSI();
            }

            var fakeElementResponse = new Response<Element>()
            {
                Success = true
            };

            if (elementId == nullResponseId)
            {
                fakeElementResponse.Data = null;
            }
            else
            {
                fakeElementResponse.Data = new Element()
                {
                    Id = 3434,
                    Description = "First Floor"
                };
            }

            return Task.FromResult(fakeElementResponse);
        }

        public Task<Response<IEnumerable<Document>>> GetDocument(string inspectionId, string fileType)
        {
            if (inspectionId == triggerExceptionId)
            {
                throw new TestExceptionInFakePSI();
            }
            if (inspectionId == nullResponseId)
            {
                return Task.FromResult(new Response<IEnumerable<Document>>()
                {
                    Data = new List<Document>()
                });
            }

            return Task.FromResult(new Response<IEnumerable<Document>>()
            {
                Data = new List<Document>()
                {
                    new Document()
                    {
                        Id = 4532,
                        Description = "A picture"
                    }
                }
            });
        }

        public Task<FileContainer> GetFile(string fileId, string fileType)
        {
            if (fileId == triggerExceptionId)
            {
                throw new TestExceptionInFakePSI();
            }
            if (fileId == nullResponseId)
            {
                return Task.FromResult(new FileContainer()
                {
                    Data = null
                });
            }

            return Task.FromResult(new FileContainer()
            {
                ContentType = "image/jpeg",
                Size = 54,
                Data = new byte[54]
            });
        }
    }

    public class TestExceptionInFakePSI : Exception { }
}
