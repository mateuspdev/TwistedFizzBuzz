# TwistedFizzBuzz

A C# library that implements the classic FizzBuzz problem with a twist, allowing for customizable ranges, tokens, and divisors.

## Features

- Process a range of numbers with standard FizzBuzz rules (divisible by 3 = "Fizz", divisible by 5 = "Buzz")
- Process a range of numbers with custom tokens and divisors
- Process a set of non-sequential numbers with standard FizzBuzz rules
- Process a set of non-sequential numbers with custom tokens and divisors
- Process numbers using tokens fetched from an API
- Ability to specify how many tokens to fetch from the API

## Project Structure

- **TwistedFizzBuzz.Library**: The core library containing the FizzBuzz implementation
  - **Models**: Contains data models like FizzBuzzToken and WordApiResponse
  - **Services**: Contains service classes like ApiService for external API interactions
  - **FizzBuzzProcessor.cs**: Main class with the core FizzBuzz logic
- **TwistedFizzBuzz.Simple**: A console application demonstrating the standard FizzBuzz problem (1-100)
- **TwistedFizzBuzz.Custom**: A console application demonstrating a custom FizzBuzz implementation
- **TwistedFizzBuzz.Tests**: Unit tests for the library

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later

### Building the Solution

```bash
dotnet build
```

### Running the Console Applications

#### TwistedFizzBuzz.Simple

A simple console application that demonstrates the standard FizzBuzz problem using the TwistedFizzBuzz library.

This application processes numbers from 1 to 100 according to the standard FizzBuzz rules:

- If a number is divisible by 3, output "Fizz"
- If a number is divisible by 5, output "Buzz"
- If a number is divisible by both 3 and 5, output "FizzBuzz"
- Otherwise, output the number itself

To run:

```bash
dotnet run --project TwistedFizzBuzz.Simple
```

#### TwistedFizzBuzz.Custom

A console application that demonstrates a custom FizzBuzz implementation using the TwistedFizzBuzz library.

This application processes numbers from -20 to 127 with the following rules:

- For multiples of 5 print "Fizz"
- For multiples of 9 print "Buzz"
- For multiples of 27 print "Bar"
- For multiples where more than one rule applies, print concatenated tokens

To run:

```bash
dotnet run --project TwistedFizzBuzz.Custom
```

### Running the Tests

```bash
dotnet test
```

## Library Usage Examples

### Processing a Range with Standard Rules

```csharp
// Process numbers from 1 to 15 with standard FizzBuzz rules
var results = FizzBuzzProcessor.ProcessRange(1, 15);
```

### Processing a Range with Custom Tokens

```csharp
// Define custom tokens and divisors
var tokens = new List<FizzBuzzToken>
{
    new(7, "Poem"),
    new(17, "Writer"),
    new(3, "College")
};

// Process numbers from 1 to 100 with custom tokens
var results = FizzBuzzProcessor.ProcessRange(1, 100, tokens);
```

### Processing Non-Sequential Numbers

```csharp
// Process a set of non-sequential numbers
int[] numbers = { -5, 6, 300, 12, 15 };
var results = FizzBuzzProcessor.ProcessNumbers(numbers);
```

### Processing Non-Sequential Numbers with Custom Tokens

```csharp
// Define custom tokens and divisors
var tokens = new List<FizzBuzzToken>
{
    new(7, "Poem"),
    new(17, "Writer"),
    new(3, "College")
};

// Process a set of non-sequential numbers with custom tokens
int[] numbers = { -5, 6, 300, 12, 15 };
var results = FizzBuzzProcessor.ProcessNumbers(numbers, tokens);
```

### Using API-Generated Tokens

```csharp
// Process numbers from 1 to 20 with a single token from the API
var results = await FizzBuzzProcessor.ProcessRangeWithApiTokenAsync(1, 20);

// Process numbers from 1 to 20 with multiple tokens from the API
var resultsWithMultipleTokens = await FizzBuzzProcessor.ProcessRangeWithApiTokenAsync(1, 20, tokenCount: 3);
```

## API Integration

The library supports integration with an external API to generate FizzBuzz tokens. If the API is unavailable, the library will fall back to the standard FizzBuzz tokens (Fizz/Buzz).

The library uses the following API:

- Primary API: https://pie-healthy-swift.glitch.me/word

The API returns a word and a number, which are used to create a FizzBuzz token.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
