﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadSafeRandomTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Math.Test
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="ThreadSafeRandom"/> class.
    /// </summary>
    public static class ThreadSafeRandomTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Reseed___Should_result_in_identical_sequences_of_numbers___When_called_with_the_same_seed()
        {
            // Arrange
            var seed = ThreadSafeRandom.Next();
            const int numbersToGenerate = 100;
            var firstSequence = new List<int>();
            var secondSequence = new List<int>();

            // Act
            ThreadSafeRandom.Reseed(seed);
            for (int x = 0; x < numbersToGenerate; x++)
            {
                firstSequence.Add(ThreadSafeRandom.Next());
            }

            ThreadSafeRandom.Reseed(seed);
            for (int x = 0; x < numbersToGenerate; x++)
            {
                secondSequence.Add(ThreadSafeRandom.Next());
            }

            // Assert
            firstSequence.Should().Equal(secondSequence);
        }

        // ReSharper restore InconsistentNaming
    }
}
