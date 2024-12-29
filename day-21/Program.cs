/*  Problem Description:

--- Day 21: Keypad Conundrum ---
As you teleport onto Santa's Reindeer-class starship, The Historians begin to panic: someone from their search party is missing. 
A quick life-form scan by the ship's computer reveals that when the missing Historian teleported, he arrived in another part of the ship.

The door to that area is locked, but the computer can't open it; 
it can only be opened by physically typing the door codes (your puzzle input) on the numeric keypad on the door.

The numeric keypad has four rows of buttons: 789, 456, 123, and finally an empty gap followed by 0A. Visually, they are arranged like this:

+---+---+---+
| 7 | 8 | 9 |
+---+---+---+
| 4 | 5 | 6 |
+---+---+---+
| 1 | 2 | 3 |
+---+---+---+
    | 0 | A |
    +---+---+
Unfortunately, the area outside the door is currently depressurized and nobody can go near the door. A robot needs to be sent instead.

The robot has no problem navigating the ship and finding the numeric keypad, but it's not designed for button pushing: 
it can't be told to push a specific button directly. Instead, it has a robotic arm that can be controlled remotely via a directional keypad.

The directional keypad has two rows of buttons: a gap / ^ (up) / A (activate) on the first row and < (left) / v (down) / > (right) on the second row. 
Visually, they are arranged like this:

    +---+---+
    | ^ | A |
+---+---+---+
| < | v | > |
+---+---+---+
When the robot arrives at the numeric keypad, its robotic arm is pointed at the A button in the bottom right corner. 
After that, this directional keypad remote control must be used to maneuver the robotic arm: 
the up / down / left / right buttons cause it to move its arm one button in that direction, 
and the A button causes the robot to briefly move forward, pressing the button being aimed at by the robotic arm.

For example, to make the robot type 029A on the numeric keypad, one sequence of inputs on the directional keypad you could use is:

< to move the arm from A (its initial position) to 0.
A to push the 0 button.
^A to move the arm to the 2 button and push it.
>^^A to move the arm to the 9 button and push it.
vvvA to move the arm to the A button and push it.
In total, there are three shortest possible sequences of button presses on this directional keypad that would cause the robot to type 029A: 
<A^A>^^AvvvA, <A^A^>^AvvvA, and <A^A^^>AvvvA.

Unfortunately, the area containing this directional keypad remote control is currently experiencing high levels of radiation and nobody can go near it. 
A robot needs to be sent instead.

When the robot arrives at the directional keypad, its robot arm is pointed at the A button in the upper right corner. 
After that, a second, different directional keypad remote control is used to control this robot 
(in the same way as the first robot, except that this one is typing on a directional keypad instead of a numeric keypad).

There are multiple shortest possible sequences of directional keypad button presses 
that would cause this robot to tell the first robot to type 029A on the door. 
One such sequence is v<<A>>^A<A>AvA<^AA>A<vAAA>^A.

Unfortunately, the area containing this second directional keypad remote control is currently -40 degrees! 
Another robot will need to be sent to type on that directional keypad, too.

There are many shortest possible sequences of directional keypad button presses 
that would cause this robot to tell the second robot to tell the first robot to eventually type 029A on the door. 
One such sequence is <vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A.

Unfortunately, the area containing this third directional keypad remote control is currently full of Historians, 
so no robots can find a clear path there. Instead, you will have to type this sequence yourself.

Were you to choose this sequence of button presses, here are all of the buttons that would be pressed on your directional keypad, 
the two robots' directional keypads, and the numeric keypad:

<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A
v<<A>>^A<A>AvA<^AA>A<vAAA>^A
<A^A>^^AvvvA
029A
In summary, there are the following keypads:

One directional keypad that you are using.
Two directional keypads that robots are using.
One numeric keypad (on a door) that a robot is using.
It is important to remember that these robots are not designed for button pushing. 
In particular, if a robot arm is ever aimed at a gap where no button is present on the keypad, even for an instant, the robot will panic unrecoverably. 
So, don't do that. All robots will initially aim at the keypad's A key, wherever it is.

To unlock the door, five codes will need to be typed on its numeric keypad. For example:

029A
980A
179A
456A
379A
For each of these, here is a shortest sequence of button presses you could type to cause the desired code to be typed on the numeric keypad:

029A: <vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A
980A: <v<A>>^AAAvA^A<vA<AA>>^AvAA<^A>A<v<A>A>^AAAvA<^A>A<vA>^A<A>A
179A: <v<A>>^A<vA<A>>^AAvAA<^A>A<v<A>>^AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A
456A: <v<A>>^AA<vA<A>>^AAvAA<^A>A<vA>^A<A>A<vA>^A<A>A<v<A>A>^AAvA<^A>A
379A: <v<A>>^AvA^A<vA<AA>>^AAvA<^A>AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A

The Historians are getting nervous; 
the ship computer doesn't remember whether the missing Historian is trapped in the area containing a giant electromagnet or molten lava. 
You'll need to make sure that for each of the five codes, you find the shortest sequence of button presses necessary.

The complexity of a single code (like 029A) is equal to the result of multiplying these two values:

The length of the shortest sequence of button presses you need to type on your directional keypad 
in order to cause the code to be typed on the numeric keypad; for 029A, this would be 68.
The numeric part of the code (ignoring leading zeroes); for 029A, this would be 29.
In the above example, complexity of the five codes can be found by calculating 68 * 29, 60 * 980, 68 * 179, 64 * 456, and 64 * 379. 
Adding these together produces 126384.

Find the fewest number of button presses you'll need to perform in order to cause the robot in front of the door to type each code. 
What is the sum of the complexities of the five codes on your list?


--- Part Two ---
Just as the missing Historian is released, The Historians realize that a second member of their search party has also been missing this entire time!

A quick life-form scan reveals the Historian is also trapped in a locked area of the ship. 
Due to a variety of hazards, robots are once again dispatched, forming another chain of remote control keypads managing robotic-arm-wielding robots.

This time, many more robots are involved. In summary, there are the following keypads:

One directional keypad that you are using.
25 directional keypads that robots are using.
One numeric keypad (on a door) that a robot is using.
The keypads form a chain, just like before: 
your directional keypad controls a robot which is typing on a directional keypad which controls a robot which is typing on a directional keypad... 
and so on, ending with the robot which is typing on the numeric keypad.

The door codes are the same this time around; only the number of robots and directional keypads has changed.

Find the fewest number of button presses you'll need to perform in order to cause the robot in front of the door to type each code. 
What is the sum of the complexities of the five codes on your list?


Solution Description:
 - The problem can be solved by using a recursive approach to calculate the shortest path to reach the destination.
    * A function to calculate the shortest code length to reach the destination
    * A function to calculate the cost of transitioning between two buttons on the keypad
    * A function to compute the complexity of a list of codes, given the depth of the directional keypads
 - The complexity of a code is the product of the shortest path length and the numeric part of the code.
 - We keep the following structures:
    * A dictionary to map coordinates to keypad buttons
    * A dictionary to map coordinates to directional keypad buttons
    * A list of keypads, starting with the numeric keypad, and adding directional keypads as needed.
        The difference between the part 1 and part 2 is the quantity of the directional keypads.
    * A dictionary to cache the cost of transitioning between buttons
*/

