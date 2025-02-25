using TwistedFizzBuzz.Library;
using TwistedFizzBuzz.Library.Models;

namespace TwistedFizzBuzz.Custom;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Custom FizzBuzz Application");
        Console.WriteLine("==========================");
        Console.WriteLine("Rules:");
        Console.WriteLine("- Output values from -20 to 127");
        Console.WriteLine("- For multiples of 5 print \"Fizz\"");
        Console.WriteLine("- For multiples of 9 print \"Buzz\"");
        Console.WriteLine("- For multiples of 27 print \"Bar\"");
        Console.WriteLine("- For multiples where more than one rule applies, print concatenated tokens");
        Console.WriteLine();
        
        // Define custom tokens
        var customTokens = new List<FizzBuzzToken>
        {
            new(5, "Fizz"),
            new(9, "Buzz"),
            new(27, "Bar")
        };
        
        // Process the range from -20 to 127 using the TwistedFizzBuzz library
        Console.WriteLine("Processing numbers from -20 to 127:\n");
        var results = FizzBuzzProcessor.ProcessRange(-20, 127, customTokens);
        
        DisplayResults(results);
    }

    static void DisplayResults(List<string> results)
    {
        // Display results in rows of 10
        for (int i = 0; i < results.Count; i++)
        {
            Console.Write(results[i]);
            
            if (i < results.Count - 1)
            {
                Console.Write(", ");
                
                if ((i + 1) % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
        
        Console.WriteLine();
    }
}
