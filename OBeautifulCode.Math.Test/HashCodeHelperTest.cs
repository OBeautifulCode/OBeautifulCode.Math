// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashCodeHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="HashCodeHelper"/> class.
    /// </summary>
    public static class HashCodeHelperTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Hash___Should_return_different_hash_code_than_Initialize___When_the_value_parameter_is_null()
        {
            // Arrange
            var initialize = HashCodeHelper.Initialize();
            double? value = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var systemUnderTest = HashCodeHelper.Initialize().Hash(value);
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            systemUnderTest.Value.Should().NotBe(initialize.Value);
        }

        [Fact]
        public static void Hash___Should_return_nonzero_hash_code___When_the_value_parameter_is_null()
        {
            // Arrange
            double? value = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var systemUnderTest = HashCodeHelper.Initialize().Hash(value);
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            systemUnderTest.Value.Should().NotBe(0);
        }

        [Fact]
        public static void Hash___Should_return_same_hash_code___When_the_values_to_hash_are_equal_and_hashed_in_same_order()
        {
            // Arrange
            var value1a = "some string";
            var value1b = "some string";

            var value2a = "some other string";
            var value2b = "some other string";

            // Act
            var systemUnderTest1a = HashCodeHelper.Initialize().Hash(value1a);
            var systemUnderTest1b = HashCodeHelper.Initialize().Hash(value1b);

            var systemUnderTest2a = HashCodeHelper.Initialize().Hash(value1a).Hash(value2a);
            var systemUnderTest2b = HashCodeHelper.Initialize().Hash(value1b).Hash(value2b);

            // Assert
            systemUnderTest1a.Value.Should().Be(systemUnderTest1b.Value);
            systemUnderTest2a.Value.Should().Be(systemUnderTest2b.Value);
        }

        [Fact]
        public static void HashElements___Should_return_different_hash_code_than_Initialize___When_the_values_parameter_is_null()
        {
            // Arrange
            var initialize = HashCodeHelper.Initialize();
            double[] values = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var systemUnderTest = HashCodeHelper.Initialize().HashElements(values);
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            systemUnderTest.Value.Should().NotBe(initialize.Value);
        }

        [Fact]
        public static void HashElements___Should_return_nonzero_hash_code___When_the_values_parameter_is_null()
        {
            // Arrange
            double[] values = null;

            // Act
            // ReSharper disable ExpressionIsAlwaysNull
            var systemUnderTest = HashCodeHelper.Initialize().HashElements(values);
            // ReSharper restore ExpressionIsAlwaysNull

            // Assert
            systemUnderTest.Value.Should().NotBe(0);
        }

        [Fact]
        public static void HashElements___Should_return_nonzero_hash_code___When_the_values_parameter_contains_null_element()
        {
            // Arrange
            var values = new double?[] { ThreadSafeRandom.NextDouble(), null, ThreadSafeRandom.NextDouble() };

            // Act
            var systemUnderTest = HashCodeHelper.Initialize().HashElements(values);

            // Assert
            systemUnderTest.Value.Should().NotBe(0);
        }

        [Fact]
        public static void HashElements___Should_return_same_hash_code_for_two_different_IEnumerable___When_both_IEnumerable_are_sequence_equal()
        {
            // Arrange
            // ReSharper disable CollectionNeverUpdated.Local
            var values1a = new List<double>();
            var values1b = new double[] { };
            // ReSharper restore CollectionNeverUpdated.Local

            var values2a = new[] { ThreadSafeRandom.NextDouble() };
            var values2b = new List<double> { values2a[0] };

            var values3a = Enumerable.Range(0, 5).Select(_ => ThreadSafeRandom.NextDouble()).ToArray();
            var values3b = new List<double>(values3a);

            var values4a = new double?[] { ThreadSafeRandom.NextDouble(), null, ThreadSafeRandom.NextDouble() };
            var values4b = new List<double?>(values4a);

            // Act
            var systemUnderTest1a = HashCodeHelper.Initialize().HashElements(values1a);
            var systemUnderTest1b = HashCodeHelper.Initialize().HashElements(values1b);

            var systemUnderTest2a = HashCodeHelper.Initialize().HashElements(values2a);
            var systemUnderTest2b = HashCodeHelper.Initialize().HashElements(values2b);

            var systemUnderTest3a = HashCodeHelper.Initialize().HashElements(values3a);
            var systemUnderTest3b = HashCodeHelper.Initialize().HashElements(values3b);

            var systemUnderTest4a = HashCodeHelper.Initialize().HashElements(values4a);
            var systemUnderTest4b = HashCodeHelper.Initialize().HashElements(values4b);

            // Assert
            systemUnderTest1a.Value.Should().Be(systemUnderTest1b.Value);
            systemUnderTest2a.Value.Should().Be(systemUnderTest2b.Value);
            systemUnderTest3a.Value.Should().Be(systemUnderTest3b.Value);
            systemUnderTest4a.Value.Should().Be(systemUnderTest4b.Value);
        }

        [Fact]
        public static void HashElements___Should_return_different_hash_codes_for_two_different_IEnumerable___When_both_IEnumerable_are_not_sequence_equal()
        {
            // Arrange
            var values1a = new List<double?> { null };
            var values1b = new double?[] { };

            var values2a = new List<double?> { null };
            var values2b = new double?[] { ThreadSafeRandom.NextDouble() };

            var values3a = new[] { ThreadSafeRandom.NextDouble() };
            var values3b = new List<double> { values3a[0], ThreadSafeRandom.NextDouble() };

            var values4a = Enumerable.Range(0, 5).Select(_ => ThreadSafeRandom.NextDouble()).ToArray();
            var values4b = new List<double>(values3a.Take(4));

            var values5a = new double?[] { ThreadSafeRandom.NextDouble(), null, ThreadSafeRandom.NextDouble() };
            var values5b = values5a.Reverse();

            // Act
            var systemUnderTest1a = HashCodeHelper.Initialize().HashElements(values1a);
            var systemUnderTest1b = HashCodeHelper.Initialize().HashElements(values1b);

            var systemUnderTest2a = HashCodeHelper.Initialize().HashElements(values2a);
            var systemUnderTest2b = HashCodeHelper.Initialize().HashElements(values2b);

            var systemUnderTest3a = HashCodeHelper.Initialize().HashElements(values3a);
            var systemUnderTest3b = HashCodeHelper.Initialize().HashElements(values3b);

            var systemUnderTest4a = HashCodeHelper.Initialize().HashElements(values4a);
            var systemUnderTest4b = HashCodeHelper.Initialize().HashElements(values4b);

            var systemUnderTest5a = HashCodeHelper.Initialize().HashElements(values5a);
            var systemUnderTest5b = HashCodeHelper.Initialize().HashElements(values5b);

            // Assert
            systemUnderTest1a.Value.Should().NotBe(systemUnderTest1b.Value);
            systemUnderTest2a.Value.Should().NotBe(systemUnderTest2b.Value);
            systemUnderTest3a.Value.Should().NotBe(systemUnderTest3b.Value);
            systemUnderTest4a.Value.Should().NotBe(systemUnderTest4b.Value);
            systemUnderTest5a.Value.Should().NotBe(systemUnderTest5b.Value);
        }

        // ReSharper restore InconsistentNaming
    }
}
