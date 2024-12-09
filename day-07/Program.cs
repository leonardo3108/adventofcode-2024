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
2. Generate all possible combinations of operators for each equation.
3. Evaluate each equation with the generated combinations of operators.
4. Calculate the sum of the test values for the equations that can be solved with at least one combination of operators.
5. Print the total calibration result.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

class Program
{

    // Class to represent an equation    
    class Equation
    {
        // Test value of the equation
        public long TestValue { get; }
        // Numbers in the equation
        public int[] Numbers { get; }

        // List of available operators to apply to the numbers
        public List<char> AvailableOperators { get; }        

        // Combination of operators that can be applied to the numbers
        public char[]? CombinationOfOperators { get; set; }

        // Result of the equation, if the combination of operators is set
        public int Result { get; set; }

        // Constructor to initialize the equation
        public Equation(long testValue, int[] numbers, List<char> availableOperators)
        {
            TestValue = testValue;
            Numbers = numbers;
            AvailableOperators = availableOperators;
        }

        // Override ToString method to print the equation
        public override string ToString()
        {
            if (CombinationOfOperators == null)
                return "" + this.TestValue + " = " + string.Join(" ? ", Numbers) + " (no viable combination found)";
            else
            {
                string equationString = this.TestValue + " = " + this.Numbers[0] + " ";
                for (int i = 0; i < this.CombinationOfOperators.Length; i++)
                {
                    equationString += this.CombinationOfOperators[i] + " " + this.Numbers[i + 1] + " ";
                }
                return equationString + " = " + this.Result + " (one viable combination)    !!!!!";
            }
        }

        public void Evaluate()
        {
            // Check if the combination of operators is set. If not, return -1.
            if (CombinationOfOperators == null)
                return;
            // Position of the current number in the equation
            int position = 0;
            // Initialize the result with the first number
            int result = this.Numbers[position];
            // Iterate through each operator in the combination
            foreach (char op in this.CombinationOfOperators)
            {
                // Move to the next number position
                position++;
                // Apply the operator to the result and the next number. 
                // If the operator is addition, add the number to the result. 
                if (op == '+') // Add
                {
                    result += this.Numbers[position];
                }
                // If the operator is multiplication, multiply the number with the result.
                else if (op == '*') // Multiply
                {
                    result *= Numbers[position];
                }
            }
            // Set the result of the equation, if it matches the test value
            if (result == this.TestValue)
            {
                Result = result;
            }
        }
    }

    /// <summary>
    /// Parse the input data into a list of equations
    /// </summary>
    /// <param name="lines">Array of strings representing the input data</param>
    /// <param name="availableOperators">List of available operators</param>
    /// <returns>List of equations</returns>
    /// <remarks>
    /// Each line represents a single equation. The test value appears before the colon on each line, and the remaining numbers are separated by spaces.
    /// </remarks>
    static List<Equation> ParseEquations(string[] lines, List<char> availableOperators)
    {
        List<Equation> equations = new List<Equation>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            long testValue = long.Parse(parts[0]);
            int[] numbers = parts[1].Trim().Split(' ').Select(int.Parse).ToArray();
            equations.Add(new Equation(testValue, numbers, availableOperators));
        }
        return equations;
    }

    /// <summary>
    /// Generate all possible combinations of operators for a given length
    /// </summary>
    /// <param name="operatorCount">Number of operators to generate</param>
    /// <param name="availableOperators">List of available operators</param>
    /// <returns>List of all possible combinations of operators for the given number of operators</returns>
    static List<char[]> GenerateCombinations(int operatorCount, List<char> availableOperators)
    {
        // List to store all possible combinations of operators
        List<char[]> operatorCombinations = new List<char[]>();
        // Count the number of available operators
        int availableOperatorsCount = availableOperators.Count;
        // Calculate the total number of combinations
        int totalCombinations = (int)Math.Pow(availableOperatorsCount, operatorCount);
        // Iterate through each combination code, wich represents a unique combination of operators
        for (int combinationCode = 0; combinationCode < totalCombinations; combinationCode++)
        {
            // Convert the combination code to an array of operators
            char[] combination = new char[operatorCount];
            int remainingCombinationCode = combinationCode;
            // Generate the combination of operators for the current combination code
            for (int i = operatorCount - 1; i >= 0; i--)
            {
                // Get the index of the operator based on the last unit of the combination code
                int lastOperatorIndex = remainingCombinationCode % availableOperatorsCount;
                // Get the operator from the available operators list using the index
                combination[i] = availableOperators[lastOperatorIndex];
                // Update the remaining combination code by dividing it by the number of available operators, to get the next unit
                remainingCombinationCode /= availableOperatorsCount;
            }
            // Add the current combination of operators to the list
            operatorCombinations.Add(combination);
            //Console.WriteLine("\tCombination generated: " + string.Join(" ", combination));
        }
        // Return the list of all possible combinations of operators
        return operatorCombinations;
    }

    /// <summary>
    /// Calculate the sum of the test values for the equations that can be solved with at least one combination of operators
    /// </summary>
    /// <param name="equations">List of equations</param>
    /// <param name="operators">List of operators</param>
    /// <returns>Sum of the test values for the equations that can be solved with at least one combination of operators</returns>
    /// <remarks>
    /// The operators are always evaluated left-to-right, not according to precedence rules.
    /// </remarks>
    /// <remarks>
    /// The operators are add (+) and multiply (*).
    /// </remarks>
    static long CalculateTotalResultFromEquations(List<Equation> equations, List<char> operators)
    {
        // Variable to store the total calibration result
        long totalCalibrationResult = 0;
        // Iterate through each equation
        foreach (Equation equation in equations)
        {
            //Console.WriteLine(equation);

            // Generate all possible combinations of operators (addition and multiplication)
            List<char[]> operatorCombinations = GenerateCombinations(equation.Numbers.Length - 1, operators);
            // Iterate through each combination of operators
            foreach (char[] combination in operatorCombinations)
            {
                // Set the current combination of operators for the equation
                equation.CombinationOfOperators = combination;

                // Evaluate the equation with the current combination of operators
                equation.Evaluate();

                // Check if the result matches the test value
                if (equation.Result == equation.TestValue)
                {
                    // If the result matches, add the test value to the total calibration result
                    totalCalibrationResult += equation.TestValue;

                    // No need to check other combinations for this equation
                    break;
                }
                else
                {
                    // Reset the combination of operators for the equation
                    equation.CombinationOfOperators = null;
                }
            }
            // Print the equation with the result (if found)
            Console.WriteLine("\t" + equation + " (calibration result: " + totalCalibrationResult + ")");
        }
        return totalCalibrationResult;
    }

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

        // List of operators
        List<char> operators = new List<char> { '+', '*' };

        // Parse the data into a list of equations
        List<Equation> equations = ParseEquations(lines, operators);

        // Calculate the total calibration result
        long totalCalibrationResult = CalculateTotalResultFromEquations(equations, operators);

        // Print the total calibration result
        Console.WriteLine("Total Calibration Result: " + totalCalibrationResult);
    }
}