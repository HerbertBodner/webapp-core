using WaCore.Bl.Services;
using Xunit;

namespace WaCore.UnitTests.Bl.Services
{
    public class PaswordServiceCheckStrengthTests
    {
        [Fact]
        public void PasswortEmpty_Blank()
        {
            Assert.Equal(PasswordService.PasswordScore.Blank, PasswordService.CheckStrength(""));
        }

        [Fact]
        public void Passwort_5Chars_TooShort()
        {
            Assert.Equal(PasswordService.PasswordScore.TooShort, PasswordService.CheckStrength("12345"));
        }

        [Fact]
        public void Passwort_6Digits_RequirementsNotMet()
        {
            Assert.Equal(PasswordService.PasswordScore.RequirementsNotMet, PasswordService.CheckStrength("123456"));
        }


        [Fact]
        public void Passwort_6Chars_Digits_VeryWeak()
        {
            Assert.Equal(PasswordService.PasswordScore.VeryWeak, PasswordService.CheckStrength("a234567"));
        }


        [Fact]
        public void Passwort_6LowerUpperCharsAndDigit_Weak()
        {
            Assert.Equal(PasswordService.PasswordScore.Weak, PasswordService.CheckStrength("aA1234"));
        }

        [Fact]
        public void Passwort_8LowerUpperCharAndDigit_Fair()
        {
            Assert.Equal(PasswordService.PasswordScore.Fair, PasswordService.CheckStrength("Passwor1"));
        }

        [Fact]
        public void Passwort_10LowerUpperCharsAndDigit_Medium()
        {
            Assert.Equal(PasswordService.PasswordScore.Medium, PasswordService.CheckStrength("Password123"));
        }

        [Fact]
        public void Passwort_10LowerUpperCharsAndDigitAndSpecialChar_Strong()
        {
            Assert.Equal(PasswordService.PasswordScore.Strong, PasswordService.CheckStrength("Password1#"));
        }

        [Fact]
        public void Passwort_12LowerUpperCharsAndDigitAndSpecialChar_VeryStrong()
        {
            Assert.Equal(PasswordService.PasswordScore.VeryStrong, PasswordService.CheckStrength("Password123#"));
        }

        [Fact]
        public void Passwort_10LowerUpperCharsAndDigitAndSpecialCharAndRepeatingChar_VeryWeak()
        {
            Assert.Equal(PasswordService.PasswordScore.VeryWeak, PasswordService.CheckStrength("aA#1111111"));
        }

    }
}
