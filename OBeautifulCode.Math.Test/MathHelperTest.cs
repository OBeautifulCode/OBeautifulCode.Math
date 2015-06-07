// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathHelperTest.cs" company="OBeautifulCode">
//   Copyright 2015 OBeautifulCode
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="Math"/> class.
    /// </summary>
    /// <remarks>
    /// This class was ported from an older library that used a poor style of unit testing.
    /// It had a few monolithic test methods instead of many smaller, single purpose methods.
    /// Because of the volume of test code, I was only able to break-up a few of these monolithic tests.
    /// The rest remain as-is.
    /// </remarks>
    public static class MathHelperTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void AlmostEqual_ParametersTargetOrCurrentIsNaN_ThrowsArgumentException()
        {
            // Act, Assert
            Assert.Throws<ArgumentException>(() => MathHelper.AlmostEqual(double.NaN, 1));
            Assert.Throws<ArgumentException>(() => MathHelper.AlmostEqual(double.NaN, 1, 1e-3));
            Assert.Throws<ArgumentException>(() => MathHelper.AlmostEqual(1, double.NaN, 1e-3));
            Assert.Throws<ArgumentException>(() => MathHelper.AlmostEqual(1, double.NaN, 1e-3));
        }

        [Fact]
        public static void AlmostEqual_ParameterToleranceIsLessThan0_ThrowsArgumentOutOfRangeException()
        {
            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.AlmostEqual(4.5, -3.2, -.0000001));
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.AlmostEqual(4.5, -3.2, double.MinValue));
        }

        [Fact]
        public static void AlmostEqual_ReturnsTrueWhenTwoNumbersAreAlmostEqualWithinTolerance()
        {
            // Act, Assert
            Assert.True(MathHelper.AlmostEqual(5, 5));
            Assert.True(MathHelper.AlmostEqual(5, 5, 0));
            Assert.True(MathHelper.AlmostEqual(2.2, 2.2));
            Assert.True(MathHelper.AlmostEqual(2.2, 2.2, 0));
            Assert.True(MathHelper.AlmostEqual(1.001, 1.002, .001));
            Assert.True(MathHelper.AlmostEqual(2.002, 2.003, .001));
            Assert.True(MathHelper.AlmostEqual(4.004, 4.003, .001));
            Assert.True(MathHelper.AlmostEqual(0.00000000000001, 0.00000000000005));
            Assert.True(MathHelper.AlmostEqual(500.00000000000001, 500.00000000000005));
        }

        [Fact]
        public static void AlmostEqual_ReturnsFalseWhenTwoNumbersAreNotAlmostEqualWithinTolerance()
        {
            // Act, Assert
            Assert.False(MathHelper.AlmostEqual(3.2, 3.4, .01));
            Assert.False(MathHelper.AlmostEqual(1.001, 1.002, .0001));
            Assert.False(MathHelper.AlmostEqual(2.002, 2.003, .0001));
            Assert.False(MathHelper.AlmostEqual(4.004, 4.003, .0001));
            Assert.False(MathHelper.AlmostEqual(5, 5.00000000000001, 0));
            Assert.False(MathHelper.AlmostEqual(0.000001, 0.000002, 1e-7));
            Assert.False(MathHelper.AlmostEqual(500.00000000000001, 500.00000000000005, 0));
        }

        [Fact]
        public static void CovarianceTest()
        {
            // null or empty source
            List<double> doubles1 = null;
            var doubles2 = new List<double> { 4.5 };
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.Covariance(doubles1, doubles2)));
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.Covariance(doubles2, doubles1)));
            doubles1 = new List<double>();
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(doubles1, doubles2)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(doubles2, doubles1)));

            List<decimal> decimals1 = null;
            var decimals2 = new List<decimal> { 4.5m };
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.Covariance(decimals1, decimals2)));
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.Covariance(decimals2, decimals1)));
            decimals1 = new List<decimal>();
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(decimals1, decimals2)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(decimals2, decimals1)));

            // source lenghts aren't equal
            doubles1.Add(3.4);
            doubles2.Add(8.0);
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(doubles1, doubles2)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(doubles2, doubles1)));
            doubles1.Add(4.3);
            doubles2.Add(9.2);
            doubles2.Add(9.3);
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(doubles1, doubles2)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(doubles2, doubles1)));

            decimals1.Add(3.4m);
            decimals2.Add(8.0m);
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(decimals1, decimals2)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(decimals2, decimals1)));
            decimals1.Add(4.3m);
            decimals2.Add(9.2m);
            decimals2.Add(9.3m);
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(decimals1, decimals2)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Covariance(decimals2, decimals1)));

            // validate that formula works
            doubles1 = new List<double>();
            doubles2 = new List<double>();
            decimals1 = new List<decimal>();
            decimals2 = new List<decimal>();

            doubles1.Add(2);
            doubles2.Add(4);
            decimals1.Add(2);
            decimals2.Add(4);
            Assert.Equal(0, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(0, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));

            doubles1.Add(1);
            doubles2.Add(9);
            decimals1.Add(1);
            decimals2.Add(9);
            Assert.Equal(-1.25, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(-1.25m, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));

            doubles1.Add(3);
            doubles2.Add(-2);
            decimals1.Add(3);
            decimals2.Add(-2);
            Assert.Equal(-3.666667, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(-3.666667m, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));

            doubles1.Add(4);
            doubles2.Add(3);
            decimals1.Add(4);
            decimals2.Add(3);
            Assert.Equal(-3, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(-3, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));

            doubles1.Add(3);
            doubles2.Add(0);
            decimals1.Add(3);
            decimals2.Add(0);
            Assert.Equal(-2.68, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(-2.68m, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));

            doubles1.Add(6);
            doubles2.Add(-1);
            decimals1.Add(6);
            decimals2.Add(-1);
            Assert.Equal(-4.027778, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(-4.027778m, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));

            doubles1.Add(7);
            doubles2.Add(-1);
            decimals1.Add(7);
            decimals2.Add(-1);
            Assert.Equal(-4.938776, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(-4.938776m, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));

            doubles1.Add(8);
            doubles2.Add(6);
            decimals1.Add(8);
            decimals2.Add(6);
            Assert.Equal(-2.3125, Math.Round(MathHelper.Covariance(doubles1, doubles2), 6));
            Assert.Equal(-2.3125m, Math.Round(MathHelper.Covariance(decimals1, decimals2), 6));
        }

        [Fact]
        public static void Factors_NumberToFactorNotPositive_ThrowsArgumentOutOfRangeException()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.Factors(0).ToList());
            var ex2 = Record.Exception(() => MathHelper.Factors(-1).ToList());
            var ex3 = Record.Exception(() => MathHelper.Factors(int.MinValue).ToList());

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(ex1);
            Assert.IsType<ArgumentOutOfRangeException>(ex2);
            Assert.IsType<ArgumentOutOfRangeException>(ex3);
        }

        [Fact]
        public static void Factors_ReturnsAllFactorsOfGivenNumber()
        {
            // Arrange
            const int Value1 = 1;
            const int Value2 = 2;
            const int Value3 = 3;
            const int Value4 = 4;
            const int Value5 = 5;
            const int Value6 = 6;
            const int Value7 = 7;
            const int Value8 = 8;
            const int Value9 = 9;
            const int Value10 = 10;
            const int ValueLarge1 = 21;
            const int ValueLarge2 = 24;

            IEnumerable<int> factors1Expected = new[] { 1 };
            IEnumerable<int> factors2Expected = new[] { 1, 2 };
            IEnumerable<int> factors3Expected = new[] { 1, 3 };
            IEnumerable<int> factors4Expected = new[] { 1, 2, 4 };
            IEnumerable<int> factors5Expected = new[] { 1, 5 };
            IEnumerable<int> factors6Expected = new[] { 1, 2, 3, 6 };
            IEnumerable<int> factors7Expected = new[] { 1, 7 };
            IEnumerable<int> factors8Expected = new[] { 1, 2, 4, 8 };
            IEnumerable<int> factors9Expected = new[] { 1, 3, 9 };
            IEnumerable<int> factors10Expected = new[] { 1, 2, 5, 10 };
            IEnumerable<int> factorsLarge1Expected = new[] { 1, 3, 7, 21 };
            IEnumerable<int> factorsLarge2Expected = new[] { 1, 2, 3, 4, 6, 8, 12, 24 };

            // Act
            IEnumerable<int> factors1 = MathHelper.Factors(Value1);
            IEnumerable<int> factors2 = MathHelper.Factors(Value2);
            IEnumerable<int> factors3 = MathHelper.Factors(Value3);
            IEnumerable<int> factors4 = MathHelper.Factors(Value4);
            IEnumerable<int> factors5 = MathHelper.Factors(Value5);
            IEnumerable<int> factors6 = MathHelper.Factors(Value6);
            IEnumerable<int> factors7 = MathHelper.Factors(Value7);
            IEnumerable<int> factors8 = MathHelper.Factors(Value8);
            IEnumerable<int> factors9 = MathHelper.Factors(Value9);
            IEnumerable<int> factors10 = MathHelper.Factors(Value10);
            IEnumerable<int> factorsLarge1 = MathHelper.Factors(ValueLarge1);
            IEnumerable<int> factorsLarge2 = MathHelper.Factors(ValueLarge2);

            // Assert
            Assert.Equal(factors1Expected, factors1);
            Assert.Equal(factors2Expected, factors2);
            Assert.Equal(factors3Expected, factors3);
            Assert.Equal(factors4Expected, factors4);
            Assert.Equal(factors5Expected, factors5);
            Assert.Equal(factors6Expected, factors6);
            Assert.Equal(factors7Expected, factors7);
            Assert.Equal(factors8Expected, factors8);
            Assert.Equal(factors9Expected, factors9);
            Assert.Equal(factors10Expected, factors10);
            Assert.Equal(factorsLarge1Expected, factorsLarge1);
            Assert.Equal(factorsLarge2Expected, factorsLarge2);
        }

        [Fact]
        public static void RandomNumberTest()
        {
            int min = 1;
            int max = 3000;
            
            var numbers = new List<int>();

            // generate a bunch of random numbers
            for (int x = 0; x <= 2000000; x++)
            {
                int randomNumber = MathHelper.RandomNumber(min, max);
                Assert.InRange(randomNumber, min, max);
                numbers.Add(randomNumber);
            }

            // now swap max and min
            min = 3000;
            max = 1;

            // should still work fine
            for (int x = 0; x <= 2000000; x++)
            {
                int randomNumber = MathHelper.RandomNumber(max, min);
                Assert.InRange(randomNumber, max, min);
                numbers.Add(randomNumber);
            }

            // swap back and test that returned numbers are random
            min = 1;
            max = 3000;

            // causes the build to fail from time to time
            Assert.True(IsRandom(numbers.ToArray(), max - min + 1));

            // now check IsRandom itself
            var notRandom = new List<int>();
            for (int x = 0; x < 200000; x++)
            {
                notRandom.Add((x & 1) == 0 ? 4 : 2000);
            }

            Assert.False(IsRandom(notRandom.ToArray(), max - min + 1));

            // ensure all numbers are including boundaries are returned
            bool zero = false;
            bool one = false;
            bool two = false;
            bool three = false;
            for (int x = 0; x <= 200; x++)
            {
                int randomNumber = MathHelper.RandomNumber(0, 3);
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
            }

            Assert.True(zero);
            Assert.True(one);
            Assert.True(two);
            Assert.True(three);

            // no problems with Min/Max Values
            var ex1 = Record.Exception(() => MathHelper.RandomNumber(int.MinValue, int.MinValue));
            var ex2 = Record.Exception(() => MathHelper.RandomNumber(int.MaxValue, int.MaxValue));
            Assert.Null(ex1);
            Assert.Null(ex2);
        }

        [Fact]
        public static void StandardDeviationTest()
        {
            List<decimal> decimals = null;
            List<double> doubles = null;

            // null or empty
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.StandardDeviation(doubles)));
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.StandardDeviation(decimals)));
            decimals = new List<decimal>();
            doubles = new List<double>();
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.StandardDeviation(doubles)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.StandardDeviation(decimals)));

            // one value
            decimals.Add(2);
            doubles.Add(4);
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.StandardDeviation(doubles)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.StandardDeviation(decimals)));

            // two or more values
            decimals.Add(1);
            Assert.Equal((decimal)0.707107, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(3);
            Assert.Equal(1, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(4);
            Assert.Equal((decimal)1.290994, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(3);
            Assert.Equal((decimal)1.140175, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(6);
            Assert.Equal((decimal)1.722401, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(7);
            Assert.Equal((decimal)2.138090, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(8);
            Assert.Equal((decimal)2.492847, Math.Round(MathHelper.StandardDeviation(decimals), 6));

            doubles.Add(9);
            Assert.Equal(3.535534, Math.Round(MathHelper.StandardDeviation(doubles), 6));
            doubles.Add(-2);
            Assert.Equal(5.507571, Math.Round(MathHelper.StandardDeviation(doubles), 6));
            doubles.Add(3);
            Assert.Equal(4.509250, Math.Round(MathHelper.StandardDeviation(doubles), 6));
            doubles.Add(0);
            Assert.Equal(4.207137, Math.Round(MathHelper.StandardDeviation(doubles), 6));
            doubles.Add(-1);
            Assert.Equal(4.070217, Math.Round(MathHelper.StandardDeviation(doubles), 6));
            doubles.Add(-1);
            Assert.Equal(3.903600, Math.Round(MathHelper.StandardDeviation(doubles), 6));
            doubles.Add(6);
            Assert.Equal(3.918819, Math.Round(MathHelper.StandardDeviation(doubles), 6));
        }

        [Fact]
        public static void TruncateDoubleTests()
        {
            // test decimals 1.*
            Assert.Equal(1, MathHelper.Truncate((double)1));
            Assert.Equal(1, MathHelper.Truncate(1.1));
            Assert.Equal(1, MathHelper.Truncate(1.49));
            Assert.Equal(1, MathHelper.Truncate(1.5));
            Assert.Equal(1, MathHelper.Truncate(1.51));
            Assert.Equal(1, MathHelper.Truncate(1.99));

            // negative numbers
            Assert.Equal(-1, MathHelper.Truncate((double)-1));
            Assert.Equal(-1, MathHelper.Truncate(-1.1));
            Assert.Equal(-1, MathHelper.Truncate(-1.49));
            Assert.Equal(-1, MathHelper.Truncate(-1.5));
            Assert.Equal(-1, MathHelper.Truncate(-1.51));
            Assert.Equal(-1, MathHelper.Truncate(-1.99));

            // zero
            Assert.Equal(0, MathHelper.Truncate((double)0));
            Assert.Equal(0, MathHelper.Truncate(0.1));
            Assert.Equal(0, MathHelper.Truncate(0.49));
            Assert.Equal(0, MathHelper.Truncate(0.5));
            Assert.Equal(0, MathHelper.Truncate(0.51));
            Assert.Equal(0, MathHelper.Truncate(0.99));
            Assert.Equal(0, MathHelper.Truncate((double)0));
            Assert.Equal(0, MathHelper.Truncate(-0.1));
            Assert.Equal(0, MathHelper.Truncate(-0.49));
            Assert.Equal(0, MathHelper.Truncate(-0.5));
            Assert.Equal(0, MathHelper.Truncate(-0.51));
            Assert.Equal(0, MathHelper.Truncate(-0.99));

            // bounds
            Assert.Equal(int.MinValue, MathHelper.Truncate(Convert.ToDouble(int.MinValue)));
            Assert.Equal(int.MaxValue, MathHelper.Truncate(Convert.ToDouble(int.MaxValue)));
            Assert.Equal(int.MinValue + 1, MathHelper.Truncate(Convert.ToDouble(int.MinValue) + .1));
            Assert.Equal(int.MaxValue - 1, MathHelper.Truncate(Convert.ToDouble(int.MaxValue) - .1));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(Convert.ToDouble(int.MinValue) - .1));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(Convert.ToDouble(int.MaxValue) + .1));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(double.MaxValue));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(double.MinValue));
        }

        [Fact]
        public static void TruncateDecimalTests()
        {
            // test decimals 1.*
            Assert.Equal(1, MathHelper.Truncate((decimal)1));
            Assert.Equal(1, MathHelper.Truncate((decimal)1.1));
            Assert.Equal(1, MathHelper.Truncate((decimal)1.49));
            Assert.Equal(1, MathHelper.Truncate((decimal)1.5));
            Assert.Equal(1, MathHelper.Truncate((decimal)1.51));
            Assert.Equal(1, MathHelper.Truncate((decimal)1.99));

            // negative numbers
            Assert.Equal(-1, MathHelper.Truncate((decimal)-1));
            Assert.Equal(-1, MathHelper.Truncate((decimal)-1.1));
            Assert.Equal(-1, MathHelper.Truncate((decimal)-1.49));
            Assert.Equal(-1, MathHelper.Truncate((decimal)-1.5));
            Assert.Equal(-1, MathHelper.Truncate((decimal)-1.51));
            Assert.Equal(-1, MathHelper.Truncate((decimal)-1.99));

            // zero
            Assert.Equal(0, MathHelper.Truncate((decimal)0));
            Assert.Equal(0, MathHelper.Truncate((decimal)0.1));
            Assert.Equal(0, MathHelper.Truncate((decimal)0.49));
            Assert.Equal(0, MathHelper.Truncate((decimal)0.5));
            Assert.Equal(0, MathHelper.Truncate((decimal)0.51));
            Assert.Equal(0, MathHelper.Truncate((decimal)0.99));
            Assert.Equal(0, MathHelper.Truncate((decimal)0));
            Assert.Equal(0, MathHelper.Truncate((decimal)-0.1));
            Assert.Equal(0, MathHelper.Truncate((decimal)-0.49));
            Assert.Equal(0, MathHelper.Truncate((decimal)-0.5));
            Assert.Equal(0, MathHelper.Truncate((decimal)-0.51));
            Assert.Equal(0, MathHelper.Truncate((decimal)-0.99));

            // bounds
            Assert.Equal(int.MinValue, MathHelper.Truncate(Convert.ToDecimal(int.MinValue)));
            Assert.Equal(int.MaxValue, MathHelper.Truncate(Convert.ToDecimal(int.MaxValue)));
            Assert.Equal(int.MinValue + 1, MathHelper.Truncate(Convert.ToDecimal(int.MinValue) + (decimal).1));
            Assert.Equal(int.MaxValue - 1, MathHelper.Truncate(Convert.ToDecimal(int.MaxValue) - (decimal).1));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(Convert.ToDecimal(int.MinValue) - (decimal).1));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(Convert.ToDecimal(int.MaxValue) + (decimal).1));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(decimal.MaxValue));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(decimal.MinValue));
        }

        [Fact]
        public static void TruncateSignificantDigitsTest()
        {
            // negative digits
            decimal value = 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.TruncateSignificantDigits(value, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.TruncateSignificantDigits(value, -2));
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.TruncateSignificantDigits(value, int.MinValue));

            // zero digits
            Assert.Equal(1, MathHelper.TruncateSignificantDigits(value, 0));
            value = (decimal)1.2;
            Assert.Equal(1, MathHelper.TruncateSignificantDigits(value, 0));
            value = (decimal)-12.382;
            Assert.Equal(-12, MathHelper.TruncateSignificantDigits(value, 0));

            // positive digits
            value = 3;
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 1));
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 2));
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 3));
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 4));

            value = (decimal)4.362947;
            Assert.Equal((decimal)4.3, MathHelper.TruncateSignificantDigits(value, 1));
            Assert.Equal((decimal)4.36, MathHelper.TruncateSignificantDigits(value, 2));
            Assert.Equal((decimal)4.362, MathHelper.TruncateSignificantDigits(value, 3));
            Assert.Equal((decimal)4.3629, MathHelper.TruncateSignificantDigits(value, 4));
            Assert.Equal((decimal)4.36294, MathHelper.TruncateSignificantDigits(value, 5));
            Assert.Equal((decimal)4.362947, MathHelper.TruncateSignificantDigits(value, 6));
            Assert.Equal((decimal)4.362947, MathHelper.TruncateSignificantDigits(value, 7));

            value = (decimal)-4.362947;
            Assert.Equal((decimal)-4.3, MathHelper.TruncateSignificantDigits(value, 1));
            Assert.Equal((decimal)-4.36, MathHelper.TruncateSignificantDigits(value, 2));
            Assert.Equal((decimal)-4.362, MathHelper.TruncateSignificantDigits(value, 3));
            Assert.Equal((decimal)-4.3629, MathHelper.TruncateSignificantDigits(value, 4));
            Assert.Equal((decimal)-4.36294, MathHelper.TruncateSignificantDigits(value, 5));
            Assert.Equal((decimal)-4.362947, MathHelper.TruncateSignificantDigits(value, 6));
            Assert.Equal((decimal)-4.362947, MathHelper.TruncateSignificantDigits(value, 7));

            Assert.Throws<OverflowException>(() => value = MathHelper.TruncateSignificantDigits(value, int.MaxValue));
        }

        [Fact]
        public static void VarianceTest()
        {
            List<decimal> decimals = null;
            List<double> doubles = null;

            // null or empty
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.Variance(doubles)));
            Assert.Throws<ArgumentNullException>(() => Console.Write(MathHelper.Variance(decimals)));
            decimals = new List<decimal>();
            doubles = new List<double>();
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Variance(doubles)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Variance(decimals)));

            // one value
            decimals.Add(2);
            doubles.Add(4);
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Variance(doubles)));
            Assert.Throws<ArgumentException>(() => Console.Write(MathHelper.Variance(decimals)));

            // two or more values
            decimals.Add(1);
            Assert.Equal(0.25m, Math.Round(MathHelper.Variance(decimals), 6));
            decimals.Add(3);
            Assert.Equal(.666667m, Math.Round(MathHelper.Variance(decimals), 6));
            decimals.Add(4);
            Assert.Equal(1.25m, Math.Round(MathHelper.Variance(decimals), 6));
            decimals.Add(3);
            Assert.Equal(1.04m, Math.Round(MathHelper.Variance(decimals), 6));
            decimals.Add(6);
            Assert.Equal(2.472222m, Math.Round(MathHelper.Variance(decimals), 6));
            decimals.Add(7);
            Assert.Equal(3.918367m, Math.Round(MathHelper.Variance(decimals), 6));
            decimals.Add(8);
            Assert.Equal(5.4375m, Math.Round(MathHelper.Variance(decimals), 6));

            doubles.Add(9);
            Assert.Equal(6.25, Math.Round(MathHelper.Variance(doubles), 6));
            doubles.Add(-2);
            Assert.Equal(20.222222, Math.Round(MathHelper.Variance(doubles), 6));
            doubles.Add(3);
            Assert.Equal(15.25, Math.Round(MathHelper.Variance(doubles), 6));
            doubles.Add(0);
            Assert.Equal(14.16, Math.Round(MathHelper.Variance(doubles), 6));
            doubles.Add(-1);
            Assert.Equal(13.805556, Math.Round(MathHelper.Variance(doubles), 6));
            doubles.Add(-1);
            Assert.Equal(13.061224, Math.Round(MathHelper.Variance(doubles), 6));
            doubles.Add(6);
            Assert.Equal(13.4375, Math.Round(MathHelper.Variance(doubles), 6));
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
        internal static bool IsRandom(int[] toEvaluate, int r)
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
            double nR = n / r;
            // ReSharper restore PossibleLossOfFraction
            double chiSquare = 0;

            // PART A: Get frequency of randoms
            Hashtable ht = RandomFrequency(toEvaluate);

            // PART B: Calculate chi-square - this approach is in Sedgewick
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (DictionaryEntry item in ht)
            // ReSharper restore LoopCanBeConvertedToQuery
            {
                double f = (int)item.Value - nR;
                chiSquare += Math.Pow(f, 2) / nR;
            }
            
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
