using System;
using System.Collections.Generic;
using Bogus;
using LBHAsbestosAPI.Entities;

namespace UnitTests.Helpers
{
    public static class Fake
    {
        static Faker random;

        static Fake()
        {
            random = new Faker();
        }

        public static int GenerateRandomId(int digits)
        {
            return random.Random.Number(
                (int)Math.Pow(10, digits-1), (int)Math.Pow(10, digits) - 1);
        }

        public static string GenerateRandomText()
        {
            return random.Lorem.Sentence(6);
        }

        public static IEnumerable<Inspection> GenerateInspection(int fakeId,
                                                                string fakeDescription)
        {
            return new List<Inspection>()
            {
                { new Inspection()
                    {
                        Id = fakeId,
                        LocationDescription = fakeDescription
                    }
                }
            };
        }

        public static Floor GenerateFloor(int fakeId, string fakeDescription)
        {
            return new Floor()
            {
                Id = fakeId,
                Description = fakeDescription
            };   
        }

        public static Room GenerateRoom(int fakeId, string fakeDescription)
        {
            return new Room()
            {
                Id = fakeId,
                Description = fakeDescription
            };
        }

        public static Element GenerateElement(int fakeId, string fakeDescription)
        {
            return new Element()
            {
                Id = fakeId,
                Description = fakeDescription
            };
        }

        public static IEnumerable<Document> GenerateDocument(int fakeId,
                                                                string fakeDescription)
        {
            return new List<Document>()
            {
                { new Document()
                    {
                        Id = fakeId,
                        Description = fakeDescription
                    }
                }
            };
        }

        public static FileResponse GenerateFakeFile(string contentType)
        {
            Random randomNumber = new Random();
            int randomFileSize = randomNumber.Next(60, 1024);

            Byte[] fileData = new byte[randomFileSize];
            randomNumber.NextBytes(fileData);

            return new FileResponse()
            {
                ContentType = contentType,
                Size = randomFileSize,
                Data = fileData
            };
        }
    }
}
