// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashCodeHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math
{
    /// <summary>
    /// Provides method to help with generating hash codes for structures and classes. This handles
    /// value types, nullable type, and objects.
    /// </summary>
    /// <remarks>
    /// Adapted from NodaTime: <a href="https://github.com/nodatime/nodatime/blob/master/src/NodaTime/Utility/HashCodeHelper.cs"/>.
    /// The basic usage pattern is:
    /// <example>
    /// <code>
    ///  public override int GetHashCode() => HashCodeHelper.Initialize().Hash(Field1).Hash(Field2).Hash(Field3).Value;
    /// </code>
    /// </example>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Instances of this type will neither be compared nor sorted nor used as hash table keys.")]
    public struct HashCodeHelper
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
        public HashCodeHelper(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the hash code value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Returns the initial value for a hash code.
        /// </summary>
        /// <returns>The initial integer wrapped in a <see cref="HashCodeHelper"/> value.</returns>
        public static HashCodeHelper Initialize() => new HashCodeHelper(HashCodeInitializer);

        /// <summary>
        /// Adds the hash value for the given value to the current hash and returns the new value.
        /// </summary>
        /// <typeparam name="T">The type of the value being hashed.</typeparam>
        /// <param name="value">The value to hash.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper Hash<T>(T value)
        {
            unchecked
            {
                var hashCode = (this.Value * HashCodeMultiplier) + value?.GetHashCode() ?? 0;
                var result = new HashCodeHelper(hashCode);
                return result;
            }
        }
    }
}
