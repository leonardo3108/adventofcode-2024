/*  Problem Description:

--- Day 12: Garden Groups ---
Why not search for the Chief Historian near the gardener and his massive farm? There's plenty of food, so The Historians grab something to eat while they search.

You're about to settle near a complex arrangement of garden plots when some Elves ask if you can lend a hand. They'd like to set up fences around each region of garden plots, but they can't figure out how much fence they need to order or how much it will cost. They hand you a map (your puzzle input) of the garden plots.

Each garden plot grows only a single type of plant and is indicated by a single letter on your map. When multiple garden plots are growing the same type of plant and are touching (horizontally or vertically), they form a region. For example:

AAAA
BBCD
BBCC
EEEC
This 4x4 arrangement includes garden plots growing five different types of plants (labeled A, B, C, D, and E), each grouped into their own region.

In order to accurately calculate the cost of the fence around a single region, you need to know that region's area and perimeter.

The area of a region is simply the number of garden plots the region contains. The above map's type A, B, and C plants are each in a region of area 4. The type E plants are in a region of area 3; the type D plants are in a region of area 1.

Each garden plot is a square and so has four sides. The perimeter of a region is the number of sides of garden plots in the region that do not touch another garden plot in the same region. The type A and C plants are each in a region with perimeter 10. The type B and E plants are each in a region with perimeter 8. The lone D plot forms its own region with perimeter 4.

Visually indicating the sides of plots in each region that contribute to the perimeter using - and |, the above map's regions' perimeters are measured as follows:

+-+-+-+-+
|A A A A|
+-+-+-+-+     +-+
              |D|
+-+-+   +-+   +-+
|B B|   |C|
+   +   + +-+
|B B|   |C C|
+-+-+   +-+ +
          |C|
+-+-+-+   +-+
|E E E|
+-+-+-+
Plants of the same type can appear in multiple separate regions, and regions can even appear within other regions. For example:

OOOOO
OXOXO
OOOOO
OXOXO
OOOOO
The above map contains five regions, one containing all of the O garden plots, and the other four each containing a single X plot.

The four X regions each have area 1 and perimeter 4. The region containing 21 type O plants is more complicated; in addition to its outer edge contributing a perimeter of 20, its boundary with each X region contributes an additional 4 to its perimeter, for a total perimeter of 36.

Due to "modern" business practices, the price of fence required for a region is found by multiplying that region's area by its perimeter. The total price of fencing all regions on a map is found by adding together the price of fence for every region on the map.

In the first example, region A has price 4 * 10 = 40, region B has price 4 * 8 = 32, region C has price 4 * 10 = 40, region D has price 1 * 4 = 4, and region E has price 3 * 8 = 24. So, the total price for the first example is 140.

In the second example, the region with all of the O plants has price 21 * 36 = 756, and each of the four smaller X regions has price 1 * 4 = 4, for a total price of 772 (756 + 4 + 4 + 4 + 4).

Here's a larger example:

RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE
It contains:

A region of R plants with price 12 * 18 = 216.
A region of I plants with price 4 * 8 = 32.
A region of C plants with price 14 * 28 = 392.
A region of F plants with price 10 * 18 = 180.
A region of V plants with price 13 * 20 = 260.
A region of J plants with price 11 * 20 = 220.
A region of C plants with price 1 * 4 = 4.
A region of E plants with price 13 * 18 = 234.
A region of I plants with price 14 * 22 = 308.
A region of M plants with price 5 * 12 = 60.
A region of S plants with price 3 * 8 = 24.
So, it has a total price of 1930.

What is the total price of fencing all regions on your map?

Resume and resolution:
The problem is about calculating the total price of fencing all regions on a map.

Resolution Steps:
1. Parse the input data into a list of strings.
2. Create a map of the garden plots.
3. Create a method to calculate the area and perimeter of a region.
4. Iterate over the garden plots to calculate the area and perimeter of each region.
5. Calculate the price of fencing each region and sum the total price.
6. Print the total price of fencing all regions on the map.

--- Part Two ---
Fortunately, the Elves are trying to order so much fence that they qualify for a bulk discount!

Under the bulk discount, instead of using the perimeter to calculate the price, you need to use the number of sides each region has. Each straight section of fence counts as a side, regardless of how long it is.

Consider this example again:

AAAA
BBCD
BBCC
EEEC
The region containing type A plants has 4 sides, as does each of the regions containing plants of type B, D, and E. However, the more complex region containing the plants of type C has 8 sides!

Using the new method of calculating the per-region price by multiplying the region's area by its number of sides, regions A through E have prices 16, 16, 32, 4, and 12, respectively, for a total price of 80.

The second example above (full of type X and O plants) would have a total price of 436.

Here's a map that includes an E-shaped region full of type E plants:

EEEEE
EXXXX
EEEEE
EXXXX
EEEEE
The E-shaped region has an area of 17 and 12 sides for a price of 204. Including the two regions full of type X plants, this map has a total price of 236.

This map has a total price of 368:

AAAAAA
AAABBA
AAABBA
ABBAAA
ABBAAA
AAAAAA
It includes two regions full of type B plants (each with 4 sides) and a single region full of type A plants (with 4 sides on the outside and 8 more sides on the inside, a total of 12 sides). Be especially careful when counting the fence around regions like the one full of type A plants; in particular, each section of fence has an in-side and an out-side, so the fence does not connect across the middle of the region (where the two B regions touch diagonally). (The Elves would have used the Möbius Fencing Company instead, but their contract terms were too one-sided.)

The larger example from before now has the following updated prices:

A region of R plants with price 12 * 10 = 120.
A region of I plants with price 4 * 4 = 16.
A region of C plants with price 14 * 22 = 308.
A region of F plants with price 10 * 12 = 120.
A region of V plants with price 13 * 10 = 130.
A region of J plants with price 11 * 12 = 132.
A region of C plants with price 1 * 4 = 4.
A region of E plants with price 13 * 8 = 104.
A region of I plants with price 14 * 16 = 224.
A region of M plants with price 5 * 6 = 30.
A region of S plants with price 3 * 6 = 18.
Adding these together produces its new total price of 1206.

What is the new total price of fencing all regions on your map?

Resume and resoning:
The problem is an extension of the previous one, with the addition of a new method of calculating the price of fencing each region.
The new method of calculating the price of fencing each region is by multiplying the region's area by its number of sides.
To know the number of sides of a region, we have to keep track of the sides of the region, just expanding sides adjacents in the same direction.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Input data: first example
        string[] lines = new string[] 
        {
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC"
        };

        // Input data: second example (Part Two)
        lines = new string[]
        {
            "EEEEE",
            "EXXXX",
            "EEEEE",
            "EXXXX",
            "EEEEE"
        };

        // Input data: third example (Part Two)
        lines = new string[]
        {
            "AAAAAA",
            "AAABBA",
            "AAABBA",
            "ABBAAA",
            "ABBAAA",
            "AAAAAA"
        };

        // Input data: second example (Part One)
        /*lines = new string[]
        {
            "OOOOO",
            "OXOXO",
            "OOOOO",
            "OXOXO",
            "OOOOO"
        };
        */

        // Input data: larger example
        lines = new string[]
        {
            "RRRRIICCFF",
            "RRRRIICCCF",
            "VVRRRCCFFF",
            "VVRCCCJFFF",
            "VVVVCJJCFE",
            "VVIVCCJJEE",
            "VVIIICJJEE",
            "MIIIIIJJEE",
            "MIIISIJEEE",
            "MMMISSJEEE"
        };

        // Read the input data from a file
        lines = File.ReadAllLines("input.txt");

        // Parse the input data into a list of strings
        List<string> gardenPlots = lines.ToList();

        // Create a map of the garden plots
        char[,] map = new char[gardenPlots.Count, gardenPlots[0].Length];
        // Fill the map with the garden plots
        for (int i = 0; i < gardenPlots.Count; i++)
            for (int j = 0; j < gardenPlots[i].Length; j++)
                map[i, j] = gardenPlots[i][j];

        // Calculate the total price of fencing all regions on the map
        (int totalPrice, int priceWithDiscount) = CalculateTotalPrice(map);

        // Print the total price of fencing all regions on the map (area * perimeter), and the price with discount (area * sides)
        Console.WriteLine($"The total price of fencing all regions on the map is {totalPrice}.");
        Console.WriteLine($"The total price of fencing all regions on the map with discount is {priceWithDiscount}.");
    }

    /// <summary>
    /// Calculate the total price of fencing all regions on the map.
    /// </summary>
    /// <param name="map">The map of garden plots.</param>
    /// <returns>The total price of fencing all regions on the map, and the price with discount.</returns>
    static (int, int) CalculateTotalPrice(char[,] map)
    {
        // Initialize the total price of fencing all regions on the map, and the price with discount, to 0
        int totalPrice = 0;
        int priceWithDiscount = 0;
        // Get the number of rows and columns of the map
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        // Initialize a matrix to store the visited garden plots
        bool[,] visited = new bool[rows, cols];

        // Iterate over the garden plots to calculate the area and perimeter of each region
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                // If the garden plot has not been visited yet
                if (!visited[i, j])
                {
                    // Initialize the area and perimeter of the region to 0
                    (int area, int perimeter) = (0, 0);
                    // Initialize the sides of the region to an empty list
                    List<(int, int, int, int, int)> sides = new List<(int, int, int, int, int)>();
                    // Calculate the area and perimeter of the region
                    CalculateAreaAndPerimeter(map, visited, i, j, map[i, j], ref area, ref perimeter, ref sides);
                    //Console.WriteLine("------------------------------------------------------------------------------");
                    
                    // Calculate the prices of fencing the region and add it to the total prices (with and without discount). 
                    // The price without discount (part one) is calculated by multiplying the area of the region by the perimeter of the region.
                    totalPrice += area * perimeter;
                    // The price with discount (part two) is calculated by multiplying the area of the region by the number of sides found of the region.
                    priceWithDiscount += area * sides.Count;
                }
        // Return the total price of fencing all regions on the map, and the price with discount
        return (totalPrice, priceWithDiscount);
    }

    /// <summary>
    /// Calculate the area and perimeter of a region.
    /// </summary>
    /// <param name="map">The map of garden plots.</param>
    /// <param name="visited">The matrix of visited garden plots.</param>
    /// <param name="i">The row index of the garden plot.</param>
    /// <param name="j">The column index of the garden plot.</param>
    /// <param name="plant">The type of plant of the region.</param>
    /// <param name="area">The area of the region. It is passed by reference to update the value.</param>
    /// <param name="perimeter">The perimeter of the region. It is passed by reference to update the value.</param>
    /// <param name="sides">The list of sides of the region. It is passed by reference to update the value.</param>
    static void CalculateAreaAndPerimeter(char[,] map, bool[,] visited, int i, int j, char plant, ref int area, ref int perimeter, 
                                          ref List<(int, int, int, int, int)> sides)
    {
        // If the garden plot is out of bounds, has been visited, or does not contain the same type of plant, give up
        if (i < 0 || i >= map.GetLength(0) || j < 0 || j >= map.GetLength(1) || visited[i, j] || map[i, j] != plant)
            return;

        // Mark the garden plot as visited
        visited[i, j] = true;
        // Increment the area of the region by 1 (this garden plot (i, j) is part of the region)
        area++;

        // Calculate the sides of the garden plot (left, right, up, down)
        var plotSides = Sides(map, i, j, plant);
        // Increment the perimeter of the region by the number of sides of the garden plot that do not touch another garden plot in the same region
        perimeter += plotSides.Item1 + plotSides.Item2 + plotSides.Item3 + plotSides.Item4;

        // Process the sides of the region and record them in the list of sides. This is done to avoid counting the same side multiple times.
        // It is done to every side found of the garden plot, in all directions (left, right, up, down)
        if (plotSides.Item1 == 1)
            findSide(i, j, 1, sides);
        if (plotSides.Item2 == 1)
            findSide(i, j, 2, sides);
        if (plotSides.Item3 == 1)
            findSide(i, j, 3, sides);
        if (plotSides.Item4 == 1)
            findSide(i, j, 4, sides);

       //Console.Write($"Location: {i}, {j}, Plant: {plant}, Sides: ({plotSides.Item1}, {plotSides.Item2}, {plotSides.Item3}, {plotSides.Item4}) ");
       //Console.WriteLine($"Area: {area}, Perimeter: {perimeter}, Sides: {sides.Count}");
       ////Console.WriteLine($"Area: {area}, Perimeter: {perimeter}, Sides: [{string.Join(",", sides)}] = {sides.Count}");

        // Recursively try to visit the neighbors of the garden plot, to complete the region and calculate the final area, perimeter and sides
        CalculateAreaAndPerimeter(map, visited, i - 1, j, plant, ref area, ref perimeter, ref sides);
        CalculateAreaAndPerimeter(map, visited, i + 1, j, plant, ref area, ref perimeter, ref sides);
        CalculateAreaAndPerimeter(map, visited, i, j - 1, plant, ref area, ref perimeter, ref sides);
        CalculateAreaAndPerimeter(map, visited, i, j + 1, plant, ref area, ref perimeter, ref sides);
    }

    /// <summary>
    /// Find a side already recorded, adjacent to the garden plot (i, j), in the direction of the side, or record a new side.
    /// </summary>
    /// <param name="i">Location of the garden plot in the row.</param>
    /// <param name="j">Location of the garden plot in the column.</param>
    /// <param name="direction">The direction of the side: 1 (left), 2 (right), 3 (up), 4 (down).</param>
    /// <param name="sides">The list of recorded sides of the region.</param>
    private static void findSide(int i, int j, int direction, List<(int, int, int, int, int)> sides) 
    {
        // Initialize the index of the side found to -1 (not found)
        int sideFoundIndex = -1;
        // Iterate over the sides of the region
        for (int sideIndex = 0; sideIndex < sides.Count; sideIndex++)
        {
            // Check if the current side and the side we are looking for are the same: to the left or to the right
            if (direction == sides[sideIndex].Item5 && (direction == 1 || direction == 2))
            {
                // Check if the current side is downsides from the side we are looking for
                if (sides[sideIndex].Item1 == i && sides[sideIndex].Item2 == j + 1)
                {
                    // Check if the side was not found yet
                    if (sideFoundIndex == -1)
                    {
                        // Augment the side to englobe the actual location (i, j) (up)
                        sides[sideIndex] = (i, j, i, sides[sideIndex].Item4, direction);
                        // Record the side as found
                        sideFoundIndex = sideIndex;
                    }
                    else
                    {
                        // Unite the two sides found
                        sides[sideIndex] = (i, int.Min(sides[sideIndex].Item2, sides[sideFoundIndex].Item2), 
                                            i, int.Max(sides[sideIndex].Item4, sides[sideFoundIndex].Item4), direction);
                        // Remove the side that was found previously
                        sides.RemoveAt(sideFoundIndex);
                        // no need to continue the search, both sides was found and united
                        return;
                    }
                }
                // Check if the current side is upsides from the side we are looking for
                if  (sides[sideIndex].Item3 == i && sides[sideIndex].Item4 == j - 1)
                {
                    // Check if the side was not found yet
                    if (sideFoundIndex == -1)
                    {
                        // Augment the side to englobe the actual location (i, j) (down)
                        sides[sideIndex] = (i, sides[sideIndex].Item2, i, j, direction);
                        // Record the side as found
                        sideFoundIndex = sideIndex;
                    }
                    else
                    {
                        // Unite the two sides found
                        sides[sideIndex] = (i, int.Min(sides[sideIndex].Item2, sides[sideFoundIndex].Item2), 
                                            i, int.Max(sides[sideIndex].Item4, sides[sideFoundIndex].Item4), direction);
                        // Remove the side that was found previously
                        sides.RemoveAt(sideFoundIndex);
                        // no need to continue the search, both sides was found and united
                        return;
                    }
                }
            }
            // Check if the current side and the side we are looking for are the same: to the top or to the bottom
            if (direction == sides[sideIndex].Item5 && (direction == 3 || direction == 4))
            {
                // Check if the current side is rightside from the side we are looking for
                if (sides[sideIndex].Item1 == i + 1 && sides[sideIndex].Item2 == j)
                {
                    // Check if the side was not found yet
                    if (sideFoundIndex == -1)
                    {
                        // Augment the side to englobe the actual location (i, j) (left)
                        sides[sideIndex] = (i, j, sides[sideIndex].Item3, j, direction);
                        // Record the side as found
                        sideFoundIndex = sideIndex;
                    }
                    else
                    {
                        // Unite the two sides found
                        sides[sideIndex] = (int.Min(sides[sideIndex].Item1, sides[sideFoundIndex].Item1), j, 
                                            int.Max(sides[sideIndex].Item3, sides[sideFoundIndex].Item3), j, direction);
                        // Remove the side that was found previously
                        sides.RemoveAt(sideFoundIndex);
                        // no need to continue the search, both sides was found and united
                        return;
                    }
                }
                // Check if the current side is leftside from the side we are looking for
                if (sides[sideIndex].Item3 == i - 1 && sides[sideIndex].Item4 == j)
                {
                    // Check if the side was not found yet
                    if (sideFoundIndex == -1)
                    {
                        // Augment the side to englobe the actual location (i, j) (right)
                        sides[sideIndex] = (sides[sideIndex].Item1, j, i, j, direction);
                        // Record the side as found
                        sideFoundIndex = sideIndex;
                    }
                    else
                    {
                        // Unite the two sides found
                        sides[sideIndex] = (int.Min(sides[sideIndex].Item1, sides[sideFoundIndex].Item1), j, 
                                            int.Max(sides[sideIndex].Item3, sides[sideFoundIndex].Item3), j, direction);
                        // Remove the side that was found previously
                        sides.RemoveAt(sideFoundIndex);
                        // no need to continue the search, both sides was found and united
                        return;
                    }
                }
            }
        }
        // Side not found: it's a new side
        if (sideFoundIndex == -1)
            // Record the new side, starting and ending in the garden plot, to the direction of the side
            sides.Add((i, j, i, j, direction));
    }

    /// <summary>
    /// Check the neighbors, left, right, up, down, of the garden plot, for a different type of plant.
    /// </summary>
    /// <param name="map">The map of garden plots.</param>
    /// <param name="i">The row index of the garden plot.</param>
    /// <param name="j">The column index of the garden plot.</param>
    /// <param name="plant">The type of plant of the region.</param>
    /// <returns>A tuple describing the sides of the garden plot (left, right, up, down): 1 if the side is different, 0 otherwise.</returns>
    private static (int, int, int, int) Sides(char[,] map, int i, int j, char plant)
    {
        // Initialize the sides of the garden plot
        (int, int, int, int) sides;
        // Check the neighbors of the garden plot, for a different type of plant: left
        sides.Item1 = (i == 0 || map[i - 1, j] != plant)? 1 : 0;
        // Check the neighbors of the garden plot, for a different type of plant: right
        sides.Item2 = (i == map.GetLength(0) - 1 || map[i + 1, j] != plant)? 1 : 0;
        // Check the neighbors of the garden plot, for a different type of plant: up
        sides.Item3 = (j == 0 || map[i, j - 1] != plant)? 1 : 0;
        // Check the neighbors of the garden plot, for a different type of plant: down
        sides.Item4 = (j == map.GetLength(1) - 1 || map[i, j + 1] != plant)? 1 : 0;
        // Return the sides of the garden plot
        return sides;
    }
}