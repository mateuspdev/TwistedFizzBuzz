using System.Net.Http.Json;
using TwistedFizzBuzz.Library.Models;

namespace TwistedFizzBuzz.Library.Services;

/// <summary>
/// Service for interacting with external APIs to fetch FizzBuzz tokens
/// </summary>
public class ApiService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string API_URL = "https://pie-healthy-swift.glitch.me/word";
    
    /// <summary>
    /// Fetches tokens from the default API
    /// </summary>
    /// <param name="count">Number of tokens to fetch (default: 1)</param>
    /// <returns>A list of tokens from the API or an empty list if tokens couldn't be fetched</returns>
    public static async Task<List<FizzBuzzToken>> GetApiTokensAsync(int count = 1)
    {
        var tokens = new List<FizzBuzzToken>();
        
        // If count is 0 or negative, return empty list
        if (count <= 0)
        {
            Console.WriteLine("Returning empty token list due to token count <= 0");
            return tokens;
        }
        
        try
        {
            // Configure the HttpClient with appropriate headers
            ConfigureHttpClient();
            
            // Make multiple requests to get the desired number of tokens
            for (int i = 0; i < count && tokens.Count < count; i++)
            {
                try
                {
                    var token = await GetSingleApiTokenAsync();
                    if (token != null)
                    {
                        tokens.Add(token);
                    }
                }
                catch (NullReferenceException ex)
                {
                    // Specific handling for null reference exceptions
                    Console.WriteLine($"Null reference in API call: {ex.Message}");
                    // Continue trying to get more tokens
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during API request: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    }
                    // Continue trying to get more tokens
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching from API: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
        }
        
        // If we couldn't get any tokens from the API, return empty list
        if (tokens.Count == 0)
        {
            Console.WriteLine("Returning empty token list due to API failure");
        }
        
        return tokens;
    }
    
    /// <summary>
    /// Configures the HttpClient with appropriate headers
    /// </summary>
    private static void ConfigureHttpClient()
    {
        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "TwistedFizzBuzz/1.0");
        }
        
        if (!_httpClient.DefaultRequestHeaders.Contains("Accept"))
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
    }
    
    /// <summary>
    /// Fetches a single token from the API
    /// </summary>
    /// <returns>A FizzBuzzToken or null if the request failed</returns>
    private static async Task<FizzBuzzToken?> GetSingleApiTokenAsync()
    {
        // Make a direct HTTP request
        var response = await _httpClient.GetAsync(API_URL);
        
        // Check if the request was successful
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var wordApiResponse = await response.Content.ReadFromJsonAsync<WordApiResponse>();
                
                if (wordApiResponse != null)
                {
                    if (!string.IsNullOrEmpty(wordApiResponse.Word))
                    {
                        int divisor = wordApiResponse.Number;
                        
                        Console.WriteLine($"Successfully fetched from API: Word={wordApiResponse.Word}, Number={divisor}");
                        return new FizzBuzzToken(divisor, wordApiResponse.Word);
                    }
                    else
                    {
                        Console.WriteLine("API response contained null or empty word");
                    }
                }
                else
                {
                    Console.WriteLine("API response deserialization returned null");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing API response: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw; // Rethrow to be caught by the caller
            }
        }
        else
        {
            Console.WriteLine($"API request failed with status code: {response.StatusCode}");
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response content: {responseContent}");
        }
        
        return null;
    }
} 