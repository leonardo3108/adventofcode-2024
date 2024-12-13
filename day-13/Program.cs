/*  Problem Description:

--- Day 13: Claw Contraption ---
Next up: the lobby of a resort on a tropical island. The Historians take a moment to admire the hexagonal floor tiles before spreading out.

Fortunately, it looks like the resort has a new arcade! Maybe you can win some prizes from the claw machines?

The claw machines here are a little unusual. Instead of a joystick or directional buttons to control the claw, these machines have two buttons labeled A and B. Worse, you can't just put in a token and play; it costs 3 tokens to push the A button and 1 token to push the B button.

With a little experimentation, you figure out that each machine's buttons are configured to move the claw a specific amount to the right (along the X axis) and a specific amount forward (along the Y axis) each time that button is pressed.

Each machine contains one prize; to win the prize, the claw must be positioned exactly above the prize on both the X and Y axes.

You wonder: what is the smallest number of tokens you would have to spend to win as many prizes as possible? You assemble a list of every machine's button behavior and prize location (your puzzle input). For example:

Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=7870, Y=6450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=18641, Y=10279
This list describes the button configuration and prize location of four different claw machines.

For now, consider just the first claw machine in the list:

Pushing the machine's A button would move the claw 94 units along the X axis and 34 units along the Y axis.
Pushing the B button would move the claw 22 units along the X axis and 67 units along the Y axis.
The prize is located at X=8400, Y=5400; this means that from the claw's initial position, it would need to move exactly 8400 units along the X axis and exactly 5400 units along the Y axis to be perfectly aligned with the prize in this machine.
The cheapest way to win the prize is by pushing the A button 80 times and the B button 40 times. This would line up the claw along the X axis (because 80*94 + 40*22 = 8400) and along the Y axis (because 80*34 + 40*67 = 5400). Doing this would cost 80*3 tokens for the A presses and 40*1 for the B presses, a total of 280 tokens.

For the second and fourth claw machines, there is no combination of A and B presses that will ever win a prize.

For the third claw machine, the cheapest way to win the prize is by pushing the A button 38 times and the B button 86 times. Doing this would cost a total of 200 tokens.

So, the most prizes you could possibly win is two; the minimum tokens you would have to spend to win all (two) prizes is 480.

You estimate that each button would need to be pressed no more than 100 times to win a prize. How else would someone be expected to play?

Figure out how to win as many prizes as possible. What is the fewest tokens you would have to spend to win all possible prizes?


Resume and resolution:

The problem is a combinatorial problem where we have to find the best combination of button presses to reach the prize. 
We can solve this problem using a brute force approach where we try all possible combinations of button presses and check if the claw reaches the prize. 
We can use a recursive function to try all possible combinations of button presses and keep track of the minimum number of tokens required to reach the prize. 
We can start with an initial position of the claw and try all possible combinations of button presses to reach the prize. 
We can keep track of the minimum number of tokens required to reach the prize and return the minimum number of tokens required to reach the prize.

Resolution steps:
1. Parse the input data to extract the button presses and prize location.
2. Estimate how many times each button would need to be pressed to win a prize, by solving a system of linear equations.
3. If the solution with integer values is found, calculate the cost of the solution and add it to the total cost (number of tokens required).
4. Otherwise, it is not possible to win a prize, and we can skip to the next machine.
5. Print the minimum number of tokens required to win all possible prizes.


--- Part Two ---
As you go to win the first prize, you discover that the claw is nowhere near where you expected it would be. Due to a unit conversion error in your measurements, the position of every prize is actually 10000000000000 higher on both the X and Y axis!

Add 10000000000000 to the X and Y position of every prize. After making this change, the example above would now look like this:

Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=10000000008400, Y=10000000005400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=10000000012748, Y=10000000012176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=10000000007870, Y=10000000006450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=10000000018641, Y=10000000010279
Now, it is only possible to win a prize on the second and fourth claw machines. Unfortunately, it will take many more than 100 presses to do so.

Using the corrected prize coordinates, figure out how to win as many prizes as possible. What is the fewest tokens you would have to spend to win all possible prizes?

Resume and resoning:

The problem is similar to the first part, but the prize coordinates have been changed.
We need to update the prize coordinates by adding 10000000000000 to the X and Y positions of every prize.
We can reuse the same code from the first part to solve the problem with the updated prize coordinates.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    /// <summary>
    /// Represents a claw machine with two buttons and a prize location.
    /// The machine has two buttons, A and B, that move the claw a specific amount along the X and Y axes.
    /// The prize location is the target location where the claw must be positioned to win the prize.
    /// The machine can be solved to find the number of times each button must be pressed to reach the prize.
    /// </summary>
    class Machine
    {
        public int XA { get; set; }      // Amount to move along the X axis when pressing button A
        public int YA { get; set; }      // Amount to move along the Y axis when pressing button A
        public int XB { get; set; }      // Amount to move along the X axis when pressing button B
        public int YB { get; set; }      // Amount to move along the Y axis when pressing button B
        public long XPrize { get; set; } // X coordinate of the prize location
        public long YPrize { get; set; } // Y coordinate of the prize location

        /// <summary>
        /// Initializes a new instance of the <see cref="Machine"/> class with the specified button presses and prize location.
        /// </summary>
        /// <param name="xA">The amount to move along the X axis when pressing button A.</param>
        /// <param name="yA">The amount to move along the Y axis when pressing button A.</param>
        /// <param name="xB">The amount to move along the X axis when pressing button B.</param>
        /// <param name="yB">The amount to move along the Y axis when pressing button B.</param>
        /// <param name="xPrize">The X coordinate of the prize location.</param>
        /// <param name="yPrize">The Y coordinate of the prize location.</param>
        /// <returns>A new instance of the <see cref="Machine"/> class.</returns>
        public Machine(int xA, int yA, int xB, int yB, int xPrize, int yPrize)
        {
            XA = xA;
            YA = yA;
            XB = xB;
            YB = yB;
            XPrize = xPrize;
            YPrize = yPrize;
        }

        // Returns a string representation of the machine, including the button presses and prize location.
        public string ToString()
        {
            return $"Machine:\n\tButton A: X+{XA}, Y+{YA}\n\tButton B: X+{XB}, Y+{YB}\n\tPrize: X={XPrize}, Y={YPrize}\n";
        }

        /// <summary>
        /// Solves the machine to find the number of times each button must be pressed to reach the prize.
        /// </summary>
        /// <returns>The number of times each button must be pressed to reach the prize.</returns>
        /// <exception cref="Exception">Thrown when no integer solution is found.</exception>
        public (long xS, long yS) Solve()
        {
            // Solve the system of linear equations to find the number of times each button must be pressed.
            long alfa = (XPrize * YB - XB * YPrize) / (XA * YB - XB * YA);
            long beta = (XPrize - XA * alfa) / XB;

            // Check if the solution is valid: the divisions for alfa and beta should be exact.
            if (XA * alfa + XB * beta == XPrize && YA * alfa + YB * beta == YPrize)
                return (alfa, beta);

            // If the solution is not valid, throw an exception.
            throw new Exception("No solution found");
        }

    }

    /// <summary>
    /// Parses a line of input data to extract the X and Y coordinates.
    /// </summary>
    /// <param name="line">The line of input data to parse.</param>
    /// <param name="description">The description of the line to parse. It should match the beginning of the line.</param>
    /// <returns>The X and Y coordinates extracted from the line.</returns>
    /// <exception cref="Exception">Thrown when the line format is incorrect: \<description\>: X+\<number\>, Y+\<number\> or X=\<number\>, Y=\<number\>.</exception>
    private static (int x, int y) ParseLine(string line, string description)
    {
        // Split the line into parts using the colon as a separator. 
        string[] parts = line.Split(':');
        // The first part should be the description.
        if (description == parts[0].Trim()) {
            // The second part should contain the X and Y coordinates. Split that part into axes using a comma as a separator.
            string[] axes = parts[1].Split(',');
            // The axes should have the format X+<number>, Y+<number> or X=<number>, Y=<number>. 
            if (!axes[0].StartsWith("X") || !axes[1].StartsWith("Y"))
                // Extract the numbers from the axes, parse them, and return them as a tuple.
                return (int.Parse(axes[0].Trim().Substring(2)), int.Parse(axes[1].Trim().Substring(2)));
            // If the axes do not have the correct format, throw an exception.
            throw new Exception("Format error. Expected: X+<number>, Y+<number> or X=<number>, Y=<number> but got: " + parts[1].Trim());
        }
        // If the description does not match the beginning of the line, throw an exception.
        throw new Exception("Format error. Expected: " + description + " but got: " + parts[0].Trim());
    }

    static void Main()
    {
        // Read the sample input data
        string[] lines = new string[]
        {
            "Button A: X+94, Y+34",
            "Button B: X+22, Y+67",
            "Prize: X=8400, Y=5400",
            "",
            "Button A: X+26, Y+66",
            "Button B: X+67, Y+21",
            "Prize: X=12748, Y=12176",
            "",
            "Button A: X+17, Y+86",
            "Button B: X+84, Y+37",
            "Prize: X=7870, Y=6450",
            "",
            "Button A: X+69, Y+23",
            "Button B: X+27, Y+71",
            "Prize: X=18641, Y=10279"
        };

        // Read the input data from the file
        lines = File.ReadAllLines("input.txt");

        // lineIndex is used to iterate over the lines of input data. It starts at 0.
        int lineIndex = 0;
        // Storings used to keep track of the total number of tokens required to win all possible prizes.
        long totalCostOne = 0;
        long totalCostTwo = 0;
        // Iterate over the lines of input data.
        while (lineIndex < lines.Length)
        {
            // Parse the button presses and prize location from the input data.
            (int xA, int yA) = ParseLine(lines[lineIndex], "Button A");
            (int xB, int yB) = ParseLine(lines[lineIndex + 1], "Button B");
            (int xPrize, int yPrize) = ParseLine(lines[lineIndex + 2], "Prize");
            // Create a new machine with the button presses and prize location data.
            Machine machine = new Machine(xA, yA, xB, yB, xPrize, yPrize);
            //Console.Write(machine.ToString());
            try
            {
                // Try to solve the machine to find the number of times each button must be pressed to reach the prize.
                (long a, long b) = machine.Solve();
                // Calculate the cost of the solution: each press of button A costs 3 tokens and each press of button B costs 1 token.
                long cost = a * 3 + b;
                // Add the cost of the solution to the total cost.
                totalCostOne += cost;
                //Console.WriteLine("\tSolution: push A " + a + " times and B " + b + " times. Cost: " + cost + ". Until now: " + totalCost + ".\n");
            }
            // If no solution is found, catch the exception, ignore the machine, and move to the next one.
            catch (Exception e)
            {
                //Console.WriteLine("\t" + e.Message + "\n");
            }
            // Part Two: Update the prize coordinates by adding 10000000000000 to the X and Y positions of every prize.
            machine.XPrize += 10000000000000;
            machine.YPrize += 10000000000000;
            try
            {
                // Try to solve the machine to find the number of times each button must be pressed to reach the prize.
                (long a, long b) = machine.Solve();
                // Calculate the cost of the solution: each press of button A costs 3 tokens and each press of button B costs 1 token.
                long cost = a * 3 + b;
                // Add the cost of the solution to the total cost.
                totalCostTwo += cost;
                //Console.WriteLine("\tSolution: push A " + a + " times and B " + b + " times. Cost: " + cost + ". Until now: " + totalCost + ".\n");
            }
            // If no solution is found, catch the exception, ignore the machine, and move to the next one.
            catch (Exception e)
            {
                //Console.WriteLine("\t" + e.Message + "\n");
            }
            // Move to the next machine by incrementing the lineIndex by 4: Button A, Button B, Prize, and an empty line.
            lineIndex += 4;
        }
        // Print the minimum number of tokens required to win all possible prizes.
        Console.WriteLine("Fewest tokens you would have to spend to win all possible prizes (Part One): " + totalCostOne);
        Console.WriteLine("Fewest tokens you would have to spend to win all possible prizes (Part Two): " + totalCostTwo);
    }
}
