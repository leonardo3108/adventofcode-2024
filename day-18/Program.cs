/*  Problem Description:

--- Day 18: RAM Run ---
You and The Historians look a lot more pixelated than you remember. You're inside a computer at the North Pole!

Just as you're about to check out your surroundings, a program runs up to you. "This region of memory isn't safe! The User misunderstood what a pushdown automaton is and their algorithm is pushing whole bytes down on top of us! Run!"

The algorithm is fast - it's going to cause a byte to fall into your memory space once every nanosecond! Fortunately, you're faster, and by quickly scanning the algorithm, you create a list of which bytes will fall (your puzzle input) in the order they'll land in your memory space.

Your memory space is a two-dimensional grid with coordinates that range from 0 to 70 both horizontally and vertically. However, for the sake of example, suppose you're on a smaller grid with coordinates that range from 0 to 6 and the following list of incoming byte positions:

5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0
Each byte position is given as an X,Y coordinate, where X is the distance from the left edge of your memory space and Y is the distance from the top edge of your memory space.

You and The Historians are currently in the top left corner of the memory space (at 0,0) and need to reach the exit in the bottom right corner (at 70,70 in your memory space, but at 6,6 in this example). You'll need to simulate the falling bytes to plan out where it will be safe to run; for now, simulate just the first few bytes falling into your memory space.

As bytes fall into your memory space, they make that coordinate corrupted. Corrupted memory coordinates cannot be entered by you or The Historians, so you'll need to plan your route carefully. You also cannot leave the boundaries of the memory space; your only hope is to reach the exit.

In the above example, if you were to draw the memory space after the first 12 bytes have fallen (using . for safe and # for corrupted), it would look like this:

...#...
..#..#.
....#..
...#..#
..#..#.
.#..#..
#.#....
You can take steps up, down, left, or right. After just 12 bytes have corrupted locations in your memory space, the shortest path from the top left corner to the exit would take 22 steps. Here (marked with O) is one such path:

OO.#OOO
.O#OO#O
.OOO#OO
...#OO#
..#OO#.
.#.O#..
#.#OOOO
Simulate the first kilobyte (1024 bytes) falling onto your memory space. Afterward, what is the minimum number of steps needed to reach the exit?


Resume and solution:

The problem is a simple BFS problem. 
We need to find the shortest path from the start to the end. 
The only difference is that we need to keep track of the corrupted cells. 
We can use a Queue to keep track of the cells that we need to visit. 
We can use a 2D array to keep track of the visited cells, corrupted, and steps.

The time complexity of the solution is O(N) where N is the number of cells in the memory space.

Resolution steps:
- Read the input file and parse the input.
- Create a 2D array to keep track of the visited cells.
- Create a Queue to keep track of the cells that we need to visit.
- Enqueue the start cell.
- While the queue is not empty, dequeue a cell.
- Check if the cell is the exit cell, if so, print the number of steps and return.
- For each neighbor cell, check if it is inside the memory space and if it is not corrupted.
- If it is not corrupted, enqueue the cell and mark it as visited.
- Print "Not found" if the exit cell is not reached.

--- Part Two ---
The Historians aren't as used to moving around in this pixelated universe as you are. 
You're afraid they're not going to be fast enough to make it to the exit before the path is completely blocked.

To determine how fast everyone needs to go, you need to determine the first byte that will cut off the path to the exit.

In the above example, after the byte at 1,1 falls, there is still a path to the exit:

O..#OOO
O##OO#O
O#OO#OO
OOO#OO#
###OO##
.##O###
#.#OOOO
However, after adding the very next byte (at 6,1), there is no longer a path to the exit:

...#...
.##..##
.#..#..
...#..#
###..##
.##.###
#.#....
So, in this example, the coordinates of the first byte that prevents the exit from being reachable are 6,1.

Simulate more of the bytes that are about to corrupt your memory space. 
What are the coordinates of the first byte that will prevent the exit from being reachable from your starting position? 
(Provide the answer as two integers separated by a comma with no other characters.)

Reasoning:
We had refactored the code to do the BFS in a function, so we can call it multiple times.
We keep track of the corrupted cells in a list, before marking them in the visited array.
That is a main loop where the fallen bytes are increased from 1024 to the total number of corrupted cells. (12 in the example)
In each iteration, we build the visited array, mark the corrupted cells, and call the BFS function.
The first time, the BFS function returns a positive number of steps to reach the exit (part one).
The next times, the BFS function continues returning a positive number of steps, until it returns -1.
It means that the exit is not reachable anymore, and we print the coordinates of the last corrupted cell.
*/

