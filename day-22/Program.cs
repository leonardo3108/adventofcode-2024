﻿/*  Problem Description:

--- Day 22: Monkey Market ---
As you're all teleported deep into the jungle, a monkey steals The Historians' device! 
You'll need to get it back while The Historians are looking for the Chief.

The monkey that stole the device seems willing to trade it, but only in exchange for an absurd number of bananas. 
Your only option is to buy bananas on the Monkey Exchange Market.

You aren't sure how the Monkey Exchange Market works, but one of The Historians senses trouble and comes over to help. 
Apparently, they've been studying these monkeys for a while and have deciphered their secrets.

Today, the Market is full of monkeys buying good hiding spots. 
Fortunately, because of the time you recently spent in this jungle, you know lots of good hiding spots you can sell! 
If you sell enough hiding spots, you should be able to get enough bananas to buy the device back.

On the Market, the buyers seem to use random prices, but their prices are actually only pseudorandom! 
If you know the secret of how they pick their prices, you can wait for the perfect time to sell.

The part about secrets is literal, the Historian explains. 
Each buyer produces a pseudorandom sequence of secret numbers where each secret is derived from the previous.

In particular, each buyer's secret number evolves into the next secret number in the sequence via the following process:

Calculate the result of multiplying the secret number by 64. Then, mix this result into the secret number. 
Finally, prune the secret number.
Calculate the result of dividing the secret number by 32. Round the result down to the nearest integer. 
Then, mix this result into the secret number. Finally, prune the secret number.
Calculate the result of multiplying the secret number by 2048. Then, mix this result into the secret number. 
Finally, prune the secret number.
Each step of the above process involves mixing and pruning:

To mix a value into the secret number, calculate the bitwise XOR of the given value and the secret number. 
Then, the secret number becomes the result of that operation. 
(If the secret number is 42 and you were to mix 15 into the secret number, the secret number would become 37.)
To prune the secret number, calculate the value of the secret number modulo 16777216. 
Then, the secret number becomes the result of that operation. 
(If the secret number is 100000000 and you were to prune the secret number, the secret number would become 16113920.)
After this process completes, the buyer is left with the next secret number in the sequence. 
The buyer can repeat this process as many times as necessary to produce more secret numbers.

So, if a buyer had a secret number of 123, that buyer's next ten secret numbers would be:

15887950
16495136
527345
704524
1553684
12683156
11100544
12249484
7753432
5908254
Each buyer uses their own secret number when choosing their price, 
so it's important to be able to predict the sequence of secret numbers for each buyer. 
Fortunately, the Historian's research has uncovered the initial secret number of each buyer (your puzzle input). For example:

1
10
100
2024
This list describes the initial secret number of four different secret-hiding-spot-buyers on the Monkey Exchange Market. 
If you can simulate secret numbers from each buyer, you'll be able to predict all of their future prices.

In a single day, buyers each have time to generate 2000 new secret numbers. 
In this example, for each buyer, their initial secret number and the 2000th new secret number they would generate are:

1: 8685429
10: 4700978
100: 15273692
2024: 8667524
Adding up the 2000th new secret number for each buyer produces 37327623.

For each buyer, simulate the creation of 2000 new secret numbers. 
What is the sum of the 2000th secret number generated by each buyer?

Resume:
The problem is about simulating the creation of 2000 new secret numbers for each buyer
 and calculating the sum of the 2000th secret number generated by each buyer.
You are given the initial secret number of each buyer. 
The secret number evolves into the next secret number in the sequence via the following process:
1. Calculate the result of multiplying the secret number by 64. Then, mix this result into the secret number. Finally, prune the secret number.
2. Calculate the result of dividing the secret number by 32. Round the result down to the nearest integer. Then, mix this result into the secret number. Finally, prune the secret number.
3. Calculate the result of multiplying the secret number by 2048. Then, mix this result into the secret number. Finally, prune the secret number.
Each step of the above process involves mixing and pruning:
- To mix a value into the secret number, calculate the bitwise XOR of the given value and the secret number.
- To prune the secret number, calculate the value of the secret number modulo 16777216. 
After this process completes, the buyer is left with the next secret number in the sequence.
The problem is asking to calculate the sum of the 2000th secret number generated by each buyer.

Resolution steps:
1. Read the input file and parse the initial secret numbers of each buyer.
2. Create a function that simulates the creation of 2000 new secret numbers for each buyer.
3. Implement the simulation process for each buyer.
4. Calculate the sum of the 2000th secret number generated by each buyer.
5. Print the sum of the 2000th secret number generated by each buyer.

--- Part Two ---
Of course, the secret numbers aren't the prices each buyer is offering! That would be ridiculous. 
Instead, the prices the buyer offers are just the ones digit of each of their secret numbers.

So, if a buyer starts with a secret number of 123, that buyer's first ten prices would be:

3 (from 123)
0 (from 15887950)
6 (from 16495136)
5 (etc.)
4
4
6
4
4
2
This price is the number of bananas that buyer is offering in exchange for your information about a new hiding spot. 
However, you still don't speak monkey, so you can't negotiate with the buyers directly. 
The Historian speaks a little, but not enough to negotiate; instead, he can ask another monkey to negotiate on your behalf.

Unfortunately, the monkey only knows how to decide when to sell by looking at the changes in price. 
Specifically, the monkey will only look for a specific sequence of four consecutive changes in price, then immediately sell when it sees that sequence.

So, if a buyer starts with a secret number of 123, that buyer's first ten secret numbers, prices, and the associated changes would be:

     123: 3 
15887950: 0 (-3)
16495136: 6 (6)
  527345: 5 (-1)
  704524: 4 (-1)
 1553684: 4 (0)
12683156: 6 (2)
11100544: 4 (-2)
12249484: 4 (0)
 7753432: 2 (-2)
Note that the first price has no associated change because there was no previous price to compare it with.

In this short example, within just these first few prices, the highest price will be 6, 
so it would be nice to give the monkey instructions that would make it sell at that time. 
The first 6 occurs after only two changes, so there's no way to instruct the monkey to sell then, 
but the second 6 occurs after the changes -1,-1,0,2. 
So, if you gave the monkey that sequence of changes, it would wait until the first time it sees that sequence 
and then immediately sell your hiding spot information at the current price, winning you 6 bananas.

Each buyer only wants to buy one hiding spot, so after the hiding spot is sold, the monkey will move on to the next buyer. 
If the monkey never hears that sequence of price changes from a buyer, the monkey will never sell, and will instead just move on to the next buyer.

Worse, you can only give the monkey a single sequence of four price changes to look for. You can't change the sequence between buyers.

You're going to need as many bananas as possible, 
so you'll need to determine which sequence of four price changes will cause the monkey to get you the most bananas overall. 
Each buyer is going to generate 2000 secret numbers after their initial secret number, 
so, for each buyer, you'll have 2000 price changes in which your sequence can occur.

Suppose the initial secret number of each buyer is:

1
2
3
2024
There are many sequences of four price changes you could tell the monkey, 
but for these four buyers, the sequence that will get you the most bananas is -2,1,-1,3. 
Using that sequence, the monkey will make the following sales:

For the buyer with an initial secret number of 1, changes -2,1,-1,3 first occur when the price is 7.
For the buyer with initial secret 2, changes -2,1,-1,3 first occur when the price is 7.
For the buyer with initial secret 3, the change sequence -2,1,-1,3 does not occur in the first 2000 changes.
For the buyer starting with 2024, changes -2,1,-1,3 first occur when the price is 9.
So, by asking the monkey to sell the first time each buyer's prices go down 2, then up 1, then down 1, then up 3, you would get 23 (7 + 7 + 9) bananas!

Figure out the best sequence to tell the monkey 
so that by looking for that same sequence of changes in every buyer's future prices, you get the most bananas in total. 
What is the most bananas you can get?

Resume:
The problem is about finding the best sequence of four price changes to tell the monkey
    so that by looking for that same sequence of changes in every buyer's future prices, you get the most bananas in total.
Each buyer is going to generate 2000 secret numbers after their initial secret number.
The price is the number of bananas that buyer is offering in exchange for your information about a new hiding spot.
The monkey will only look for a specific sequence of four consecutive changes in price, then immediately sell when it sees that sequence.
The problem is asking to determine which sequence of four price changes will cause the monkey to get you the most bananas overall.

Resolution steps:
1. Read the input file and parse the initial secret numbers of each buyer.
2. Create a function that simulates the creation of 2000 new secret numbers for each buyer.
3. Implement the simulation process for each buyer.
4. Calculate the price of each buyer based on the secret number.
5. Calculate the changes in price for each buyer.
6. Store all the four price changes sequences for each buyer together with the price (bananas) when the sequence first occurs.
7. For each sequence, calculate the total bananas you can get adding the bananas of each buyer when the sequence first occurs.
8. Calculate the most bananas you can get, with the best sequence of four price changes to tell the monkey.
8. Print the quantity of bananas you can get.
*/

