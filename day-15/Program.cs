/*  Problem Description:

--- Day 15: Warehouse Woes ---
You appear back inside your own mini submarine! Each Historian drives their mini submarine in a different direction; maybe the Chief has his own submarine down here somewhere as well?

You look up to see a vast school of lanternfish swimming past you. On closer inspection, they seem quite anxious, so you drive your mini submarine over to see if you can help.

Because lanternfish populations grow rapidly, they need a lot of food, and that food needs to be stored somewhere. That's why these lanternfish have built elaborate warehouse complexes operated by robots!

These lanternfish seem so anxious because they have lost control of the robot that operates one of their most important warehouses! It is currently running amok, pushing around boxes in the warehouse with no regard for lanternfish logistics or lanternfish inventory management strategies.

Right now, none of the lanternfish are brave enough to swim up to an unpredictable robot so they could shut it off. However, if you could anticipate the robot's movements, maybe they could find a safe option.

The lanternfish already have a map of the warehouse and a list of movements the robot will attempt to make (your puzzle input). The problem is that the movements will sometimes fail as boxes are shifted around, making the actual movements of the robot difficult to predict.

For example:

##########
#..O..O.O#
#......O.#
#.OO..O.O#
#..O@..O.#
#O#..O...#
#O..O..O.#
#.OO.O.OO#
#....O...#
##########

<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
As the robot (@) attempts to move, if there are any boxes (O) in the way, the robot will also attempt to push those boxes. However, if this action would cause the robot or a box to move into a wall (#), nothing moves instead, including the robot. The initial positions of these are shown on the map at the top of the document the lanternfish gave you.

The rest of the document describes the moves (^ for up, v for down, < for left, > for right) that the robot will attempt to make, in order. (The moves form a single giant sequence; they are broken into multiple lines just to make copy-pasting easier. Newlines within the move sequence should be ignored.)

Here is a smaller example to get started:

########
#..O.O.#
##@.O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

<^^>>>vv<v>>v<<
Were the robot to attempt the given sequence of moves, it would push around the boxes as follows:

Initial state:
########
#..O.O.#
##@.O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

Move <:
########
#..O.O.#
##@.O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

Move ^:
########
#.@O.O.#
##..O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

Move ^:
########
#.@O.O.#
##..O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

Move >:
########
#..@OO.#
##..O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

Move >:
########
#...@OO#
##..O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

Move >:
########
#...@OO#
##..O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

Move v:
########
#....OO#
##..@..#
#...O..#
#.#.O..#
#...O..#
#...O..#
########

Move v:
########
#....OO#
##..@..#
#...O..#
#.#.O..#
#...O..#
#...O..#
########

Move <:
########
#....OO#
##.@...#
#...O..#
#.#.O..#
#...O..#
#...O..#
########

Move v:
########
#....OO#
##.....#
#..@O..#
#.#.O..#
#...O..#
#...O..#
########

Move >:
########
#....OO#
##.....#
#...@O.#
#.#.O..#
#...O..#
#...O..#
########

Move >:
########
#....OO#
##.....#
#....@O#
#.#.O..#
#...O..#
#...O..#
########

Move v:
########
#....OO#
##.....#
#.....O#
#.#.O@.#
#...O..#
#...O..#
########

Move <:
########
#....OO#
##.....#
#.....O#
#.#O@..#
#...O..#
#...O..#
########

Move <:
########
#....OO#
##.....#
#.....O#
#.#O@..#
#...O..#
#...O..#
########
The larger example has many more moves; after the robot has finished those moves, the warehouse would look like this:

##########
#.O.O.OOO#
#........#
#OO......#
#OO@.....#
#O#.....O#
#O.....OO#
#O.....OO#
#OO....OO#
##########
The lanternfish use their own custom Goods Positioning System (GPS for short) to track the locations of the boxes. The GPS coordinate of a box is equal to 100 times its distance from the top edge of the map plus its distance from the left edge of the map. (This process does not stop at wall tiles; measure all the way to the edges of the map.)

So, the box shown below has a distance of 1 from the top edge of the map and 4 from the left edge of the map, resulting in a GPS coordinate of 100 * 1 + 4 = 104.

#######
#...O..
#......
The lanternfish would like to know the sum of all boxes' GPS coordinates after the robot finishes moving. In the larger example, the sum of all boxes' GPS coordinates is 10092. In the smaller example, the sum is 2028.

Predict the motion of the robot and boxes in the warehouse. After the robot is finished moving, what is the sum of all boxes' GPS coordinates?

Resume and resolution:
The problem is a simulation problem where we need to simulate the movement of the robot and the boxes in the warehouse.
The robot will move in the direction given by the input, and if there is a box in the way, it will push that box in the same direction.
If the box is pushed to another boxes, all the boxes will be pushed in the same direction.
But if there is a wall in the way, the robot (and the boxes) will not move.
The robot, the boxes and the walls are represented by the characters '@', 'O' and '#' respectively.
The initial state of the warehouse, and the comands of move for the robot are given by the input.
The GPS coordinate of a box is equal to 100 times its distance from the top edge of the map plus its distance from the left edge of the map.
The sum of all boxes' GPS coordinates after the robot finishes moving is the answer to the problem.

Resolution steps:
1. Read the input file and parse the initial state of the warehouse and the commands of move for the robot.
2. Create a function to simulate the movement of the robot and the boxes, recursively.
3. Execute the commands of move for the robot and simulate the movement of the robot and the boxes.
4. Calculate the sum of all boxes' GPS coordinates after the robot finishes moving.
5. Print the result.


-- Part Two ---
The lanternfish use your information to find a safe moment to swim in and turn off the malfunctioning robot! Just as they start preparing a festival in your honor, reports start coming in that a second warehouse's robot is also malfunctioning.

This warehouse's layout is surprisingly similar to the one you just helped. There is one key difference: everything except the robot is twice as wide! The robot's list of movements doesn't change.

To get the wider warehouse's map, start with your original map and, for each tile, make the following changes:

If the tile is #, the new map contains ## instead.
If the tile is O, the new map contains [] instead.
If the tile is ., the new map contains .. instead.
If the tile is @, the new map contains @. instead.
This will produce a new warehouse map which is twice as wide and with wide boxes that are represented by []. (The robot does not change size.)

The larger example from before would now look like this:

####################
##....[]....[]..[]##
##............[]..##
##..[][]....[]..[]##
##....[]@.....[]..##
##[]##....[]......##
##[]....[]....[]..##
##..[][]..[]..[][]##
##........[]......##
####################
Because boxes are now twice as wide but the robot is still the same size and speed, boxes can be aligned such that they directly push two other boxes at once. For example, consider this situation:

#######
#...#.#
#.....#
#..OO@#
#..O..#
#.....#
#######

<vv<<^^<<^^
After appropriately resizing this map, the robot would push around these boxes as follows:

Initial state:
##############
##......##..##
##..........##
##....[][]@.##
##....[]....##
##..........##
##############

Move <:
##############
##......##..##
##..........##
##...[][]@..##
##....[]....##
##..........##
##############

Move v:
##############
##......##..##
##..........##
##...[][]...##
##....[].@..##
##..........##
##############

Move v:
##############
##......##..##
##..........##
##...[][]...##
##....[]....##
##.......@..##
##############

Move <:
##############
##......##..##
##..........##
##...[][]...##
##....[]....##
##......@...##
##############

Move <:
##############
##......##..##
##..........##
##...[][]...##
##....[]....##
##.....@....##
##############

Move ^:
##############
##......##..##
##...[][]...##
##....[]....##
##.....@....##
##..........##
##############

Move ^:
##############
##......##..##
##...[][]...##
##....[]....##
##.....@....##
##..........##
##############

Move <:
##############
##......##..##
##...[][]...##
##....[]....##
##....@.....##
##..........##
##############

Move <:
##############
##......##..##
##...[][]...##
##....[]....##
##...@......##
##..........##
##############

Move ^:
##############
##......##..##
##...[][]...##
##...@[]....##
##..........##
##..........##
##############

Move ^:
##############
##...[].##..##
##...@.[]...##
##....[]....##
##..........##
##..........##
##############
This warehouse also uses GPS to locate the boxes. For these larger boxes, distances are measured from the edge of the map to the closest edge of the box in question. So, the box shown below has a distance of 1 from the top edge of the map and 5 from the left edge of the map, resulting in a GPS coordinate of 100 * 1 + 5 = 105.

##########
##...[]...
##........
In the scaled-up version of the larger example from above, after the robot has finished all of its moves, the warehouse would look like this:

####################
##[].......[].[][]##
##[]...........[].##
##[]........[][][]##
##[]......[]....[]##
##..##......[]....##
##..[]............##
##..@......[].[][]##
##......[][]..[]..##
####################
The sum of these boxes' GPS coordinates is 9021.

Predict the motion of the robot and boxes in this new, scaled-up warehouse. What is the sum of all boxes' final GPS coordinates?

Resume and resolution:
In this part of the problem, each location in the warehouse is doubled in width, following some rules.
The robot's list of movements doesn't change. 
Each box is represented by '[]', and occupies two columns. If it is pushed, both columns are pushed.
The GPS coordinate of a box is measured from the edge of the map to the closest edge of the box in question.

Reasoning for resolution:
The resolution is similar to the first part of the problem, but we have to apply the following updates:
 - It is necessary to apply the rules to double the width of the warehouse.
 - The simulation of the movement of the robot must consider the new format of the boxes.
   In the new format, the robot will push two columns of boxes at once (tie movement).
   So, it is necessary first to verify if the robot can move. If so, then execute the movements.
 - The calculation of the GPS coordinate of a box must consider the new format of the boxes.
*/

