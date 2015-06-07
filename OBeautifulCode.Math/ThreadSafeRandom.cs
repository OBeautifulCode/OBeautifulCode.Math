// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadSafeRandom.cs" company="OBeautifulCode">
//   Copyright 2015 OBeautifulCode
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math
{
    using System;

    /// <summary>
    /// Represents a thread-safe pseudo-random number generator, 
    /// a device that produces a sequence of numbers that meet 
    /// certain statistical requirements for randomness.
    /// </summary>
    /// <remarks>
    /// Adapted from: 
    /// <a href="http://blogs.msdn.com/b/pfxteam/archive/2009/02/19/9434171.aspx"/>
    /// <a href="http://codeblog.jonskeet.uk/2009/11/04/revisiting-randomness/"/>
    /// System.Random is not thread-safe, hence the need for this class.
    /// </remarks>
    public class ThreadSafeRandom
    {
        /// <summary>
        /// A single random number generator for the app domain,
        /// used to seed thread-specific random number generators
        /// </summary>
        private static readonly Random Global = new Random();

        /// <summary>
        /// Lock object for access to global random number generator.
        /// </summary>
        private static readonly object GlobalLock = new object();

        /// <summary>
        /// A per-thread random number generator.
        /// </summary>
        [ThreadStatic]
        private static Random local;

        /// <summary>
        /// Returns a nonnegative random integer.
        /// </summary>
        /// <remarks>
        /// Random.Next generates a random number whose value ranges from zero to less than <see cref="Int32.MaxValue"/>. 
        /// To generate a random number whose value ranges from zero to some other positive number, use the <see cref="Next(int)"/> method overload. 
        /// To generate a random number within a different range, use the <see cref="Next(int,int)"/> method overload
        /// </remarks>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero and less than MaxValue
        /// </returns>
        public static int Next()
        {
            EstablishLocal();
            return local.Next();
        }

        /// <summary>
        /// Returns a nonnegative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero, and less than maxValue; that is, the range of return values 
        /// ordinarily includes zero but not maxValue. However, if maxValue equals zero, maxValue is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">maxValue is less than zero.</exception>
        public static int Next(int maxValue)
        {
            EstablishLocal();
            return local.Next(maxValue);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <remarks>
        /// Unlike the other overloads of the Next method, which return only non-negative values, this method can return a negative random integer.
        /// </remarks>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of 
        /// return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">minValue is greater than maxValue.</exception>
        public static int Next(int minValue, int maxValue)
        {
            EstablishLocal();
            return local.Next(minValue, maxValue);
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        /// <remarks>
        /// Each element of the array of bytes is set to a random number greater than or equal to zero, and less than or equal to MaxValue.  
        /// To generate a cryptographically secured random number suitable for creating a random password, 
        /// for example, use a method such as RNGCryptoServiceProvider.GetBytes.
        /// </remarks>
        /// <exception cref="ArgumentNullException">buffer is null.</exception>
        public static void NextBytes(byte[] buffer)
        {
            EstablishLocal();
            local.NextBytes(buffer);
        }

        /// <summary>
        /// Returns a random floating-point number between 0.0 and 1.0.
        /// </summary>
        /// <remarks>
        /// This method is the public version of the protected method, Sample.
        /// </remarks>
        /// <returns>
        /// A double-precision floating point number greater than or equal to 0.0, and less than 1.0.
        /// </returns>
        public static double NextDouble()
        {
            EstablishLocal();
            return local.NextDouble();
        }

        /// <summary>
        /// Instantiates thread-specific random number generator
        ///  if it hasn't been instantiated.
        /// </summary>
        private static void EstablishLocal()
        {
            if (local == null)
            {
                int seed;
                lock (GlobalLock)
                {
                    seed = Global.Next();
                }

                local = new Random(seed);
            }
        }
    }
}
