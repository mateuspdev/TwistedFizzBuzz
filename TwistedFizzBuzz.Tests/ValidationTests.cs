using System;
using System.Collections.Generic;
using TwistedFizzBuzz.Library;
using TwistedFizzBuzz.Library.Models;
using Xunit;
using Xunit.Abstractions;

namespace TwistedFizzBuzz.Tests;

public class ValidationTests
{
    private readonly ITestOutputHelper _output;

    public ValidationTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void FizzBuzzToken_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        int divisor = 7;
        string token = "Lucky";
        
        // Act
        var fizzBuzzToken = new FizzBuzzToken(divisor, token);
        
        // Assert
        Assert.Equal(divisor, fizzBuzzToken.Divisor);
        Assert.Equal(token, fizzBuzzToken.Token);
    }
    
    [Fact]
    public void FizzBuzzToken_Constructor_WithNullToken_SetsEmptyString()
    {
        // Arrange
        int divisor = 7;
        string? token = null;
        
        // Act
        var fizzBuzzToken = new FizzBuzzToken(divisor, token);
        
        // Assert
        Assert.Equal(divisor, fizzBuzzToken.Divisor);
        Assert.Equal(string.Empty, fizzBuzzToken.Token);
    }
    
    [Fact]
    public void ProcessSingleNumber_WithZeroDivisor_HandlesGracefully()
    {
        // Arrange
        int number = 10;
        var tokens = new List<FizzBuzzToken>
        {
            new(0, "Zero") // Division by zero should be handled gracefully
        };
        _output.WriteLine($"Testing ProcessSingleNumber with zero divisor for number {number}");
        
        // Act & Assert
        // This should not throw an exception when we try to process a number
        var exception = Record.Exception(() => 
        {
            var results = FizzBuzzProcessor.ProcessRange(number, number, tokens);
            _output.WriteLine($"Result: {results[0]}");
        });
        
        Assert.Null(exception);
    }
    
    [Fact]
    public void ProcessRange_WithExtremeValues_HandlesGracefully()
    {
        // Arrange
        int start = int.MinValue;
        int end = int.MinValue + 10;
        _output.WriteLine($"Testing ProcessRange with extreme values from {start} to {end}");
        
        // Act & Assert
        // This should not throw an exception
        var exception = Record.Exception(() => 
        {
            var results = FizzBuzzProcessor.ProcessRange(start, end);
            _output.WriteLine($"Processed {results.Count} numbers");
            
            foreach (var result in results)
            {
                _output.WriteLine(result);
            }
        });
        
        Assert.Null(exception);
    }
    
    [Fact]
    public void ProcessNumbers_WithNullArray_ReturnsEmptyList()
    {
        // Arrange
        int[]? numbers = null;
        _output.WriteLine("Testing ProcessNumbers with null array");
        
        // Act
        var results = FizzBuzzProcessor.ProcessNumbers(numbers);
        
        // Assert
        Assert.NotNull(results);
        Assert.Empty(results);
    }
    
    [Fact]
    public void ProcessRange_WithNullTokens_UsesDefaultTokens()
    {
        // Arrange
        int start = 1;
        int end = 15;
        List<FizzBuzzToken>? tokens = null;
        _output.WriteLine($"Testing ProcessRange with null tokens from {start} to {end}");
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(start, end, tokens);
        
        // Assert
        Assert.Equal(15, results.Count);
        Assert.Equal("FizzBuzz", results[14]); // 15 should be "FizzBuzz" with default tokens
    }
} 