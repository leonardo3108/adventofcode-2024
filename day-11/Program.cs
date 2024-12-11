/*  Problem Description:

--- Day 11: Plutonian Pebbles ---
The ancient civilization on Pluto was known for its ability to manipulate spacetime, and while The Historians explore their infinite corridors, you've noticed a strange set of physics-defying stones.

At first glance, they seem like normal stones: they're arranged in a perfectly straight line, and each stone has a number engraved on it.

The strange part is that every time you blink, the stones change.

Sometimes, the number engraved on a stone changes. Other times, a stone might split in two, causing all the other stones to shift over a bit to make room in their perfectly straight line.

As you observe them for a while, you find that the stones have a consistent behavior. Every time you blink, the stones each simultaneously change according to the first applicable rule in this list:

If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
If the stone is engraved with a number that has an even number of digits, it is replaced by two stones. The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.
No matter how the stones change, their order is preserved, and they stay on their perfectly straight line.

How will the stones evolve if you keep blinking at them? You take a note of the number engraved on each stone in the line (your puzzle input).

If you have an arrangement of five stones engraved with the numbers 0 1 10 99 999 and you blink once, the stones transform as follows:

The first stone, 0, becomes a stone marked 1.
The second stone, 1, is multiplied by 2024 to become 2024.
The third stone, 10, is split into a stone marked 1 followed by a stone marked 0.
The fourth stone, 99, is split into two stones marked 9.
The fifth stone, 999, is replaced by a stone marked 2021976.
So, after blinking once, your five stones would become an arrangement of seven stones engraved with the numbers 1 2024 1 0 9 9 2021976.

Here is a longer example:

Initial arrangement:
125 17

After 1 blink:
253000 1 7

After 2 blinks:
253 0 2024 14168

After 3 blinks:
512072 1 20 24 28676032

After 4 blinks:
512 72 2024 2 0 2 4 2867 6032

After 5 blinks:
1036288 7 2 20 24 4048 1 4048 8096 28 67 60 32

After 6 blinks:
2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2
In this example, after blinking six times, you would have 22 stones. After blinking 25 times, you would have 55312 stones!

Consider the arrangement of stones in front of you. How many stones will you have after blinking 25 times?

Resume and resolution:

The problem is simple, we need to iterate over the stones and apply the rules to each stone. 
The rules are simple:
- if the number is 0, we replace it with 1, 
- if the number has an even number of digits, we split it into two stones, and 
- if none of the rules apply, we multiply the number by 2024.

Resolution steps:
1. Read the input file and store the stones in a list.
2. Iterate over the stones and apply the rules.
3. Print the number of stones after 25 blinks.


--- Part Two ---
The Historians sure are taking a long time. To be fair, the infinite corridors are very large.

How many stones would you have after blinking a total of 75 times?

Resume and resolution:
The problem is the same as the first part, but we need to iterate 75 times instead of 25.

Reasoning:
Unfortunately, the number of stones grows exponentially, and we can't iterate 75 times. 
We need to find a way to optimize the solution.
There is no interaction between the stones, so each stone can be processed independently.
We can store in a dictionary what is the result of processing a given number for a stone after a given number of blinks.
So, we can reuse it when we find the same number again.

Resolution steps:
1. Create a dictionary to store the results of processing a number for a stone after a given number of blinks.
2. Read the input file and store the stones in a list.
3. Iterate over the stones. For each stone, verify if we have already processed it for the current number of blinks.
4. If we have, we can use the result stored in the dictionary. 
5. If not, we process the stone, and store the result in the dictionary.
6. Print the number of stones after 75 blinks.
*/

using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Sample input
        string input = "125 17";

        // Read the input from a file
        input = File.ReadAllText("input.txt");

        // Store the stones in a list.
        long[] stones = input.Split(' ').Select(long.Parse).ToArray();
        Console.WriteLine("Initial arrangement: " + string.Join(" ", stones) + ".");

        // Dictionary to store the results of processing a number for a stone after a given number of blinks.
        Dictionary<(long, int), long> processed = new Dictionary<(long, int), long>();

        // Process the stones for 25 and 75 blinks.
        Console.WriteLine("After 25 blinks: " + processStones(stones, 25, processed) + ".");
        Console.WriteLine("After 75 blinks: " + processStones(stones, 75, processed) + ".");
    }

    /// <summary>
    /// Process various stones for the given number of blinks.
    /// </summary>
    /// <param name="stones">List of numbers engraved on the stones.</param>
    /// <param name="blinks">Number of blinks to process.</param>
    /// <param name="processed">Dictionary to store the results of processing a number for a stone after a given number of blinks.</param>
    /// <returns>Number of stones after the given number of blinks.</returns>
    private static long processStones(long[] stones, int blinks, Dictionary<(long, int), long> processed)
    {
        // result stores the number of stones after the given number of blinks.
        long result = 0;
        // Process each stone.
        foreach (long stone in stones)
            // Acumulate the number of stones after processing the current stone.
            result += processStone(stone, blinks, processed);
        // Return the number of stones after the given number of blinks.
        return result;
    }

    /// <summary>
    /// Process one stone for the given number of blinks.
    /// </summary>
    /// <param name="stone">Number engraved on the stone.</param>
    /// <param name="blinks">Number of blinks to process.</param>
    /// <param name="processed">Dictionary to store the results of processing a number for a stone after a given number of blinks.</param>
    /// <returns>Number of stones after the given number of blinks.</returns>
    private static long processStone(long stone, int blinks, Dictionary<(long, int), long> processed)
    {
        // result stores the number of stones after the given number of blinks.
        long result = 0;
        // Check if we have already processed the current stone for the given number of blinks.
        if (processed.TryGetValue((stone, blinks), out result))
            // If we have, just return the stored result.
            return result;
        // If we haven't, we need to process the stone, store the result in the dictionary, and return it.
        // For zero blinks, we have only the original stone.
        if (blinks == 0) 
            result = 1;
        // Rules for processing the stone. Rule 1: if the stone is 0, it is replaced by 1.
        else if (stone == 0)
            result = processStone(1, blinks - 1, processed);
        else 
        {
            // Rule 2: if the stone has an even number of digits, it is replaced by two stones.
            int digits = stone.ToString().Length;
            if (digits % 2 == 0)
            {
                // The left half of the digits are engraved on the new left stone.
                long left = long.Parse(stone.ToString().Substring(0, digits / 2));
                // The right half of the digits are engraved on the new right stone.
                long right = long.Parse(stone.ToString().Substring(digits / 2));
                // Process the left and right stones and sum the resulting numbers of stones.
                result = processStone(left, blinks - 1, processed) + processStone(right, blinks - 1, processed);
            }
            // Rule 3: if none of the other rules apply, the stone is replaced by a new stone
            else
                // the old stone's number multiplied by 2024 is engraved on the new stone.
                result = processStone(stone * 2024, blinks - 1, processed);
        }
        // Store the result in the dictionary and return it.
        processed[(stone, blinks)] = result;
        return result;
    }
}
