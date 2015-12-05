using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Bl.Services;
using Xunit;

namespace WaCore.UnitTests.Bl.Services
{
    public class PasswordServiceHashPasswordTests
    {
        private static PasswordService GetPasswordService()
        {
            return new PasswordService();
        }

        [Fact]
        public void ValidatePasswort_WithCorrectPassword_ReturnsTrue()
        {
            var password = "myTest#123";
            var hashedPwd = "9wBETD5vvX0dL8PNvwzdDQ==:xnXDFvUoUmJfmIU7CAANEyhppdE6wxQyfkhIVW6X2Hs=";
            var passwordService = GetPasswordService();
            var h = passwordService.HashPassword(password);
            Assert.True(passwordService.ValidatePassword(password, hashedPwd));
        }

        [Fact]
        public void ValidatePasswort_WithWrongPassword_ReturnsFalse()
        {
            var password = "wrongPassword";
            var hashedPwd = "9wBETD5vvX0dL8PNvwzdDQ==:xnXDFvUoUmJfmIU7CAANEyhppdE6wxQyfkhIVW6X2Hs=";
            var passwordService = GetPasswordService();
            Assert.False(passwordService.ValidatePassword(password, hashedPwd));
        }

        [Fact]
        public void ValidatePasswort_WithEmptyPassword_ReturnsFalse()
        {
            var password = "";
            var hashedPwd = "9wBETD5vvX0dL8PNvwzdDQ==:xnXDFvUoUmJfmIU7CAANEyhppdE6wxQyfkhIVW6X2Hs=";
            var passwordService = GetPasswordService();
            Assert.False(passwordService.ValidatePassword(password, hashedPwd));
        }


    }
}
