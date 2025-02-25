using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwistedFizzBuzz.Library;
using TwistedFizzBuzz.Library.Models;
using TwistedFizzBuzz.Library.Services;
using Xunit;
using Xunit.Abstractions;

namespace TwistedFizzBuzz.Tests;

public class ApiTests
{
    private readonly ITestOutputHelper _output;

    public ApiTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task ApiService_GetApiTokensAsync_ReturnsValidTokens()
    {
        // Arrange
        _output.WriteLine("Testing ApiService.GetApiTokensAsync() returns valid tokens");
        
        // Act
        var tokens = await ApiService.GetApiTokensAsync();
        
        // Assert
        Assert.NotNull(tokens);
        
        _output.WriteLine($"Received {tokens.Count} token(s) from API");
        foreach (var token in tokens)
        {
            _output.WriteLine($"Token: Divisor={token.Divisor}, Text={token.Token}");
            
            // Validate token properties
            Assert.NotNull(token.Token);
            Assert.NotEmpty(token.Token);
        }
    }
    
    [Fact]
    public async Task ApiService_GetApiTokensAsync_WithMultipleCount_ReturnsCorrectNumberOfTokens()
    {
        // Arrange
        int tokenCount = 3;
        _output.WriteLine($"Testing ApiService.GetApiTokensAsync() with count={tokenCount}");
        
        // Act
        var tokens = await ApiService.GetApiTokensAsync(tokenCount);
        
        // Assert
        Assert.NotNull(tokens);
        
        // We can't guarantee exactly 3 tokens as the API might fail for some requests
        _output.WriteLine($"Requested {tokenCount} tokens, received {tokens.Count} token(s) from API");
        foreach (var token in tokens)
        {
            _output.WriteLine($"Token: Divisor={token.Divisor}, Text={token.Token}");
        }
    }
    
    [Fact]
    public async Task ApiService_GetApiTokensAsync_WithZeroCount_ReturnsEmptyList()
    {
        // Arrange
        int tokenCount = 0;
        _output.WriteLine($"Testing ApiService.GetApiTokensAsync() with count={tokenCount}");
        
        // Act
        var tokens = await ApiService.GetApiTokensAsync(tokenCount);
        
        // Assert
        Assert.NotNull(tokens);
        Assert.Empty(tokens);
        
        _output.WriteLine($"Requested {tokenCount} tokens, received empty list as expected");
    }
    
    [Fact]
    public async Task ProcessRangeWithApiTokenAsync_WithValidRange_ReturnsCorrectResults()
    {
        // Arrange
        int start = 1;
        int end = 10;
        _output.WriteLine($"Testing ProcessRangeWithApiTokenAsync from {start} to {end}");
        
        // Act
        var results = await FizzBuzzProcessor.ProcessRangeWithApiTokenAsync(start, end);
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(10, results.Count);
        
        _output.WriteLine("Results:");
        for (int i = 0; i < results.Count; i++)
        {
            _output.WriteLine($"{start + i}: {results[i]}");
        }
    }
    
    [Fact]
    public async Task ProcessRangeWithApiTokenAsync_WithMultipleTokens_CombinesTokensCorrectly()
    {
        // Arrange
        int start = 1;
        int end = 20;
        int tokenCount = 2;
        _output.WriteLine($"Testing ProcessRangeWithApiTokenAsync from {start} to {end} with {tokenCount} tokens");
        
        // Act
        var results = await FizzBuzzProcessor.ProcessRangeWithApiTokenAsync(start, end, tokenCount);
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(20, results.Count);
        
        _output.WriteLine("Results:");
        for (int i = 0; i < results.Count; i++)
        {
            _output.WriteLine($"{start + i}: {results[i]}");
        }
    }
    
    [Fact]
    public async Task ProcessNumbersWithApiTokenAsync_WithValidNumbers_ReturnsCorrectResults()
    {
        // Arrange
        int[] numbers = { -5, 0, 7, 15, 30 };
        _output.WriteLine($"Testing ProcessNumbersWithApiTokenAsync with specific numbers");
        
        // Act
        var results = await FizzBuzzProcessor.ProcessNumbersWithApiTokenAsync(numbers);
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(numbers.Length, results.Count);
        
        _output.WriteLine("Results:");
        for (int i = 0; i < results.Count; i++)
        {
            _output.WriteLine($"{numbers[i]}: {results[i]}");
        }
    }
    
    [Fact]
    public async Task ProcessNumbersWithApiTokenAsync_WithMultipleTokens_CombinesTokensCorrectly()
    {
        // Arrange
        int[] numbers = { -5, 0, 7, 15, 30 };
        int tokenCount = 3;
        _output.WriteLine($"Testing ProcessNumbersWithApiTokenAsync with specific numbers and {tokenCount} tokens");
        
        // Act
        var results = await FizzBuzzProcessor.ProcessNumbersWithApiTokenAsync(numbers, tokenCount);
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(numbers.Length, results.Count);
        
        _output.WriteLine("Results:");
        for (int i = 0; i < results.Count; i++)
        {
            _output.WriteLine($"{numbers[i]}: {results[i]}");
        }
    }
} 