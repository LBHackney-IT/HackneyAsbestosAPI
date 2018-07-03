using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var fakeRepository = new Mock<IPsi2000Api>();
            var fakeInspection = new InspectionResponse()
            {
                Data = new List<Inspection>()
            };

            fakeInspection.Data.Add(new Inspection()
            {
                Id = 433,
                LocationDescription = "Under the bridge"
            });

            fakeRepository
                .Setup(m => m.GetInspections(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeInspection));

            var asbestosService = new AsbestosService(fakeRepository.Object);
            var responseData = await asbestosService.GetInspection("random string");

            Assert.Equal(433, responseData.ElementAt(0).Id);
            Assert.Equal("Under the bridge", responseData.ElementAt(0).LocationDescription);
        }
    }
}
