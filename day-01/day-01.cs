/*  Problem Description:

--- Day 1: Historian Hysteria ---
The Chief Historian is always present for the big Christmas sleigh launch, but nobody has seen him in months! Last anyone heard, he was visiting locations that are historically significant to the North Pole; a group of Senior Historians has asked you to accompany them as they check the places they think he was most likely to visit.

As each location is checked, they will mark it on their list with a star. They figure the Chief Historian must be in one of the first fifty places they'll look, so in order to save Christmas, you need to help them get fifty stars on their list before Santa takes off on December 25th.

Collect stars by solving puzzles. Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

You haven't even left yet and the group of Elvish Senior Historians has already hit a problem: their list of locations to check is currently empty. Eventually, someone decides that the best place to check first would be the Chief Historian's office.

Upon pouring into the office, everyone confirms that the Chief Historian is indeed nowhere to be found. Instead, the Elves discover an assortment of notes and lists of historically significant locations! This seems to be the planning the Chief Historian was doing before he left. Perhaps these notes can be used to determine which locations to search?

Throughout the Chief's office, the historically significant locations are listed not by name but by a unique number called the location ID. To make sure they don't miss anything, The Historians split into two groups, each searching the office and trying to create their own complete list of location IDs.

There's just one problem: by holding the two lists up side by side (your puzzle input), it quickly becomes clear that the lists aren't very similar. Maybe you can help The Historians reconcile their lists?

For example:

3   4
4   3
2   5
1   3
3   9
3   3
Maybe the lists are only off by a small amount! To find out, pair up the numbers and measure how far apart they are. Pair up the smallest number in the left list with the smallest number in the right list, then the second-smallest left number with the second-smallest right number, and so on.

Within each pair, figure out how far apart the two numbers are; you'll need to add up all of those distances. For example, if you pair up a 3 from the left list with a 7 from the right list, the distance apart is 4; if you pair up a 9 with a 3, the distance apart is 6.

In the example list above, the pairs and distances would be as follows:

The smallest number in the left list is 1, and the smallest number in the right list is 3. The distance between them is 2.
The second-smallest number in the left list is 2, and the second-smallest number in the right list is another 3. The distance between them is 1.
The third-smallest number in both lists is 3, so the distance between them is 0.
The next numbers to pair up are 3 and 4, a distance of 1.
The fifth-smallest numbers in each list are 3 and 5, a distance of 2.
Finally, the largest number in the left list is 4, while the largest number in the right list is 9; these are a distance 5 apart.
To find the total distance between the left list and the right list, add up the distances between all of the pairs you found. In the example above, this is 2 + 1 + 0 + 1 + 2 + 5, a total distance of 11!

Your actual left and right lists contain many location IDs. What is the total distance between your lists?

--- Part Two ---
Your analysis only confirmed what everyone feared: the two lists of location IDs are indeed very different.

Or are they?

The Historians can't agree on which group made the mistakes or how to read most of the Chief's handwriting, but in the commotion you notice an interesting detail: a lot of location IDs appear in both lists! Maybe the other numbers aren't location IDs at all but rather misinterpreted handwriting.

This time, you'll need to figure out exactly how often each number from the left list appears in the right list. Calculate a total similarity score by adding up each number in the left list after multiplying it by the number of times that number appears in the right list.

Here are the same example lists again:

3   4
4   3
2   5
1   3
3   9
3   3
For these example lists, here is the process of finding the similarity score:

The first number in the left list is 3. It appears in the right list three times, so the similarity score increases by 3 * 3 = 9.
The second number in the left list is 4. It appears in the right list once, so the similarity score increases by 4 * 1 = 4.
The third number in the left list is 2. It does not appear in the right list, so the similarity score does not increase (2 * 0 = 0).
The fourth number, 1, also does not appear in the right list.
The fifth number, 3, appears in the right list three times; the similarity score increases by 9.
The last number, 3, appears in the right list three times; the similarity score again increases by 9.
So, for these example lists, the similarity score at the end of this process is 31 (9 + 4 + 0 + 0 + 9 + 9).

Once again consider your left and right lists. What is their similarity score?
*/

/* Resume and resolution:

The puzzle involves two lists of location IDs found in the Chief Historian's office. 
These lists are supposed to represent historically significant locations, but they are not identical. 
The task is to reconcile these lists by pairing up the numbers and measuring how far apart they are. 
The goal is to determine the total distance between the two lists.

To solve the puzzle, you need to pair the smallest number in the left list 
with the smallest number in the right list, the second smallest with the second smallest, and so on. 
For each pair, you calculate the distance between the two numbers and sum up all these distances to get the total distance. 
An example is provided to illustrate this process, showing how to pair the numbers and calculate the distances step by step.

In Part Two of the puzzle, the task is to calculate a similarity score between two lists of location IDs. 
The initial analysis showed that the lists are very different, but upon closer inspection, it is noticed that many location IDs appear in both lists. 
The goal is to determine how often each number from the left list appears in the right list and use this information to calculate a similarity score.

To solve the puzzle, you need to iterate over the numbers in the left list and count how many times each number appears in the right list.
For each number in the left list, you multiply it by the number of times it appears in the right list and add this product to the similarity score.
*/

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        //int[] leftList = { 3, 4, 2, 1, 3, 3 };
        //int[] rightList = { 4, 3, 5, 3, 9, 3 };

        // Get the list from the file input.txt. Each line contains a number from the left list and a number from the right list, separated with spaces.
        string[] lines = System.IO.File.ReadAllLines("input.txt");
        int[] leftList = new int[lines.Length];
        int[] rightList = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] numbers = lines[i].Split("   ");
            leftList[i] = int.Parse(numbers[0]);
            rightList[i] = int.Parse(numbers[1]);
        } 

        // Sort both lists
        Array.Sort(leftList);
        Array.Sort(rightList);

        // Calculate the total distance
        int totalDistance = 0;
        for (int i = 0; i < leftList.Length; i++)
        {
            int distance = Math.Abs(leftList[i] - rightList[i]);
            totalDistance += distance;
            // Console.WriteLine("| " + leftList[i] + " - " + rightList[i] + " | = " + distance + "  (Total: " + totalDistance + ")");
        }

        // Output the result
        Console.WriteLine("Total Distance: " + totalDistance);

        // Calculate the similarity score
        int similarityScore = 0;
        for (int i = 0; i < leftList.Length; i++)
        {
            int count = rightList.Count(x => x == leftList[i]);
            similarityScore += leftList[i] * count;
            // Console.WriteLine(leftList[i] + " appears " + count + " times in the right list. Score: " + similarityScore);            
        }
        // Output the result
        Console.WriteLine("Similarity Score: " + similarityScore);
    }
}