using System;
using Bogus;

namespace UnitTests.Helpers
{
    public static class Fake
    {
        static Faker random;

        static Fake()
        {
            random = new Faker();
        }

        public static int GenerateRandomId(int digits)
        {
            return random.Random.Number(
                (int)Math.Pow(10, digits-1), (int)Math.Pow(10, digits) - 1);
        }

        public static string GenerateRandomText()
        {
            return random.Lorem.Sentence(6);
        }
    }
}