class Program
{
    static void Main()
    {
        // Read the simplest input sample
        string[] lines = new string[]
        {
            "########",
            "#..O.O.#",
            "##@.O..#",
            "#...O..#",
            "#.#.O..#",
            "#...O..#",
            "#......#",
            "########",
            "",
            "<^^>>>vv<v>>v<<"
        };

        // Read the larger input sample
        lines = new string[]
        {
            "##########",
            "#..O..O.O#",
            "#......O.#",
            "#.OO..O.O#",
            "#..O@..O.#",
            "#O#..O...#",
            "#O..O..O.#",
            "#.OO.O.OO#",
            "#....O...#",
            "##########",
            "",
            "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
            "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
            "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
            "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
            "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
            "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
            ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
            "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
            "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
            "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^",
        };

        // Read the input file
        lines = File.ReadAllLines("input.txt");

        // Parse the warehouse data from input: warehouse map, robot's commands, robot's initial position, and warehouse dimensions
        (char[,] warehouse, string commands, int robotLine, int robotRow, int mapHeight, int mapWidth) = ParseWarehouseData(lines);

        // Part One - execute the robot commands
        ExecuteRobotCommands(robotLine, robotRow, warehouse, commands, false);

        // Calculate the sum of all boxes' GPS coordinates
        int sum = SumBoxGPSCoordinates(mapHeight, mapWidth, warehouse);

        // Print the result
        Console.WriteLine("The sum of all boxes' GPS coordinates after the robot finishes moving is " + sum + " in the first warehouse.");

        // Part Two - example of building the second warehouse map and executing the robot commands
        /*
        lines = new string[]
        {
            "#######",
            "#...#.#",
            "#.....#",
            "#..OO@#",
            "#..O..#",
            "#.....#",
            "#######",
            "",
            //"<vv<<^^<<^^"
            "<v<^^vv<^^<<^^"
        };
        */

        // Parse again the warehouse data from input: warehouse map, robot's commands, robot's initial position, and warehouse dimensions
        // This is necessary because the warehouse map is changed during the simulation of the robot commands
        (warehouse, commands, robotLine, robotRow, mapHeight, mapWidth) = ParseWarehouseData(lines);

        // Buid the second warehouse map similiar to the first warehouse map, but with double width for boxes
        char[,] warehouse2 = new char[mapHeight, mapWidth * 2];
        // Iterate over the first warehouse
        for (int lineIndex = 0; lineIndex < mapHeight; lineIndex++)
            for (int charIndex = 0; charIndex < mapWidth; charIndex++)
            {
                // Apply the rules to double the width of each tile in the first warehouse
                char c = warehouse[lineIndex, charIndex];
                // If the tile has a robot...
                if (c == '@')
                {
                    // Update the robot's position in the second warehouse. The column index is doubled, the line index remains the same.
                    (robotLine, robotRow) = (lineIndex, charIndex * 2);
                    // In the second warehouse, the two tiles will be '@' and '.'
                    warehouse2[lineIndex, charIndex * 2] = '@';
                    warehouse2[lineIndex, charIndex * 2 + 1] = '.';
                }
                // If the tile has a wall, a box or an empty space...
                else
                {
                    // In the second warehouse, if the tile is a box, then it will be represented by '[]'. Wall and empty space are doubled.
                    warehouse2[lineIndex, charIndex * 2] = c == 'O' ? '[' : c;
                    warehouse2[lineIndex, charIndex * 2 + 1] = c == 'O' ? ']' : c;
                }
            }
        // The width of the warehouse map is doubled
        mapWidth *= 2;

        // Part Two - execute the robot commands
        ExecuteRobotCommands(robotLine, robotRow, warehouse2, commands, false);

        // Calculate the sum of all boxes' GPS coordinates
        sum = SumBoxGPSCoordinates(mapHeight, mapWidth, warehouse2);

        // Print the result
        Console.WriteLine("The sum of all boxes' GPS coordinates after the robot finishes moving is " + sum + " in the second warehouse.");
    }

