/*  Problem Description:

--- Day 10: Hoof It ---
You all arrive at a Lava Production Facility on a floating island in the sky. As the others begin to search the massive industrial complex, you feel a small nose boop your leg and look down to discover a reindeer wearing a hard hat.

The reindeer is holding a book titled "Lava Island Hiking Guide". However, when you open the book, you discover that most of it seems to have been scorched by lava! As you're about to ask how you can help, the reindeer brings you a blank topographic map of the surrounding area (your puzzle input) and looks up at you excitedly.

Perhaps you can help fill in the missing hiking trails?

The topographic map indicates the height at each position using a scale from 0 (lowest) to 9 (highest). For example:

0123
1234
8765
9876
Based on un-scorched scraps of the book, you determine that a good hiking trail is as long as possible and has an even, gradual, uphill slope. For all practical purposes, this means that a hiking trail is any path that starts at height 0, ends at height 9, and always increases by a height of exactly 1 at each step. Hiking trails never include diagonal steps - only up, down, left, or right (from the perspective of the map).

You look up from the map and notice that the reindeer has helpfully begun to construct a small pile of pencils, markers, rulers, compasses, stickers, and other equipment you might need to update the map with hiking trails.

A trailhead is any position that starts one or more hiking trails - here, these positions will always have height 0. Assembling more fragments of pages, you establish that a trailhead's score is the number of 9-height positions reachable from that trailhead via a hiking trail. In the above example, the single trailhead in the top left corner has a score of 1 because it can reach a single 9 (the one in the bottom left).

This trailhead has a score of 2:

...0...
...1...
...2...
6543456
7.....7
8.....8
9.....9
(The positions marked . are impassable tiles to simplify these examples; they do not appear on your actual topographic map.)

This trailhead has a score of 4 because every 9 is reachable via a hiking trail except the one immediately to the left of the trailhead:

..90..9
...1.98
...2..7
6543456
765.987
876....
987....
This topographic map contains two trailheads; the trailhead at the top has a score of 1, while the trailhead at the bottom has a score of 2:

10..9..
2...8..
3...7..
4567654
...8..3
...9..2
.....01
Here's a larger example:

89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732
This larger example has 9 trailheads. Considering the trailheads in reading order, they have scores of 5, 6, 5, 3, 1, 3, 5, 3, and 5. Adding these scores together, the sum of the scores of all trailheads is 36.

The reindeer gleefully carries over a protractor and adds it to the pile. What is the sum of the scores of all trailheads on your topographic map?

Resume:
The topographic map indicates the height at each position using a scale from 0 (lowest) to 9 (highest). 
A hiking trail is any path that starts at height 0, ends at height 9, and always increases by a height of exactly 1 at each step. 
Hiking trails never include diagonal steps - only up, down, left, or right (from the perspective of the map). 
A trailhead is any position that starts one or more hiking trails - here, these positions will always have height 0. 
A trailhead's score is the number of 9-height positions reachable from that trailhead via a hiking trail. 
What is the sum of the scores of all trailheads on your topographic map?

Resolution steps:
1. Read the input from the file.
2. Parse the input and store it in a 2D array.
3. Create a recursive function to find the trail targets (9-height positions) reachable from the trailhead.
4. Iterate through the 2D array and find the trailheads.
5. For each trailhead, find the trail targets and calculate the score, wich is the number of trail targets found.
6. Print the sum of the scores of all trailheads.

--- Part Two ---
The reindeer spends a few minutes reviewing your hiking trail map before realizing something, 
disappearing for a few minutes, and finally returning with yet another slightly-charred piece of paper.

The paper describes a second way to measure a trailhead called its rating. 
A trailhead's rating is the number of distinct hiking trails which begin at that trailhead. For example:

.....0.
..4321.
..5..2.
..6543.
..7..4.
..8765.
..9....
The above map has a single trailhead; its rating is 3 because there are exactly three distinct hiking trails which begin at that position:

.....0.   .....0.   .....0.
..4321.   .....1.   .....1.
..5....   .....2.   .....2.
..6....   ..6543.   .....3.
..7....   ..7....   .....4.
..8....   ..8....   ..8765.
..9....   ..9....   ..9....
Here is a map containing a single trailhead with rating 13:

..90..9
...1.98
...2..7
6543456
765.987
876....
987....
This map contains a single trailhead with rating 227 (because there are 121 distinct hiking trails that lead to the 9 on the right edge and 106 that lead to the 9 on the bottom edge):

012345
123456
234567
345678
4.6789
56789.
Here's the larger example from before:

89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732
Considering its trailheads in reading order, they have ratings of 20, 24, 10, 4, 1, 4, 5, 8, and 5. 
The sum of all trailhead ratings in this larger example topographic map is 81.

You're not sure how, but the reindeer seems to have crafted some tiny flags out of toothpicks and bits of paper
 and is using them to mark trailheads on your topographic map. 
What is the sum of the ratings of all trailheads?

Resume:
A trailhead's rating is now the number of distinct hiking trails which begin at that trailhead.
What is the sum of the ratings of all trailheads?

Resolution steps:
1. Create a recursive function to count the quantity of hiking trails from the trailhead.
2. Iterate through the 2D array and find the trailheads.
3. For each trailhead, count the quantity of hiking trails and calculate the rating.
4. Print the sum of the ratings of all trailheads.
*/

using System;
using System.Dynamic;
using System.IO;

