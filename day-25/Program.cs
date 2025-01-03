/*
--- Day 25: Code Chronicle ---
Out of ideas and time, The Historians agree that they should go back to check the Chief Historian's office one last time, 
just in case he went back there without you noticing.

When you get there, you are surprised to discover that the door to his office is locked! You can hear someone inside, but knocking yields no response. 
The locks on this floor are all fancy, expensive, virtual versions of five-pin tumbler locks, 
so you contact North Pole security to see if they can help open the door.

Unfortunately, they've lost track of which locks are installed and which keys go with them, 
so the best they can do is send over schematics of every lock and every key for the floor you're on (your puzzle input).

The schematics are in a cryptic file format, but they do contain manufacturer information, so you look up their support number.

"Our Virtual Five-Pin Tumbler product? That's our most expensive model! Way more secure than--" 
You explain that you need to open a door and don't have a lot of time.

"Well, you can't know whether a key opens a lock without actually trying the key in the lock (due to quantum hidden variables), 
but you can rule out some of the key/lock combinations."

"The virtual system is complicated, but part of it really is a crude simulation of a five-pin tumbler lock, mostly for marketing reasons. 
If you look at the schematics, you can figure out whether a key could possibly fit in a lock."

He transmits you some example schematics:

#####
.####
.####
.####
.#.#.
.#...
.....

#####
##.##
.#.##
...##
...#.
...#.
.....

.....
#....
#....
#...#
#.#.#
#.###
#####

.....
.....
#.#..
###..
###.#
###.#
#####

.....
.....
.....
#....
#.#..
#.#.#
#####
"The locks are schematics that have the top row filled (#) and the bottom row empty (.); the keys have the top row empty and the bottom row filled. 
If you look closely, you'll see that each schematic is actually a set of columns of various heights, 
either extending downward from the top (for locks) or upward from the bottom (for keys)."

"For locks, those are the pins themselves; you can convert the pins in schematics to a list of heights, one per column. 
For keys, the columns make up the shape of the key where it aligns with pins; those can also be converted to a list of heights."

"So, you could say the first lock has pin heights 0,5,3,4,3:"

#####
.####
.####
.####
.#.#.
.#...
.....
"Or, that the first key has heights 5,0,2,1,3:"

.....
#....
#....
#...#
#.#.#
#.###
#####
"These seem like they should fit together; in the first four columns, the pins and key don't overlap. However, this key cannot be for this lock: 
in the rightmost column, the lock's pin overlaps with the key, 
which you know because in that column the sum of the lock height and key height is more than the available space."

"So anyway, you can narrow down the keys you'd need to try by just testing each key with each lock, which means you would have to check... 
wait, you have how many locks? But the only installation that size is at the North--" You disconnect the call.

In this example, converting both locks to pin heights produces:

0,5,3,4,3
1,2,0,5,3
Converting all three keys to heights produces:

5,0,2,1,3
4,3,4,0,2
3,0,2,0,1
Then, you can try every key with every lock:

Lock 0,5,3,4,3 and key 5,0,2,1,3: overlap in the last column.
Lock 0,5,3,4,3 and key 4,3,4,0,2: overlap in the second column.
Lock 0,5,3,4,3 and key 3,0,2,0,1: all columns fit!
Lock 1,2,0,5,3 and key 5,0,2,1,3: overlap in the first column.
Lock 1,2,0,5,3 and key 4,3,4,0,2: all columns fit!
Lock 1,2,0,5,3 and key 3,0,2,0,1: all columns fit!
So, in this example, the number of unique lock/key pairs that fit together without overlapping in any column is 3.

Analyze your lock and key schematics. How many unique lock/key pairs fit together without overlapping in any column?

Resume:
You need to find the number of unique lock/key pairs that fit together without overlapping in any column.
The input is a list of locks and keys. Each lock and key is represented by a string of characters.
The locks are schematics that have the top row filled (#) and the bottom row empty (.); the keys have the top row empty and the bottom row filled.
You need to convert the pins in schematics to a list of heights, one per column.
Then, you can try every key with every lock and find the number of unique lock/key pairs that fit together without overlapping in any column.

Resolution steps:
1. Create a method to convert the pins in schematics to a list of heights, one per column.
2. Create a method to check if a key fits in a lock without overlapping in any column.
3. Create a method to find the number of unique lock/key pairs that fit together without overlapping in any column.
4. Read the input file and convert the locks and keys to a list of heights.
5. Find the number of unique lock/key pairs that fit together without overlapping in any column.
*/