using System.Text;

class Program
{
    static void Main()
    {
        // First sample: from a initial secret number of 123, show the 10 new secret numbers, prices, changes, and pairs of sequences and bananas.
        Console.WriteLine("First sample: ");
        ProcessSecretNumber(123, 10, 3);
        Console.WriteLine();

        // Second sample: from the initial secret numbers of 1, 10, 100, and 2024, calculate the sum of the 2000th secret number generated by each buyer.
        Console.WriteLine("Second sample:");
        string[] lines = { "1", "10", "100", "2024" };
        int numberOfIterations = 2000;
        (long sum, _) = ProcessSecretNumbers(lines, numberOfIterations, 1);
        Console.WriteLine("Sum of the " + numberOfIterations + "th secret number generated by each buyer: " + sum + ".\n");

        // Third sample: from the initial secret numbers of 1, 2, 3, and 2024, calculate the most bananas you can get.
        Console.WriteLine("Third sample:");
        lines = new string[] { "1", "2", "3", "2024" };
        numberOfIterations = 2000;
        (_, long bananas) = ProcessSecretNumbers(lines, numberOfIterations, 2);
        Console.WriteLine("The most bananas: " + bananas + ".");
        Console.WriteLine();
        
        // Read the secret numbers of each buyer from the input file and calculate:
        // the sum of the 2000th secret number generated by each buyer, and the most bananas you can get.
        Console.WriteLine("Input from file:");
        lines = File.ReadAllLines("input.txt");
        (sum, bananas) = ProcessSecretNumbers(lines, numberOfIterations, 0);
        Console.WriteLine("Sum of the " + numberOfIterations + "th secret number generated by each buyer: " + sum + ".");
        Console.WriteLine("Quantity of bananas: " + bananas + ".");
    }