    /// <summary>
    /// Parse the initial state of the warehouse, it's elements, and the commands of move for the robot.
    /// </summary>
    /// <param name="lines">The lines of the input data.</param>
    /// <returns>A tuple with 
    /// the warehouse map, 
    /// the commands of move for the robot, 
    /// the robot's initial position (line and row), 
    /// and the warehouse dimensions (height and width).
    /// </returns>
    private static (char[,], string, int, int, int, int) ParseWarehouseData(string[] lines)
    {
        // Parse the initial state of the warehouse and the commands of move for the robot
        // First, find the empty line that separates the warehouse map from the commands. 
        // Its index corresponds to the height of the warehouse map.
        int mapHeight = 0;
        for (; mapHeight < lines.Length; mapHeight++)
            if (lines[mapHeight].Trim() == "")
                break;

        // The width of the warehouse map is the length of the first line (or any line of the warehouse map).
        int mapWidth = lines[0].Length;

        // Create the warehouse map and find the initial position of the robot
        char[,] warehouse;
        (int robotLine, int robotRow) = (0, 0);
        warehouse = new char[mapHeight, mapWidth];
        for (int lineIndex = 0; lineIndex < mapHeight; lineIndex++)
            for (int charIndex = 0; charIndex < mapWidth; charIndex++)
            {
                warehouse[lineIndex, charIndex] = lines[lineIndex][charIndex];
                if (lines[lineIndex][charIndex] == '@')
                    (robotLine, robotRow) = (lineIndex, charIndex);
            }

        // Parse the commands of move for the robot
        string commands = "";
        for (int lineIndex = mapHeight + 1; lineIndex < lines.Length; lineIndex++)
            commands += lines[lineIndex].Trim();
        
        // Return the informations gathered from the input
        return (warehouse, commands, robotLine, robotRow, mapHeight, mapWidth);
    }

