using System;
using Xunit;
using LBHAsbestosAPI.Validators;

namespace LBHAsbestosAPI.Tests.Validation
{
    public class IdValidationTests
    {
        [Theory]
        [InlineData("12345678", true)]
        [InlineData("12345678910", false)]
        [InlineData("abc", false)]
        [InlineData("A1234567", false)]
        [InlineData("1!234567", false)]
        [InlineData("123 5678", false)]
        public void return_boolean_if_propertyid_is_valid(string propertyId, bool expected)
        {
            var validationResult = IdValidator.ValidatePropertyId(propertyId);
            Assert.Equal(expected, validationResult);
        }

        [Theory]
        [InlineData("123456", true)]
        [InlineData("12345678", false)]
        [InlineData("abc", false)]
        [InlineData("A1234567", false)]
        [InlineData("1!234567", false)]
        [InlineData("12 456", false)]
        public void return_boolean_if_id_is_valid(string roomId, bool expected)
        {
            var validationResult = IdValidator.ValidateId(roomId);
            Assert.Equal(expected, validationResult);
        }
    }
}
