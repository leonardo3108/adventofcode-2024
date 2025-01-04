/*

--- Day 16: Reindeer Maze ---
It's time again for the Reindeer Olympics! This year, the big event is the Reindeer Maze, where the Reindeer compete for the lowest score.

You and The Historians arrive to search for the Chief right as the event is about to start. It wouldn't hurt to watch a little, right?

The Reindeer start on the Start Tile (marked S) facing East and need to reach the End Tile (marked E). They can move forward one tile at a time (increasing their score by 1 point), but never into a wall (#). They can also rotate clockwise or counterclockwise 90 degrees at a time (increasing their score by 1000 points).

To figure out the best place to sit, you start by grabbing a map (your puzzle input) from a nearby kiosk. For example:

###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############
There are many paths through this maze, but taking any of the best paths would incur a score of only 7036. This can be achieved by taking a total of 36 steps forward and turning 90 degrees a total of 7 times:


###############
#.......#....E#
#.#.###.#.###^#
#.....#.#...#^#
#.###.#####.#^#
#.#.#.......#^#
#.#.#####.###^#
#..>>>>>>>>v#^#
###^#.#####v#^#
#>>^#.....#v#^#
#^#.#.###.#v#^#
#^....#...#v#^#
#^###.#.#.#v#^#
#S..#.....#>>^#
###############
Here's a second example:

#################
#...#...#...#..E#
#.#.#.#.#.#.#.#.#
#.#.#.#...#...#.#
#.#.#.#.###.#.#.#
#...#.#.#.....#.#
#.#.#.#.#.#####.#
#.#...#.#.#.....#
#.#.#####.#.###.#
#.#.#.......#...#
#.#.###.#####.###
#.#.#...#.....#.#
#.#.#.#####.###.#
#.#.#.........#.#
#.#.#.#########.#
#S#.............#
#################
In this maze, the best paths cost 11048 points; following one such path would look like this:

#################
#...#...#...#..E#
#.#.#.#.#.#.#.#^#
#.#.#.#...#...#^#
#.#.#.#.###.#.#^#
#>>v#.#.#.....#^#
#^#v#.#.#.#####^#
#^#v..#.#.#>>>>^#
#^#v#####.#^###.#
#^#v#..>>>>^#...#
#^#v###^#####.###
#^#v#>>^#.....#.#
#^#v#^#####.###.#
#^#v#^........#.#
#^#v#^#########.#
#S#>>^..........#
#################
Note that the path shown above includes one 90 degree turn as the very first move, rotating the Reindeer from facing East to facing North.

Analyze your map carefully. What is the lowest score a Reindeer could possibly get?

--- Part Two ---
Now that you know what the best paths look like, you can figure out the best spot to sit.

Every non-wall tile (S, ., or E) is equipped with places to sit along the edges of the tile. While determining which of these tiles would be the best spot to sit depends on a whole bunch of factors (how comfortable the seats are, how far away the bathrooms are, whether there's a pillar blocking your view, etc.), the most important factor is whether the tile is on one of the best paths through the maze. If you sit somewhere else, you'd miss all the action!

So, you'll need to determine which tiles are part of any best path through the maze, including the S and E tiles.

In the first example, there are 45 tiles (marked O) that are part of at least one of the various best paths through the maze:

###############
#.......#....O#
#.#.###.#.###O#
#.....#.#...#O#
#.###.#####.#O#
#.#.#.......#O#
#.#.#####.###O#
#..OOOOOOOOO#O#
###O#O#####O#O#
#OOO#O....#O#O#
#O#O#O###.#O#O#
#OOOOO#...#O#O#
#O###.#.#.#O#O#
#O..#.....#OOO#
###############
In the second example, there are 64 tiles that are part of at least one of the best paths:

#################
#...#...#...#..O#
#.#.#.#.#.#.#.#O#
#.#.#.#...#...#O#
#.#.#.#.###.#.#O#
#OOO#.#.#.....#O#
#O#O#.#.#.#####O#
#O#O..#.#.#OOOOO#
#O#O#####.#O###O#
#O#O#..OOOOO#OOO#
#O#O###O#####O###
#O#O#OOO#..OOO#.#
#O#O#O#####O###.#
#O#O#OOOOOOO..#.#
#O#O#O#########.#
#O#OOO..........#
#################
Analyze your map further. How many tiles are part of at least one of the best paths through the maze?

Your puzzle answer was 483.

Both parts of this puzzle are complete! They provide two gold stars: **

At this point, all that is left is for you to admire your Advent calendar.

If you still want to see it, you can get your puzzle input.


Solution addapted from https://aoc.csokavar.hu/2024/16/. Steps:
1. Parse the input data and fill the map.
2. Implement Dijkstra algorithm to find, for each position and direction, a score or distance.
3. Find the best spots to reach the end position. 
    Best spots are the ones within the best paths, reaching the end position with the lowest score.
4. Print the results.
*/

