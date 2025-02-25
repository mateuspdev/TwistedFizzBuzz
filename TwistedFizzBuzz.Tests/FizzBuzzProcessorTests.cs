using System.Collections.Generic;
using System.Threading.Tasks;
using TwistedFizzBuzz.Library;
using TwistedFizzBuzz.Library.Models;
using TwistedFizzBuzz.Library.Services;
using Xunit;
using Xunit.Abstractions;

namespace TwistedFizzBuzz.Tests;

public class TwistedFizzBuzzTests
{
    private readonly ITestOutputHelper _output;

    public TwistedFizzBuzzTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ProcessRange_WithDefaultTokens_ReturnsCorrectResults()
    {
        // Arrange
        int start = 1;
        int end = 15;
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(start, end);
        
        // Assert
        Assert.Equal(15, results.Count);
        Assert.Equal("1", results[0]);
        Assert.Equal("2", results[1]);
        Assert.Equal("Fizz", results[2]);
        Assert.Equal("4", results[3]);
        Assert.Equal("Buzz", results[4]);
        Assert.Equal("Fizz", results[5]);
        Assert.Equal("7", results[6]);
        Assert.Equal("8", results[7]);
        Assert.Equal("Fizz", results[8]);
        Assert.Equal("Buzz", results[9]);
        Assert.Equal("11", results[10]);
        Assert.Equal("Fizz", results[11]);
        Assert.Equal("13", results[12]);
        Assert.Equal("14", results[13]);
        Assert.Equal("FizzBuzz", results[14]);
    }
    
    [Fact]
    public void ProcessRange_WithReverseRange_ReturnsCorrectResults()
    {
        // Arrange
        int start = 5;
        int end = 1;
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(start, end);
        
        // Assert
        Assert.Equal(5, results.Count);
        Assert.Equal("Buzz", results[0]);
        Assert.Equal("4", results[1]);
        Assert.Equal("Fizz", results[2]);
        Assert.Equal("2", results[3]);
        Assert.Equal("1", results[4]);
    }
    
    [Fact]
    public void ProcessRange_WithNegativeNumbers_ReturnsCorrectResults()
    {
        // Arrange
        int start = -5;
        int end = 0;
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(start, end);
        
        // Assert
        Assert.Equal(6, results.Count);
        Assert.Equal("Buzz", results[0]); // -5
        Assert.Equal("-4", results[1]);
        Assert.Equal("Fizz", results[2]); // -3
        Assert.Equal("-2", results[3]);
        Assert.Equal("-1", results[4]);
        Assert.Equal("FizzBuzz", results[5]); // 0
    }
    
    [Fact]
    public void ProcessRange_WithCustomTokens_ReturnsCorrectResults()
    {
        // Arrange
        int start = 1;
        int end = 20;
        var tokens = new List<FizzBuzzToken>
        {
            new(7, "Poem"),
            new(3, "College"),
            new(5, "Writer")
        };
        
        // Act
        var results = FizzBuzzProcessor.ProcessRange(start, end, tokens);
        
        // Assert
        Assert.Equal(20, results.Count);
        Assert.Equal("1", results[0]);
        Assert.Equal("2", results[1]);
        Assert.Equal("College", results[2]); // 3
        Assert.Equal("4", results[3]);
        Assert.Equal("Writer", results[4]); // 5
        Assert.Equal("College", results[5]); // 6
        Assert.Equal("Poem", results[6]); // 7
        Assert.Equal("8", results[7]);
        Assert.Equal("College", results[8]); // 9
        Assert.Equal("Writer", results[9]); // 10
        Assert.Equal("11", results[10]);
        Assert.Equal("College", results[11]); // 12
        Assert.Equal("13", results[12]);
        Assert.Equal("Poem", results[13]); // 14
        Assert.Equal("CollegeWriter", results[14]); // 15
        Assert.Equal("16", results[15]);
        Assert.Equal("17", results[16]);
        Assert.Equal("College", results[17]); // 18
        Assert.Equal("19", results[18]);
        Assert.Equal("Writer", results[19]); // 20
    }
    
    [Fact]
    public void ProcessNumbers_WithDefaultTokens_ReturnsCorrectResults()
    {
        // Arrange
        int[] numbers = { -5, 6, 300, 12, 15 };
        
        // Act
        var results = FizzBuzzProcessor.ProcessNumbers(numbers);
        
        // Assert
        Assert.Equal(5, results.Count);
        Assert.Equal("Buzz", results[0]); // -5
        Assert.Equal("Fizz", results[1]); // 6
        Assert.Equal("FizzBuzz", results[2]); // 300
        Assert.Equal("Fizz", results[3]); // 12
        Assert.Equal("FizzBuzz", results[4]); // 15
    }
    
    [Fact]
    public void ProcessNumbers_WithCustomTokens_ReturnsCorrectResults()
    {
        // Arrange
        int[] numbers = { 7, 17, 21, 51, 119, 357 };
        var tokens = new List<FizzBuzzToken>
        {
            new(7, "Poem"),
            new(17, "Writer"),
            new(3, "College")
        };
        
        // Act
        var results = FizzBuzzProcessor.ProcessNumbers(numbers, tokens);
        
        // Assert
        Assert.Equal(6, results.Count);
        Assert.Equal("Poem", results[0]); // 7
        Assert.Equal("Writer", results[1]); // 17
        Assert.Equal("PoemCollege", results[2]); // 21
        Assert.Equal("WriterCollege", results[3]); // 51
        Assert.Equal("PoemWriter", results[4]); // 119
        Assert.Equal("PoemWriterCollege", results[5]); // 357
    }
    
    [Fact]
    public async Task ApiService_GetApiTokensAsync_MayReturnEmptyList()
    {
        // Act
        var tokens = await ApiService.GetApiTokensAsync();
        
        // Assert
        Assert.NotNull(tokens);
        // Note: The API might return an empty list if it fails
        _output.WriteLine($"API returned {tokens.Count} token(s)");
        
        // No assertion on tokens.Count since it might be empty
    }
    
    [Fact]
    public async Task ApiService_GetApiTokensAsync_WithCount_MayReturnEmptyList()
    {
        // Arrange
        int tokenCount = 3;
        
        // Act
        var tokens = await ApiService.GetApiTokensAsync(tokenCount);
        
        // Assert
        Assert.NotNull(tokens);
        // Note: The API might return an empty list if it fails
        _output.WriteLine($"Requested {tokenCount} tokens, API returned {tokens.Count} token(s)");
        
        // No assertion on tokens.Count since it might be empty
    }
    
    [Fact]
    public async Task ProcessRangeWithApiTokenAsync_ReturnsResults()
    {
        // Arrange
        int start = 1;
        int end = 10;
        
        // Act
        var results = await FizzBuzzProcessor.ProcessRangeWithApiTokenAsync(start, end);
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(10, results.Count);
    }
    
    [Fact]
    public async Task ProcessRangeWithApiTokenAsync_WithMultipleTokens_ReturnsResults()
    {
        // Arrange
        int start = 1;
        int end = 10;
        int tokenCount = 2;
        
        // Act
        var results = await FizzBuzzProcessor.ProcessRangeWithApiTokenAsync(start, end, tokenCount);
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(10, results.Count);
    }
}