/*  Problem Description:
 
--- Day 5: Print Queue ---
Satisfied with their search on Ceres, the squadron of scholars suggests subsequently scanning the stationery stacks of sub-basement 17.

The North Pole printing department is busier than ever this close to Christmas, and while The Historians continue their search of this historically significant facility, an Elf operating a very familiar printer beckons you over.

The Elf must recognize you, because they waste no time explaining that the new sleigh launch safety manual updates won't print correctly. Failure to update the safety manuals would be dire indeed, so you offer your services.

Safety protocols clearly indicate that new pages for the safety manuals must be printed in a very specific order. The notation X|Y means that if both page number X and page number Y are to be produced as part of an update, page number X must be printed at some point before page number Y.

The Elf has for you both the page ordering rules and the pages to produce in each update (your puzzle input), but can't figure out whether each update has the pages in the right order.

For example:

47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47
The first section specifies the page ordering rules, one per line. The first rule, 47|53, means that if an update includes both page number 47 and page number 53, then page number 47 must be printed at some point before page number 53. (47 doesn't necessarily need to be immediately before 53; other pages are allowed to be between them.)

The second section specifies the page numbers of each update. Because most safety manuals are different, the pages needed in the updates are different too. The first update, 75,47,61,53,29, means that the update consists of page numbers 75, 47, 61, 53, and 29.

To get the printers going as soon as possible, start by identifying which updates are already in the right order.

In the above example, the first update (75,47,61,53,29) is in the right order:

75 is correctly first because there are rules that put each other page after it: 75|47, 75|61, 75|53, and 75|29.
47 is correctly second because 75 must be before it (75|47) and every other page must be after it according to 47|61, 47|53, and 47|29.
61 is correctly in the middle because 75 and 47 are before it (75|61 and 47|61) and 53 and 29 are after it (61|53 and 61|29).
53 is correctly fourth because it is before page number 29 (53|29).
29 is the only page left and so is correctly last.
Because the first update does not include some page numbers, the ordering rules involving those missing page numbers are ignored.

The second and third updates are also in the correct order according to the rules. Like the first update, they also do not include every page number, and so only some of the ordering rules apply - within each update, the ordering rules that involve missing page numbers are not used.

The fourth update, 75,97,47,61,53, is not in the correct order: it would print 75 before 97, which violates the rule 97|75.

The fifth update, 61,13,29, is also not in the correct order, since it breaks the rule 29|13.

The last update, 97,13,75,29,47, is not in the correct order due to breaking several rules.

For some reason, the Elves also need to know the middle page number of each update being printed. Because you are currently only printing the correctly-ordered updates, you will need to find the middle page number of each correctly-ordered update. In the above example, the correctly-ordered updates are:

75,47,61,53,29
97,61,53,29,13
75,29,13
These have middle page numbers of 61, 53, and 29 respectively. Adding these page numbers together gives 143.

Of course, you'll need to be careful: the actual list of page ordering rules is bigger and more complicated than the above example.

Determine which updates are already in the correct order. What do you get if you add up the middle page number from those correctly-ordered updates?

Resume and resolution:
You need to determine which updates are already in the correct order based on the page ordering rules provided. 
The goal is to find the middle page number of each correctly-ordered update and calculate their sum.

To solve this problem, you can follow these steps:
1. Read the input data containing page ordering rules and updates.
2. Parse the page ordering rules and updates.
3. Implement a function to check if an update is in the correct order based on the rules.
4. Implement a function to find the middle page number of a correctly-ordered update.
5. Iterate over the updates and check if they are in the correct order.
6. Calculate the sum of the middle page numbers from the correctly-ordered updates.
7. Print the sum as the result.

--- Part Two ---
While the Elves get to work printing the correctly-ordered updates, you have a little time to fix the rest of them.

For each of the incorrectly-ordered updates, use the page ordering rules to put the page numbers in the right order. For the above example, here are the three incorrectly-ordered updates and their correct orderings:

75,97,47,61,53 becomes 97,75,47,61,53.
61,13,29 becomes 61,29,13.
97,13,75,29,47 becomes 97,75,47,29,13.
After taking only the incorrectly-ordered updates and ordering them correctly, their middle page numbers are 47, 29, and 47. Adding these together produces 123.

Find the updates which are not in the correct order. What do you get if you add up the middle page numbers after correctly ordering just those updates?

Resume and resolution:
You need to find the updates that are not in the correct order based on the page ordering rules provided.
The goal is to correct the order of the incorrectly-ordered updates and calculate the sum of their middle page numbers.

To solve this problem, you can follow these steps:
1. Read the input data containing page ordering rules and updates.
2. Parse the page ordering rules and updates.
3. Implement a function to check if an update is in the correct order based on the rules.
4. Implement a function to find the middle page number of an update.
5. Iterate over the updates and check if they are in the correct order.
6. For the updates that are not in the correct order, reorder them based on the rules.
7. Calculate the sum of the middle page numbers from the corrected updates.
8. Print the sum as the result.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Read the input data containing page ordering rules and updates.
        string[] lines = File.ReadAllLines("input.txt");

        // Parse the page ordering rules and updates.
        List<(int, int)> rules = new List<(int, int)>();
        List<List<int>> updates = new List<List<int>>();

        // Flag to switch between reading rules and updates. Start with reading rules.
        bool readingRules = true;

        foreach (string line in lines)
        {
            if (readingRules)  // Read the rules until an empty line is found.
            {   // Check if the line is empty to switch to reading the updates.
                if (string.IsNullOrWhiteSpace(line)) 
                    readingRules = false;
                else
                {
                    // Parse the rules and add them to the list.
                    string[] parts = line.Split('|');
                    rules.Add((int.Parse(parts[0]), int.Parse(parts[1]))); 
                }
            }
            else  // Parse the updates and add them to the list.
                updates.Add(line.Split(',').Select(int.Parse).ToList());
        }

        // Function to check if an update is in the correct order based on the rules.
        bool IsUpdateInOrder(List<int> update, List<(int, int)> rules)
        {
            foreach ((int before, int after) in rules)
            {   // if the update contains both before and after, and if before comes after after, the update is not in  the correct order.
                if (update.Contains(before) && update.Contains(after) && update.IndexOf(before) > update.IndexOf(after))
                    return false;
            }
            return true;  // If no rule is violated, the update is in the correct order.
        }

        // Function to find the middle page number of a update.
        int FindMiddlePage(List<int> update)
        {
            return update[update.Count / 2];
        }

        // Function to reorder an update based on the rules.
        List<int> ReorderUpdate(List<int> update, List<(int, int)> rules)
        {
            // Create a dictionary to store the index of each page number in the update.
            Dictionary<int, int> indexMap = new Dictionary<int, int>();

            // Fill the dictionary with the index of each page number in the update.
            for (int i = 0; i < update.Count; i++)
                indexMap[update[i]] = i;

            // Sort the update based on the rules.
            update.Sort((a, b) =>
            {
                // Check if there is a rule that a must come before b.
                if (rules.Any(rule => rule.Item1 == a && rule.Item2 == b))
                    return -1;
                // Check if there is a rule that b must come before a.
                if (rules.Any(rule => rule.Item1 == b && rule.Item2 == a))
                    return 1;
                // If there is no rule, compare the index of a and b in the update.
                return indexMap[a].CompareTo(indexMap[b]);
            });

            // Return the reordered update.
            return update;
        }

        // Iterate over the updates
        int resultForCorrectOrderUpdates = 0;
        int resultForIncorrectOrderUpdates = 0;

        foreach (List<int> update in updates)
        {
            // For the correctly-ordered updates, and acumulate the middle page numbers.
            if (IsUpdateInOrder(update, rules))
            {
                resultForCorrectOrderUpdates += FindMiddlePage(update);
            }
            // For the incorrectly-ordered updates, reorder them based on the rules, and acumulate the middle page numbers.
            else 
            {
                resultForIncorrectOrderUpdates += FindMiddlePage(ReorderUpdate(update, rules));
            }
        }

        // Print the sum of the middle page numbers from the correctly-ordered and incorrectly-ordered updates. 
        Console.WriteLine("Adding up the middle page numbers of the correctly-ordered updates gives: " + resultForCorrectOrderUpdates);
        Console.WriteLine("Adding up the middle page numbers of the incorrectly-ordered updates gives: " + resultForIncorrectOrderUpdates);
    }
}