class Program {
    // Direction vectors: left, up, right, down
    static readonly (int, int)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };

    /// <summary>
    /// Find the best spots to reach the end position. Best spots are the ones within the best paths, reaching the end position with the lowest score.
    /// </summary>
    /// <param name="map">The map</param>
    /// <param name="scores">The scores for the map</param>
    /// <param name="start">The start position and direction</param>
    /// <returns>Number of best spots</returns>
    static int FindBestSpots(Dictionary<(int, int), char> map, Dictionary<((int, int), (int, int)), int> scores, ((int, int), (int, int)) start) {
        // initialize the queue of positions and directions to visit with the start position and score
        var toVisit = new PriorityQueue<((int, int), (int, int)), int>();
        toVisit.Enqueue(start, scores[start]);

        // keep memory of the best spots (position and direction), starting from the start position and direction
        var bestSpots = new HashSet<((int, int) pos, (int, int) dir)> { start };
        // iterate through the positions and directions to visit
        while (toVisit.TryDequeue(out var state, out var remainingScore)) {
            // iterate through the possible movement options
            foreach (var (next, score) in MovementOptions(map, state, forward: true)) {
                // calculate the next remaining score, which is the current remaining score minus the actual score
                var nextRemainingScore = remainingScore - score;
                /// if the next remaining score is the same as the score of the next position and direction
                if (!bestSpots.Contains(next) && scores[next] == nextRemainingScore) {
                    // add the next position and direction to the best spots and mark it to visit
                    bestSpots.Add(next);
                    toVisit.Enqueue(next, nextRemainingScore);
                }
            }
        }
        return bestSpots.DistinctBy(state => state.pos).Count();
    }

    /// <summary>
    ///  Implement Dijkstra algorithm to find, for each position and direction, a score or distance.
    /// </summary>
    /// <param name="map">The map</param>
    /// <param name="end">The end position</param>
    /// <returns>Dictionary with the scores to all tiles and directions</returns>
    static Dictionary<((int, int), (int, int)), int> Dijkstra(Dictionary<(int, int), char> map, (int, int) end) {
        // initialize the dictionary with the scores and the queue of positions and directions to visit
        var scores = new Dictionary<((int, int), (int, int)), int>();
        var toVisit = new PriorityQueue<((int, int), (int, int)), int>();
        foreach (var dir in directions) {
            toVisit.Enqueue((end, dir), 0);
            scores[(end, dir)] = 0;
        }
        // iterate through the positions and directions to find the scores
        while (toVisit.TryDequeue(out var cur, out var totalDistance)) {
            foreach (var (next, actual) in MovementOptions(map, cur, forward: false)) {
                // calculate the next cost: antecessor cost + actual cost
                var nextCost = totalDistance + actual;
                // Check if there is a anterior score, and if the next cost is less than the current cost
                if (!scores.TryGetValue(next, out var currentCost) || nextCost < currentCost) {
                    // update the score and revisit the next position
                    scores[next] = nextCost;
                    toVisit.Enqueue(next, nextCost);
                }
            }
        }
        return scores;
    }

    /// <summary>
    /// Returns the possible movement options for the current state
    /// </summary>
    /// <param name="map">The map</param>
    /// <param name="state">The current state: position and direction</param>
    /// <param name="forward">Keep moving forward?</param>
    /// <returns>Possible movement options</returns>
    static IEnumerable<(((int, int), (int, int)), int cost)> MovementOptions(Dictionary<(int, int), char> map, 
                                                                       ((int y, int x) pos, (int dy, int dx) dir) state, 
                                                                       bool forward) {
        // List of possible movement options (walk forward, turn left, turn right, turn back)
        var result = new List<(((int, int), (int, int)), int)>();
        foreach ((int dy, int dx) in directions) 
        {
            // if the direction is the same as the current direction
            if ((dy, dx) == state.dir) 
            {
                // As we move forward, or backward, find the next position
                var pos = forward ? (state.pos.y + dy, state.pos.x + dx) : (state.pos.y - dy, state.pos.x - dx);
                // if the next position is not a wall, it is a valid move, add 1 to the score
                if (map.GetValueOrDefault(pos) != '#')
                    result.Add(((pos, (dy, dx)), 1));
            }
            // if the direction is not the same as the current direction, add 1000 to the score
            else if ((dy, dx) != (-state.dir.dy, -state.dir.dx))
                result.Add(((state.pos, (dy, dx)), 1000));
        }
        return result;
    }

    public static void Main() {
        // input data from file
        var input = File.ReadLines("input.txt").ToArray();

        // parse input data and fill the map
        var map = new Dictionary<(int, int), char>();
        var start = ((0, 0), (1, 0));
        var end = (0, 0);
        for (int y = 0; y < input.Length; y++)
            for (int x = 0; x < input[y].Length; x++) 
            {
                var key = (x, -y);
                if (input[y][x] == 'S')
                    start = (key, (1, 0));
                else if (input[y][x] == 'E')
                    end = key;
                map[key] = input[y][x];
            }

        // calculate the scores for the map, and print the result
        var scores = Dijkstra(map, end);
        Console.WriteLine("Part one: " + scores[start]);
        // find the best spots
        Console.WriteLine("Part two: " + FindBestSpots(map, scores, start));
    }
}