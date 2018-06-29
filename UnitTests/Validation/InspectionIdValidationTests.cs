using System;
using Xunit;
using LBHAsbestosAPI.Repositories;
using System.Net;
using LBHAsbestosAPI.Validators;

namespace UnitTests
{
    public class InspectionIdValidationTests
    {
        [Theory]
        [InlineData("123456678", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("1", false)]
        [InlineData("123456789", false)]
        [InlineData("ABC", false)]
        [InlineData("A1234567", false)]
        [InlineData("?1234567", false)]
        public void return_boolean_if_InspectionId_is_valid(string inspectionId, bool expected)
        {
            var validationResult = InspectionIdValidator.Validate(inspectionId);
            Assert.Equal(expected, validationResult);
        }
    }
}
