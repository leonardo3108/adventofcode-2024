/*  Problem Description:

--- Day 14: Restroom Redoubt ---
One of The Historians needs to use the bathroom; fortunately, you know there's a bathroom near an unvisited location on their list, and so you're all quickly teleported directly to the lobby of Easter Bunny Headquarters.

Unfortunately, EBHQ seems to have "improved" bathroom security again after your last visit. The area outside the bathroom is swarming with robots!

To get The Historian safely to the bathroom, you'll need a way to predict where the robots will be in the future. Fortunately, they all seem to be moving on the tile floor in predictable straight lines.

You make a list (your puzzle input) of all of the robots' current positions (p) and velocities (v), one robot per line. For example:

p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3
Each robot's position is given as p=x,y where x represents the number of tiles the robot is from the left wall and y represents the number of tiles from the top wall (when viewed from above). So, a position of p=0,0 means the robot is all the way in the top-left corner.

Each robot's velocity is given as v=x,y where x and y are given in tiles per second. Positive x means the robot is moving to the right, and positive y means the robot is moving down. So, a velocity of v=1,-2 means that each second, the robot moves 1 tile to the right and 2 tiles up.

The robots outside the actual bathroom are in a space which is 101 tiles wide and 103 tiles tall (when viewed from above). However, in this example, the robots are in a space which is only 11 tiles wide and 7 tiles tall.

The robots are good at navigating over/under each other (due to a combination of springs, extendable legs, and quadcopters), so they can share the same tile and don't interact with each other. Visually, the number of robots on each tile in this example looks like this:

1.12.......
...........
...........
......11.11
1.1........
.........1.
.......1...
These robots have a unique feature for maximum bathroom security: they can teleport. When a robot would run into an edge of the space they're in, they instead teleport to the other side, effectively wrapping around the edges. Here is what robot p=2,4 v=2,-3 does for the first few seconds:

Initial state:
...........
...........
...........
...........
..1........
...........
...........

After 1 second:
...........
....1......
...........
...........
...........
...........
...........

After 2 seconds:
...........
...........
...........
...........
...........
......1....
...........

After 3 seconds:
...........
...........
........1..
...........
...........
...........
...........

After 4 seconds:
...........
...........
...........
...........
...........
...........
..........1

After 5 seconds:
...........
...........
...........
.1.........
...........
...........
...........
The Historian can't wait much longer, so you don't have to simulate the robots for very long. Where will the robots be after 100 seconds?

In the above example, the number of robots on each tile after 100 seconds has elapsed looks like this:

......2..1.
...........
1..........
.11........
.....1.....
...12......
.1....1....
To determine the safest area, count the number of robots in each quadrant after 100 seconds. Robots that are exactly in the middle (horizontally or vertically) don't count as being in any quadrant, so the only relevant robots are:

..... 2..1.
..... .....
1.... .....
           
..... .....
...12 .....
.1... 1....
In this example, the quadrants contain 1, 3, 4, and 1 robot. Multiplying these together gives a total safety factor of 12.

Predict the motion of the robots in your list within a space which is 101 tiles wide and 103 tiles tall. What will the safety factor be after exactly 100 seconds have elapsed?


Resume and resolution:

The problem is about predicting the position of robots in a grid and counting the number of robots in each quadrant after 100 seconds. 
The robots move in a straight line and wrap around the edges of the grid. 
The input is a list of robots with their position and velocity. 
Position and velocity are given as x and y coordinates: number of tiles from the left and top walls, and number of tiles moved per second in each direction. 

The quadrants are top-left, top-right, bottom-left, and bottom-right. 
They and are defined by the center of the grid whose coordinates are the middle of the grid in the x and y direction.

The solution is to simulate the movement of the robots and count the number of robots in each quadrant after 100 seconds. 
The safety factor is the product of the number of robots in each quadrant.

Resolution steps:
1. Parse the input to get the position and velocity of each robot.
2. Simulate the movement of the robots for 100 seconds.
3. Count the number of robots in each quadrant.
4. Calculate the safety factor as the product of the number of robots in each quadrant.
5. Print the safety factor.


--- Part Two ---
During the bathroom break, someone notices that these robots seem awfully similar to ones built and used at the North Pole. 
If they're the same type of robots, they should have a hard-coded Easter egg: 
very rarely, most of the robots should arrange themselves into a picture of a Christmas tree.

What is the fewest number of seconds that must elapse for the robots to display the Easter egg?

Resume and reasoning:
The problem is about finding the fewest number of seconds that must elapse for the robots to display a Christmas tree.
we can use the same code to simulate the movement of the robots, but we need to find the time when the robots are arranged in a picture of a Christmas tree.
At any time, we can search for a pattern that looks like a Christmas tree in the grid of robots. 
If we find the pattern, we just need to print the time when the pattern is found.

How to manually find the pattern of a Christmas tree:
1. Run the simulation for 101 x 103 = 10303 seconds.
2. Print the grid representation of the robots at each second in a bitmap image.
3. Open the images and look for a pattern of clustering in x coordinate. We have found the pattern at 23 seconds.
4. Look for a pattern of clustering in y coordinate. We have found the pattern at 89 seconds.
5. These patterns ocurred every 101 seconds, for the x coordinate, and every 103 seconds, for the y coordinate.
6. Find the image where both patterns are found at the same time. So, we have to find the first collision of the two patterns: 23, 124, 225... and 89, 192, 295...
7. Created a python script to find the first collision of the two patterns. The script is in the file "find.py". It found the first collision at 7093 seconds.
8. Verify the pattern at 7093 seconds. The pattern is a Christmas tree. So 7093 is the awnser.

Another way to find the pattern:
1. Run the simulation for 101 x 103 = 10303 seconds.
2. Form the grid representation of the robots at each second.
3. Check if the grid contains a pattern of a square of 5 tiles with robots. If the pattern is found in 5 consecutive lines, print the second.
*/