    /// <summary>
    /// Execute the commands of move for the robot and simulate the movement of the robot and the boxes in the warehouse.
    /// </summary>
    /// <param name="robotLine">The initial line of the robot.</param>
    /// <param name="robotRow">The initial row of the robot.</param>
    /// <param name="warehouse">The warehouse map.</param>
    /// <param name="commands">The commands of move for the robot.</param>
    /// <param name="showMap">A flag that indicates if the warehouse map and the commands of move for the robot should be shown (for debug).</param>
    private static void ExecuteRobotCommands(int robotLine, int robotRow, char[,] warehouse, string commands, bool showMap = false)
    {
        // If the flag showMap is true, then show the warehouse map and the commands of move for the robot
        if (showMap)
            ShowWarehouseMapAndCommands(warehouse, commands);
        // Iterate over the commands of move for the robot
        while (commands.Length > 0)
        {
            // Simulate the movement of the robot
            (_, robotLine, robotRow, _) = SimulateMovement(warehouse, commands, robotLine, robotRow, true);
            // Remove the actual command of move for the robot
            commands = commands.Substring(1);
            // If the flag showMap is true, then show the warehouse map and the commands of move for the robot
            if (showMap)
                ShowWarehouseMapAndCommands(warehouse, commands);
        }
    }

