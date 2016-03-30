// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadSafeRandomTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Test
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="ThreadSafeRandom"/> class.
    /// </summary>
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
        /// <param name="numbers">The random numbers to test.</param>
        /// <param name="range">The the size of the range of numbers.</param>
        /// <remarks>
        /// See here for algorithm: <a href="https://en.wikibooks.org/wiki/Algorithm_Implementation/Pseudorandom_Numbers/Chi-Square_Test"/>.
        /// </remarks>
        /// <returns>
        /// Returns true if the set of numbers is random, false if not.
        /// </returns>
        private static bool IsRandom(int[] numbers, int range)
        {
#pragma warning disable SA1305 // Field names must not use Hungarian notation
            // Calculates the chi-square value for N positive integers less than r
            // Source: "Algorithms in C" - Robert Sedgewick - pp. 517
            // NB: Sedgewick recommends: "...to be sure, the test should be tried a few times,
            // since it could be wrong in about one out of ten times."

            // Calculate the number of samples - N
            int n = numbers.Length;

            // According to Sedgewick: "This is valid if N is greater than about 10r"
            if (n <= 10 * range)
            {
                return false;
            }

            double nR = Convert.ToDouble(n) / Convert.ToDouble(range);
            double chiSquare = 0;

            // PART A: Get frequency of randoms
            var frequencies = CalculateFrequency(numbers);

            // PART B: Calculate chi-square - this approach is in Sedgewick
            // ReSharper disable LoopCanBeConvertedToQuery

            foreach (var frequency in frequencies.Values)
            {
                double f = frequency - nR;
                chiSquare += Math.Pow(f, 2) / nR;
            }

            // ReSharper restore LoopCanBeConvertedToQuery

            // PART C: According to Swdgewick: "The statistic should be within 2(r)^1/2 of r
            // This is valid if N is greater than about 10r"
            var result = (range - (2 * Math.Sqrt(range)) <= chiSquare) & (range + (2 * Math.Sqrt(range)) >= chiSquare);
            return result;
#pragma warning restore SA1305 // Field names must not use Hungarian notation
        }

        /// <summary>
        /// Calculates the frequency of occurrence of each integer in a set of integers.
        /// </summary>
        /// <param name="numbers">The integers to count.</param>
        /// <returns>
        /// A dictionary where keys are the numbers observed and values are their frequency.
        /// </returns>
        private static IDictionary<int, int> CalculateFrequency(int[] numbers)
        {
            var result = new Dictionary<int, int>();
            foreach (int number in numbers)
            {
                if (result.ContainsKey(number))
                {
                    result[number] = result[number] + 1;
                }
                else
                {
                    result[number] = 1;
                }
            }

            return result;
        }

        // ReSharper restore InconsistentNaming
    }
}