using System.Diagnostics;

class Program {
    static void Main()
    {
        // Sample input
        string[] lines = {
            "029A",
            "980A",
            "179A",
            "456A",
            "379A"
        };

        // Compute and print the sum of the complexities of all codes using 2 directional keypads (Sample for Part 1)
        Console.WriteLine("Sum of complexities  (sample):  " + ComputeComplexity(lines, 2));

        // Input from file
        lines = File.ReadAllLines("input.txt");

        // Compute and print the sum of the complexities of all codes using 2 directional keypads (Part 1)
        Console.WriteLine("Sum of complexities (part one): " + ComputeComplexity(lines, 2));
        // Compute and print the sum of the complexities of all codes using 2 directional keypads (Part 2)
        Console.WriteLine("Sum of complexities (part two): " + ComputeComplexity(lines, 25));
    }
    
    /// <summary>
    /// Compute the complexity of a list of codes, given the depth of the directional keypads
    /// </summary>
    /// <param name="lines">The list of codes</param>
    /// <param name="depth">The depth of the directional keypads</param>
    /// <returns>The sum of the complexities of all codes</returns>
    /// <remarks>
    /// The complexity of a code is the product of the shortest path length and the numeric part of the code.
    /// </remarks>
    private static long ComputeComplexity(string[] lines, int depth) {
        // Create a dictionary to map coordinates to keypad buttons
        Dictionary<(int x, int y), char> numericKeypad = new Dictionary<(int, int), char>
        {
            { (0, 0), '7' }, { (1, 0), '8' }, { (2, 0), '9' },
            { (0,-1), '4' }, { (1,-1), '5' }, { (2,-1), '6' },
            { (0,-2), '1' }, { (1,-2), '2' }, { (2,-2), '3' },
            { (0,-3), ' ' }, { (1,-3), '0' }, { (2,-3), 'A' }
        };
        // Create a dictionary to map coordinates to directional keypad buttons
        Dictionary<(int x, int y), char> directionalKeypad = new Dictionary<(int, int), char>
        {
            { (0, 0), ' ' }, { (1, 0), '^' }, { (2, 0), 'A' },
            { (0,-1), '<' }, { (1,-1), 'v' }, { (2,-1), '>' }
        };

        // Build a list of keypads, starting with the numeric keypad, and adding directional keypads as needed
        List<Dictionary<(int x, int y), char>> keypads = new List<Dictionary<(int x, int y), char>>() { numericKeypad };
        for (int tier = 0; tier < depth; tier++)
            keypads.Add(directionalKeypad);

        Dictionary<(char currentButton, char nextButton, int depth), long> 
            keypadTransitionCosts = new Dictionary<(char currentButton, char nextButton, int depth), long>();

        // Calculate the complexity of each code, as the product of the shortest path length and the numeric part of the code
        // Sum the complexities of all codes to get the final result
        var sum = 0L;
        foreach (string line in lines)
            sum += ShortestCodeLength(line, keypads, keypadTransitionCosts) * int.Parse(line.Substring(0, 3));
        return sum;
    }