    /// <summary>
    /// Calculate the sum of all boxes' GPS coordinates. 
    /// The GPS coordinate of a box is equal to 100 times its distance from the top edge of the map
    /// plus its distance from the left edge of the map (measured until the left edge of the box).
    /// </summary>
    /// <param name="mapHeight">The height of the warehouse map.</param>
    /// <param name="mapWidth">The width of the warehouse map.</param>
    /// <param name="warehouse">The warehouse map.</param>
    /// <returns>The sum of all boxes' GPS coordinates.</returns>
    private static int SumBoxGPSCoordinates(int mapHeight, int mapWidth, char[,] warehouse)
    {
        // Calculate the sum of all boxes' GPS coordinates after the robot finishes moving
        // Iterate over the warehouse and find the boxes. Calculate the GPS coordinate of each box and add it to the sum.
        int sum = 0;
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (warehouse[y, x] == 'O' || warehouse[y, x] == '[')
                {
                    sum += 100 * y + x;
                    //Console.WriteLine($"Box at ({y}, {x}). GPS coordinate: {100 * y + x}. Sum: {sum}.");
                }
            }
        }
        return sum;
    }

    /// <summary>
    /// Simulate the movement of the robot and the boxes in the warehouse.
    /// </summary>
    /// <param name="warehouse">The warehouse map. It changes during the simulation.</param>
    /// <param name="commands">The commands of move for the robot. Only the first command is effectively used.</param>
    /// <param name="currentRow">The current row for the simulation.</param>
    /// <param name="currentColumn">The current column for the simulation.</param>
    /// <param name="executeMoves">A flag that indicates if the movements are executed or just simulated.</param>
    /// <returns>A tuple with:
    /// a flag that indicates if the robot has moved,
    /// the new positions of the robot (row and column), and
    /// a flag that indicates if a box has effectively moved.
    /// </returns>
    /// <remarks>
    /// The utility of the return values depends on the what is being moved.
    /// If the robot is being moved, the important information is the new position of the robot.
    /// If a box is being moved, the important information is the flag that indicates if the box has moved.
    /// The flag that indicates if there was a box moved is used to fix the check of tied movement.
    /// </remarks>
    /// <seealso cref="SimulateTiedMove"/>
    static (bool, int, int, bool) SimulateMovement(char[,] warehouse, string commands, int currentRow, int currentColumn, 
                                                   bool executeMoves = true)
    {
        // Verify what object is in the current position
        char thing = warehouse[currentRow, currentColumn];
        // If it is a wall, the movement is blocked
        if (thing == '#')
            return (false, currentRow, currentColumn, false);
        // If it is a empty space, or there are no more commands, there is no blocking
        if (thing == '.' || commands.Length == 0)
            return (true, currentRow, currentColumn, false);

        // Get the actual command of move for the robot, and initialize the new position's coordinates
        char actualCommand = commands[0];
        int newRow = currentRow;
        int newCol = currentColumn;

        // Calculate the new position's coordinates based on the actual command
        if (actualCommand == '^')
            newRow--;
        else if (actualCommand == 'v')
            newRow++;
        else if (actualCommand == '<')
            newCol--;
        else if (actualCommand == '>')
            newCol++;

        // Verify if it is possible to move to the new position, and store the result in a variable
        // It is necessary to check only if the object is the robot or if is already in mode of checking the possibility of moving
        // If the object is a box, this is not necessary, or we would check the same thing twice
        bool canMove = true;
        bool movedBox = false;
        if (thing == '@' || !executeMoves)
            canMove = SimulateTiedMove(warehouse, commands, actualCommand, newRow, newCol, false);
        // Verified that the movement is not blocked, we can effectively execute the movement
        if (executeMoves && canMove)
        {
            // Execute the movement
            SimulateTiedMove(warehouse, commands, actualCommand, newRow, newCol, true);
            // Update the warehouse map with the new position of the object
            warehouse[newRow, newCol] = thing;
            // Update the warehouse map with empty space in the old position
            warehouse[currentRow, currentColumn] = '.';
            // Update the current position of the object, and set the flag that the object has moved
            (currentRow, currentColumn) = (newRow, newCol);

            // If the object is a box, then set the flag that a box has effectively moved
            if (thing == '[' || thing == ']')
                movedBox = true;
        }
        // Return the information about the movement
        return (canMove, currentRow, currentColumn, movedBox);
    }

    /// <summary>
    /// Simulate the movement of the robot and the boxes in the warehouse, considering that the two sides of a box has to be moved together.
    /// </summary>
    /// <param name="warehouse">The warehouse map. It changes during the simulation.</param>
    /// <param name="commands">The commands of move for the robot. Only the first command is effectively used.</param>
    private static bool SimulateTiedMove(char[,] warehouse, string commands, char actualCommand, int newRow, int newCol, 
                                          bool executeMoves = true)
    {
        // Do the movement for the new position
        (bool canMove, _, _, bool boxMoved) = SimulateMovement(warehouse, commands, newRow, newCol, executeMoves);

        // If the movement is blocked, just forward the information
        if (!canMove)
            return false;
        // If we are moving vertically, and we had effectively moved a box, leaving an empty space in the position in check,
        // then we have to check one row more to reach the box.
        int checkRow = newRow;
        if (actualCommand == '^')
        {
            if (boxMoved && warehouse[newRow, newCol] == '.')
                checkRow--;
        }
        else if (actualCommand == 'v')
        {
            if (boxMoved && warehouse[newRow, newCol] == '.')
                checkRow++;
        }
        // If we not are moving vertically, then we don't have to tie the movement of the box
        else
            return canMove;
        // If the movement is not blocked, the move is vertical, and if there is a box (formatted as '[]') in the check position
        // then the movement must be done also for a second column of boxes
        // If the new position is the first column of a box, then also make the movement for the second column of the box
        if (warehouse[checkRow, newCol] == '[')
            (canMove, _, _, _) = SimulateMovement(warehouse, commands, newRow, newCol + 1, executeMoves);
        // If the new position is the second column of a box, then also make the movement for the first column of the box
        else if (warehouse[checkRow, newCol] == ']')
            (canMove, _, _, _) = SimulateMovement(warehouse, commands, newRow, newCol - 1, executeMoves);
        // if there is a block in the way of the other column of the box, then all the movement must be blocked
        // Return if the movement is blocked or not
        return canMove;
    }

    /// <summary>
    /// Print the warehouse map and the commands of move for the robot.
    /// </summary>
    /// <param name="warehouse">The warehouse map.</param>
    /// <param name="commands">The commands of move for the robot.</param>
    static void ShowWarehouseMapAndCommands(char[,] warehouse, string commands = "")
    {        
        // Print the warehouse map
        Console.WriteLine("Warehouse:");
        for (int i = 0; i < warehouse.GetLength(0); i++)
        {
            for (int j = 0; j < warehouse.GetLength(1); j++)
                Console.Write(warehouse[i, j]);
            Console.WriteLine();
        }
        // Print the commands of move for the robot, and an empty line
        Console.WriteLine("Commands: " + commands);
        Console.WriteLine();
    }
}