﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashCodeHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    public static class HashCodeHelperTest
    {
        private static readonly HashCodeHelper ObjectForEquatableTests = A.Dummy<HashCodeHelper>();

        private static readonly HashCodeHelper ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests =
            new HashCodeHelper(ObjectForEquatableTests.Value);

        private static readonly HashCodeHelper[] ObjectsThatAreNotEqualToObjectForEquatableTests =
        {
            A.Dummy<HashCodeHelper>(),
            new HashCodeHelper(A.Dummy<int>().ThatIsNot(ObjectForEquatableTests.Value)),
        };

        private static readonly object ObjectThatIsNotTheSameTypeAsObjectForEquatableTests = A.Dummy<object>();

        [Fact]
        public static void Hash___Should_return_different_hash_code_than_Initialize___When_the_value_parameter_is_null()
        {
            // Arrange
            var initialize = HashCodeHelper.Initialize();
            double? value = null;

            // Act
            var systemUnderTest = HashCodeHelper.Initialize().Hash(value);

            // Assert
            systemUnderTest.Value.Should().NotBe(initialize.Value);
        }

        [Fact]
        public static void Hash___Should_return_nonzero_hash_code___When_the_value_parameter_is_null()
        {
            // Arrange
            double? value = null;

            // Act
            var systemUnderTest = HashCodeHelper.Initialize().Hash(value);

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
            var systemUnderTest = HashCodeHelper.Initialize().HashElements(values);

            // Assert
            systemUnderTest.Value.Should().NotBe(initialize.Value);
        }

        [Fact]
        public static void HashElements___Should_return_nonzero_hash_code___When_the_values_parameter_is_null()
        {
            // Arrange
            double[] values = null;

            // Act
            var systemUnderTest = HashCodeHelper.Initialize().HashElements(values);

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
            var values1a = new List<double>();
            var values1b = new double[] { };

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

        [Fact]
        public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = ObjectForEquatableTests == ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests == _).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests == ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = ObjectForEquatableTests != ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests != _).ToList();

            // sAssert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests != ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_HashCodeHelper___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(ObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_HashCodeHelper___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Equals_with_HashCodeHelper___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_not_of_the_same_type()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectThatIsNotTheSameTypeAsObjectForEquatableTests);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals((object)_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_objects___When_objects_have_different_property_values()
        {
            // Arrange, Act
            var hashCode1 = ObjectForEquatableTests.GetHashCode();
            var hashCode2 = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => _.GetHashCode()).ToList();

            // Assert
            hashCode2.ForEach(_ => _.Should().NotBe(hashCode1));
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_objects___When_objects_have_the_same_property_values()
        {
            // Arrange, Act
            var hash1 = ObjectForEquatableTests.GetHashCode();
            var hash2 = ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }
    }
}
