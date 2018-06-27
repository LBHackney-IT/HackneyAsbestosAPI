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
            var validatorId = new InspectionValidator();
            var validationResult = validatorId.Validate(inspectionId);
            Assert.Equal(expected, validationResult.Success);
        }

        [Theory]
        [InlineData("123456678", null)]
        [InlineData(null, "The id cannot be empty")]
        [InlineData("",  "The id cannot be empty")]
        [InlineData("   ", "The id cannot be empty")]
        [InlineData("1", "The id does not meet the required lenght")]
        [InlineData("123456789", "The id exceeds the required length")]
        [InlineData("ABC", "The id must contain only numbers")]
        [InlineData("A1234567", "The id must contain only numbers")]
        [InlineData("?1234567", "The id must contain only numbers")]
        public void return_error_message_if_inspectionid_is_not_valid(string id, string expectedMessage)
        {
            var validatorId = new InspectionValidator();
            var validationResult = validatorId.Validate(id);
            Assert.Equal(expectedMessage, validationResult.ErrorMesage);
        }
    }
}
