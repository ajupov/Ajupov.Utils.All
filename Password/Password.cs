using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Ajupov.Utils.All.Password
{
    public static class Password
    {
        private const int IterationsCount = 1000;
        private const int HashLength = 256;
        private const int SaltLength = 16;

        public static string ToPasswordHash(string value)
        {
            var salt = GetSalt();
            var bytes = GetBytes(value, salt);
            var array = GetArray(salt);
            var result = array;

            CopyArray(bytes, result);

            return Convert.ToBase64String(array);
        }

        public static bool IsVerifiedPassword(string value, string hashedValue)
        {
            var hashedPasswordArray = Convert.FromBase64String(hashedValue);
            if (hashedPasswordArray.Length != HashLength)
            {
                return false;
            }

            var salt = GetVerifySalt(hashedPasswordArray);
            var array = GetVerifyArray(hashedPasswordArray);

            return IsEqual(
                KeyDerivation.Pbkdf2(value, salt, KeyDerivationPrf.HMACSHA512, IterationsCount,
                    HashLength - SaltLength - 1), array);
        }

        private static byte[] GetSalt()
        {
            byte[] salt;

            using (var generator = RandomNumberGenerator.Create())
            {
                salt = new byte[SaltLength];
                generator.GetBytes(salt);
            }

            return salt;
        }

        private static byte[] GetBytes(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, IterationsCount,
                HashLength - SaltLength - 1);
        }

        private static byte[] GetArray(byte[] salt)
        {
            var array = new byte[HashLength];
            array[0] = 0;

            Buffer.BlockCopy(salt, 0, array, 1, SaltLength);

            return array;
        }

        private static void CopyArray(byte[] bytes, byte[] result)
        {
            Buffer.BlockCopy(bytes, 0, result, SaltLength + 1, HashLength - SaltLength - 1);
        }

        private static byte[] GetVerifyArray(byte[] hashedPasswordArray)
        {
            var array = new byte[HashLength - SaltLength - 1];

            Buffer.BlockCopy(hashedPasswordArray, SaltLength + 1, array, 0, HashLength - SaltLength - 1);

            return array;
        }

        private static byte[] GetVerifySalt(byte[] hashedPasswordArray)
        {
            var salt = new byte[SaltLength];

            Buffer.BlockCopy(hashedPasswordArray, 1, salt, 0, salt.Length);

            return salt;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool IsEqual(IReadOnlyList<byte> byteArray1, IReadOnlyList<byte> byteArray2)
        {
            if (byteArray1 == null && byteArray2 == null)
            {
                return true;
            }

            if (byteArray1 == null || byteArray2 == null || byteArray1.Count != byteArray2.Count)
            {
                return false;
            }

            var result = true;

            for (var index = 0; index < byteArray1.Count; ++index)
            {
                result &= byteArray1[index] == byteArray2[index];
            }

            return result;
        }
    }
}