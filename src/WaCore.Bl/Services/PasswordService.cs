using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Cryptography.KeyDerivation;
using WaCore.Contracts.Bl.Services;
using WaCore.Contracts.Enums;

namespace WaCore.Bl.Services
{
    public class PasswordService : IPasswordService
    {
        const int ITERATIONS = 10000;

        public PasswordScore CheckStrength(string password)
        {
            int score = 0;

            // using three requirements here:  min length and two types of characters (numbers and letters)
            bool blnMinLengthRequirementMet = false;
            bool blnRequirement1Met = false;
            bool blnRequirement2Met = false;

            // check for chars in password
            if (password.Length < 1)
                return PasswordScore.Blank;

            // if less than 6 chars, return as too short, else, plus one
            if (password.Length < 6)
            {
                return PasswordScore.TooShort;
            }
            else
            {
                score++;
                blnMinLengthRequirementMet = true;
            }

            // if 8 or more chars, plus one
            if (password.Length >= 8)
                score++;

            // if 10 or more chars, plus one
            if (password.Length >= 10)
                score++;

            // if 12 or more chars, plus one
            if (password.Length >= 12)
                score++;

            // if password has a number, plus one
            if (Regex.IsMatch(password, @"[\d]", RegexOptions.ECMAScript))
            {
                score++;
                blnRequirement1Met = true;
            }

            // if password has lower case letter, plus one
            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript))
            {
                score++;
                blnRequirement2Met = true;
            }

            // if password has upper case letter, plus one
            if (Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
            {
                score++;
                blnRequirement2Met = true;
            }

            // if password has a special character, plus one
            if (Regex.IsMatch(password, @"[~`!@#$%\^\&\*\(\)\-_\+=\[\{\]\}\|\\;:'\""<\,>\.\?\/£]", RegexOptions.ECMAScript))
                score++;

            // if password is longer than 2 characters and has 3 repeating characters, minus one (to minimum of score of 3)
            List<char> lstPass = password.ToList();
            if (lstPass.Count >= 3)
            {
                for (int i = 2; i < lstPass.Count; i++)
                {
                    char charCurrent = lstPass[i];
                    if (charCurrent == lstPass[i - 1] && charCurrent == lstPass[i - 2] && score >= 4)
                    {
                        score--;
                    }
                }
            }

            if (!blnMinLengthRequirementMet || !blnRequirement1Met || !blnRequirement2Met)
            {
                return PasswordScore.RequirementsNotMet;
            }

            return (PasswordScore)score;
        }

        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            var saltBase64 = Convert.ToBase64String(salt);
            string hashedPwdBase64 = Convert.ToBase64String(GetHashedPassword(password, salt, ITERATIONS));
            return saltBase64 + ":" + hashedPwdBase64;
        }

        private byte[] GetHashedPassword(string password, byte[] salt, int iteration)
        {
            return KeyDerivation.Pbkdf2(
                            password: password,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA512,
                            iterationCount: iteration,
                            numBytesRequested: 256 / 8);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var saltBase64 = Convert.ToBase64String(salt);
            return salt;
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            if (split.Length != 2)
            {
                return false;
            }
            var salt = Convert.FromBase64String(split[0]);
            var pwdHash = Convert.FromBase64String(split[1]);

            byte[] testHash = GetHashedPassword(password, salt, ITERATIONS);
            return SlowEquals(pwdHash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }
    }
}