class Program
{
    static void Main()
    {
        // input data (example), grid of 7x7, starts with 12 corrupted cells
        string[] lines = new string[]
        {
            "5,4",
            "4,2",
            "4,5",
            "3,0",
            "2,1",
            "6,3",
            "2,4",
            "1,5",
            "0,6",
            "3,3",
            "2,6",
            "5,1",
            "1,2",
            "5,5",
            "2,5",
            "6,5",
            "1,4",
            "0,4",
            "6,4",
            "1,1",
            "6,1",
            "1,0",
            "0,5",
            "1,6",
            "2,0"
        };
        int memoryWidth = 7;
        int fallen = 12;

        // input data from file, grid of 71x71, starts with 1024 corrupted cells
        lines = File.ReadAllLines("input.txt");
        memoryWidth = 71;
        fallen = 1024;

        //Parse the input into a list of corrupted cells
        List<(int, int)> corrupted = new List<(int, int)>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            corrupted.Add((int.Parse(parts[0]), int.Parse(parts[1])));
        }

        // Iterate over the corrupted cells, from 1024 to the total number of corrupted cells
        // The first time, the BFS function returns a positive number of steps to reach the exit (part one).
        bool first = true;
        (int x, int y) = (0,0);
        for (; fallen < corrupted.Count() ; fallen++)
        {
            // Create a 2D array to keep track of the visited cells
            int[,] visited = new int[memoryWidth, memoryWidth];
            // Mark the corrupted cells
            for (int alreadyFallen = 0; alreadyFallen < fallen; alreadyFallen++)
            {
                (x, y) = corrupted[alreadyFallen];
                visited[x, y] = -1;
            }
            // Call the BFS function
            int steps = FindShortestPath(visited);
            // No path to the exit: print the coordinates of the last corrupted cell (part two)
            if (steps == -1) 
            {
                Console.Write("Coordinates of the first byte that will prevent the exit from being reachable from your starting position: ");
                Console.WriteLine(x + "," + y);
                break;
            }
            // Positive number of steps to reach the exit, first time only: print the number of steps (part one)
            else if (first)
            {
                Console.WriteLine("Minimum number of steps needed to reach the exit: " + steps);
                first = false;
            }
            //else Console.WriteLine("Nothing");
        }
    }

    /// <summary>
    /// Find the shortest path from the start to the end.
    /// </summary>
    /// <param name="visited">Grid of visited cells</param>
    /// <returns>Number of steps to reach the exit, or -1 if the exit is not reachable</returns>
    private static int FindShortestPath(int[,] visited)
    {
        // Get the memory width, the directions, and create a queue of cells to visit
        int memoryWidth = visited.GetLength(0);
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        Queue<(int, int)> queue = new Queue<(int, int)>();

        // Enqueue the start cell, mark it as visited, and set the steps to 1 (0 is a non-visited cell)
        queue.Enqueue((0, 0));
        int steps = 1;
        visited[0, 0] = steps;

        // While the queue is not empty, there are cells to visit
        while (queue.Count > 0)
        {
            // Dequeue a cell and get the steps
            (int x, int y) = queue.Dequeue();
            steps = visited[x, y];
            //Console.WriteLine("Dequeue: (" + x + ", " + y + ") => " + (visited[x, y] - 1));

            // Check if the cell is the exit cell. If so, return the number of steps
            if (x == memoryWidth - 1 && y == memoryWidth - 1)
                return visited[x, y] - 1;
            // Check all the four neighbors of the cell
            for (int i = 0; i < 4; i++)
            {
                // Get the neighbor cell
                (int nx, int ny) = (x + dx[i], y + dy[i]);
                //Console.Write("\tNeighbor: (" + nx + ", " + ny);
                // Check if the neighbor cell is inside the memory space and if it is not visited or corrupted
                if (nx >= 0 && nx < memoryWidth && ny >= 0 && ny < memoryWidth && visited[nx, ny] == 0)
                {
                    // Enqueue the neighbor cell, mark it as visited, and increment the steps
                    queue.Enqueue((nx, ny));
                    visited[nx, ny] = steps + 1;
                    //Console.WriteLine(") => " + (visited[nx, ny] - 1) + " => Enqueue.");
                }
                //else if (!(nx >= 0 && nx < memoryWidth && ny >= 0 && ny < memoryWidth))
                //Console.WriteLine(") (border).");
                //else
                //Console.WriteLine(") => " + (visited[nx, ny] - 1) + " (proibited).");
            }
        }
        // All the accessible cells were visited, but the exit cell was not reached
        return -1;
    }

    /// <summary>
    /// Print the grid of visited cells. For debugging purposes.
    /// </summary>
    /// <param name="visited">Grid of visited cells</param>
    private static void PrintGrid(int[,] visited)
    {
        int memoryWidth = visited.GetLength(0);
        // Iterate over the grid and print the visited cells.
        // Prints # for corrupted, . for non-visited, and for visited cells the last digit of the number of steps
        for (int y = 0; y < memoryWidth; y++)
        {
            for (int x = 0; x < memoryWidth; x++)
            {
                if (visited[x, y] == -1)
                    Console.Write("#");
                else if (visited[x, y] == 0)
                    Console.Write(".");
                else
                    Console.Write(visited[x, y] % 10);
            }
            Console.WriteLine();
        }
    }
}