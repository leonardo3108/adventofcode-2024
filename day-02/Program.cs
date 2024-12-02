/*  Problem Description:
--- Day 2: Red-Nosed Reports ---
Fortunately, the first location The Historians want to search isn't a long walk from the Chief Historian's office.

While the Red-Nosed Reindeer nuclear fusion/fission plant appears to contain no sign of the Chief Historian, the engineers there run up to you as soon as they see you. Apparently, they still talk about the time Rudolph was saved through molecular synthesis from a single electron.

They're quick to add that - since you're already here - they'd really appreciate your help analyzing some unusual data from the Red-Nosed reactor. You turn to check if The Historians are waiting for you, but they seem to have already divided into groups that are currently searching every corner of the facility. You offer to help with the unusual data.

The unusual data (your puzzle input) consists of many reports, one report per line. Each report is a list of numbers called levels that are separated by spaces. For example:

7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
This example data contains six reports each containing five levels.

The engineers are trying to figure out which reports are safe. The Red-Nosed reactor safety systems can only tolerate levels that are either gradually increasing or gradually decreasing. So, a report only counts as safe if both of the following are true:

The levels are either all increasing or all decreasing.
Any two adjacent levels differ by at least one and at most three.
In the example above, the reports can be found safe or unsafe by checking those rules:

7 6 4 2 1: Safe because the levels are all decreasing by 1 or 2.
1 2 7 8 9: Unsafe because 2 7 is an increase of 5.
9 7 6 2 1: Unsafe because 6 2 is a decrease of 4.
1 3 2 4 5: Unsafe because 1 3 is increasing but 3 2 is decreasing.
8 6 4 4 1: Unsafe because 4 4 is neither an increase or a decrease.
1 3 6 7 9: Safe because the levels are all increasing by 1, 2, or 3.
So, in this example, 2 reports are safe.

Analyze the unusual data from the engineers. How many reports are safe?

--- Part Two ---
The engineers are surprised by the low number of safe reports until they realize they forgot to tell you about the Problem Dampener.

The Problem Dampener is a reactor-mounted module that lets the reactor safety systems tolerate a single bad level in what would otherwise be a safe report. It's like the bad level never happened!

Now, the same rules apply as before, except if removing a single level from an unsafe report would make it safe, the report instead counts as safe.

More of the above example's reports are now safe:

7 6 4 2 1: Safe without removing any level.
1 2 7 8 9: Unsafe regardless of which level is removed.
9 7 6 2 1: Unsafe regardless of which level is removed.
1 3 2 4 5: Safe by removing the second level, 3.
8 6 4 4 1: Safe by removing the third level, 4.
1 3 6 7 9: Safe without removing any level.
Thanks to the Problem Dampener, 4 reports are actually safe!

Update your analysis by handling situations where the Problem Dampener can remove a single level from unsafe reports. How many reports are now safe?
*/

/* Resume and resolution:
You need to analyze data from the Red-Nosed reactor. 
The data consists of multiple reports, each containing a list of numbers called levels. 
The goal is to determine how many of these reports are safe based on specific criteria.

Criteria for Safe Reports:
Monotonic Sequence: The levels in a report must be either all increasing or all decreasing.
Difference Constraint: Any two adjacent levels must differ by at least one and at most three.

To solve this problem, you can follow these steps:
1. Read the input data containing multiple reports.
2. For each report, check if it is safe based on the given criteria.
3. Count the number of safe reports.
4. Print the total number of safe reports.

Part Two:
The Problem Dampener allows the reactor safety systems to tolerate a single bad level in an otherwise safe report.
If removing a single level from an unsafe report would make it safe, the report counts as safe.

To solve this problem, you can follow these steps:
1. Read the input data containing multiple reports.
2. For each report, check if it is safe based on the given criteria.
3. If the report is unsafe, try removing each level and check if the modified report is safe.
4. Count the number of safe reports.
5. Print the total number of safe reports.

*/

using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Read the input data containing multiple reports.
        string[] lines = File.ReadAllLines("input.txt");

        // Initialize a counter to keep track of the number of safe reports.
        int safeReports = 0;
        // Part Two: Calculate the number of safe reports with the Problem Dampener.
        int safeReportsWithDampener = 0;

        // Iterate over each report in the input data.
        foreach (string line in lines)
        {
            // Split the report into individual levels.
            int[] levels = line.Split(' ').Select(int.Parse).ToArray();

            // Check if the report is safe based on the given criteria.
            if (IsSafeReport(levels))
            {
                // Increment the counter if the report is safe.
                safeReports++;
                safeReportsWithDampener++;
            }
            else
            {
                // Try removing each level and check if the modified report is safe.
                for (int i = 0; i < levels.Length; i++)
                {
                    int[] modifiedLevels = levels.Where((_, index) => index != i).ToArray();
                    if (IsSafeReport(modifiedLevels))
                    {
                        safeReportsWithDampener++;
                        break;
                    }
                }
            }
        }
        // Print the total number of safe reports.
        Console.WriteLine($"Number of safe reports: {safeReports}");
        Console.WriteLine($"Number of safe reports with the Problem Dampener: {safeReportsWithDampener}");
    }


    // Function to check if a report is safe based on the given criteria.
    static bool IsSafeReport(int[] levels)
    {
        // Check if the levels are either all increasing or all decreasing.
        bool increasing = true;
        bool decreasing = true;

        for (int i = 1; i < levels.Length; i++)
        {
            if (levels[i] > levels[i - 1])
            {
                decreasing = false;
            }
            else if (levels[i] < levels[i - 1])
            {
                increasing = false;
            }
        }

        // Check if the difference constraint is satisfied.
        for (int i = 1; i < levels.Length; i++)
        {
            if (Math.Abs(levels[i] - levels[i - 1]) < 1 || Math.Abs(levels[i] - levels[i - 1]) > 3)
            {
                return false;
            }
        }

        // Return true if the report is safe based on both criteria.
        return increasing || decreasing;
    }
}