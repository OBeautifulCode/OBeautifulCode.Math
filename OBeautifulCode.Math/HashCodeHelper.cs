﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashCodeHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Math source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Recipes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides methods to help with generating hash codes for structures and classes. This handles
    /// value types, nullable type, and objects.
    /// </summary>
    /// <remarks>
    /// Adapted from NodaTime: <a href="https://github.com/nodatime/nodatime/blob/master/src/NodaTime/Utility/HashCodeHelper.cs"/>.
    /// The basic usage pattern is as follows.
    /// <example>
    /// <code>
    ///  public override int GetHashCode() => HashCodeHelper.Initialize().Hash(Field1).Hash(Field2).Hash(Field3).Value;
    /// </code>
    /// </example>
    /// </remarks>
#if !OBeautifulCodeMathRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.String", "See package version number")]
    internal
#else
    public
#endif
    struct HashCodeHelper : IEquatable<HashCodeHelper>
    {
        /// <summary>
        /// The multiplier for each value.
        /// </summary>
        private const int HashCodeMultiplier = 37;

        /// <summary>
        /// The initial hash value.
        /// </summary>
        private const int HashCodeInitializer = 17;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCodeHelper"/> struct.
        /// </summary>
        /// <param name="value">The hash code value.</param>
        public HashCodeHelper(
            int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the hash code value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Determines whether two objects of type <see cref="HashCodeHelper"/> are equal.
        /// </summary>
        /// <param name="item1">The first item to compare.</param>
        /// <param name="item2">The second item to compare.</param>
        /// <returns>True if the two items are equal; false otherwise.</returns>
        public static bool operator ==(
            HashCodeHelper item1,
            HashCodeHelper item2)
        {
            var result = item1.Value == item2.Value;
            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="HashCodeHelper" /> are not equal.
        /// </summary>
        /// <param name="item1">The first item to compare.</param>
        /// <param name="item2">The item to compare.</param>
        /// <returns>True if the two items not equal; false otherwise.</returns>
        public static bool operator !=(
            HashCodeHelper item1,
            HashCodeHelper item2)
            => !(item1 == item2);

        /// <summary>
        /// Returns the initial value for a hash code.
        /// </summary>
        /// <returns>The initial integer wrapped in a <see cref="HashCodeHelper"/> value.</returns>
        public static HashCodeHelper Initialize() => new HashCodeHelper(HashCodeInitializer);

        /// <summary>
        /// Returns the initial value for a hash code.
        /// </summary>
        /// <param name="seedValue">Seed value to initialize with (often the hash code from a base class using it's base properties).</param>
        /// <returns>The initial integer wrapped in a <see cref="HashCodeHelper"/> value.</returns>
        public static HashCodeHelper Initialize(int seedValue) => new HashCodeHelper(seedValue);

        /// <summary>
        /// Adds the hash value for the given value to the current hash and returns the new value.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper Hash(
            object value)
        {
            unchecked
            {
                var hashCode = (this.Value * HashCodeMultiplier) + (value?.GetHashCode() ?? 0);
                var result = new HashCodeHelper(hashCode);
                return result;
            }
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>
        /// to the current hash and returns the new value.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionary<TKey, TValue>(
            IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            HashCodeHelper helper = this;
            if (dictionary == null)
            {
                helper = helper.Hash(null);
            }
            else
            {
                var keysInOrder = dictionary.OrderBy(_ => _.Key).Select(_ => _.Key).ToList();

                helper = helper
                    .HashElements(keysInOrder)
                    .HashElements(keysInOrder.Select(_ => dictionary[_]));
            }

            return helper;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values are sequence equal.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSequenceEqualsValueEquality<TKey, TValue>(
            IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            HashCodeHelper helper = this;
            if (dictionary == null)
            {
                helper = helper.Hash(null);
            }
            else
            {
                var keysInOrder = dictionary.OrderBy(_ => _.Key).Select(_ => _.Key).ToList();

                helper = helper.HashElements(keysInOrder);

                foreach (var key in keysInOrder)
                {
                    var values = dictionary[key];

                    helper = helper.HashElements((IEnumerable)values);
                }
            }

            return helper;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values have no symmetric difference.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TElementValue">The dictionary's values' element type.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <param name="elementValueEqualityComparer">Optional equality comparer used to get the distinct set of elements within each of the dictionary's values.  Default is to use <see cref="EqualityComparer{T}.Default"/>.</param>
        /// <param name="elementValueComparer">Optional comparer used to order the elements within each of the dictionary's values.  Default is to use <see cref="Comparer{TKey}.Default"/>.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue>(
            IReadOnlyDictionary<TKey, TElementValue[]> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            var result = this.HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue[], TElementValue>(dictionary, elementValueEqualityComparer, elementValueComparer);

            return result;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values have no symmetric difference.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TElementValue">The dictionary's values' element type.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <param name="elementValueEqualityComparer">Optional equality comparer used to get the distinct set of elements within each of the dictionary's values.  Default is to use <see cref="EqualityComparer{T}.Default"/>.</param>
        /// <param name="elementValueComparer">Optional comparer used to order the elements within each of the dictionary's values.  Default is to use <see cref="Comparer{TKey}.Default"/>.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue>(
            IReadOnlyDictionary<TKey, IEnumerable<TElementValue>> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            var result = this.HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, IEnumerable<TElementValue>, TElementValue>(dictionary, elementValueEqualityComparer, elementValueComparer);

            return result;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values have no symmetric difference.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TElementValue">The dictionary's values' element type.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <param name="elementValueEqualityComparer">Optional equality comparer used to get the distinct set of elements within each of the dictionary's values.  Default is to use <see cref="EqualityComparer{T}.Default"/>.</param>
        /// <param name="elementValueComparer">Optional comparer used to order the elements within each of the dictionary's values.  Default is to use <see cref="Comparer{TKey}.Default"/>.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue>(
            IReadOnlyDictionary<TKey, ICollection<TElementValue>> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            var result = this.HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, ICollection<TElementValue>, TElementValue>(dictionary, elementValueEqualityComparer, elementValueComparer);

            return result;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values have no symmetric difference.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TElementValue">The dictionary's values' element type.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <param name="elementValueEqualityComparer">Optional equality comparer used to get the distinct set of elements within each of the dictionary's values.  Default is to use <see cref="EqualityComparer{T}.Default"/>.</param>
        /// <param name="elementValueComparer">Optional comparer used to order the elements within each of the dictionary's values.  Default is to use <see cref="Comparer{TKey}.Default"/>.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue>(
            IReadOnlyDictionary<TKey, IReadOnlyCollection<TElementValue>> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            var result = this.HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, IReadOnlyCollection<TElementValue>, TElementValue>(dictionary, elementValueEqualityComparer, elementValueComparer);

            return result;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values have no symmetric difference.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TElementValue">The dictionary's values' element type.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <param name="elementValueEqualityComparer">Optional equality comparer used to get the distinct set of elements within each of the dictionary's values.  Default is to use <see cref="EqualityComparer{T}.Default"/>.</param>
        /// <param name="elementValueComparer">Optional comparer used to order the elements within each of the dictionary's values.  Default is to use <see cref="Comparer{TKey}.Default"/>.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue>(
            IReadOnlyDictionary<TKey, List<TElementValue>> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            var result = this.HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, List<TElementValue>, TElementValue>(dictionary, elementValueEqualityComparer, elementValueComparer);

            return result;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values have no symmetric difference.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TElementValue">The dictionary's values' element type.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <param name="elementValueEqualityComparer">Optional equality comparer used to get the distinct set of elements within each of the dictionary's values.  Default is to use <see cref="EqualityComparer{T}.Default"/>.</param>
        /// <param name="elementValueComparer">Optional comparer used to order the elements within each of the dictionary's values.  Default is to use <see cref="Comparer{TKey}.Default"/>.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue>(
            IReadOnlyDictionary<TKey, IList<TElementValue>> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            var result = this.HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, IList<TElementValue>, TElementValue>(dictionary, elementValueEqualityComparer, elementValueComparer);

            return result;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>,
        /// to the current hash and returns the new value.
        /// This method should be used when the dictionary's values are enumerable and where another dictionary
        /// with the same keys results in the same hash code when the corresponding values have no symmetric difference.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TElementValue">The dictionary's values' element type.</typeparam>
        /// <param name="dictionary">The dictionary to hash.</param>
        /// <param name="elementValueEqualityComparer">Optional equality comparer used to get the distinct set of elements within each of the dictionary's values.  Default is to use <see cref="EqualityComparer{T}.Default"/>.</param>
        /// <param name="elementValueComparer">Optional comparer used to order the elements within each of the dictionary's values.  Default is to use <see cref="Comparer{TKey}.Default"/>.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TElementValue>(
            IReadOnlyDictionary<TKey, IReadOnlyList<TElementValue>> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            var result = this.HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, IReadOnlyList<TElementValue>, TElementValue>(dictionary, elementValueEqualityComparer, elementValueComparer);

            return result;
        }

        /// <summary>
        /// Adds the hash value for all elements of the specified <see cref="IEnumerable"/> to the current hash and returns the new value.
        /// </summary>
        /// <param name="values">The enumerable to hash.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper HashElements(
            IEnumerable values)
        {
            HashCodeHelper helper = this;
            if (values == null)
            {
                helper = helper.Hash(null);
            }
            else
            {
                foreach (var value in values)
                {
                    helper = helper.Hash(value);
                }
            }

            return helper;
        }

        /// <inheritdoc />
        public bool Equals(
            HashCodeHelper other)
            => this == other;

        /// <inheritdoc />
        public override bool Equals(
            object obj)
        {
            if (!(obj is HashCodeHelper))
            {
                return false;
            }

            var result = this.Equals((HashCodeHelper)obj);
            return result;
        }

        /// <summary>
        /// Returns the hash code for this object.
        /// </summary>
        /// <returns>The hash code for this object.</returns>
        public override int GetHashCode() =>
            this.Value.GetHashCode();

        private HashCodeHelper HashDictionaryHavingEnumerableValuesForSymmetricDifferenceValueEquality<TKey, TValue, TElementValue>(
            IReadOnlyDictionary<TKey, TValue> dictionary,
            IEqualityComparer<TElementValue> elementValueEqualityComparer = null,
            IComparer<TElementValue> elementValueComparer = null)
        {
            if (elementValueEqualityComparer == null)
            {
                elementValueEqualityComparer = EqualityComparer<TElementValue>.Default;
            }

            if (elementValueComparer == null)
            {
                elementValueComparer = Comparer<TElementValue>.Default;
            }

            HashCodeHelper helper = this;
            if (dictionary == null)
            {
                helper = helper.Hash(null);
            }
            else
            {
                var keysInOrder = dictionary.OrderBy(_ => _.Key).Select(_ => _.Key).ToList();

                helper = helper.HashElements(keysInOrder);

                foreach (var key in keysInOrder)
                {
                    var values = (IEnumerable<TElementValue>)dictionary[key];

                    helper = helper.HashElements(values.Distinct(elementValueEqualityComparer).OrderBy(_ => _, elementValueComparer));
                }
            }

            return helper;
        }
    }
}
