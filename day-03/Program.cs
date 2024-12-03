/* Problem Statement:

--- Day 3: Mull It Over ---
"Our computers are having issues, so I have no idea if we have any Chief Historians in stock! You're welcome to check the warehouse, though," says the mildly flustered shopkeeper at the North Pole Toboggan Rental Shop. The Historians head out to take a look.

The shopkeeper turns to you. "Any chance you can see why our computers are having issues again?"

The computer appears to be trying to run a program, but its memory (your puzzle input) is corrupted. All of the instructions have been jumbled up!

It seems like the goal of the program is just to multiply some numbers. It does that with instructions like mul(X,Y), where X and Y are each 1-3 digit numbers. For instance, mul(44,46) multiplies 44 by 46 to get a result of 2024. Similarly, mul(123,4) would multiply 123 by 4.

However, because the program's memory has been corrupted, there are also many invalid characters that should be ignored, even if they look like part of a mul instruction. Sequences like mul(4*, mul(6,9!, ?(12,34), or mul ( 2 , 4 ) do nothing.

For example, consider the following section of corrupted memory:

xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
Only the four highlighted sections are real mul instructions. Adding up the result of each instruction produces 161 (2*4 + 5*5 + 11*8 + 8*5).

Scan the corrupted memory for uncorrupted mul instructions. What do you get if you add up all of the results of the multiplications?

--- Part Two ---
As you scan through the corrupted memory, you notice that some of the conditional statements are also still intact. If you handle some of the uncorrupted conditional statements in the program, you might be able to get an even more accurate result.

There are two new instructions you'll need to handle:

The do() instruction enables future mul instructions.
The don't() instruction disables future mul instructions.
Only the most recent do() or don't() instruction applies. At the beginning of the program, mul instructions are enabled.

For example:

xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
This corrupted memory is similar to the example from before, but this time the mul(5,5) and mul(11,8) instructions are disabled because there is a don't() instruction before them. The other mul instructions function normally, including the one at the end that gets re-enabled by a do() instruction.

This time, the sum of the results is 48 (2*4 + 8*5).

Handle the new instructions; what do you get if you add up all of the results of just the enabled multiplications?
*/

/* Resume and resolution:
The problem described in the provided code selection is about analyzing corrupted memory from a computer program. 
The goal of the program is to multiply numbers using instructions like mul(X,Y), where X and Y are numbers. 
However, the memory is corrupted, and there are many invalid characters and sequences that should be ignored.

Key Points:
Valid Instructions: The valid instructions are in the format mul(X,Y), where X and Y are 1-3 digit numbers.
Invalid Characters: The memory contains invalid characters and sequences that should be ignored.

Example: Given a corrupted memory string: xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
Only the valid mul instructions should be considered:
mul(2,4) -> 2 * 4 = 8
mul(5,5) -> 5 * 5 = 25
mul(11,8) -> 11 * 8 = 88
mul(8,5) -> 8 * 5 = 40

Objective:
Scan the corrupted memory for valid mul instructions and add up the results of the multiplications. 
The example provided results in a total of 161 (24 + 25 + 118 + 85).

To solve this problem, you can follow these steps:
1. Initialize a variable to store the total sum of the multiplications.
2. Split the corrupted memory string into individual instructions.
3. Iterate through each instruction and extract the numbers X and Y.
4. Multiply X by Y and add the result to the total sum.
5. Return the total sum of the multiplications.

Part Two:

There are two new instructions introduced: do() and don't(). 
The do() instruction enables future mul instructions, while the don't() instruction disables them. 
Only the most recent do() or don't() instruction affects the subsequent mul instructions. 
Initially, all mul instructions are enabled.

An example is given to illustrate how these instructions work: xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
In this example, the mul(5,5) and mul(11,8) instructions are disabled due to the preceding don't() instruction. 
The other mul instructions are enabled, including the final mul(8,5) which is re-enabled by a do() instruction.

The sum of the results from the enabled multiplications in this example is 48, calculated as 2*4 + 8*5.
The task is to handle these new instructions and determine the sum of the results of the enabled multiplications in your own corrupted memory input.

To solve this problem, you can follow these steps:
1. Split the corrupted memory string into individual instructions.
2. For each mul instruction, check if it is enabled based on the last conditional flag (do() or don't()).
3. If the mul instruction is enabled, extract the numbers X and Y, multiply them, and add the result to the total sum.
4. Print the total sum of the multiplications.
*/

using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // string memory = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

        // get the memory string from input.txt file
        string memory = System.IO.File.ReadAllText("input.txt");

        // Console.WriteLine("Memory: " + memory);

        Regex multiplicationRegex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
        MatchCollection matches = multiplicationRegex.Matches(memory);

        // create a function to find the last do() or don't() instruction
        // based on the memory part before the current match
        // return false if the last is a don't() instruction, otherwise return true
        bool findLastDoOrDont(string memoryPart)
        {
            // Console.WriteLine("\t\tmemory: " + memoryPart);
            int lastDo = memoryPart.LastIndexOf("do()");
            int lastDont = memoryPart.LastIndexOf("don't()");
            // return True if the last is a do() instruction, otherwise return False
            return lastDo >= lastDont;
        }

        // create a function to process the multiplication instructions
        int ProcessMultiplicationInstructions(string memory, MatchCollection matches, bool conditional)
        {
            int totalSum = 0;
            foreach (Match match in matches)
            {
                // check if the instruction is enabled based on the conditional flag
                // if conditional is true, check the last do() or don't() instruction
                // if conditional is false, consider all instructions as enabled
                bool enabled = conditional? findLastDoOrDont(memory.Substring(0, match.Index)): true;

                if (enabled)
                {
                    // take the numbers from the match
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    totalSum += x * y;
                    // Console.WriteLine("\tinstruction: " + match.Value + " -> " + x + " * " + y + " = " + (x * y) + " (total: " + totalSum + ")");
                }
            }

            return totalSum;
        }
        Console.WriteLine("Part One - Sum of multiplications: " + ProcessMultiplicationInstructions(memory, matches, false));
        Console.WriteLine("Part Two - Sum of multiplications: " + ProcessMultiplicationInstructions(memory, matches, true));
    }
}