    /// <summary>
    /// Process the secret numbers of each buyer and calculate 
    /// the sum of the numberOfIterations'th secret number generated by each buyer, and the most bananas you can get.
    /// </summary>
    /// <param name="lines">Lines with the initial secret numbers of each buyer.</param>
    /// <param name="numberOfIterations">Number of iterations to generate new secret numbers.</param>
    /// <param name="debugLevel">Debug level: 0 - no debug, 1 - show secret numbers, 2 - show best sequence, 3 - show iterations.</param>
    /// <returns>A tuple with the sum of the numberOfIterations'th secret number generated by each buyer, and the most bananas you can get.</returns>
    private static (long, long) ProcessSecretNumbers(string[] lines, int numberOfIterations, int debugLevel)
    {
        // Parse the initial secret numbers of each buyer.
        int[] initialSecretNumbers = lines.Select(int.Parse).ToArray();
        // Structures to store the sum of the secret numbers and the sequences of changes for each buyer.
        long sum = 0;
        Dictionary<(int, int, int, int), int> individualSequences, overallSequences = new Dictionary<(int, int, int, int), int>();
        Dictionary<(int, int, int, int), int>[] allIndividualSequences = new Dictionary<(int, int, int, int), int>[initialSecretNumbers.Length];
        // iterate over the buyers, getting the initial secret number for each buyer
        int index = 0;
        foreach (int initialSecretNumber in initialSecretNumbers)
        {
            // Generate the new secret number and obtain the pairs of sequences of 4 changes of price and the price after the sequence first occurs.
            (long newSecretNumber, individualSequences) = ProcessSecretNumber(initialSecretNumber, numberOfIterations, debugLevel);
            // Store the sequences of changes for each buyer. Used to debug the best sequence.
            allIndividualSequences[index++] = individualSequences;
            // iterate over the sequences of changes for each buyer
            foreach (var sequence in individualSequences)
            {
                // Acumulate the quantity of bananas for each sequence of changes into a structure that stores the overall sequences of changes.
                if (overallSequences.ContainsKey(sequence.Key))
                    overallSequences[sequence.Key] += sequence.Value;
                else
                    overallSequences[sequence.Key] = sequence.Value;
            }
            if (debugLevel == 1) Console.WriteLine(initialSecretNumber + ": " + newSecretNumber);
            // Calculate the sum of the numberOfIterations'th secret number generated by each buyer.
            sum += newSecretNumber;
        }

        // Find the best sequence of changes to tell the monkey.
        long maxBananas = 0;
        var bestSequence = (0, 0, 0, 0);
        // iterate over the overall sequences of changes, to find the sequence that will get the most bananas.
        foreach (var sequence in overallSequences)
            if (sequence.Value > maxBananas)
            {
                bestSequence = sequence.Key;
                maxBananas = sequence.Value;
            }

        // For debug level 2, print the best sequence of changes and the price when the sequence first occurs for each buyer.
        if (debugLevel == 2) {
            string bestSequenceStr = bestSequence.Item1 + "," + bestSequence.Item2 + "," + bestSequence.Item3 + "," + bestSequence.Item4;
            Console.WriteLine("Sequence that will get the most bananas: " + bestSequenceStr);
            index = 0;
            foreach (var eachIndividualSequences in allIndividualSequences)
            {
                Console.Write("\tFor the buyer with an initial secret number of " + initialSecretNumbers[index++] + ", changes ");
                if (eachIndividualSequences.ContainsKey(bestSequence))
                    Console.WriteLine(bestSequenceStr + " first occur when the price is " + eachIndividualSequences[bestSequence] + ".");
                else
                    Console.WriteLine(bestSequenceStr + " does not occur in the first " + numberOfIterations + " changes.");
            }
        }
        // Return the sum of the numberOfIterations'th secret number generated by each buyer, and the most bananas you can get.
        return (sum, maxBananas);
    }