using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Read the input sample
        string[] lines = new string[] {
            "p=0,4 v=3,-3",
            "p=6,3 v=-1,-3",
            "p=10,3 v=-1,2",
            "p=2,0 v=2,-1",
            "p=0,0 v=1,3",
            "p=3,0 v=-2,-2",
            "p=7,6 v=-1,-3",
            "p=3,0 v=-1,-2",
            "p=9,3 v=2,3",
            "p=7,3 v=-1,2",
            "p=2,4 v=2,-3",
            "p=9,5 v=-3,-3"
        };
        // Define the size of the bathroom for the sample
        (int wide, int tall) bathroom = (11, 7);

        // Parse the input from file        
        lines = File.ReadAllLines("input.txt");
        // Define the actual size of the bathroom
        bathroom = (101, 103);

        // Parse the input to get the position and velocity of each robot. Store them in a list of tuples
        List<(int x, int y, int vx, int vy)> robots = new List<(int x, int y, int vx, int vy)>();
        // Parse the input to get the position and velocity of each robot
        foreach (string line in lines)
        {
            // Parse the line to get the position and velocity of the robot
            string[] parts = line.Split(new[] { ' ', ',', '=', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(parts[1]);
            int y = int.Parse(parts[2]);
            int vx = int.Parse(parts[4]);
            int vy = int.Parse(parts[5]);
            // Add the robot informations to the list
            robots.Add((x, y, vx, vy));
            //Console.WriteLine("Robot@0:  p = (" + x + ", " + y + ")  v = (" + vx + ", " + vy + ")");
        }

        Dictionary<(int x, int y), int> baseGrid = new Dictionary<(int x, int y), int>();
        for (int x = 0; x < bathroom.wide; x++)
            for (int y = 0; y < bathroom.tall; y++)
                baseGrid[(x, y)] = 0;
        OutputGridRepresentation(bathroom, MapRobotCoordinates(robots, baseGrid), 0, "jpg");

        // Simulate the movement of the robots. Iterate over each second.
        for (int i = 0; i < bathroom.wide * bathroom.tall; i++)
        {
            // Iterate over each robot, to update its position based on its velocity
            for (int j = 0; j < robots.Count; j++)
            {
                // retrieve the robot informations
                var robot = robots[j];
                // Update the position of the robot based on its velocity
                robot.x += robot.vx;
                robot.y += robot.vy;
                // Robot teleport to the other side if it reaches the edge of the bathroom
                if (robot.x < 0)
                    robot.x += bathroom.wide;
                else if (robot.x >= bathroom.wide)
                    robot.x -= bathroom.wide;
                if (robot.y < 0)
                    robot.y += bathroom.tall;
                else if (robot.y >= bathroom.tall)
                    robot.y -= bathroom.tall;
                // Update the robot informations in the list
                robots[j] = robot;
            }
            // After 100 seconds, we calculate the safety factor
            if (i == 99)
            {
                // Calculate the safety factor and print it
                Console.WriteLine("Safety factor after exactly 100 seconds: " + CalculateSafetyFactor(bathroom, robots));
            }
            // Output the grid representation of the robots after each second
            OutputGridRepresentation(bathroom, MapRobotCoordinates(robots, baseGrid), i + 1, "jpg");
        }

    }

    /// <summary>
    ///     Calcule the safery factor for the bathroom. It is the product of the number of robots in each quadrant.
    /// </summary>
    /// <param name="bathroom">The size of the bathroom (how many tiles wide and tall)</param>
    /// <param name="robots">The list of robots with their positions and velocities</param>
    /// <returns>The safety factor for the bathroom</returns>
    private static int CalculateSafetyFactor((int wide, int tall) bathroom, List<(int x, int y, int vx, int vy)> robots)
    {
        // Define the center of the bathroom
        (int x, int y) center = (bathroom.wide / 2, bathroom.tall / 2);
        //Console.WriteLine("Center: (" + center.x + ", " + center.y + ")");

        // Count the number of robots in each quadrant. Initialize the counters.
        int top_left = 0, top_right = 0, bottom_left = 0, bottom_right = 0;
        // Iterate over each robot to count the number of robots in each quadrant
        foreach (var robot in robots)
        {
            // Position x below the center: left quadrants
            if (robot.x < center.x)
            {   // Position y below the center: top left quadrant
                if (robot.y < center.y)
                    top_left++;
                // Position y above the center: bottom left quadrant
                else if (robot.y > center.y)
                    bottom_left++;
                // Position y at the center: not in any quadrant
            }
            // Position x above the center: right quadrants
            else if (robot.x > center.x)
            {   // Position y below the center: top right quadrant
                if (robot.y < center.y)
                    top_right++;
                // Position y above the center: bottom right quadrant
                else if (robot.y > center.y)
                    bottom_right++;
                // Position y at the center: not in any quadrant
            }
            // Position x at the center: not in any quadrant
            //Console.WriteLine("Robot@100: (" + robot.x + ", " + robot.y + ")  Quadrants: (" + top_left + ", " + top_right + ", " + bottom_left + ", " + bottom_right + ")");
        }

        // Calculate the safety factor as the product of the number of robots in each quadrant
        int safety_factor = top_left * top_right * bottom_left * bottom_right;
        return safety_factor;
    }

    /// <summary>
    ///     Update the grid with the robots positions. 
    /// </summary>
    /// <param name="robots">The list of robots with their positions and velocities</param>
    /// <param name="baseGrid">The grid to update with the robots positions. All tiles must be initialized to 0</param>
    /// <returns>The updated grid with the robots positions: the counter of robots in each tile</returns>
    private static Dictionary<(int x, int y), int> MapRobotCoordinates(List<(int x, int y, int vx, int vy)> robots, Dictionary<(int x, int y), int> baseGrid)
    {
        // Copy the base grid to avoid modifying it
        Dictionary<(int x, int y), int> grid = new Dictionary<(int x, int y), int>(baseGrid);
        // Update the grid with the robots positions. Iterate over each robot.
        foreach (var robot in robots)
            // Increment the counter of the grid tile where the robot is located
            grid[(robot.x, robot.y)]++;
        // Return the updated grid
        return grid;
    }

    /// <summary>
    /// Output the grid representation of the robots at a given second.
    /// </summary>
    /// <param name="bathroom">Size of the bathroom (how many tiles wide and tall)</param>
    /// <param name="grid">Dictionary of the robots positions</param>
    /// <param name="second">Current second, used to name the output file</param>
    /// <param name="type">Type of the output file: txt, jpg, bmp</param>
    /// <remarks>For the type jpg or bmp, the output is a bitmap image. Only works on Windows.</remarks>
    private static void OutputGridRepresentation((int wide, int tall) bathroom, Dictionary<(int x, int y), int> grid, int second = 0, string type = "txt")
    {   
        // Check if the grid contains a pattern of a square of 5 tiles with robots. Store quantity of consecutive lines containing 5 tiles with robots.
        int linesWithPattern = 0;
        // Iterate over each line of the grid.
        for (int y = 0; y < bathroom.tall; y++)
        {
            // Form a string with the robots positions in the line.
            string line = "";
            for (int x = 0; x < bathroom.wide; x++)
                line += grid[(x, y)] > 0 ? grid[(x, y)].ToString() : ".";
            // Check if the line contains a pattern of 5 tiles with robots.
            if (line.Contains("11111"))
            {
                // Increment the counter of consecutive lines with the pattern.
                linesWithPattern += 1;
                // If the pattern is found in 5 consecutive lines, print the second, and break the loop.
                if (linesWithPattern == 5)
                {
                    Console.WriteLine("May found pattern at " + second);
                    break;
                }
            }
            // If the line does not contain the pattern, reset the counter, to find only consecutive lines with the pattern.
            else
                linesWithPattern = 0;
        }        
        // Define the output filename
        string filename = "output\\" + second.ToString().PadLeft(5, '0');
        // Output the textual grid representation if the type is txt or the pattern is found in 5 consecutive lines.
        if (type == "txt" || linesWithPattern == 5)
            // Write the grid representation to a text file
            using (StreamWriter outputFile = new StreamWriter(filename + ".txt"))
                // Iterate over each line of the grid
                for (int y = 0; y < bathroom.tall; y++)
                {
                    // Form a string with the robots positions in the line
                    string line = "";
                    for (int x = 0; x < bathroom.wide; x++)
                        line += grid[(x, y)] > 0 ? grid[(x, y)].ToString() : ".";
                    // Write the line to the output file
                    outputFile.WriteLine(line);
                }
        // Output the bitmap image of the grid representation if the type is jpg or bmp
        else if (type == "jpg" || type == "bmp")
        {
            // Create a bitmap image with the grid representation
            Bitmap bitmap = new Bitmap(bathroom.wide, bathroom.tall);
            // Iterate over each tile of the grid
            for (int x = 0; x < bathroom.wide; x++)
                for (int y = 0; y < bathroom.tall; y++)
                    // Set the pixel color: white for tiles with robots, black for empty tiles
                    bitmap.SetPixel(x, y, grid[(x, y)] > 0 ? Color.White : Color.Black);
            // Save the bitmap image to a file, using jpg or bmp format
            if (type == "bmp")
                bitmap.Save(filename + ".Bmp", ImageFormat.Bmp);
            else if (type == "jpg")
                bitmap.Save(filename + ".jpg", ImageFormat.Jpeg);
        }
    }    
}