class Program
{
    static void Main()
    {
        // input example
        var input = new string[] {
            "#####",
            ".####",
            ".####",
            ".####",
            ".#.#.",
            ".#...",
            ".....",
            "",
            "#####",
            "##.##",
            ".#.##",
            "...##",
            "...#.",
            "...#.",
            ".....",
            "",
            ".....",
            "#....",
            "#....",
            "#...#",
            "#.#.#",
            "#.###",
            "#####",
            "",
            ".....",
            ".....",
            "#.#..",
            "###..",
            "###.#",
            "###.#",
            "#####",
            "",
            ".....",
            ".....",
            ".....",
            "#....",
            "#.#..",
            "#.#.#",
            "#####"
        };

        // read the input file
        input = File.ReadAllLines("input.txt");

        // parse input into locks and keys information
        var locks = new List<int[]>();
        var keys = new List<int[]>();
        int state = 0;
        int[]? actualLock = null, actualKey = null;
        // available space is the max sum of the lock and key heights. Start with -1 because the first lock will be counted one line more.
        int availableSpace = -1;
        // iterate over the input lines
        foreach (var line in input)
        {
            // if the line is empty
            if (line == "")
            {
                // parse the actual lock or key
                ParseLockAndKey(locks, keys, actualLock, actualKey);
                // reset the state to wait for a new lock or key
                state = 0;
                actualLock = null;
                actualKey = null;
            }
            // if the state is 0 (waiting for a lock or key), and the line starts with '#'
            else if (state == 0 && line[0] == '#')
            {
                // it is a lock: set the state to 1 and start a new lock structure
                state = 1;
                actualLock = new int[line.Length];
            }
            // If the state is 0 (waiting for a lock or key), and the line starts with '.'
            else if (state == 0 && line[0] == '.')
            {
                // it is a key: set the state to 2 and start a new key structure
                state = 2;
                actualKey = new int[line.Length];
            }
            // if the state is 1 (parsing a lock)
            else if (state == 1 && actualLock != null)
            {
                // take the new line and check the heights of the lock. Where there is a '#', increment the actual lock heights.
                for (int i = 0; i < line.Length; i++)
                    if (line[i] == '#')
                        actualLock[i]++;
                // if this is the first lock, let's count the available space too
                if (locks.Count == 0)
                    availableSpace++;
            }
            // if the state is 2 (parsing a key)
            else if (state == 2 && actualKey != null)
            {
                // take the new line and check the heights of the key. Where there is a '#', increment the actual key heights.
                for (int i = 0; i < line.Length; i++)
                    if (line[i] == '#')
                        actualKey[i]++;
            }
        }
        // parse the last lock or key, if it exists
        if (actualLock != null || actualKey != null)
            ParseLockAndKey(locks, keys, actualLock, actualKey);

        // Print the locks and keys, and the available space
        foreach (var lockHeights in locks)
            PrintLockHeights(lockHeights);
        foreach (var keyHeights in keys)
            PrintKeyHeights(keyHeights);
        Console.WriteLine("Available Space: " + availableSpace);

        // find the number of unique lock/key pairs that fit together without overlapping in any column, and print the result
        Console.WriteLine("Quantity of unique lock/key pairs fit together: " + FindNumberOfUniqueLockKeyPairs(locks, keys, availableSpace));
    }
    /// <summary>
    /// Print the key heights, for debugging purposes
    /// </summary>
    /// <param name="keyHeights">Heights of the key</param>
    /// <param name="newLine">If true, print a new line after the key heights</param>
    private static void PrintKeyHeights(int[] keyHeights, bool newLine = true)
    {
        Console.Write("Key: " + string.Join(",", keyHeights) + " ");
        if (newLine)
            Console.WriteLine();
    }

    /// <summary>
    /// Print the lock heights, for debugging purposes
    /// </summary>
    /// <param name="lockHeights">Heights of the lock</param>
    /// <param name="newLine">If true, print a new line after the lock heights</param>
    private static void PrintLockHeights(int[] lockHeights, bool newLine = true)
    {
        Console.Write("Lock: " + string.Join(",", lockHeights) + " ");
        if (newLine)
            Console.WriteLine();
    }

    /// <summary>
    /// Parse the actual lock or key and add it to the locks or keys list
    /// </summary>
    /// <param name="locks">List of locks</param>
    /// <param name="keys">List of keys</param>
    /// <param name="actualLock">The actual lock, if it exists</param>
    /// <param name="actualKey">The actual key, if it exists</param>
    private static void ParseLockAndKey(List<int[]> locks, List<int[]> keys, int[]? actualLock, int[]? actualKey)
    {
        if (actualLock != null)
            locks.Add(actualLock);
        // For the keys, decrement all the heights by 1, because the line at the bottom should not be counted
        if (actualKey != null)
        {
            for (int i = 0; i < actualKey.Length; i++)
                actualKey[i] -= 1;
            keys.Add(actualKey);
        }
    }

    /// <summary>
    /// Check if a key fits in a lock without overlapping in any column
    /// </summary>
    /// <param name="lockHeights">Heights of the lock</param>
    /// <param name="keyHeights">Heights of the key</param>
    /// <param name="availableSpace">Available space</param>
    /// <returns>True if the key fits in the lock without overlapping in any column, otherwise false</returns>
    /// <remarks>Check if the sum of the lock height and key height is more than the available space</remarks> 
    static bool CanKeyFitInLock(int[] lockHeights, int[] keyHeights, int availableSpace)
    {
        // iterate over every column corresponding to the lock and key
        for (int i = 0; i < lockHeights.Length; i++)
            // If the sum of the lock height and key height is more than the available space, the key does not fit in the lock
            if (lockHeights[i] + keyHeights[i] > availableSpace)
                return false;
        // No overlapping in any column: the key fits in the lock
        return true;
    }

    /// <summary>
    /// Find the number of unique lock/key pairs that fit together without overlapping in any column
    /// </summary>
    /// <param name="locks">List of locks</param>
    /// <param name="keys">List of keys</param>
    /// <param name="availableSpace">Available space. The max sum of the lock and key heights</param>
    /// <returns>The number of unique lock/key pairs that fit together without overlapping in any column</returns>
    static int FindNumberOfUniqueLockKeyPairs(List<int[]> locks, List<int[]> keys, int availableSpace)
    {
        var count = 0;
        Console.WriteLine("\nLocks and Keys that fit together without overlapping in any column:");
        // iterate over every combination of lock and key
        foreach (var keyHeights in keys)
            foreach (var lockHeights in locks)
                // If the key fits in the lock without overlapping in any column, increment the count
                if (CanKeyFitInLock(lockHeights, keyHeights, availableSpace))
                {
                    count++;
                    PrintKeyHeights(keyHeights, false);
                    PrintLockHeights(lockHeights, true);

                }
        Console.WriteLine();
        // return the number of unique lock/key pairs that fit together without overlapping in any column
        return count;
    }
}