/*
--- Day 7: Bridge Repair ---
The Historians take you to a familiar rope bridge over a river in the middle of a jungle. The Chief isn't on this side of the bridge, though; maybe he's on the other side?

When you go to cross the bridge, you notice a group of engineers trying to repair it. (Apparently, it breaks pretty frequently.) You won't be able to cross until it's fixed.

You ask how long it'll take; the engineers tell you that it only needs final calibrations, but some young elephants were playing nearby and stole all the operators from their calibration equations! They could finish the calibrations if only someone could determine which test values could possibly be produced by placing any combination of operators into their calibration equations (your puzzle input).

For example:

190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
Each line represents a single equation. The test value appears before the colon on each line; it is your job to determine whether the remaining numbers can be combined with operators to produce the test value.

Operators are always evaluated left-to-right, not according to precedence rules. Furthermore, numbers in the equations cannot be rearranged. Glancing into the jungle, you can see elephants holding two different types of operators: add (+) and multiply (*).

Only three of the above equations can be made true by inserting operators:

190: 10 19 has only one position that accepts an operator: between 10 and 19. Choosing + would give 29, but choosing * would give the test value (10 * 19 = 190).
3267: 81 40 27 has two positions for operators. Of the four possible configurations of the operators, two cause the right side to match the test value: 81 + 40 * 27 and 81 * 40 + 27 both equal 3267 (when evaluated left-to-right)!
292: 11 6 16 20 can be solved in exactly one way: 11 + 6 * 16 + 20.
The engineers just need the total calibration result, which is the sum of the test values from just the equations that could possibly be true. In the above example, the sum of the test values for the three equations listed above is 3749.

Determine which equations could possibly be true. What is their total calibration result?

Resume and resolution:

The problem is about finding the possible combinations of operators that can be applied to a list of numbers to get a specific result. 
The operators are always evaluated left-to-right, and the numbers in the equations cannot be rearranged. The operators are add (+) and multiply (*).

Resolution Steps:
1. Parse the input data into a list of equations.
2. For each equation, and each number in the equation, try every possible operator to get the partial results.
3. Remove results above the test value, and continue to the next number in the equation.
3. At the end of the equation, check if there is a result that matches the test value. If so, the equation is valid.
4. Calculate the sum of the test values for the valid equations.
5. Print the total calibration result.


--- Part Two ---
The engineers seem concerned; the total calibration result you gave them is nowhere close to being within safety tolerances. Just then, you spot your mistake: some well-hidden elephants are holding a third type of operator.

The concatenation operator (||) combines the digits from its left and right inputs into a single number. For example, 12 || 345 would become 12345. All operators are still evaluated left-to-right.

Now, apart from the three equations that could be made true using only addition and multiplication, the above example has three more equations that can be made true by inserting operators:

156: 15 6 can be made true through a single concatenation: 15 || 6 = 156.
7290: 6 8 6 15 can be made true using 6 * 8 || 6 * 15.
192: 17 8 14 can be made true using 17 || 8 + 14.
Adding up all six test values (the three that could be made before using only + and * plus the new three that can now be made by also using ||) produces the new total calibration result of 11387.

Using your new knowledge of elephant hiding spots, determine which equations could possibly be true. What is their total calibration result?

Resume and resolution:

The problem is an extension of the previous one, with the addition of a new operator, the concatenation operator (||).
The concatenation operator combines the digits from its left and right inputs into a single number. All operators are still evaluated left-to-right.

Resolution:
In addition to the previous steps, we need to add the concatenation operator to the possible operations.
When adding the concatenation operator, we need to consider the number of digits in the left and right inputs to calculate the new number.
We had refactored the code to extract the calculation of the calibration result for an equation into a separate method.
We added a flag to indicate if the concatenation operator should be used.
Then, we could calculate the calibration result for the two parts of the problem by calling the method with the appropriate flag.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

class Program
{
    static void Main()
    {
        // Sample input data
        string[] lines = new string[]
        {
            "190: 10 19",
            "3267: 81 40 27",
            "83: 17 5",
            "156: 15 6",
            "7290: 6 8 6 15",
            "161011: 16 10 13",
            "192: 17 8 14",
            "21037: 9 7 18 13",
            "292: 11 6 16 20"
        };

        // Read the input file
        lines = File.ReadAllLines("input.txt");

        // Initialize the calibration result, which is the sum of the test values for the valid equations, to 0. 
        // There are two calibration results, one for part one and one for part two.
        long calibrationResultOne = 0;
        long calibrationResultTwo = 0;

        // Parse each line of the input data to get the test value and the numbers
        foreach (var line in lines)
        {
            // Split the line into the test value and the numbers
            string[] parts = line.Split(": ");
            // First part is the test value
            long testValue = long.Parse(parts[0]);
            // Second part is the list of numbers. Split the numbers and convert them to an array of integers
            int[] numbers = parts[1].Split(" ").Select(int.Parse).ToArray();

            // Calculate the calibration result for the equation and add it to the total calibration result
            calibrationResultOne += CalibrationResultForEquation(testValue, numbers, false);
            calibrationResultTwo += CalibrationResultForEquation(testValue, numbers, true);
        }

        // Print the total calibration results
        Console.WriteLine($"Total calibration result - Part One: {calibrationResultOne}");
        Console.WriteLine($"Total calibration result - Part Two: {calibrationResultTwo}");
    }

    /// <summary>
    /// Calculate the calibration result for an equation with a test value and a list of numbers.
    /// </summary>
    /// <param name="testValue">The test value to check if the equation is valid.</param>
    /// <param name="numbers">The list of numbers in the equation.</param>
    /// <param name="useConcatenation">Flag to indicate if the concatenation operator should be used.</param>
    /// <returns>The test value if the equation is valid, 0 otherwise.</returns>
    private static long CalibrationResultForEquation(long testValue, int[] numbers, bool useConcatenation = false)
    {
        // Calculate all possible results for the equation, iterating over the numbers and finding all possible partial results.
        HashSet<long> results = new HashSet<long>();
        // Initialize the results with the first number. No operation so far.
        results.Add(numbers[0]);

        foreach (var number in numbers.Skip(1))
        {
            // Create a new set to store the partial results for the current number
            HashSet<long> newResults = new HashSet<long>();
            foreach (var result in results)
            {
                // Try to add the number to the result. If overflows, skip.
                if (result + number <= testValue)
                    newResults.Add(result + number);
                // Try to multiply the number with the result. If overflows, skip.
                if (result * number <= testValue)
                    newResults.Add(result * number);
                // Try to concatenate the number with the result. If overflows, skip.
                if (useConcatenation 
                        && long.TryParse(result.ToString() + number.ToString(), out long concatenatedResult) 
                        && concatenatedResult <= testValue)
                    newResults.Add(concatenatedResult);
            }
            // Update the results with the new partial results
            results = newResults;
        }
        // If the equation is valid, i.e., if there is one result that matches the test value, return the test value. Otherwise, return 0.
        return (results.Contains(testValue)) ? testValue : 0;
    }
}