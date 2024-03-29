﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Recipes.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static class MathHelperTest
    {
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
        public static void GenerateTruthTable___Should_throw_ArgumentOutOfRangeException___When_parameter_numberOfInputs_is_less_than_0()
        {
            // Arrange, Act
            var actual = Record.Exception(() => MathHelper.GenerateTruthTable(A.Dummy<NegativeInteger>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void GenerateTruthTable___Should_return_expected_truth_table___When_parameter_resultsInBigEndian_is_true()
        {
            // Arrange
            var numberOfInputsAndExpected = new[]
            {
                new { NumberOfInputs = 0, Expected = new List<List<bool>>() },
                new
                {
                    NumberOfInputs = 1,
                    Expected = new List<List<bool>>
                    {
                        new List<bool> { false },
                        new List<bool> { true },
                    },
                },
                new
                {
                    NumberOfInputs = 2,
                    Expected = new List<List<bool>>
                    {
                        new List<bool> { false, false },
                        new List<bool> { false, true },
                        new List<bool> { true, false },
                        new List<bool> { true, true },
                    },
                },
                new
                {
                    NumberOfInputs = 3,
                    Expected = new List<List<bool>>
                    {
                        new List<bool> { false, false, false },
                        new List<bool> { false, false, true },
                        new List<bool> { false, true, false },
                        new List<bool> { false, true, true },
                        new List<bool> { true, false, false },
                        new List<bool> { true, false, true },
                        new List<bool> { true, true, false },
                        new List<bool> { true, true, true },
                    },
                },
            };

            var expected = numberOfInputsAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = numberOfInputsAndExpected.Select(_ => MathHelper.GenerateTruthTable(_.NumberOfInputs, resultInBigEndian: true).Select(i => i.ToList()).ToList()).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GenerateTruthTable___Should_return_expected_truth_table___When_parameter_resultsInBigEndian_is_false()
        {
            // Arrange
            var numberOfInputsAndExpected = new[]
            {
                new { NumberOfInputs = 0, Expected = new List<List<bool>>() },
                new
                {
                    NumberOfInputs = 1,
                    Expected = new List<List<bool>>
                    {
                        new List<bool> { false },
                        new List<bool> { true },
                    },
                },
                new
                {
                    NumberOfInputs = 2,
                    Expected = new List<List<bool>>
                    {
                        new List<bool> { false, false },
                        new List<bool> { true, false },
                        new List<bool> { false, true },
                        new List<bool> { true, true },
                    },
                },
                new
                {
                    NumberOfInputs = 3,
                    Expected = new List<List<bool>>
                    {
                        new List<bool> { false, false, false },
                        new List<bool> { true, false, false },
                        new List<bool> { false, true, false },
                        new List<bool> { true, true, false },
                        new List<bool> { false, false, true },
                        new List<bool> { true, false, true },
                        new List<bool> { false, true, true },
                        new List<bool> { true, true, true },
                    },
                },
            };

            var expected = numberOfInputsAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = numberOfInputsAndExpected.Select(_ => MathHelper.GenerateTruthTable(_.NumberOfInputs, resultInBigEndian: false).Select(i => i.ToList()).ToList()).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void IsAlmostEqualTo_with_doubles___Should_throw_ArgumentException___When_parameters_value1_or_value2_is_NaN()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsAlmostEqualTo(double.NaN, 1));
            var ex2 = Record.Exception(() => MathHelper.IsAlmostEqualTo(double.NaN, 1, 1e-3));
            var ex3 = Record.Exception(() => MathHelper.IsAlmostEqualTo(1, double.NaN));
            var ex4 = Record.Exception(() => MathHelper.IsAlmostEqualTo(1, double.NaN, 1e-3));
            var ex5 = Record.Exception(() => MathHelper.IsAlmostEqualTo(double.NaN, double.NaN));
            var ex6 = Record.Exception(() => MathHelper.IsAlmostEqualTo(double.NaN, double.NaN, 1e-3));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
            ex4.Should().BeOfType<ArgumentException>();
            ex5.Should().BeOfType<ArgumentException>();
            ex6.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsAlmostEqualTo_with_doubles___Should_throw_ArgumentOutOfRangeException___When_parameter_tolerance_is_less_than_0()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsAlmostEqualTo(4.5, -3.2, -.0000001));
            var ex2 = Record.Exception(() => MathHelper.IsAlmostEqualTo(4.5, -3.2, double.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsAlmostEqualTo_with_doubles___Should_return_true___When_two_values_are_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsAlmostEqualTo(5d, 5d);
            var result2 = MathHelper.IsAlmostEqualTo(5d, 5d, 0d);
            var result3 = MathHelper.IsAlmostEqualTo(2.2d, 2.2d);
            var result4 = MathHelper.IsAlmostEqualTo(2.23d, 2.29d, 0.060000001d);
            var result5 = MathHelper.IsAlmostEqualTo(1.001d, 1.002d, .001000001d);
            var result6 = MathHelper.IsAlmostEqualTo(2.012d, 2.013d, .001d);
            var result7 = MathHelper.IsAlmostEqualTo(0.000000001d, 0.000000005d);
            var result8 = MathHelper.IsAlmostEqualTo(5000000.000000001d, 5000000.000000005d);
            var result9 = MathHelper.IsAlmostEqualTo(35.123418d, 35.123417d, 5e-3d);
            var result10 = MathHelper.IsAlmostEqualTo(-.0001, .0001, .0002);

            var result11 = MathHelper.IsAlmostEqualTo(-5d, -5d);
            var result12 = MathHelper.IsAlmostEqualTo(-5d, -5d, 0d);
            var result13 = MathHelper.IsAlmostEqualTo(-2.2d, -2.2d);
            var result14 = MathHelper.IsAlmostEqualTo(-2.23d, -2.29d, 0.060000001d);
            var result15 = MathHelper.IsAlmostEqualTo(-1.001d, -1.002d, .001000001d);
            var result16 = MathHelper.IsAlmostEqualTo(-2.012d, -2.013d, .001d);
            var result17 = MathHelper.IsAlmostEqualTo(-0.000000001d, -0.000000005d);
            var result18 = MathHelper.IsAlmostEqualTo(-5000000.000000001d, -5000000.000000005d);
            var result19 = MathHelper.IsAlmostEqualTo(-35.123418d, -35.123417d, 5e-3d);
            var result20 = MathHelper.IsAlmostEqualTo(.0001d, -.0001d, .0002d);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();

            result11.Should().BeTrue();
            result12.Should().BeTrue();
            result13.Should().BeTrue();
            result14.Should().BeTrue();
            result15.Should().BeTrue();
            result16.Should().BeTrue();
            result17.Should().BeTrue();
            result18.Should().BeTrue();
            result19.Should().BeTrue();
            result20.Should().BeTrue();
        }

        [Fact]
        public static void IsAlmostEqualTo_with_doubles___Should_return_false___When_two_values_are_not_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsAlmostEqualTo(5d, -5d);
            var result2 = MathHelper.IsAlmostEqualTo(.00000001d, -.00000001d);
            var result3 = MathHelper.IsAlmostEqualTo(3.2d, 3.5d, .02d);
            var result4 = MathHelper.IsAlmostEqualTo(1.001d, 1.002d, .0001d);
            var result5 = MathHelper.IsAlmostEqualTo(2.022d, 2.013d, .008d);
            var result6 = MathHelper.IsAlmostEqualTo(0.00000001d, 0.00000005d);
            var result7 = MathHelper.IsAlmostEqualTo(5000000.0000011d, 5000000.0000012d);
            var result8 = MathHelper.IsAlmostEqualTo(0.000016d, 0.000015d, 1e-7d);
            var result9 = MathHelper.IsAlmostEqualTo(35.123418d, 35.123417d, 1e-7d);
            var result10 = MathHelper.IsAlmostEqualTo(-0.025d, 0.025d, .049999999d);

            var result11 = MathHelper.IsAlmostEqualTo(-5d, 5d);
            var result12 = MathHelper.IsAlmostEqualTo(-.00000001d, .00000001d);
            var result13 = MathHelper.IsAlmostEqualTo(-3.2d, -3.5d, .02d);
            var result14 = MathHelper.IsAlmostEqualTo(-1.001d, -1.002d, .0001d);
            var result15 = MathHelper.IsAlmostEqualTo(-2.022d, -2.013d, .008d);
            var result16 = MathHelper.IsAlmostEqualTo(-0.00000001d, -0.00000005d);
            var result17 = MathHelper.IsAlmostEqualTo(-5000000.0000011d, -5000000.0000012d);
            var result18 = MathHelper.IsAlmostEqualTo(-0.000016d, -0.000015d, 1e-7d);
            var result19 = MathHelper.IsAlmostEqualTo(-35.123418d, -35.123417d, 1e-7d);
            var result20 = MathHelper.IsAlmostEqualTo(0.025d, -0.025d, .049999999d);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
            result6.Should().BeFalse();
            result7.Should().BeFalse();
            result8.Should().BeFalse();
            result9.Should().BeFalse();
            result10.Should().BeFalse();

            result11.Should().BeFalse();
            result12.Should().BeFalse();
            result13.Should().BeFalse();
            result14.Should().BeFalse();
            result15.Should().BeFalse();
            result16.Should().BeFalse();
            result17.Should().BeFalse();
            result18.Should().BeFalse();
            result19.Should().BeFalse();
            result20.Should().BeFalse();
        }

        [Fact]
        public static void IsAlmostEqualTo_with_decimals___Should_throw_ArgumentOutOfRangeException___When_parameter_tolerance_is_less_than_0()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsAlmostEqualTo(4.5m, -3.2m, -.0000001m));
            var ex2 = Record.Exception(() => MathHelper.IsAlmostEqualTo(4.5m, -3.2m, decimal.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsAlmostEqualTo_with_decimals___Should_return_true___When_two_values_are_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsAlmostEqualTo(5m, 5m);
            var result2 = MathHelper.IsAlmostEqualTo(5m, 5m, 0m);
            var result3 = MathHelper.IsAlmostEqualTo(2.2m, 2.2m);
            var result4 = MathHelper.IsAlmostEqualTo(2.23m, 2.29m, 0.06m);
            var result5 = MathHelper.IsAlmostEqualTo(1.001m, 1.002m, .001m);
            var result6 = MathHelper.IsAlmostEqualTo(2.012m, 2.013m, .001m);
            var result7 = MathHelper.IsAlmostEqualTo(0.000000001m, 0.000000005m);
            var result8 = MathHelper.IsAlmostEqualTo(5000000.000000001m, 5000000.000000005m);
            var result9 = MathHelper.IsAlmostEqualTo(35.123418m, 35.123417m, 5e-3m);
            var result10 = MathHelper.IsAlmostEqualTo(-.0001m, .0001m, .0002m);

            var result11 = MathHelper.IsAlmostEqualTo(-5m, -5m);
            var result12 = MathHelper.IsAlmostEqualTo(-5m, -5m, 0m);
            var result13 = MathHelper.IsAlmostEqualTo(-2.2m, -2.2m);
            var result14 = MathHelper.IsAlmostEqualTo(-2.23m, -2.29m, 0.06m);
            var result15 = MathHelper.IsAlmostEqualTo(-1.001m, -1.002m, .001m);
            var result16 = MathHelper.IsAlmostEqualTo(-2.012m, -2.013m, .001m);
            var result17 = MathHelper.IsAlmostEqualTo(-0.000000001m, -0.000000005m);
            var result18 = MathHelper.IsAlmostEqualTo(-5000000.000000001m, -5000000.000000005m);
            var result19 = MathHelper.IsAlmostEqualTo(-35.123418m, -35.123417m, 5e-3m);
            var result20 = MathHelper.IsAlmostEqualTo(.0001m, -.0001m, .0002m);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();

            result11.Should().BeTrue();
            result12.Should().BeTrue();
            result13.Should().BeTrue();
            result14.Should().BeTrue();
            result15.Should().BeTrue();
            result16.Should().BeTrue();
            result17.Should().BeTrue();
            result18.Should().BeTrue();
            result19.Should().BeTrue();
            result20.Should().BeTrue();
        }

        [Fact]
        public static void IsAlmostEqualTo_with_decimals___Should_return_false___When_two_values_are_not_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsAlmostEqualTo(5m, -5m);
            var result2 = MathHelper.IsAlmostEqualTo(.00000001m, -.00000001m);
            var result3 = MathHelper.IsAlmostEqualTo(3.2m, 3.5m, .02m);
            var result4 = MathHelper.IsAlmostEqualTo(1.001m, 1.002m, .0001m);
            var result5 = MathHelper.IsAlmostEqualTo(2.022m, 2.013m, .008m);
            var result6 = MathHelper.IsAlmostEqualTo(0.00000001m, 0.00000005m);
            var result7 = MathHelper.IsAlmostEqualTo(5000000.0000011m, 5000000.0000012m);
            var result8 = MathHelper.IsAlmostEqualTo(0.000016m, 0.000015m, 1e-7m);
            var result9 = MathHelper.IsAlmostEqualTo(35.123418m, 35.123417m, 1e-7m);
            var result10 = MathHelper.IsAlmostEqualTo(-0.025m, 0.025m, .049999999m);

            var result11 = MathHelper.IsAlmostEqualTo(-5m, 5m);
            var result12 = MathHelper.IsAlmostEqualTo(-.00000001m, .00000001m);
            var result13 = MathHelper.IsAlmostEqualTo(-3.2m, -3.5m, .02m);
            var result14 = MathHelper.IsAlmostEqualTo(-1.001m, -1.002m, .0001m);
            var result15 = MathHelper.IsAlmostEqualTo(-2.022m, -2.013m, .008m);
            var result16 = MathHelper.IsAlmostEqualTo(-0.00000001m, -0.00000005m);
            var result17 = MathHelper.IsAlmostEqualTo(-5000000.0000011m, -5000000.0000012m);
            var result18 = MathHelper.IsAlmostEqualTo(-0.000016m, -0.000015m, 1e-7m);
            var result19 = MathHelper.IsAlmostEqualTo(-35.123418m, -35.123417m, 1e-7m);
            var result20 = MathHelper.IsAlmostEqualTo(0.025m, -0.025m, .049999999m);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
            result6.Should().BeFalse();
            result7.Should().BeFalse();
            result8.Should().BeFalse();
            result9.Should().BeFalse();
            result10.Should().BeFalse();

            result11.Should().BeFalse();
            result12.Should().BeFalse();
            result13.Should().BeFalse();
            result14.Should().BeFalse();
            result15.Should().BeFalse();
            result16.Should().BeFalse();
            result17.Should().BeFalse();
            result18.Should().BeFalse();
            result19.Should().BeFalse();
            result20.Should().BeFalse();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_doubles___Should_throw_ArgumentException___When_parameters_value1_or_value2_is_NaN()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(double.NaN, 1));
            var ex2 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(double.NaN, 1, 1e-3));
            var ex3 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(1, double.NaN));
            var ex4 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(1, double.NaN, 1e-3));
            var ex5 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(double.NaN, double.NaN));
            var ex6 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(double.NaN, double.NaN, 1e-3));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
            ex4.Should().BeOfType<ArgumentException>();
            ex5.Should().BeOfType<ArgumentException>();
            ex6.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_doubles___Should_throw_ArgumentOutOfRangeException___When_parameter_tolerance_is_less_than_0()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(4.5, -3.2, -.0000001));
            var ex2 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(4.5, -3.2, double.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_doubles___Should_return_true___When_two_values_are_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsGreaterThanOrAlmostEqualTo(5d, 5d);
            var result2 = MathHelper.IsGreaterThanOrAlmostEqualTo(5d, 5d, 0d);
            var result3 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.2d, 2.2d);
            var result4 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.23d, 2.29d, 0.060000001d);
            var result5 = MathHelper.IsGreaterThanOrAlmostEqualTo(1.001d, 1.002d, .001000001d);
            var result6 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.012d, 2.013d, .001d);
            var result7 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.000000001d, 0.000000005d);
            var result8 = MathHelper.IsGreaterThanOrAlmostEqualTo(5000000.000000001d, 5000000.000000005d);
            var result9 = MathHelper.IsGreaterThanOrAlmostEqualTo(35.123418d, 35.123417d, 5e-3d);
            var result10 = MathHelper.IsGreaterThanOrAlmostEqualTo(-.0001, .0001, .0002);

            var result11 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5d, -5d);
            var result12 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5d, -5d, 0d);
            var result13 = MathHelper.IsGreaterThanOrAlmostEqualTo(-2.2d, -2.2d);
            var result14 = MathHelper.IsGreaterThanOrAlmostEqualTo(-2.23d, -2.29d, 0.060000001d);
            var result15 = MathHelper.IsGreaterThanOrAlmostEqualTo(-1.001d, -1.002d, .001000001d);
            var result16 = MathHelper.IsGreaterThanOrAlmostEqualTo(-2.012d, -2.013d, .001d);
            var result17 = MathHelper.IsGreaterThanOrAlmostEqualTo(-0.000000001d, -0.000000005d);
            var result18 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5000000.000000001d, -5000000.000000005d);
            var result19 = MathHelper.IsGreaterThanOrAlmostEqualTo(-35.123418d, -35.123417d, 5e-3d);
            var result20 = MathHelper.IsGreaterThanOrAlmostEqualTo(.0001d, -.0001d, .0002d);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();

            result11.Should().BeTrue();
            result12.Should().BeTrue();
            result13.Should().BeTrue();
            result14.Should().BeTrue();
            result15.Should().BeTrue();
            result16.Should().BeTrue();
            result17.Should().BeTrue();
            result18.Should().BeTrue();
            result19.Should().BeTrue();
            result20.Should().BeTrue();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_doubles___Should_return_true___When_first_value_is_greater_than_second_value()
        {
            // Arrange, Act
            var result1 = MathHelper.IsGreaterThanOrAlmostEqualTo(5d, -5d);
            var result2 = MathHelper.IsGreaterThanOrAlmostEqualTo(.00000001d, -.00000001d);
            var result3 = MathHelper.IsGreaterThanOrAlmostEqualTo(3.5d, 3.2d, .02d);
            var result4 = MathHelper.IsGreaterThanOrAlmostEqualTo(1.002d, 1.001d, .0001d);
            var result5 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.022d, 2.013d, .008d);
            var result6 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.00000005d, 0.00000001d);
            var result7 = MathHelper.IsGreaterThanOrAlmostEqualTo(5000000.0000012d, 5000000.0000011d);
            var result8 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.000016d, 0.000015d, 1e-7d);
            var result9 = MathHelper.IsGreaterThanOrAlmostEqualTo(35.123418d, 35.123417d, 1e-7d);
            var result10 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.025d, -0.025d, .049999999d);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_doubles___Should_return_false___When_first_value_is_less_than_but_not_almost_equal_to_the_second_value_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5d, 5d);
            var result2 = MathHelper.IsGreaterThanOrAlmostEqualTo(-.00000001d, .00000001d);
            var result3 = MathHelper.IsGreaterThanOrAlmostEqualTo(3.2d, 3.5d, .02d);
            var result4 = MathHelper.IsGreaterThanOrAlmostEqualTo(1.001d, 1.002d, .0001d);
            var result5 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.013d, 2.022d, .008d);
            var result6 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.00000001d, 0.00000005d);
            var result7 = MathHelper.IsGreaterThanOrAlmostEqualTo(5000000.0000011d, 5000000.0000012d);
            var result8 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.000015d, 0.000016d, 1e-7d);
            var result9 = MathHelper.IsGreaterThanOrAlmostEqualTo(35.123417d, 35.123418d, 1e-7d);
            var result10 = MathHelper.IsGreaterThanOrAlmostEqualTo(-0.025d, 0.025d, .049999999d);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
            result6.Should().BeFalse();
            result7.Should().BeFalse();
            result8.Should().BeFalse();
            result9.Should().BeFalse();
            result10.Should().BeFalse();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_decimals___Should_throw_ArgumentOutOfRangeException___When_parameter_tolerance_is_less_than_0()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(4.5m, -3.2m, -.0000001m));
            var ex2 = Record.Exception(() => MathHelper.IsGreaterThanOrAlmostEqualTo(4.5m, -3.2m, decimal.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_decimals___Should_return_true___When_two_values_are_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsGreaterThanOrAlmostEqualTo(5m, 5m);
            var result2 = MathHelper.IsGreaterThanOrAlmostEqualTo(5m, 5m, 0m);
            var result3 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.2m, 2.2m);
            var result4 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.23m, 2.29m, 0.06m);
            var result5 = MathHelper.IsGreaterThanOrAlmostEqualTo(1.001m, 1.002m, .001m);
            var result6 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.012m, 2.013m, .001m);
            var result7 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.000000001m, 0.000000005m);
            var result8 = MathHelper.IsGreaterThanOrAlmostEqualTo(5000000.000000001m, 5000000.000000005m);
            var result9 = MathHelper.IsGreaterThanOrAlmostEqualTo(35.123418m, 35.123417m, 5e-3m);
            var result10 = MathHelper.IsGreaterThanOrAlmostEqualTo(-.0001m, .0001m, .0002m);

            var result11 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5m, -5m);
            var result12 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5m, -5m, 0m);
            var result13 = MathHelper.IsGreaterThanOrAlmostEqualTo(-2.2m, -2.2m);
            var result14 = MathHelper.IsGreaterThanOrAlmostEqualTo(-2.23m, -2.29m, 0.06m);
            var result15 = MathHelper.IsGreaterThanOrAlmostEqualTo(-1.001m, -1.002m, .001m);
            var result16 = MathHelper.IsGreaterThanOrAlmostEqualTo(-2.012m, -2.013m, .001m);
            var result17 = MathHelper.IsGreaterThanOrAlmostEqualTo(-0.000000001m, -0.000000005m);
            var result18 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5000000.000000001m, -5000000.000000005m);
            var result19 = MathHelper.IsGreaterThanOrAlmostEqualTo(-35.123418m, -35.123417m, 5e-3m);
            var result20 = MathHelper.IsGreaterThanOrAlmostEqualTo(.0001m, -.0001m, .0002m);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();

            result11.Should().BeTrue();
            result12.Should().BeTrue();
            result13.Should().BeTrue();
            result14.Should().BeTrue();
            result15.Should().BeTrue();
            result16.Should().BeTrue();
            result17.Should().BeTrue();
            result18.Should().BeTrue();
            result19.Should().BeTrue();
            result20.Should().BeTrue();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_decimals___Should_return_true___When_first_value_is_greater_than_second_value()
        {
            // Arrange, Act
            var result1 = MathHelper.IsGreaterThanOrAlmostEqualTo(5m, -5m);
            var result2 = MathHelper.IsGreaterThanOrAlmostEqualTo(.00000001m, -.00000001m);
            var result3 = MathHelper.IsGreaterThanOrAlmostEqualTo(3.5m, 3.2m, .02m);
            var result4 = MathHelper.IsGreaterThanOrAlmostEqualTo(1.002m, 1.001m, .0001m);
            var result5 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.022m, 2.013m, .008m);
            var result6 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.00000005m, 0.00000001m);
            var result7 = MathHelper.IsGreaterThanOrAlmostEqualTo(5000000.0000012m, 5000000.0000011m);
            var result8 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.000016m, 0.000015m, 1e-7m);
            var result9 = MathHelper.IsGreaterThanOrAlmostEqualTo(35.123418m, 35.123417m, 1e-7m);
            var result10 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.025m, -0.025m, .049999999m);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();
        }

        [Fact]
        public static void IsGreaterThanOrAlmostEqualTo_with_decimals___Should_return_false___When_first_value_is_less_than_but_not_almost_equal_to_second_value_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsGreaterThanOrAlmostEqualTo(-5m, 5m);
            var result2 = MathHelper.IsGreaterThanOrAlmostEqualTo(-.00000001m, .00000001m);
            var result3 = MathHelper.IsGreaterThanOrAlmostEqualTo(3.2m, 3.5m, .02m);
            var result4 = MathHelper.IsGreaterThanOrAlmostEqualTo(1.001m, 1.002m, .0001m);
            var result5 = MathHelper.IsGreaterThanOrAlmostEqualTo(2.013m, 2.022m, .008m);
            var result6 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.00000001m, 0.00000005m);
            var result7 = MathHelper.IsGreaterThanOrAlmostEqualTo(5000000.0000011m, 5000000.0000012m);
            var result8 = MathHelper.IsGreaterThanOrAlmostEqualTo(0.000015m, 0.000016m, 1e-7m);
            var result9 = MathHelper.IsGreaterThanOrAlmostEqualTo(35.123417m, 35.123418m, 1e-7m);
            var result10 = MathHelper.IsGreaterThanOrAlmostEqualTo(-0.025m, 0.025m, .049999999m);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
            result6.Should().BeFalse();
            result7.Should().BeFalse();
            result8.Should().BeFalse();
            result9.Should().BeFalse();
            result10.Should().BeFalse();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_doubles___Should_throw_ArgumentException___When_parameters_value1_or_value2_is_NaN()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(double.NaN, 1));
            var ex2 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(double.NaN, 1, 1e-3));
            var ex3 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(1, double.NaN));
            var ex4 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(1, double.NaN, 1e-3));
            var ex5 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(double.NaN, double.NaN));
            var ex6 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(double.NaN, double.NaN, 1e-3));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
            ex4.Should().BeOfType<ArgumentException>();
            ex5.Should().BeOfType<ArgumentException>();
            ex6.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_doubles___Should_throw_ArgumentOutOfRangeException___When_parameter_tolerance_is_less_than_0()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(4.5, -3.2, -.0000001));
            var ex2 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(4.5, -3.2, double.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_doubles___Should_return_true___When_two_values_are_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsLessThanOrAlmostEqualTo(5d, 5d);
            var result2 = MathHelper.IsLessThanOrAlmostEqualTo(5d, 5d, 0d);
            var result3 = MathHelper.IsLessThanOrAlmostEqualTo(2.2d, 2.2d);
            var result4 = MathHelper.IsLessThanOrAlmostEqualTo(2.23d, 2.29d, 0.060000001d);
            var result5 = MathHelper.IsLessThanOrAlmostEqualTo(1.001d, 1.002d, .001000001d);
            var result6 = MathHelper.IsLessThanOrAlmostEqualTo(2.012d, 2.013d, .001d);
            var result7 = MathHelper.IsLessThanOrAlmostEqualTo(0.000000001d, 0.000000005d);
            var result8 = MathHelper.IsLessThanOrAlmostEqualTo(5000000.000000001d, 5000000.000000005d);
            var result9 = MathHelper.IsLessThanOrAlmostEqualTo(35.123418d, 35.123417d, 5e-3d);
            var result10 = MathHelper.IsLessThanOrAlmostEqualTo(-.0001, .0001, .0002);

            var result11 = MathHelper.IsLessThanOrAlmostEqualTo(-5d, -5d);
            var result12 = MathHelper.IsLessThanOrAlmostEqualTo(-5d, -5d, 0d);
            var result13 = MathHelper.IsLessThanOrAlmostEqualTo(-2.2d, -2.2d);
            var result14 = MathHelper.IsLessThanOrAlmostEqualTo(-2.23d, -2.29d, 0.060000001d);
            var result15 = MathHelper.IsLessThanOrAlmostEqualTo(-1.001d, -1.002d, .001000001d);
            var result16 = MathHelper.IsLessThanOrAlmostEqualTo(-2.012d, -2.013d, .001d);
            var result17 = MathHelper.IsLessThanOrAlmostEqualTo(-0.000000001d, -0.000000005d);
            var result18 = MathHelper.IsLessThanOrAlmostEqualTo(-5000000.000000001d, -5000000.000000005d);
            var result19 = MathHelper.IsLessThanOrAlmostEqualTo(-35.123418d, -35.123417d, 5e-3d);
            var result20 = MathHelper.IsLessThanOrAlmostEqualTo(.0001d, -.0001d, .0002d);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();

            result11.Should().BeTrue();
            result12.Should().BeTrue();
            result13.Should().BeTrue();
            result14.Should().BeTrue();
            result15.Should().BeTrue();
            result16.Should().BeTrue();
            result17.Should().BeTrue();
            result18.Should().BeTrue();
            result19.Should().BeTrue();
            result20.Should().BeTrue();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_doubles___Should_return_true___When_first_value_is_less_than_second_value()
        {
            // Arrange, Act
            var result1 = MathHelper.IsLessThanOrAlmostEqualTo(-5d, 5d);
            var result2 = MathHelper.IsLessThanOrAlmostEqualTo(-.00000001d, .00000001d);
            var result3 = MathHelper.IsLessThanOrAlmostEqualTo(3.2d, 3.5d, .02d);
            var result4 = MathHelper.IsLessThanOrAlmostEqualTo(1.001d, 1.002d, .0001d);
            var result5 = MathHelper.IsLessThanOrAlmostEqualTo(2.013d, 2.022d, .008d);
            var result6 = MathHelper.IsLessThanOrAlmostEqualTo(0.00000001d, 0.00000005d);
            var result7 = MathHelper.IsLessThanOrAlmostEqualTo(5000000.0000011d, 5000000.0000012d);
            var result8 = MathHelper.IsLessThanOrAlmostEqualTo(0.000015d, 0.000016d, 1e-7d);
            var result9 = MathHelper.IsLessThanOrAlmostEqualTo(35.123417d, 35.123418d, 1e-7d);
            var result10 = MathHelper.IsLessThanOrAlmostEqualTo(-0.025d, 0.025d, .049999999d);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_doubles___Should_return_false___When_first_value_is_greater_than_but_not_almost_equal_to_the_second_value_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsLessThanOrAlmostEqualTo(5d, -5d);
            var result2 = MathHelper.IsLessThanOrAlmostEqualTo(.00000001d, -.00000001d);
            var result3 = MathHelper.IsLessThanOrAlmostEqualTo(3.5d, 3.2d, .02d);
            var result4 = MathHelper.IsLessThanOrAlmostEqualTo(1.002d, 1.001d, .0001d);
            var result5 = MathHelper.IsLessThanOrAlmostEqualTo(2.022d, 2.013d, .008d);
            var result6 = MathHelper.IsLessThanOrAlmostEqualTo(0.00000005d, 0.00000001d);
            var result7 = MathHelper.IsLessThanOrAlmostEqualTo(5000000.0000012d, 5000000.0000011d);
            var result8 = MathHelper.IsLessThanOrAlmostEqualTo(0.000016d, 0.000015d, 1e-7d);
            var result9 = MathHelper.IsLessThanOrAlmostEqualTo(35.123418d, 35.123417d, 1e-7d);
            var result10 = MathHelper.IsLessThanOrAlmostEqualTo(0.025d, -0.025d, .049999999d);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
            result6.Should().BeFalse();
            result7.Should().BeFalse();
            result8.Should().BeFalse();
            result9.Should().BeFalse();
            result10.Should().BeFalse();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_decimals___Should_throw_ArgumentOutOfRangeException___When_parameter_tolerance_is_less_than_0()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(4.5m, -3.2m, -.0000001m));
            var ex2 = Record.Exception(() => MathHelper.IsLessThanOrAlmostEqualTo(4.5m, -3.2m, decimal.MinValue));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_decimals___Should_return_true___When_two_values_are_almost_equal_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsLessThanOrAlmostEqualTo(5m, 5m);
            var result2 = MathHelper.IsLessThanOrAlmostEqualTo(5m, 5m, 0m);
            var result3 = MathHelper.IsLessThanOrAlmostEqualTo(2.2m, 2.2m);
            var result4 = MathHelper.IsLessThanOrAlmostEqualTo(2.23m, 2.29m, 0.06m);
            var result5 = MathHelper.IsLessThanOrAlmostEqualTo(1.001m, 1.002m, .001m);
            var result6 = MathHelper.IsLessThanOrAlmostEqualTo(2.012m, 2.013m, .001m);
            var result7 = MathHelper.IsLessThanOrAlmostEqualTo(0.000000001m, 0.000000005m);
            var result8 = MathHelper.IsLessThanOrAlmostEqualTo(5000000.000000001m, 5000000.000000005m);
            var result9 = MathHelper.IsLessThanOrAlmostEqualTo(35.123418m, 35.123417m, 5e-3m);
            var result10 = MathHelper.IsLessThanOrAlmostEqualTo(-.0001m, .0001m, .0002m);

            var result11 = MathHelper.IsLessThanOrAlmostEqualTo(-5m, -5m);
            var result12 = MathHelper.IsLessThanOrAlmostEqualTo(-5m, -5m, 0m);
            var result13 = MathHelper.IsLessThanOrAlmostEqualTo(-2.2m, -2.2m);
            var result14 = MathHelper.IsLessThanOrAlmostEqualTo(-2.23m, -2.29m, 0.06m);
            var result15 = MathHelper.IsLessThanOrAlmostEqualTo(-1.001m, -1.002m, .001m);
            var result16 = MathHelper.IsLessThanOrAlmostEqualTo(-2.012m, -2.013m, .001m);
            var result17 = MathHelper.IsLessThanOrAlmostEqualTo(-0.000000001m, -0.000000005m);
            var result18 = MathHelper.IsLessThanOrAlmostEqualTo(-5000000.000000001m, -5000000.000000005m);
            var result19 = MathHelper.IsLessThanOrAlmostEqualTo(-35.123418m, -35.123417m, 5e-3m);
            var result20 = MathHelper.IsLessThanOrAlmostEqualTo(.0001m, -.0001m, .0002m);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();

            result11.Should().BeTrue();
            result12.Should().BeTrue();
            result13.Should().BeTrue();
            result14.Should().BeTrue();
            result15.Should().BeTrue();
            result16.Should().BeTrue();
            result17.Should().BeTrue();
            result18.Should().BeTrue();
            result19.Should().BeTrue();
            result20.Should().BeTrue();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_decimals___Should_return_true___When_first_value_is_less_than_second_value()
        {
            // Arrange, Act
            var result1 = MathHelper.IsLessThanOrAlmostEqualTo(-5m, 5m);
            var result2 = MathHelper.IsLessThanOrAlmostEqualTo(-.00000001m, .00000001m);
            var result3 = MathHelper.IsLessThanOrAlmostEqualTo(3.2m, 3.3m, .02m);
            var result4 = MathHelper.IsLessThanOrAlmostEqualTo(1.001m, 1.002m, .0001m);
            var result5 = MathHelper.IsLessThanOrAlmostEqualTo(2.013m, 2.022m, .008m);
            var result6 = MathHelper.IsLessThanOrAlmostEqualTo(0.00000001m, 0.00000005m);
            var result7 = MathHelper.IsLessThanOrAlmostEqualTo(5000000.0000011m, 5000000.0000012m);
            var result8 = MathHelper.IsLessThanOrAlmostEqualTo(0.000015m, 0.000016m, 1e-7m);
            var result9 = MathHelper.IsLessThanOrAlmostEqualTo(35.123417m, 35.123418m, 1e-7m);
            var result10 = MathHelper.IsLessThanOrAlmostEqualTo(-0.025m, 0.025m, .049999999m);

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
            result5.Should().BeTrue();
            result6.Should().BeTrue();
            result7.Should().BeTrue();
            result8.Should().BeTrue();
            result9.Should().BeTrue();
            result10.Should().BeTrue();
        }

        [Fact]
        public static void IsLessThanOrAlmostEqualTo_with_decimals___Should_return_false___When_first_value_is_greater_than_but_not_almost_equal_to_second_value_within_tolerance()
        {
            // Arrange, Act
            var result1 = MathHelper.IsLessThanOrAlmostEqualTo(5m, -5m);
            var result2 = MathHelper.IsLessThanOrAlmostEqualTo(.00000001m, -.00000001m);
            var result3 = MathHelper.IsLessThanOrAlmostEqualTo(3.5m, 3.2m, .02m);
            var result4 = MathHelper.IsLessThanOrAlmostEqualTo(1.002m, 1.001m, .0001m);
            var result5 = MathHelper.IsLessThanOrAlmostEqualTo(2.022m, 2.013m, .008m);
            var result6 = MathHelper.IsLessThanOrAlmostEqualTo(0.00000005m, 0.00000001m);
            var result7 = MathHelper.IsLessThanOrAlmostEqualTo(5000000.0000012m, 5000000.0000011m);
            var result8 = MathHelper.IsLessThanOrAlmostEqualTo(0.000016m, 0.000015m, 1e-7m);
            var result9 = MathHelper.IsLessThanOrAlmostEqualTo(35.123418m, 35.123417m, 1e-7m);
            var result10 = MathHelper.IsLessThanOrAlmostEqualTo(0.025m, -0.025m, .049999999m);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
            result6.Should().BeFalse();
            result7.Should().BeFalse();
            result8.Should().BeFalse();
            result9.Should().BeFalse();
            result10.Should().BeFalse();
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
        public static void IsEven___Should_return_true___When_value_is_even()
        {
            // Arrange
            var values = new[] { int.MinValue, -10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10 };

            // Act
            var actuals = values.Select(_ => _.IsEven());

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsEven___Should_return_false___When_value_is_not_even()
        {
            // Arrange
            var values = new[] { -11, -9, -7, -5, -3, -1, 1, 3, 5, 7, 9, 11, int.MaxValue };

            // Act
            var actuals = values.Select(_ => _.IsEven());

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsOdd___Should_return_true___When_value_is_odd()
        {
            // Arrange
            var values = new[] { -11, -9, -7, -5, -3, -1, 1, 3, 5, 7, 9, 11, int.MaxValue };

            // Act
            var actuals = values.Select(_ => _.IsOdd());

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsOdd___Should_return_false___When_value_is_not_odd()
        {
            // Arrange
            var values = new[] { int.MinValue, -10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10 };

            // Act
            var actuals = values.Select(_ => _.IsOdd());

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void Round___Should_round_value_to_digits_using_specified_strategy___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new { Value = (decimal?)null, Digits = 2, Strategy = MidpointRounding.AwayFromZero, Expected = (decimal?)null },
                new { Value = (decimal?)1.011m, Digits = 2, Strategy = MidpointRounding.AwayFromZero, Expected = (decimal?)1.01m },
                new { Value = (decimal?)1.019m, Digits = 2, Strategy = MidpointRounding.AwayFromZero, Expected = (decimal?)1.02m },
                new { Value = (decimal?)1.05m, Digits = 1, Strategy = MidpointRounding.AwayFromZero, Expected = (decimal?)1.1m },
                new { Value = (decimal?)1.05m, Digits = 1, Strategy = MidpointRounding.ToEven, Expected = (decimal?)1.0m },
                new { Value = (decimal?)1.15m, Digits = 1, Strategy = MidpointRounding.ToEven, Expected = (decimal?)1.2m },
            };

            // Act
            var actual = tests.Select(_ => _.Value.Round(_.Digits, _.Strategy)).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(tests.Select(_ => _.Expected).ToList());
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
            Assert.Equal(0.707107M, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(3);
            Assert.Equal(1, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(4);
            Assert.Equal(1.290994M, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(3);
            Assert.Equal(1.140175M, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(6);
            Assert.Equal(1.722401M, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(7);
            Assert.Equal(2.138090M, Math.Round(MathHelper.StandardDeviation(decimals), 6));
            decimals.Add(8);
            Assert.Equal(2.492847M, Math.Round(MathHelper.StandardDeviation(decimals), 6));

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
        public static void ToGuid___Converts_two_int_of_the_same_value_to_the_same_GUID___When_called()
        {
            // Arrange
            var int1 = A.Dummy<int>();
            var int2 = int1;

            // Act
            var actual1 = int1.ToGuid();
            var actual2 = int2.ToGuid();

            // Assert
            actual1.Should().Be(actual2);
        }

        [Fact]
        public static void ToGuid___Converts_int_MaxValue_and_int_MinValue_to_GUIDs___When_called()
        {
            // Arrange
            var int1 = int.MaxValue;
            var int2 = int.MinValue;

            // Act
            var actual1 = int1.ToGuid();
            var actual2 = int2.ToGuid();

            // Assert
            actual1.Should().NotBe(Guid.Empty);
            actual2.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public static void ToGuid___Converts_0_to_empty_GUID___When_called()
        {
            // Arrange, Act
            var actual = 0.ToGuid();

            // Assert
            actual.Should().Be(Guid.Empty);
        }

        [Fact]
        public static void TruncateDecimalTests()
        {
            // test decimals 1.*
            Assert.Equal(1, MathHelper.Truncate(1M));
            Assert.Equal(1, MathHelper.Truncate(1.1M));
            Assert.Equal(1, MathHelper.Truncate(1.49M));
            Assert.Equal(1, MathHelper.Truncate(1.5M));
            Assert.Equal(1, MathHelper.Truncate(1.51M));
            Assert.Equal(1, MathHelper.Truncate(1.99M));

            // negative numbers
            Assert.Equal(-1, MathHelper.Truncate(-1M));
            Assert.Equal(-1, MathHelper.Truncate(-1.1M));
            Assert.Equal(-1, MathHelper.Truncate(-1.49M));
            Assert.Equal(-1, MathHelper.Truncate(-1.5M));
            Assert.Equal(-1, MathHelper.Truncate(-1.51M));
            Assert.Equal(-1, MathHelper.Truncate(-1.99M));

            // zero
            Assert.Equal(0, MathHelper.Truncate(0M));
            Assert.Equal(0, MathHelper.Truncate(0.1M));
            Assert.Equal(0, MathHelper.Truncate(0.49M));
            Assert.Equal(0, MathHelper.Truncate(0.5M));
            Assert.Equal(0, MathHelper.Truncate(0.51M));
            Assert.Equal(0, MathHelper.Truncate(0.99M));
            Assert.Equal(0, MathHelper.Truncate(0M));
            Assert.Equal(0, MathHelper.Truncate(-0.1M));
            Assert.Equal(0, MathHelper.Truncate(-0.49M));
            Assert.Equal(0, MathHelper.Truncate(-0.5M));
            Assert.Equal(0, MathHelper.Truncate(-0.51M));
            Assert.Equal(0, MathHelper.Truncate(-0.99M));

            // bounds
            Assert.Equal(int.MinValue, MathHelper.Truncate(Convert.ToDecimal(int.MinValue)));
            Assert.Equal(int.MaxValue, MathHelper.Truncate(Convert.ToDecimal(int.MaxValue)));
            Assert.Equal(int.MinValue + 1, MathHelper.Truncate(Convert.ToDecimal(int.MinValue) + .1M));
            Assert.Equal(int.MaxValue - 1, MathHelper.Truncate(Convert.ToDecimal(int.MaxValue) - .1M));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(Convert.ToDecimal(int.MinValue) - .1M));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(Convert.ToDecimal(int.MaxValue) + .1M));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(decimal.MaxValue));
            Assert.Throws<OverflowException>(() => MathHelper.Truncate(decimal.MinValue));
        }

        [Fact]
        public static void TruncateDoubleTests()
        {
            // test decimals 1.*
            Assert.Equal(1, MathHelper.Truncate(1D));
            Assert.Equal(1, MathHelper.Truncate(1.1));
            Assert.Equal(1, MathHelper.Truncate(1.49));
            Assert.Equal(1, MathHelper.Truncate(1.5));
            Assert.Equal(1, MathHelper.Truncate(1.51));
            Assert.Equal(1, MathHelper.Truncate(1.99));

            // negative numbers
            Assert.Equal(-1, MathHelper.Truncate(-1D));
            Assert.Equal(-1, MathHelper.Truncate(-1.1));
            Assert.Equal(-1, MathHelper.Truncate(-1.49));
            Assert.Equal(-1, MathHelper.Truncate(-1.5));
            Assert.Equal(-1, MathHelper.Truncate(-1.51));
            Assert.Equal(-1, MathHelper.Truncate(-1.99));

            // zero
            Assert.Equal(0, MathHelper.Truncate(0D));
            Assert.Equal(0, MathHelper.Truncate(0.1));
            Assert.Equal(0, MathHelper.Truncate(0.49));
            Assert.Equal(0, MathHelper.Truncate(0.5));
            Assert.Equal(0, MathHelper.Truncate(0.51));
            Assert.Equal(0, MathHelper.Truncate(0.99));
            Assert.Equal(0, MathHelper.Truncate(0D));
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
        public static void TruncateSignificantDigitsTest()
        {
            // negative digits
            decimal value = 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.TruncateSignificantDigits(value, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.TruncateSignificantDigits(value, -2));
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.TruncateSignificantDigits(value, int.MinValue));

            // zero digits
            Assert.Equal(1, MathHelper.TruncateSignificantDigits(value, 0));
            value = 1.2M;
            Assert.Equal(1, MathHelper.TruncateSignificantDigits(value, 0));
            value = -12.382M;
            Assert.Equal(-12, MathHelper.TruncateSignificantDigits(value, 0));

            // positive digits
            value = 3;
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 1));
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 2));
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 3));
            Assert.Equal(3, MathHelper.TruncateSignificantDigits(value, 4));

            value = 4.362947M;
            Assert.Equal(4.3M, MathHelper.TruncateSignificantDigits(value, 1));
            Assert.Equal(4.36M, MathHelper.TruncateSignificantDigits(value, 2));
            Assert.Equal(4.362M, MathHelper.TruncateSignificantDigits(value, 3));
            Assert.Equal(4.3629M, MathHelper.TruncateSignificantDigits(value, 4));
            Assert.Equal(4.36294M, MathHelper.TruncateSignificantDigits(value, 5));
            Assert.Equal(4.362947M, MathHelper.TruncateSignificantDigits(value, 6));
            Assert.Equal(4.362947M, MathHelper.TruncateSignificantDigits(value, 7));

            value = -4.362947M;
            Assert.Equal(-4.3M, MathHelper.TruncateSignificantDigits(value, 1));
            Assert.Equal(-4.36M, MathHelper.TruncateSignificantDigits(value, 2));
            Assert.Equal(-4.362M, MathHelper.TruncateSignificantDigits(value, 3));
            Assert.Equal(-4.3629M, MathHelper.TruncateSignificantDigits(value, 4));
            Assert.Equal(-4.36294M, MathHelper.TruncateSignificantDigits(value, 5));
            Assert.Equal(-4.362947M, MathHelper.TruncateSignificantDigits(value, 6));
            Assert.Equal(-4.362947M, MathHelper.TruncateSignificantDigits(value, 7));

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
    }
}
