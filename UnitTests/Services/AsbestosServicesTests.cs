using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;
using LBHAsbestosAPI.Services;
using Moq;
using Xunit;

namespace UnitTests
{
    public class AsbestosServicesTests
    {
        [Fact]
        public async Task can_access_inspection_data_from_inspectionrequest()
        {
            IEnumerable<Inspection> responseData;
            IAsbestosService asbestosService;

            // Case for the test running isolated from the other tests
            if (!TestStatus.IsRunningTests)
            {
                var fakeRepository = new Mock<IPsi2000Api>();
                var fakeInspection = new InspectionResponse()
                {
                    Data = new List<Inspection>()
                };

                fakeInspection.Data.Add(new Inspection()
                {
                    Id = 655,
                    LocationDescription = "A house"
                });

                fakeRepository
                    .Setup(m => m.GetInspections(It.IsAny<string>()))
                    .Returns(Task.FromResult(fakeInspection)); 

                asbestosService = new AsbestosService(fakeRepository.Object);
            }
            // Case for the test running in conjunction with the solution tests.
            // The asbestos service uses the fakePsi2000Api repository.
            else
            {
                asbestosService = new AsbestosService(new Psi2000Api());
            }

            responseData = await asbestosService.GetInspection("random string");

            Assert.Equal(655, responseData.ElementAt(0).Id);
            Assert.Equal("A house", responseData.ElementAt(0).LocationDescription);
        }
    }
}
