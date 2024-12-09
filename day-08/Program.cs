/*  Problem Description:

--- Day 8: Resonant Collinearity ---
You find yourselves on the roof of a top-secret Easter Bunny installation.

While The Historians do their thing, you take a look at the familiar huge antenna. Much to your surprise, it seems to have been reconfigured to emit a signal that makes people 0.1% more likely to buy Easter Bunny brand Imitation Mediocre Chocolate as a Christmas gift! Unthinkable!

Scanning across the city, you find that there are actually many such antennas. Each antenna is tuned to a specific frequency indicated by a single lowercase letter, uppercase letter, or digit. You create a map (your puzzle input) of these antennas. For example:

............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............
The signal only applies its nefarious effect at specific antinodes based on the resonant frequencies of the antennas. In particular, an antinode occurs at any point that is perfectly in line with two antennas of the same frequency - but only when one of the antennas is twice as far away as the other. This means that for any pair of antennas with the same frequency, there are two antinodes, one on either side of them.

So, for these two antennas with frequency a, they create the two antinodes marked with #:

..........
...#......
..........
....a.....
..........
.....a....
..........
......#...
..........
..........
Adding a third antenna with the same frequency creates several more antinodes. It would ideally add four antinodes, but two are off the right side of the map, so instead it adds only two:

..........
...#......
#.........
....a.....
........a.
.....a....
..#.......
......#...
..........
..........
Antennas with different frequencies don't create antinodes; A and a count as different frequencies. However, antinodes can occur at locations that contain antennas. In this diagram, the lone antenna with frequency capital A creates no antinodes but has a lowercase-a-frequency antinode at its location:

..........
...#......
#.........
....a.....
........a.
.....a....
..#.......
......A...
..........
..........
The first example has antennas with two different frequencies, so the antinodes they create look like this, plus an antinode overlapping the topmost A-frequency antenna:

......#....#
...#....0...
....#0....#.
..#....0....
....0....#..
.#....A.....
...#........
#......#....
........A...
.........A..
..........#.
..........#.
Because the topmost A-frequency antenna overlaps with a 0-frequency antinode, there are 14 total unique locations that contain an antinode within the bounds of the map.

Calculate the impact of the signal. How many unique locations within the bounds of the map contain an antinode?

Resume and resolution:
The problem is about calculating the number of antinodes in a given map.
The antinode is a point that is perfectly in line with two antennas of the same frequency - 
but only when one of the antennas is twice as far away as the other.
The input is a map of antennas with different frequencies.
The output is the number of unique locations within the bounds of the map contain an antinode.

Resolution Steps:
1. Read the input file and store the antennas.
2. Iterate over each pair of antennas of same frequency and calculate the antinodes.
3. Check if the antinode positions are within the bounds of the map.
4. Store the antinode positions in a set to get the unique locations.
5. Count the number of unique locations within the bounds of the map that contain an antinode.


--- Part Two ---
Watching over your shoulder as you work, one of The Historians asks if you took the effects of resonant harmonics into your calculations.

Whoops!

After updating your model, it turns out that an antinode occurs at any grid position exactly in line with at least two antennas of the same frequency, regardless of distance. This means that some of the new antinodes will occur at the position of each antenna (unless that antenna is the only one of its frequency).

So, these three T-frequency antennas now create many antinodes:

T....#....
...T......
.T....#...
.........#
..#.......
..........
...#......
..........
....#.....
..........
In fact, the three T-frequency antennas are all exactly in line with two antennas, so they are all also antinodes! This brings the total number of antinodes in the above example to 9.

The original example now has 34 antinodes, including the antinodes that appear on every antenna:

##....#....#
.#.#....0...
..#.#0....#.
..##...0....
....0....#..
.#...#A....#
...#..#.....
#....#.#....
..#.....A...
....#....A..
.#........#.
...#......##
Calculate the impact of the signal using this updated model. How many unique locations within the bounds of the map contain an antinode?

Resume and resolution:
The extension of the problem is about calculating the number of antinodes in a given map considering every resonant harmonics.
Not only in the case when one of the antennas is twice as far away as the other, but three times, four times, and so on. 
Including the point where the antennas are located.

Resolution Steps:
1. Read the input file and store the antennas.
2. Iterate over each pair of antennas of same frequency.
3. Calculate the antinodes for each pair of antennas, considering every resonant harmonics, which is 1, 2, 3, 4, ...
4. Stop the calculation when the antinode position is out of the map.
5. Store the antinode positions in a set to get the unique locations.
6. Count the number of unique locations within the bounds of the map that contain an antinode.
*/