    /// <summary>
    ///    Calculate the shortest code length to reach the destination
    /// </summary>
    /// <param name="inputSequence">The sequence of buttons to press</param>
    /// <param name="keypads">The list of keypads to use</param>
    /// <param name="keypadTransitionCosts">The dictionary to cache the cost of transitioning between buttons</param>
    /// <returns>The shortest code length to reach the destination</returns>
    private static long ShortestCodeLength(string inputSequence, 
                                           List<Dictionary<(int x, int y), char>> keypads, 
                                           Dictionary<(char currentButton, char nextButton, int depth), long> keypadTransitionCosts) 
    {
        // if there are no more keypads to code, just return the length of the input sequence
        if (keypads.Count == 0)
            return inputSequence.Length;
        else 
        {
            // the robot starts (and finishes) at the 'A' key
            char currentButton = 'A';
            long totalLength = 0L;

            // calculate the cost of each transition, and sum them up
            foreach (var nextButton in inputSequence) {
                totalLength += GetTransitionCost(currentButton, nextButton, keypads, keypadTransitionCosts);
                currentButton = nextButton;
            }

            // at the end the current key should be reset to 'A'
            Debug.Assert(currentButton == 'A', "currentButton == 'A'");
            return totalLength;
        }
    }

    /// <summary>
    ///     Calculate the cost of transitioning between two buttons on the keypad
    /// </summary>
    /// <param name="currentButton">The current button</param>
    /// <param name="nextButton">The next button</param>
    /// <param name="keypads">The list of keypads</param>
    /// <param name="keypadTransitionCosts">The dictionary to cache the cost of transitioning between buttons</param>
    /// <returns>The cost of transitioning between the current and next buttons</returns>
    private static long GetTransitionCost(char currentButton, char nextButton, 
                                          List<Dictionary<(int x, int y), char>> keypads, 
                                          Dictionary<(char currentButton, char nextButton, int depth), long> keypadTransitionCosts) 
    {
        // get the depth of the current keypad list
        int depth = keypads.Count;
        // if the cost of the transition is not already calculated, calculate it recursively
        if (!keypadTransitionCosts.ContainsKey((currentButton, nextButton, depth)))
        {
            // get the current keypad and the rest of the keypads
            var activeKeypad = keypads.First();
            var subsequentKeypads = new List<Dictionary<(int x, int y), char>>(keypads);
            subsequentKeypads.RemoveAt(0);

            // get the coordinates of the current and next buttons (reverse the dictionary)
            var currentCoordinates = activeKeypad.Single(kvp => kvp.Value == currentButton).Key;
            var  nextCoordinates  =  activeKeypad.Single(kvp => kvp.Value == nextButton).Key;

            // calculate the number of steps in the horizontal and vertical directions
            int dy = nextCoordinates.y - currentCoordinates.y;
            string vert = new string(dy < 0 ? 'v' : '^', Math.Abs(dy));
            var dx = nextCoordinates.x - currentCoordinates.x;
            string horiz = new string(dx < 0 ? '<' : '>', Math.Abs(dx));

            var cost = long.MaxValue;
            // build the code to reach the next button, but avoid passing over the ' ' key
            if (activeKeypad[(currentCoordinates.x, nextCoordinates.y)] != ' ')
                cost = Math.Min(cost, ShortestCodeLength(vert + horiz + "A", subsequentKeypads, keypadTransitionCosts));
            if (activeKeypad[(nextCoordinates.x, currentCoordinates.y)] != ' ')
                cost = Math.Min(cost, ShortestCodeLength(horiz + vert + "A", subsequentKeypads, keypadTransitionCosts));

            // store the cost of the transition in the dictionary
            keypadTransitionCosts[(currentButton, nextButton, depth)] = cost;
        }
        // return the cost of the transition, stored in the dictionary
        return keypadTransitionCosts[(currentButton, nextButton, depth)];
    }
}