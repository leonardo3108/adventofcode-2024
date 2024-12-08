/*  Problem Description:
if (mode == 1)   
{
    // Create a copy of the map to simulate the path with the new obstruction
    string[] mapCopy = (string[])map.Clone();
    
    // Add new obstruction to the map copy, in the new position, to check if the guard gets stuck in a loop
    mapCopy[newX] = mapCopy[newX].Substring(0, newY) + 'O' + mapCopy[newX].Substring(newY + 1);

    // Simulate the path of the guard with the new obstruction, to check if it gets stuck in a loop
    HashSet<(int, int, char)> checkLoop = SimulateGuardPath(mapCopy, x, y, direction, 2);
    if (checkLoop.Count > 0)
    {
        // Add the new obstruction to the set of obstructions that cause the guard to get stuck in a loop
        MarkLocation(newX, newY, 'O', mode, trackedLocations);
    }
}
--- Day 6: Guard Gallivant ---
The Historians use their fancy device again, this time to whisk you all away to the North Pole prototype suit manufacturing lab... in the year 1518! It turns out that having direct access to history is very convenient for a group of historians.

You still have to be careful of time paradoxes, and so it will be important to avoid anyone from 1518 while The Historians search for the Chief. Unfortunately, a single guard is patrolling this part of the lab.

Maybe you can work out where the guard will go ahead of time so that The Historians can search safely?

You start by making a map (your puzzle input) of the situation. For example:

....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...
The map shows the current position of the guard with ^ (to indicate the guard is currently facing up from the perspective of the map). Any obstructions - crates, desks, alchemical reactors, etc. - are shown as #.

Lab guards in 1518 follow a very strict patrol protocol which involves repeatedly following these steps:

If there is something directly in front of you, turn right 90 degrees.
Otherwise, take a step forward.
Following the above protocol, the guard moves up several times until she reaches an obstacle (in this case, a pile of failed suit prototypes):

....#.....
....^....#
..........
..#.......
.......#..
..........
.#........
........#.
#.........
......#...
Because there is now an obstacle in front of the guard, she turns right before continuing straight in her new facing direction:

....#.....
........>#
..........
..#.......
.......#..
..........
.#........
........#.
#.........
......#...
Reaching another obstacle (a spool of several very long polymers), she turns right again and continues downward:

....#.....
.........#
..........
..#.......
.......#..
..........
.#......v.
........#.
#.........
......#...
This process continues for a while, but the guard eventually leaves the mapped area (after walking past a tank of universal solvent):

....#.....
.........#
..........
..#.......
.......#..
..........
.#........
........#.
#.........
......#v..
By predicting the guard's route, you can determine which specific positions in the lab will be in the patrol path. Including the guard's starting position, the positions visited by the guard before leaving the area are marked with an X:

....#.....
....XXXXX#
....X...X.
..#.X...X.
..XXXXX#X.
..X.X.X.X.
.#XXXXXXX.
.XXXXXXX#.
#XXXXXXX..
......#X..
In this example, the guard will visit 41 distinct positions on your map.

Predict the path of the guard. How many distinct positions will the guard visit before leaving the mapped area?

Resume and resolution:
The puzzle involves predicting the path of a guard on a grid map. 
The guard moves according to specific rules and visits distinct positions. 
The goal is to determine how many distinct positions the guard will visit before leaving the mapped area.

Resolution Steps:

1. Analyze the grid to understand the positions of walls (X), open spaces (.), and starting points (#).
2. Determine the rules for how the guard moves (e.g., direction changes, obstacles).
3. Use a data structure (like a set) to keep track of all distinct positions the guard visits.
4. Implement a loop to simulate the guard's movement according to the defined rules.
5. Ensure the guard's movement stops when it reaches the boundary of the grid or an obstacle.
6. Count the number of distinct positions stored in the tracking data structure.
7. Provide the final count of distinct positions visited by the guard.


--- Part Two ---
While The Historians begin working around the guard's patrol route, you borrow their fancy device and step outside the lab. From the safety of a supply closet, you time travel through the last few months and record the nightly status of the lab's guard post on the walls of the closet.

Returning after what seems like only a few seconds to The Historians, they explain that the guard's patrol area is simply too large for them to safely search the lab without getting caught.

Fortunately, they are pretty sure that adding a single new obstruction won't cause a time paradox. They'd like to place the new obstruction in such a way that the guard will get stuck in a loop, making the rest of the lab safe to search.

To have the lowest chance of creating a time paradox, The Historians would like to know all of the possible positions for such an obstruction. The new obstruction can't be placed at the guard's starting position - the guard is there right now and would notice.

In the above example, there are only 6 different positions where a new obstruction would cause the guard to get stuck in a loop. The diagrams of these six situations use O to mark the new obstruction, | to show a position where the guard moves up/down, - to show a position where the guard moves left/right, and + to show a position where the guard moves both up/down and left/right.

Option one, put a printing press next to the guard's starting position:

....#.....
....+---+#
....|...|.
..#.|...|.
....|..#|.
....|...|.
.#.O^---+.
........#.
#.........
......#...
Option two, put a stack of failed suit prototypes in the bottom right quadrant of the mapped area:


....#.....
....+---+#
....|...|.
..#.|...|.
..+-+-+#|.
..|.|.|.|.
.#+-^-+-+.
......O.#.
#.........
......#...
Option three, put a crate of chimney-squeeze prototype fabric next to the standing desk in the bottom right quadrant:

....#.....
....+---+#
....|...|.
..#.|...|.
..+-+-+#|.
..|.|.|.|.
.#+-^-+-+.
.+----+O#.
#+----+...
......#...
Option four, put an alchemical retroencabulator near the bottom left corner:

....#.....
....+---+#
....|...|.
..#.|...|.
..+-+-+#|.
..|.|.|.|.
.#+-^-+-+.
..|...|.#.
#O+---+...
......#...
Option five, put the alchemical retroencabulator a bit to the right instead:

....#.....
....+---+#
....|...|.
..#.|...|.
..+-+-+#|.
..|.|.|.|.
.#+-^-+-+.
....|.|.#.
#..O+-+...
......#...
Option six, put a tank of sovereign glue right next to the tank of universal solvent:

....#.....
....+---+#
....|...|.
..#.|...|.
..+-+-+#|.
..|.|.|.|.
.#+-^-+-+.
.+----++#.
#+----++..
......#O..
It doesn't really matter what you choose to use as an obstacle so long as you and The Historians can put it into position without the guard noticing. The important thing is having enough options that you can find one that minimizes time paradoxes, and in this example, there are 6 different positions you could choose.

You need to get the guard stuck in a loop by adding a single new obstruction. How many different positions could you choose for this obstruction?

Resume and resolution:
The puzzle involves finding possible positions for a new obstruction that will cause the guard to get stuck in a loop.
The goal is to determine the number of different positions where the obstruction can be placed to create a loop for the guard.

Resolution Steps:
1. Use the simulation of the guard's path to identify the distinct positions visited by the guard.
2. Create a set of potential obstructions based on the distinct positions visited by the guard, excluding the starting position.
3. Simulate the guard's path with each potential obstruction to check if it gets stuck in a loop.
4. Remove the obstructions that cause the guard to get stuck in a loop.
5. Count the remaining obstructions as the number of different positions that could cause the guard to get stuck in a loop.
6. Provide the final count of different positions for the obstruction.
*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.PortableExecutable;

class Program
{
    static readonly char[] guardRepresentations = new[] { '^', '>', 'v', '<' };
    
    /// <summary>
    /// Finds the position of the guard in the given map.
    /// </summary>
    /// <param name="map">A 2D array of strings representing the map.</param>
    /// <returns>A tuple containing the row and column indices of the guard's position.</returns>
    /// <exception cref="Exception">Thrown when the guard is not found in the map.</exception>
    static (int, int, char) FindGuardPosition(string[] map)
    {
        // Loop through each cell in the map
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                // Check if the current character represents the guard
                if (guardRepresentations.Contains(map[i][j]))
                    return (i, j, map[i][j]);
            }
        }
        throw new Exception("Guard not found");   // Guard not found in the map
    }

    /// <summary>
    ///    Moves the guard in the given direction.
    /// </summary>
    /// <param name="x">The current row index of the guard.</param>
    /// <param name="y">The current column index of the guard.</param>
    /// <param name="direction">The current direction the guard is facing.</param>
    /// <returns>A tuple containing the new row and column indices of the guard after moving.</returns>
    /// <exception cref="Exception">Thrown when an invalid direction is provided.</exception>
    static (int, int) Move(int x, int y, char direction)
    {
        // Move the guard based on the current direction
        return direction switch
        {
            '^' => (x - 1, y),   // Move up
            '>' => (x, y + 1),   // Move right
            'v' => (x + 1, y),   // Move down
            '<' => (x, y - 1),   // Move left
            _ => throw new Exception("Invalid direction"),   // Invalid direction provided
        };
    }

    /// <summary>
    ///   Turns the guard to the right based on the current direction.
    /// </summary>
    /// <param name="direction">The current direction the guard is facing.</param>
    /// <returns>The new direction the guard is facing after turning right.</returns>
    /// <exception cref="Exception">Thrown when an invalid direction is provided.</exception>
    static char TurnRight(char direction)
    {
        // Turn the guard to the right based on the current direction
        return direction switch
        {
            '^' => '>',  // Turn right from facing up
            '>' => 'v',  // Turn down from facing right
            'v' => '<',  // Turn left from facing down
            '<' => '^',  // Turn up from facing left
            _ => throw new Exception("Invalid direction"),   // Invalid direction provided
        };
    }

    /// <summary>
    ///  Checks if the given position is out of bounds of the map.
    ///  </summary>
    /// <param name="x">The row index of the position.</param>
    /// <param name="y">The column index of the position.</param>
    /// <param name="rows">The total number of rows in the map.</param>
    /// <param name="cols">The total number of columns in the map.</param>
    /// <returns>True if the position is out of bounds, false otherwise.</returns>
    static bool IsOutOfBounds(int x, int y, int rows, int cols)
    {
        // Check if the position is out of bounds
        return x < 0 || x >= rows || y < 0 || y >= cols;
    }

    /// <summary>
    /// Simulates the path of the guard on the map.
    /// </summary>
    /// <param name="map">A 2D array of strings representing the map.</param>
    /// <param name="x0">The initial row index of the guard.</param>
    /// <param name="y0">The initial column index of the guard.</param>
    /// <param name="d0">The initial direction the guard is facing.</param>
    /// <returns>A tuple containing a boolean indicating if the guard is stuck in a loop and a list of guard positions visited.</returns>
    private static (bool, List<(int, int, char)>) SimulateGuardPath(string[] map, int x0, int y0, char d0)
    {
        (int x, int y, char direction) = (x0, y0, d0);

        // Get the dimensions of the map
        int rows = map.Length;
        int cols = map[0].Length;

        // List to store the tracked locations of the guard
        List<(int, int, char)> guardPositions = new List<(int, int, char)>();

        // Determine if the guard is stuck in a loop. Initialize to false.
        bool loopDetected = false;

        while (true) 
        {
            //Console.WriteLine($"Guard position: ({x}, {y}), Direction: {direction}. Tracking: {trackedMainLocations.Count}");

            // Move the guard in the current direction
            (int newX, int newY) = Move(x, y, direction);

            // Check if the new position is out of bounds
            if (IsOutOfBounds(newX, newY, rows, cols))
                break;

            // Check if the new position is already visited
            if (guardPositions.Contains((newX, newY, direction)))
            {
                // Guard is stuck in a loop: stop the simulation and return the tracked locations so far
                loopDetected = true;
                break;
            }
            
            // Check if the new position is an obstruction
            if (map[newX][newY] == '#' || map[newX][newY] == 'O')
            {
                // Turn the guard to the right
                direction = TurnRight(direction);
            }
            else
            {
                // Move the guard forward
                (x, y) = (newX, newY);

                // Add the new position to the set of tracked locations
                guardPositions.Add((x, y, direction));
            }
        }

        return (loopDetected, guardPositions);
   }

    static HashSet<(int, int)> ExtractDistinctPositions(List<(int, int, char)> tracking)
    {
        HashSet<(int, int)> distinctPositions = new HashSet<(int, int)>();
        foreach (var (x, y, _) in tracking)
        {
            distinctPositions.Add((x, y));
        }
        return distinctPositions;
    }

    static void Main()
    {

        string[] map = {
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#..."
        };

        // get map from input.txt file
        map = System.IO.File.ReadAllLines("input.txt");

        // Find the position of the guard in the map
        (int x0, int y0, char d0) = FindGuardPosition(map);

        // Simulate the path of the guard. Mode 0 means straight path, without putting new obstructions and get all distinct positions visited.
        List<(int, int, char)> tracking;
        (_, tracking) = SimulateGuardPath(map, x0, y0, d0);

        // Extract the distinct positions visited by the guard
        HashSet<(int, int)> positionsVisited = ExtractDistinctPositions(tracking);

        // Output the number of distinct positions visited by the guard
        Console.WriteLine($"Distinct positions visited: {positionsVisited.Count}");

        // The distinct positions visited by the guard are the potential locations for obstructions
        HashSet<(int, int)> efectiveObstructions = positionsVisited;

        // Remove the starting position. It is not allowed to place an obstruction at the guard's starting position.
        efectiveObstructions.Remove((x0, y0));

        foreach (var (x, y) in efectiveObstructions)
        {
            // Copy the map to simulate the path with the new obstruction
            string[] mapCopy = (string[])map.Clone();

            // Add new obstruction to the map copy, in the new position, to check if the guard gets stuck in a loop
            mapCopy[x] = mapCopy[x].Substring(0, y) + 'O' + mapCopy[x].Substring(y + 1);

            // Simulate the path of the guard with the new obstruction, to check if it gets stuck in a loop
            List<(int, int, char)> checkLoop;
            bool loopDetected;
            (loopDetected, checkLoop) = SimulateGuardPath(mapCopy, x0, y0, d0);

            // The guard not stuck in a loop (means the obstruction does not cause a loop)
            if (!loopDetected)
            {
                // Remove the obstruction from the set of efective obstructions
                efectiveObstructions.Remove((x, y));
            }
        }

        // Output the number of distinct positions where placing an obstruction causes the guard to get stuck in a loop
        Console.WriteLine($"Distinct obstructions found: {efectiveObstructions.Count}");
    }
}