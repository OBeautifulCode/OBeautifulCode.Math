// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathHelper.cs" company="OBeautifulCode">
//   Copyright 2015 OBeautifulCode
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Conditions;

    /// <summary>
    /// Supports various mathematical and numerical methods.
    /// </summary>
    public class MathHelper
    {
        /// <summary>
        /// Determines if two doubles are almost equal (given some level of tolerance).
        /// </summary>
        /// <param name="target">A target.</param>
        /// <param name="current">The number to be compared to the target.</param>
        /// <param name="tolerance">Differences smaller than tolerance are not considered. Defaults to 1e-8.</param>
        /// <returns>
        /// true if target and current are almost equal, false if not.
        /// </returns>
        /// <exception cref="ArgumentException">target or current is double.NaN</exception>
        /// <exception cref="ArgumentOutOfRangeException">tolerance is not &gt;= 0</exception>
        public static bool AlmostEqual(double target, double current, double tolerance = 1e-8)
        {
            if (double.IsNaN(target) || double.IsNaN(current))
            {
                throw new ArgumentException("target or current is NaN");
            }

            Condition.Requires(tolerance, "tolerance").IsGreaterOrEqual(0);

            double diff = Math.Abs(target - current);
            double mag = Math.Max(Math.Abs(target), Math.Abs(current));
            if (mag > tolerance)
            {
                return (diff / mag) <= tolerance;
            }

            return diff <= tolerance;
        }

        /// <summary>
        /// Calculates the covariance of two sets of doubles.
        /// </summary>
        /// <param name="values1">The first set of doubles.</param>
        /// <param name="values2">The second set of doubles.</param>
        /// <returns>
        /// Returns the covariance of two sets of doubles.
        /// </returns>
        /// <exception cref="ArgumentNullException">values1 or values2 is null.</exception>
        /// <exception cref="ArgumentException">values1 or values2 is empty.</exception>
        /// <exception cref="ArgumentException">Length of sources (values1, values2) is different.</exception>
        public static double Covariance(IEnumerable<double> values1, IEnumerable<double> values2)
        {
            // check parameters
            // ReSharper disable PossibleMultipleEnumeration
            Condition.Requires(values1, "values1").IsNotEmpty();
            Condition.Requires(values2, "values2").IsNotEmpty();
            var values1List = values1 as IList<double> ?? values1.ToArray();
            var values2List = values2 as IList<double> ?? values2.ToArray();
            // ReSharper restore PossibleMultipleEnumeration
            int valuesCount = values1List.Count();
            if (valuesCount != values2List.Count())
            {
                throw new ArgumentException("Length of sources is different.");
            }

            // covariance of one item is always 0
            if (valuesCount == 1)
            {
                return 0;
            }

            // do the math
            double avg1 = values1List.Average();
            double avg2 = values2List.Average();
            double cov = 0;
            for (int i = 0; i < valuesCount; i++)
            {
                cov += (values1List[i] - avg1) * (values2List[i] - avg2);
            }

            cov /= valuesCount;
            return cov;
        }

        /// <summary>
        /// Calculates the covariance of two sets of decimal.
        /// </summary>
        /// <param name="values1">The first set of decimals.</param>
        /// <param name="values2">The second set of decimals.</param>
        /// <returns>
        /// Returns the covariance of two sets of decimals.
        /// </returns>
        /// <exception cref="ArgumentNullException">values1 or values2 is null.</exception>
        /// <exception cref="ArgumentException">values1 or values2 is empty.</exception>
        /// <exception cref="ArgumentException">Length of sources (values1, values2) is different.</exception>
        public static decimal Covariance(IEnumerable<decimal> values1, IEnumerable<decimal> values2)
        {
            // check parameters
            // ReSharper disable PossibleMultipleEnumeration
            Condition.Requires(values1, "values1").IsNotEmpty();
            Condition.Requires(values2, "values2").IsNotEmpty();
            var values1List = values1 as IList<decimal> ?? values1.ToArray();
            var values2List = values2 as IList<decimal> ?? values2.ToArray();
            // ReSharper restore PossibleMultipleEnumeration
            int valuesCount = values1List.Count();
            if (valuesCount != values2List.Count())
            {
                throw new ArgumentException("Length of sources is different.");
            }

            // covariance of one item is always 0
            if (valuesCount == 1)
            {
                return 0;
            }

            // do the math
            decimal avg1 = values1List.Average();
            decimal avg2 = values2List.Average();
            decimal cov = 0;
            for (int i = 0; i < valuesCount; i++)
            {
                cov += (values1List[i] - avg1) * (values2List[i] - avg2);
            }

            cov /= valuesCount;
            return cov;
        }

        /// <summary>
        /// Determines the factors of a number.
        /// </summary>
        /// <param name="x">The number whose factors are to be returned</param>
        /// <returns>
        /// Returns the factors of a number.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">x must be &gt;= 0</exception>
        public static IEnumerable<int> Factors(int x)
        {
            Condition.Requires(x, "x").IsGreaterOrEqual(0);
            var results = new List<int>();
            int max = x / 2;
            for (int i = 1; i <= max; i++)
            {
                if (0 == (x % i))
                {
                    results.Add(i);
                }
            }

            return results;
        }

        /// <summary>
        /// Gets a random number from a random number generator with minimum value returned being 0.
        /// </summary>
        /// <remarks>
        /// This method is uses <see cref="ThreadSafeRandom"/> to guarantee thread safety.
        /// </remarks>
        /// <param name="maximum">the maximum number to return</param>
        /// <returns>the random number.</returns>
        public static int RandomNumber(int maximum)
        {
            return RandomNumber(0, maximum);
        }

        /// <summary>
        /// Gets a random number from a random number generator.
        /// </summary>
        /// <param name="minimum">the minimum number to return</param>
        /// <param name="maximum">the maximum number to return (if <see cref="Int32.MaxValue"/>, then maximum is automatically converted to <see cref="Int32.MaxValue"/> - 1)</param> 
        /// <returns>the random number.</returns>
        /// <remarks>
        /// This method is uses <see cref="ThreadSafeRandom"/> to guarantee thread safety.
        /// Will swap maximum and minimum if minimum &gt; maximum
        /// </remarks>
        public static int RandomNumber(int minimum, int maximum)
        {
            // if passed incorrect arguments, swap them
            if (minimum > maximum)
            {
                int t = minimum;
                minimum = maximum;
                maximum = t;
            }

            // maxValue parameter to Next is exclusive - so we have to add 1 to include maximum
            if (maximum != int.MaxValue)
            {
                maximum++;
            }

            return ThreadSafeRandom.Next(minimum, maximum);
        }

        /// <summary>
        /// Calculates the standard deviation of a set of doubles.
        /// </summary>
        /// <param name="values">The double values to use in the calculation.</param>
        /// <returns>
        /// Returns the standard deviation of the set.
        /// </returns>
        /// <exception cref="ArgumentNullException">values is null.</exception>
        /// <exception cref="ArgumentException">values has 1 or fewer items.</exception>
        /// <exception cref="ArgumentException">There is only one value in values.  Two or more required.</exception>
        public static double StandardDeviation(IEnumerable<double> values)
        {
            // check parameters
            // ReSharper disable PossibleMultipleEnumeration
            Condition.Requires(values, "values").IsLongerThan(1);
            var valuesList = values as IList<double> ?? values.ToArray();
            // ReSharper restore PossibleMultipleEnumeration

            // do the math
            double avg = valuesList.Average();
            double sumOfSqrs = valuesList.Sum(value => Math.Pow(value - avg, 2));
            return Math.Sqrt(sumOfSqrs / Convert.ToDouble(valuesList.Count() - 1));
        }

        /// <summary>
        /// Returns the standard deviation of a set of decimals.
        /// </summary>
        /// <param name="values">The decimal values to use in the calculation.</param>
        /// <returns>
        /// Returns the standard deviation of the set.
        /// </returns>
        /// <exception cref="ArgumentNullException">values is null.</exception>
        /// <exception cref="ArgumentException">values is empty.</exception>
        /// <exception cref="ArgumentException">There is only one value in values.  Two or more required.</exception>
        public static decimal StandardDeviation(IEnumerable<decimal> values)
        {
            // ReSharper disable PossibleMultipleEnumeration
            Condition.Requires(values, "values").IsNotEmpty();
            return Convert.ToDecimal(StandardDeviation(values.Select(Convert.ToDouble)));
            // ReSharper restore PossibleMultipleEnumeration
        }

        /// <summary>
        /// Truncates everything after the decimal point of a decimal and returns the resulting integer number.
        /// </summary>
        /// <param name="value">The decimal to truncate into an integer.</param>
        /// <returns>Integer with the truncated double.</returns>
        /// <remarks>1.49 will return 1, 1.51 will return 1, 1.99 will return 1</remarks>
        /// <exception cref="OverflowException">value overflows the bounds of an <see cref="Int32"/>.</exception>
        public static int Truncate(decimal value)
        {
            if ((value > int.MaxValue) || (value < int.MinValue))
            {
                throw new OverflowException("decimal value overflows the bounds of an Int32");
            }

            // if we get here then we don't need to check whether value-.5 will overflow a double's max/min values
            return Convert.ToInt32(Math.Truncate(value));
        }

        /// <summary>
        /// Truncates everything after the decimal point of a double and returns the resulting integer number.
        /// </summary>
        /// <param name="value">The double to truncate.</param>
        /// <returns>Integer with the truncated double.</returns>
        /// <remarks>1.49 will return 1, 1.51 will return 1, 1.99 will return 1</remarks>
        /// <exception cref="OverflowException">value overflows the bounds of an <see cref="Int32"/>.</exception>
        public static int Truncate(double value)
        {
            if ((value > int.MaxValue) || (value < int.MinValue))
            {
                throw new OverflowException("double value overflows the bounds of an Int32");
            }

            // if we get here then we don't need to check whether value-.5 will overflow a double's max/min values
            return Convert.ToInt32(Math.Truncate(value));
        }

        /// <summary>
        /// Truncates a decimal to a given number of digits.
        /// </summary>
        /// <param name="value">The value to truncate.</param>
        /// <param name="digits">The number of digits to keep.</param>
        /// <returns>
        /// Returns the truncated decimal.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">digits must be >=0</exception>
        /// <exception cref="OverflowException">digits is too high.</exception>
        public static decimal TruncateSignificantDigits(decimal value, int digits)
        {
            Condition.Requires(digits, "digits").IsGreaterOrEqual(0);
            if (digits == 0)
            {
                return decimal.Truncate(value);
            }

            return decimal.Truncate(value * (decimal)Math.Pow(10, digits)) / (decimal)Math.Pow(10, digits);
        }

        /// <summary>
        /// Calculates the variance of a set of doubles.
        /// </summary>
        /// <param name="values">The values used in the calculation.</param>
        /// <returns>
        /// Returns the variance of the set of doubles.
        /// </returns>
        /// <exception cref="ArgumentNullException">values is null.</exception>
        /// <exception cref="ArgumentException">values is empty.</exception>
        /// <exception cref="ArgumentException">There is only one value in values.  Two or more required.</exception>
        public static double Variance(IEnumerable<double> values)
        {
            // check parameters
            // ReSharper disable PossibleMultipleEnumeration
            Condition.Requires(values, "values").IsNotEmpty();
            var valuesList = values as IList<double> ?? values.ToArray();
            // ReSharper restore PossibleMultipleEnumeration
            if (valuesList.Count() == 1)
            {
                throw new ArgumentException("Two values are required");
            }

            // Get average
            double avg = valuesList.Average();
            double sum = valuesList.Sum(value => Math.Pow(value - avg, 2));
            return sum / valuesList.Count();
        }

        /// <summary>
        /// Returns the variance of a set of decimals.
        /// </summary>
        /// <param name="values">The values used in the calculation.</param>
        /// <returns>
        /// Returns the variance of the set of decimals.
        /// </returns>
        /// <exception cref="ArgumentNullException">values is null.</exception>
        /// <exception cref="ArgumentException">values is empty.</exception>
        /// <exception cref="ArgumentException">There is only one value in values.  Two or more required.</exception>
        public static decimal Variance(IEnumerable<decimal> values)
        {
            // ReSharper disable PossibleMultipleEnumeration
            Condition.Requires(values, "values").IsNotEmpty();
            return Convert.ToDecimal(Variance(values.Select(Convert.ToDouble)));
            // ReSharper restore PossibleMultipleEnumeration
        }        
    }
}
