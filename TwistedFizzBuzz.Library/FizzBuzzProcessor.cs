using System.Text;
using TwistedFizzBuzz.Library.Models;
using TwistedFizzBuzz.Library.Services;

namespace TwistedFizzBuzz.Library;

/// <summary>
/// Main class for the TwistedFizzBuzz implementation
/// </summary>
public class FizzBuzzProcessor
{
    /// <summary>
    /// Default FizzBuzz tokens
    /// </summary>
    private static readonly List<FizzBuzzToken> DefaultTokens = new()
    {
        new(3, "Fizz"),
        new(5, "Buzz")
    };

    /// <summary>
    /// Processes a range of numbers using the classic FizzBuzz rules (3 for Fizz, 5 for Buzz)
    /// </summary>
    /// <param name="start">Start of the range (inclusive)</param>
    /// <param name="end">End of the range (inclusive)</param>
    /// <returns>A list of FizzBuzz results</returns>
    public static List<string> ProcessRange(int start, int end)
    {
        return ProcessRange(start, end, DefaultTokens);
    }

    /// <summary>
    /// Processes a range of numbers using custom tokens and divisors
    /// </summary>
    /// <param name="start">Start of the range (inclusive)</param>
    /// <param name="end">End of the range (inclusive)</param>
    /// <param name="tokens">List of custom tokens with their divisors</param>
    /// <returns>A list of FizzBuzz results</returns>
    public static List<string> ProcessRange(int start, int end, List<FizzBuzzToken>? tokens)
    {
        var results = new List<string>();
        
        // Use default tokens if null or empty list is provided
        tokens = tokens == null || tokens.Count == 0 ? DefaultTokens : tokens;
        
        // Handle reverse ranges (when start > end)
        if (start > end)
        {
            for (int i = start; i >= end; i--)
            {
                results.Add(ProcessSingleNumber(i, tokens));
            }
        }
        else
        {
            for (int i = start; i <= end; i++)
            {
                results.Add(ProcessSingleNumber(i, tokens));
            }
        }
        
        return results;
    }

    /// <summary>
    /// Processes a set of non-sequential numbers using the classic FizzBuzz rules
    /// </summary>
    /// <param name="numbers">Array of numbers to process</param>
    /// <returns>A list of FizzBuzz results</returns>
    public static List<string> ProcessNumbers(int[]? numbers)
    {
        return ProcessNumbers(numbers, DefaultTokens);
    }

    /// <summary>
    /// Processes a set of non-sequential numbers using custom tokens and divisors
    /// </summary>
    /// <param name="numbers">Array of numbers to process</param>
    /// <param name="tokens">List of custom tokens with their divisors</param>
    /// <returns>A list of FizzBuzz results</returns>
    public static List<string> ProcessNumbers(int[]? numbers, List<FizzBuzzToken>? tokens)
    {
        var results = new List<string>();
        
        // Return empty list if numbers is null
        if (numbers == null)
        {
            return results;
        }
        
        // Use default tokens if null or empty list is provided
        tokens = tokens == null || tokens.Count == 0 ? DefaultTokens : tokens;
        
        foreach (var number in numbers)
        {
            results.Add(ProcessSingleNumber(number, tokens));
        }
        
        return results;
    }

    /// <summary>
    /// Processes a single number according to FizzBuzz rules with the given tokens
    /// </summary>
    /// <param name="number">The number to process</param>
    /// <param name="tokens">List of tokens with their divisors</param>
    /// <returns>The FizzBuzz result for the number</returns>
    private static string ProcessSingleNumber(int number, List<FizzBuzzToken> tokens)
    {
        var result = new StringBuilder();
        
        foreach (var token in tokens)
        {
            try
            {
                // Skip tokens with zero divisor
                if (token.Divisor == 0)
                {
                    continue;
                }
                
                if (number % token.Divisor == 0)
                {
                    result.Append(token.Token);
                }
            }
            catch
            {
                // Ignore any exceptions during processing
                continue;
            }
        }
        
        return result.Length > 0 ? result.ToString() : number.ToString();
    }

    /// <summary>
    /// Asynchronously fetches tokens from the API and processes a range of numbers
    /// </summary>
    /// <param name="start">Start of the range (inclusive)</param>
    /// <param name="end">End of the range (inclusive)</param>
    /// <param name="tokenCount">Number of tokens to fetch from the API (default: 1)</param>
    /// <returns>A list of FizzBuzz results using API-provided tokens</returns>
    public static async Task<List<string>> ProcessRangeWithApiTokenAsync(int start, int end, int tokenCount = 1)
    {
        var apiTokens = await ApiService.GetApiTokensAsync(tokenCount);
        
        if (apiTokens.Count == 0)
        {
            Console.WriteLine("API returned no tokens, using default tokens");
        }
        
        return ProcessRange(start, end, apiTokens);
    }

    /// <summary>
    /// Asynchronously fetches tokens from the API and processes a set of non-sequential numbers
    /// </summary>
    /// <param name="numbers">Array of numbers to process</param>
    /// <param name="tokenCount">Number of tokens to fetch from the API (default: 1)</param>
    /// <returns>A list of FizzBuzz results using API-provided tokens</returns>
    public static async Task<List<string>> ProcessNumbersWithApiTokenAsync(int[]? numbers, int tokenCount = 1)
    {
        var apiTokens = await ApiService.GetApiTokensAsync(tokenCount);
        
        if (apiTokens.Count == 0)
        {
            Console.WriteLine("API returned no tokens, using default tokens");
        }
        
        return ProcessNumbers(numbers, apiTokens);
    }
}