using System;
using System.Collections.Generic;
using System.IO;

class Program
{

    /// <summary>
    /// Find the antinode positions for a pair of antennas
    /// </summary>
    /// <param name="a">First antenna position</param>
    /// <param name="b">Second antenna position</param>
    /// <param name="rows">Length of the map</param>
    /// <param name="cols">Width of the map</param>
    /// <param name="antinodeLocations">Set to store the unique antinode locations</param>
    /// <param name="expandedAntinodeLocations">Set to store the expanded unique antinode locations</param>
    private static void FindAntinodes((int, int) a, (int, int) b, int rows, int cols, 
                                      HashSet<(int, int)> antinodeLocations, 
                                      HashSet<(int, int)> expandedAntinodeLocations)
    {
        // Get the positions of the antennas
        int xA = a.Item1;
        int yA = a.Item2;
        int xB = b.Item1;
        int yB = b.Item2;

        // The antinode positions are calculated by the number of harmonics, wich is 1, 2, 3, 4, ...
        int harmonics = 1;

        //string antinodeLocationsStr = "\t\tAntinodes: ";
        while(true)
        {
            // Calculate the antipode positions
            int x = xA + (xB - xA) * harmonics;
            int y = yA + (yB - yA) * harmonics;

            // Check if the antipode positions are within the bounds of the map
            if (x >= 0 && x < cols && y >= 0 && y < rows)
            {
                // Store the antinode position (initial version), only in second harmonic
                if (harmonics == 2)
                    antinodeLocations.Add((x, y));
                // Store the antinode position (expanded version), in all harmonics
                expandedAntinodeLocations.Add((x, y));
                //antinodeLocationsStr += (x, y) + " ";
                // Find the next harmonic
                harmonics++;
            }
            else
                // Break the loop when the antinode position is out of the map
                break;
        }
        //if (harmonics > 2) Console.WriteLine(antinodeLocationsStr);
    }
    static void Main()
    {
        // Sample input
        string[] lines = new string[]
        {
            "............",
            "........0...",
            ".....0......",
            ".......0....",
            "....0.......",
            "......A.....",
            "............",
            "............",
            "........A...",
            ".........A..",
            "............",
            "............"
        };

        // Read the input file
        lines = File.ReadAllLines("input.txt");

        // List to store the antennas
        List<(int, int, char)> antennas = new List<(int, int, char)>();
        // Dictionary to store the position of the antennas for each frequency
        Dictionary<char, List<(int, int)>> positions = new Dictionary<char, List<(int, int)>>();
        // Set to store the unique antinode locations
        HashSet<(int, int)> antinodeLocations = new HashSet<(int, int)>();
        // Set to store the expanded unique antinode locations
        HashSet<(int, int)> expandedAntinodeLocations = new HashSet<(int, int)>();

        // Get the dimensions of the map
        int rows = lines.Length;
        int cols = lines[0].Length;

        // Register the antennas
        //Console.WriteLine("Antennas:");
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                // Check if there is an antenna in the cell
                char frequency = lines[y][x];
                if (frequency != '.')
                {
                    // Store the antenna position and frequency in the list of antennas
                    antennas.Add((x, y, frequency));
                    //Console.WriteLine("\t" + antennas[antennas.Count - 1]);
                    // Store the antenna position in the dictionary of positions for each frequency
                    if (!positions.ContainsKey(frequency))
                    {
                        positions[frequency] = new List<(int, int)>();
                    }
                    positions[frequency].Add((x, y));
                }
            }
        }

        // Get the pairs of antennas with the same frequency and calculate the antinodes
        foreach (char frequency in positions.Keys)
        {
            //Console.WriteLine("Pairs of antennas: " + frequency);
            List<(int, int)> points = positions[frequency];
            // Iterate over the pairs of antennas
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    //Console.WriteLine("\t" + points[i] + " - " + points[j]);

                    // Calculate the antinodes
                    FindAntinodes(points[i], points[j], rows, cols, antinodeLocations, expandedAntinodeLocations);
                    FindAntinodes(points[j], points[i], rows, cols, antinodeLocations, expandedAntinodeLocations);
                }
            }
        }

        // Print the number of antinode unique locations
        Console.WriteLine("Number of unique locations within the bounds of the map that contain an antinode: " + antinodeLocations.Count);
        // Print the number of expanded antinode unique locations
        Console.WriteLine("Number of expanded unique locations within the bounds of the map that contain an antinode: " + expandedAntinodeLocations.Count);
    }
}

