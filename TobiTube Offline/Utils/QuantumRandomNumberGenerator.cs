using System;
using System.Linq;
using System.Security.Cryptography;

namespace Utils
{
    public static class RandomNumberGenerator
    {
        private static uint seed1;
        private static uint seed2;
        private static uint seed3;
        private static readonly byte[] hmacKey;
        private static readonly object lockObj = new object();

        static RandomNumberGenerator()
        {
            // Инициализация ключа HMAC и начальных значений
            using (var rng = new RNGCryptoServiceProvider())
            {
                hmacKey = new byte[64];
                rng.GetBytes(hmacKey);
            }

            // Инициализация seed'ов
            ReinitializeSeeds();
        }

        public static void ReinitializeSeeds()
        {
            seed1 = (uint)DateTime.Now.Ticks;
            seed2 = (uint)Environment.TickCount;

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[4];
                rng.GetBytes(buffer);
                seed3 = BitConverter.ToUInt32(buffer, 0);
            }
        }

        public static float GetRandomFloat()
        {
            lock (lockObj)
            {
                // Создаем новый экземпляр HMAC для потокобезопасности
                using (var hmac = new HMACSHA256(hmacKey))
                {
                    byte[] seedBytes = BitConverter.GetBytes(seed1)
                        .Concat(BitConverter.GetBytes(seed2))
                        .Concat(BitConverter.GetBytes(seed3))
                        .ToArray();

                    byte[] hash = hmac.ComputeHash(seedBytes);

                    // Обновляем seed'ы для следующего вызова
                    seed1 = BitConverter.ToUInt32(hash, 0);
                    seed2 = BitConverter.ToUInt32(hash, 4);
                    seed3 = BitConverter.ToUInt32(hash, 8);

                    // Используем последние 4 байта для генерации float
                    uint randomUint = BitConverter.ToUInt32(hash, 28);
                    return randomUint / (uint.MaxValue + 1f);
                }
            }
        }

        public static int GetRandomInt(int min, int max)
        {
            if (min >= max)
                throw new ArgumentException("min must be less than max");

            float randomFloat = GetRandomFloat();
            int range = max - min + 1;
            return (int)(randomFloat * range) + min;
        }
    }
}