namespace TwistedFizzBuzz.Library.Models;

/// <summary>
/// Represents a token with its associated divisor for FizzBuzz
/// </summary>
public class FizzBuzzToken(int divisor, string? token)
{
    public int Divisor { get; set; } = divisor;
    public string Token { get; set; } = token ?? string.Empty;
} 