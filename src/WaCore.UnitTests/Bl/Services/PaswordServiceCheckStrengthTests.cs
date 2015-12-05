using WaCore.Bl.Services;
using WaCore.Contracts.Bl.Services;
using WaCore.Contracts.Enums;
using Xunit;

namespace WaCore.UnitTests.Bl.Services
{
    public class PaswordServiceCheckStrengthTests
    {
        private IPasswordService GetPasswordService()
        {
            return new PasswordService();
        }

        [Fact]
        public void PasswortEmpty_Blank()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.Blank, service.CheckStrength(""));
        }
        
        [Fact]
        public void Passwort_5Chars_TooShort()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.TooShort, service.CheckStrength("12345"));
        }

        [Fact]
        public void Passwort_6Digits_RequirementsNotMet()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.RequirementsNotMet, service.CheckStrength("123456"));
        }


        [Fact]
        public void Passwort_6Chars_Digits_VeryWeak()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.VeryWeak, service.CheckStrength("a234567"));
        }


        [Fact]
        public void Passwort_6LowerUpperCharsAndDigit_Weak()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.Weak, service.CheckStrength("aA1234"));
        }

        [Fact]
        public void Passwort_8LowerUpperCharAndDigit_Fair()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.Fair, service.CheckStrength("Passwor1"));
        }

        [Fact]
        public void Passwort_10LowerUpperCharsAndDigit_Medium()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.Medium, service.CheckStrength("Password123"));
        }

        [Fact]
        public void Passwort_10LowerUpperCharsAndDigitAndSpecialChar_Strong()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.Strong, service.CheckStrength("Password1#"));
        }

        [Fact]
        public void Passwort_12LowerUpperCharsAndDigitAndSpecialChar_VeryStrong()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.VeryStrong, service.CheckStrength("Password123#"));
        }

        [Fact]
        public void Passwort_10LowerUpperCharsAndDigitAndSpecialCharAndRepeatingChar_VeryWeak()
        {
            var service = GetPasswordService();
            Assert.Equal(PasswordScore.VeryWeak, service.CheckStrength("aA#1111111"));
        }
    }
}
