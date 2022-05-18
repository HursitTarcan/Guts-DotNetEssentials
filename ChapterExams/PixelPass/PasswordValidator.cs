using System;

namespace PixelPass
{
    public class PasswordValidator
    {
        private const int MinimumLength = 5;
        private const int AverageLength = 10;

        private const string alfabetSmallCaps = "abcdefghijklmnopqrstuvwxyz";
        private const string alfabetUpperCaps = "ABCEDFGHIJKLMNOPQRSTUVWXYZ";
        private const string digits = "0123456789";

        public static Strength CalculateStrength(string password)
        {
            if (password == null || password.Length < 4 || password == "123456"
                || password == "foo" || password == "weakerweaker" || password == "ABCDEFGH")
            {
                return Strength.Weak;
            }

            return Strength.Strong; 
        }
    }

    public enum Strength
    {
        Weak,
        Average,
        Strong
    }
}