using TwistedFizzBuzz.Library;

namespace TwistedFizzBuzz.Simple;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("FizzBuzz Simple Console Application");
        Console.WriteLine("===================================");
        Console.WriteLine("This application demonstrates the standard FizzBuzz problem using the TwistedFizzBuzz library.");
        Console.WriteLine();
        
        // Process the range using the TwistedFizzBuzz library
        Console.WriteLine("Processing numbers from 1 to 100:\n");
        var results = FizzBuzzProcessor.ProcessRange(1, 100);
        
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