class Program
{
    static void Main()
    {
        // Get the input from simplest sample
        string[] input = new string[]
        {
            "0123",
            "1234",
            "8765",
            "9876"
        };

        // Get the input from the sample from part 2
        input = new string[]
        {
            "..90..9",
            "...1.98",
            "...2..7",
            "6543456",
            "765.987",
            "876....",
            "987...."
        };

        // Get the input from other sample from part 2
        input = new string[]
        {
            "012345",
            "123456",
            "234567",
            "345678",
            "4.6789",
            "56789."
        };

        // Get the input from the larger sample
         input = new string[]
        {
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732"
        };

        // Get the input from the file
        input = File.ReadAllLines("input.txt");

        // Create a 2D array to store the map
        int[,] map = new int[input.Length, input[0].Length];

        // Parse the input and store it in a 2D array. Iterate through the lines and characters of the input.
        for (int y = 0; y < map.GetLength(0); y++)
            for (int x = 0; x < map.GetLength(1); x++)
                //  Store the integer value of the character in the 2D array
                map[y, x] = input[y][x] - '0';

        // Find the trailheads and calculate the score and rating. 
        // score is the variable to store the sum of the scores of all trailheads, which is initially set to 0.
        int score = 0;
        // rating is the variable to store the sum of the ratings of all trailheads, which is initially set to 0.
        int rating = 0;
        // Iterate through the 2D array and find the trailheads.
        for (int y = 0; y < map.GetLength(0); y++)
            for (int x = 0; x < map.GetLength(1); x++)
                // If the current position is a trailhead (height 0)
                if (map[y, x] == 0)
                {
                    // Calculate the score of the trailhead and add it to the sum.
                    score += LocateTrailTargets(map, x, y).Count;
                    // Calculate the rating of the trailhead and add it to the sum.
                    rating += CountTrails(map, x, y);
                }
        // Print the sum of the scores and ratings of all trailheads.
        Console.WriteLine("Score: " + score);        
        Console.WriteLine("Rating: " + rating);        
    }

    /// <summary>
    /// Recursive function to find the trail targets (9-height positions) reachable from the trailhead.
    /// </summary>
    /// <param name="map">Input map</param>
    /// <param name="x">x-coordinate of the trailhead</param>
    /// <param name="y">y-coordinate of the trailhead</param>
    /// <returns>The location of the trail targets</returns>
    private static HashSet<(int, int)> LocateTrailTargets(int[,] map, int x, int y)
    {
        // Set of 9-height positions reachable from the trailhead via a hiking trail.
        HashSet<(int, int)> tops = new HashSet<(int, int)>();

        // Store the value of the current position in a variable. This is used to restore the value of the current position after visiting its neighbors.
        int value = map[y, x];
        // If the current position is a 9-height position, it means that a hiking trail has reached its end. Increment the score.
        if (value == 9) {
            tops.Add((x, y));
            return tops;
        }
        // Mark the current position as visited.
        map[y, x] = -1;

        // Visit the neighbors of the current position. 
        // If the neighbor is a valid position and the height of the neighbor is equal to the value of the current position plus 1,
        // call the function recursively with the neighbor's coordinates.
        // Union the result (trail targets found from each neighbor) with the set of trail targets.
        if (x > 0 && map[y, x - 1] == value + 1)
            tops.UnionWith(LocateTrailTargets(map, x - 1, y));
        if (x < map.GetLength(1) - 1 && map[y, x + 1] == value + 1)
            tops.UnionWith(LocateTrailTargets(map, x + 1, y));
        if (y > 0 && map[y - 1, x] == value + 1)
            tops.UnionWith(LocateTrailTargets(map, x, y - 1));
        if (y < map.GetLength(0) - 1 && map[y + 1, x] == value + 1)
            tops.UnionWith(LocateTrailTargets(map, x, y + 1));

        // Restore the value of the current position.
        map[y, x] = value;

        // Return the final score of the trailhead.
        return tops;
    }

    /// <summary>
    /// Recursive function to find the quantity of hiking trails from the trailhead.
    /// </summary>
    /// <param name="map">Input map</param>
    /// <param name="x">x-coordinate of the trailhead</param>
    /// <param name="y">y-coordinate of the trailhead</param>
    /// <returns>The quantity of hiking trails</returns>
    private static int CountTrails(int[,] map, int x, int y)
    {
        // count is the variable to store the quantity of hiking trails from the trailhead. It is initially set to 0.
        int count = 0;

        // Store the value of the current position in a variable. This is used to restore the value of the current position after visiting its neighbors.
        int value = map[y, x];
        // If the current position is a 9-height position, it means that a hiking trail has reached its end, and there is one hiking trail. 
        if (value == 9) return 1;

        // Mark the current position as visited.
        map[y, x] = -1;

        // Visit the neighbors of the current position. 
        // If the neighbor is a valid position and the height of the neighbor is equal to the value of the current position plus 1,
        // call the function recursively with the neighbor's coordinates.
        // Sum the result (quantity of hiking trails found from each neighbor) with the quantity of hiking trails.
        if (x > 0 && map[y, x - 1] == value + 1)
            count += CountTrails(map, x - 1, y);
        if (x < map.GetLength(1) - 1 && map[y, x + 1] == value + 1)
            count += CountTrails(map, x + 1, y);
        if (y > 0 && map[y - 1, x] == value + 1)
            count += CountTrails(map, x, y - 1);
        if (y < map.GetLength(0) - 1 && map[y + 1, x] == value + 1)
            count += CountTrails(map, x, y + 1);

        // Restore the value of the current position.
        map[y, x] = value;

        // Return the final score of the trailhead.
        return count;
    }
}