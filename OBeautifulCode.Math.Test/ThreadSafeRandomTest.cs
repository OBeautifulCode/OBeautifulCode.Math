// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadSafeRandomTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Test
{
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
            Assert.True(MathHelperTest.IsRandom(numbers.ToArray(), Max));

            // now check IsRandom itself
            var notRandom = new ConcurrentBag<int>();
            Parallel.For(0, 2000001, x => notRandom.Add((x & 1) == 0 ? 4 : 2000));
            Assert.False(MathHelperTest.IsRandom(notRandom.ToArray(), Max));

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
            Assert.True(MathHelperTest.IsRandom(numbers.ToArray(), Max));

            // now check IsRandom itself
            var notRandom = new ConcurrentBag<int>();
            Parallel.For(0, 2000001, x => notRandom.Add((x & 1) == 0 ? 4 : 2000));
            Assert.False(MathHelperTest.IsRandom(notRandom.ToArray(), Max - Min));

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

        // ReSharper restore InconsistentNaming
    }
}
