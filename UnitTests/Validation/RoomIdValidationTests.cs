using System;
using LBHAsbestosAPI.Validators;
using Xunit;

namespace UnitTests.Validation
{
    public class RoomIdValidationTests
    {
        [Theory]
        [InlineData("123456", true)]
        [InlineData("1", false)]
        [InlineData("12345678", false)]
        [InlineData("abc", false)]
        [InlineData("A1234567", false)]
        [InlineData("1!234567", false)]
        [InlineData("12 456", false)]
        public void return_boolean_if_InspectionId_is_valid(string roomId, bool expected)
        {
            var validationResult = IdValidator.ValidateRoomId(roomId);
            Assert.Equal(expected, validationResult);
        }
    }
}
