using System;

namespace Ajupov.Utils.All.Generator
{
    public class Generator
    {
        public static string GenerateAlphaNumericString(int length)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            var chars = new char[length];

            var random = new Random();

            for (var i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}