    /// <summary>
    /// Process the secret number and simulate the creation of secret numbers for a buyer.
    /// </summary>
    /// <param name="secretNumber">Initial secret number of the buyer.</param>
    /// <param name="iterations">Number of iterations to generate new secret numbers.</param>
    /// <param name="debugLevel">Debug level: 0 - no debug, 1 - show secret numbers, 2 - show best sequence, 3 - show iterations.</param>
    /// <returns>A tuple with the new secret number and a dictionary with 
    /// the sequences of 4 changes of prices, and the price after the sequence first occurs.</returns> 
    static (long, Dictionary<(int, int, int, int), int>) ProcessSecretNumber(long secretNumber, int iterations, int debugLevel = 0)
    {
        // Store the changes of price and the prices for all iterations.
        int[] changes = new int[iterations];
        int price = Convert.ToInt32(secretNumber % 10);
        int lastPrice = 0;
        // Store the sequences of 4 changes of prices, and the price after the sequence first occurs.   
        Dictionary<(int, int, int, int), int> sequences = new Dictionary<(int, int, int, int), int>();

        // For debug level 3, prepare reports with the secret numbers, prices, changes, and sequences.
        StringBuilder secretNumbersStr = new StringBuilder("\tSecret numbers: ").Append(secretNumber);
        StringBuilder pricesStr = new StringBuilder("\tPrices: ").Append(price);
        StringBuilder changesStr = new StringBuilder("\tChanges: ");
        StringBuilder sequencesStr = new StringBuilder("\tSequences: ");

        // Iterate to generate the new secret numbers and calculate the changes of prices.
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            // First step: Calculate the result of multiplying the secret number by 64, mix this result into the secret numbe, and prune.
            long result = secretNumber * 64L;
            secretNumber ^= result;
            secretNumber = secretNumber % 16777216L;            

            // Second step: Calculate the result of dividing the secret number by 32, mix this result into the secret number, and prune.
            result = Convert.ToInt64(Math.Floor(secretNumber / 32.0));
            secretNumber ^= result;
            secretNumber = secretNumber % 16777216L;

            // Third step: Calculate the result of multiplying the secret number by 2048, mix this result into the secret number, and prune.
            result = secretNumber * 2048L;
            secretNumber ^= result;
            secretNumber = secretNumber % 16777216L;

            // Calculate the price and the change of price.
            lastPrice = price;
            price = Convert.ToInt32(secretNumber % 10);
            changes[iteration] = price - lastPrice;

            // After the 3rd iteration...
            if (iteration >= 3)
            {
                //... get the last 4 changes of prices.
                (int, int, int, int) sequence = (changes[iteration - 3], changes[iteration - 2], changes[iteration - 1], changes[iteration]);
                // Is the first time this sequence occurs for this buyer?
                if (!sequences.ContainsKey(sequence))
                {
                    // Store the price when the sequence first occurs.
                    sequences[sequence] = price;

                    // For debug level 3, prepare a report with the sequence of changes and the price
                    sequencesStr.Append(changes[iteration - 3]).Append(",").Append(changes[iteration - 2]).Append(",");
                    sequencesStr.Append(changes[iteration - 1]).Append(",").Append(changes[iteration]).Append(" > ").Append(price).Append(", ");
                }
            }

            // For debug level 3, update the reports with the secret numbers, prices, and changes.
            secretNumbersStr.Append(" > ").Append(secretNumber);
            pricesStr.Append(" > ");
            pricesStr.Append(price);
            changesStr.Append(changes[iteration]).Append(", ");
        }
        // For debug level 3, print the reports with the secret numbers, prices, changes, and sequences.
        if (debugLevel == 3)
        {
            Console.WriteLine(secretNumbersStr.Append('.'));
            Console.WriteLine(pricesStr.Append('.'));
            Console.WriteLine(changesStr.Remove(changesStr.Length - 2, 2).Append('.'));
            Console.WriteLine(sequencesStr.Remove(sequencesStr.Length - 2, 2).Append('.'));
        }
        // Return the new secret number, and dictionary with the sequences of 4 changes of prices and the price after the sequence first occurs.
        return (secretNumber, sequences);
    }
}