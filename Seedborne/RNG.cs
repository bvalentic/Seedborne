using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne
{
    public class Rng
    {//(pseudo)random number generator

        public string Seed { get; }
        public double SeedValue { get; }

        public Rng(string seed)
        {
            Seed = seed;
            SeedValue = Germinate(Seed);
        }

        public double Germinate(string seed)
        {
            var splitSeed = Seed.ToCharArray();
            var seedValues = new double[splitSeed.Length + 1];
            var asciiValues = Encoding.ASCII.GetBytes(splitSeed);
            var seedLength = seedValues.Length;
            seedValues[seedLength - 1] = 1;

            for (var i = 0; i < asciiValues.Length; i++)
            {
                var value = Convert.ToDouble(asciiValues[i]);
                seedValues[i] = value;
            }

            for (var j = 0; j < seedLength; j++)
            {
                seedValues[seedLength - 1] *= (seedValues[j]);
            }

            var seedValue = Math.Abs(seedValues[seedLength - 1]);

            return seedValue;
        }

        public int Rand()
        {
            string seedString = SeedValue.ToString();
            int randValue = 0;

            if (seedString.Length >= 6)
            {
                var randString = seedString[4].ToString() + seedString[5].ToString();
                randValue = int.Parse(randString);
            }
            

            return randValue;
        }

    }
}
