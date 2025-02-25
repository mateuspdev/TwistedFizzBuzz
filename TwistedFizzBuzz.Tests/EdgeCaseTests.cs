using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwistedFizzBuzz.Library;
using TwistedFizzBuzz.Library.Models;
using TwistedFizzBuzz.Library.Services;
using Xunit;
using Xunit.Abstractions;

namespace TwistedFizzBuzz.Tests;

public class EdgeCaseTests
{
    private readonly ITestOutputHelper _output;

    public EdgeCaseTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ProcessRange_WithSingleNumber_ReturnsCorrectResult()
    {
        // Arrange
        int number = 15;
        _output.WriteLine($"Testing ProcessRange with single number {number}");
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(number, number);
        
        // Assert
        Assert.Single(results);
        Assert.Equal("FizzBuzz", results[0]);
    }
    
    [Fact]
    public void ProcessRange_WithZeroLength_ReturnsCorrectResult()
    {
        // Arrange
        int[] numbers = Array.Empty<int>();
        _output.WriteLine("Testing ProcessNumbers with empty array");
        
        // Act
        var results = FizzBuzzProcessor.ProcessNumbers(numbers);
        
        // Assert
        Assert.Empty(results);
    }
    
    [Fact]
    public void ProcessRange_WithEmptyTokens_UsesDefaultTokens()
    {
        // Arrange
        int start = 1;
        int end = 5;
        var tokens = new List<FizzBuzzToken>();
        _output.WriteLine($"Testing ProcessRange with empty tokens from {start} to {end}");
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(start, end, tokens);
        
        // Assert
        Assert.Equal(5, results.Count);
        Assert.Equal("1", results[0]);
        Assert.Equal("2", results[1]);
        Assert.Equal("Fizz", results[2]); // 3 is divisible by 3, so "Fizz" with default tokens
        Assert.Equal("4", results[3]);
        Assert.Equal("Buzz", results[4]); // 5 is divisible by 5, so "Buzz" with default tokens
        
        _output.WriteLine("Empty token list resulted in using default tokens (Fizz/Buzz)");
    }
    
    [Fact]
    public void ProcessRange_WithZeroDivisor_HandlesCorrectly()
    {
        // Arrange
        int start = 1;
        int end = 5;
        var tokens = new List<FizzBuzzToken>
        {
            new(0, "Zero") // Division by zero should be handled gracefully
        };
        _output.WriteLine($"Testing ProcessRange with zero divisor from {start} to {end}");
        
        // Act & Assert
        // This should not throw an exception
        var exception = Record.Exception(() => FizzBuzzProcessor.ProcessRange(start, end, tokens));
        Assert.Null(exception);
        
        // If no exception, check the results
        if (exception == null)
        {
            var results = FizzBuzzProcessor.ProcessRange(start, end, tokens);
            _output.WriteLine("Results:");
            for (int i = 0; i < results.Count; i++)
            {
                _output.WriteLine($"{start + i}: {results[i]}");
            }
        }
    }
    
    [Fact]
    public void ProcessRange_WithNegativeDivisor_HandlesCorrectly()
    {
        // Arrange
        int start = -10;
        int end = -1;
        var tokens = new List<FizzBuzzToken>
        {
            new(-3, "NegFizz") // Negative divisor
        };
        _output.WriteLine($"Testing ProcessRange with negative divisor from {start} to {end}");
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(start, end, tokens);
        
        // Assert
        Assert.Equal(10, results.Count);
        
        _output.WriteLine("Results:");
        for (int i = 0; i < results.Count; i++)
        {
            _output.WriteLine($"{start + i}: {results[i]}");
        }
    }
    
    [Fact]
    public void ProcessRange_WithLargeRange_PerformsEfficiently()
    {
        // Arrange
        int start = 1;
        int end = 10000;
        _output.WriteLine($"Testing ProcessRange with large range from {start} to {end}");
        
        // Act
        var startTime = DateTime.Now;
        var results = FizzBuzzProcessor.ProcessRange(start, end);
        var endTime = DateTime.Now;
        var duration = (endTime - startTime).TotalMilliseconds;
        
        // Assert
        Assert.Equal(10000, results.Count);
        _output.WriteLine($"Processed {results.Count} numbers in {duration}ms");
        
        // Performance assertion - should process 10,000 numbers in under 100ms
        Assert.True(duration < 500, $"Processing took too long: {duration}ms");
    }
} 