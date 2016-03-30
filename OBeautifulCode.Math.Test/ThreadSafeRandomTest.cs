// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadSafeRandomTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Test
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="ThreadSafeRandom"/> class.
    /// </summary>
    /// <remarks>
    /// This class was ported from an older library that used a poor style of unit testing.
    /// It had a few monolithic test methods instead of many smaller, single purpose methods.
    /// Because of the volume of test code, I was only able to break-up a few of these monolithic tests.
    /// The rest remain as-is.
    /// </remarks>
    public static class ThreadSafeRandomTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Next_MaxValueProvided_GeneratesRandomNumbers()
        {
            const int Max = 3000;
            var numbers = new ConcurrentBag<int>();

            // generate a bunch of random numbers
            Parallel.For(
                0,
                2000001,
                x =>
                {
                    // ReSharper disable AccessToModifiedClosure
                    int randomNumber = ThreadSafeRandom.Next(Max);
                    Assert.InRange(randomNumber, 0, Max - 1);
                    numbers.Add(randomNumber);
                    // ReSharper restore AccessToModifiedClosure
                });

            // this next line causes the build to fail from time to time
            Assert.True(IsRandom(numbers.ToArray(), Max));

            // now check IsRandom itself
            var notRandom = new ConcurrentBag<int>();
            Parallel.For(0, 2000001, x => notRandom.Add((x & 1) == 0 ? 4 : 2000));
            Assert.False(IsRandom(notRandom.ToArray(), Max));

            // ensure all numbers are including boundaries are returned
            bool zero = false;
            bool one = false;
            bool two = false;
            bool three = false;
            Parallel.For(
                0,
                201,
                x =>
                {
                    int randomNumber = ThreadSafeRandom.Next(0, 4);
                    Assert.InRange(randomNumber, 0, 3);
                    if (randomNumber == 0)
                    {
                        zero = true;
                    }

                    if (randomNumber == 1)
                    {
                        one = true;
                    }

                    if (randomNumber == 2)
                    {
                        two = true;
                    }

                    if (randomNumber == 3)
                    {
                        three = true;
                    }
                });

            Assert.True(zero);
            Assert.True(one);
            Assert.True(two);
            Assert.True(three);

            // no problems with Min/Max Values
            var ex = Record.Exception(() => ThreadSafeRandom.Next(int.MinValue, int.MinValue));
            Assert.Null(ex);
        }

        [Fact]
        public static void Next_MinAndMaxValueProvided_GeneratesRandomNumbers()
        {
            const int Min = 1;
            const int Max = 3000;
            var numbers = new ConcurrentBag<int>();

            // generate a bunch of random numbers
            Parallel.For(
                0,
                2000001,
                x =>
                {
                    // ReSharper disable AccessToModifiedClosure
                    int randomNumber = ThreadSafeRandom.Next(Min, Max);
                    Assert.InRange(randomNumber, Min, Max - 1);
                    numbers.Add(randomNumber);
                    // ReSharper restore AccessToModifiedClosure
                });

            // this next line causes the build to fail from time to time
            Assert.True(IsRandom(numbers.ToArray(), Max));

            // now check IsRandom itself
            var notRandom = new ConcurrentBag<int>();
            Parallel.For(0, 2000001, x => notRandom.Add((x & 1) == 0 ? 4 : 2000));
            Assert.False(IsRandom(notRandom.ToArray(), Max - Min));

            // ensure all numbers are including boundaries are returned
            bool zero = false;
            bool one = false;
            bool two = false;
            bool three = false;
            Parallel.For(
                0,
                201,
                x =>
                {
                    int randomNumber = ThreadSafeRandom.Next(0, 4);
                    Assert.InRange(randomNumber, 0, 3);
                    if (randomNumber == 0)
                    {
                        zero = true;
                    }

                    if (randomNumber == 1)
                    {
                        one = true;
                    }

                    if (randomNumber == 2)
                    {
                        two = true;
                    }

                    if (randomNumber == 3)
                    {
                        three = true;
                    }
                });

            Assert.True(zero);
            Assert.True(one);
            Assert.True(two);
            Assert.True(three);

            // no problems with Min/Max Values
            var ex = Record.Exception(() => ThreadSafeRandom.Next(int.MinValue, int.MinValue));
            Assert.Null(ex);
        }

        /// <summary>
        /// Determines if a set of numbers is random.
        /// </summary>
        /// <param name="toEvaluate">The random numbers to test.</param>
        /// <param name="r">The the size of the range of numbers.</param>
        /// <remarks>
        /// See here for algorithm: <a href="https://en.wikibooks.org/wiki/Algorithm_Implementation/Pseudorandom_Numbers/Chi-Square_Test"/>.
        /// </remarks>
        /// <returns>
        /// Returns true if the set of numbers is random, false if not.
        /// </returns>
        private static bool IsRandom(int[] toEvaluate, int r)
        {
            // Calculates the chi-square value for N positive integers less than r
            // Source: "Algorithms in C" - Robert Sedgewick - pp. 517
            // NB: Sedgewick recommends: "...to be sure, the test should be tried a few times,
            // since it could be wrong in about one out of ten times."

            // Calculate the number of samples - N
            int n = toEvaluate.Length;

            // According to Sedgewick: "This is valid if N is greater than about 10r"
            if (n <= 10 * r)
            {
                return false;
            }

            // ReSharper disable PossibleLossOfFraction
#pragma warning disable SA1305 // Field names must not use Hungarian notation
            double nR = n / r;
#pragma warning restore SA1305 // Field names must not use Hungarian notation
            // ReSharper restore PossibleLossOfFraction

            double chiSquare = 0;

            // PART A: Get frequency of randoms
            Hashtable ht = RandomFrequency(toEvaluate);

            // PART B: Calculate chi-square - this approach is in Sedgewick
            // ReSharper disable LoopCanBeConvertedToQuery

            foreach (DictionaryEntry item in ht)
            {
                double f = (int)item.Value - nR;
                chiSquare += Math.Pow(f, 2) / nR;
            }

            // ReSharper restore LoopCanBeConvertedToQuery

            // PART C: According to Swdgewick: "The statistic should be within 2(r)^1/2 of r
            // This is valid if N is greater than about 10r"
            if ((r - (2 * Math.Sqrt(r)) <= chiSquare) & (r + (2 * Math.Sqrt(r)) >= chiSquare))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the frequency of occurrence of a randomly generated array of integers
        /// </summary>
        /// <param name="randomNums">The random numbers to test.</param>
        /// <returns>
        /// A hash table, key being the random number and value its frequency.
        /// </returns>
        private static Hashtable RandomFrequency(int[] randomNums)
        {
            var ht = new Hashtable();
            int n = randomNums.Length;

            for (int i = 0; i <= n - 1; i++)
            {
                if (ht.ContainsKey(randomNums[i]))
                {
                    ht[randomNums[i]] = (int)ht[randomNums[i]] + 1;
                }
                else
                {
                    ht[randomNums[i]] = 1;
                }
            }

            return ht;
        }

        // ReSharper restore InconsistentNaming
    }
}
