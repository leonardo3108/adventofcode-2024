/*  Problem Description:

--- Day 4: Ceres Search ---
"Looks like the Chief's not here. Next!" One of The Historians pulls out a device and pushes the only button on it. 
After a brief flash, you recognize the interior of the Ceres monitoring station!

As the search for the Chief continues, a small Elf who lives on the station tugs on your shirt; 
she'd like to know if you could help her with her word search (your puzzle input). She only has to find one word: XMAS.

This word search allows words to be horizontal, vertical, diagonal, written backwards, or even overlapping other words. 
It's a little unusual, though, as you don't merely need to find one instance of XMAS - you need to find all of them. 
Here are a few ways XMAS might appear, where irrelevant characters have been replaced with .:


..X...
.SAMX.
.A..A.
XMAS.S
.X....
The actual word search will be full of letters instead. For example:

MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
In this word search, XMAS occurs a total of 18 times; 
here's the same word search again, but where letters not involved in any XMAS have been replaced with .:

....XXMAS.
.SAMXMS...
...S..A...
..A.A.MS.X
XMASAMX.MM
X.....XA.A
S.S.S.S.SS
.A.A.A.A.A
..M.M.M.MM
.X.X.XMASX
Take a look at the little Elf's word search. How many times does XMAS appear?

Resume and resolution:
The problem described in the provided code selection is about helping an Elf find a specific word in a word search.
The word search can contain the word XMAS in various ways, including horizontally, vertically, diagonally, written backward, or overlapping other words.
The goal is to find all instances of the word XMAS in the word search.

Key Points:
Word Search: The word search contains letters, and the word XMAS can appear in various ways.
Word Appearance: The word XMAS can appear horizontally, vertically, diagonally, written backward, or overlapping other words.

Objective:
Find all instances of the word XMAS in the word search and count how many times it appears.
The example provided has 18 instances of the word XMAS.

To solve this problem, you can follow these steps:
1. Parse the word search to identify all instances of the word XMAS.
2. Consider all possible orientations of the word XMAS in the word search.
3. Count the total number of times the word XMAS appears in the word search.
4. Print the total count of XMAS appearances.

--- Part Two ---
The Elf looks quizzically at you. Did you misunderstand the assignment?

Looking for the instructions, you flip over the word search to find that this isn't actually an XMAS puzzle; 
it's an X-MAS puzzle in which you're supposed to find two MAS in the shape of an X. One way to achieve that is like this:

M.S
.A.
M.S
Irrelevant characters have again been replaced with . in the above diagram. Within the X, each MAS can be written forwards or backwards.

Here's the same example from before, but this time all of the X-MASes have been kept instead:

.M.S......
..A..MSMS.
.M.S.MAA..
..A.ASMSM.
.M.S.M....
..........
S.S.S.S.S.
.A.A.A.A..
M.M.M.M.M.
..........
In this example, an X-MAS appears 9 times.

Flip the word search from the instructions back over to the word search side and try again. How many times does an X-MAS appear?

Resume and resolution:
The problem described in the provided code selection is an extension of the previous word search task.
This time, the Elf wants to find two instances of the word MAS in the shape of an X in the word search.
The word MAS can appear in the shape of an X with various orientations, including forward or backward.

Key Points:
Word Search: The word search contains letters, and the word MAS should appear in the shape of an X.
Word Appearance: The word MAS can appear in the shape of an X with different orientations.

Objective:
Find all instances of the word MAS in the shape of an X in the word search and count how many times it appears.
The example provided has 9 instances of the word MAS in the shape of an X.

To solve this problem, you can follow these steps:
1. Parse the word search to identify all instances of the word MAS. Store the positions of the center letter and direction.
2. Find pairs of MAS that form an X shape with opposite directions.
3. Count the total number of times the word MAS appears in the shape of an X.
4. Print the total count of X-MAS appearances.
*/

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Read the word search from the input file.
        string[] wordSearch = File.ReadAllLines("input.txt");

        // Define the word to search for.
        string targetWord = "XMAS";

        // Count the number of times the target word appears in the word search.
        int count = CountWordAppearances(wordSearch, targetWord);

        // Print the total count of the target word appearances.
        Console.WriteLine("XMAS ocurrences: " + count);

        // Define the word to search for in the shape of an X.
        string targetWordX = "MAS";

        // Count the number of times the target word appears in the shape of an X.
        int countX = CountWordAppearances(wordSearch, targetWordX, true);

        // Print the total count of the target word appearances in the shape of an X.
        Console.WriteLine("X-MAS ocurrences: " + countX);
    }

    static int CountWordAppearances(string[] wordSearch, string targetWord, bool isX = false)
    {
        // Define the directions to search for the target word.
        int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
        int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

        // Reduce the directions to search for the target word in the shape of an X.
        if (isX)
        {
            dx = new int[] { 1, 1, -1, -1 };
            dy = new int[] { 1, -1, 1, -1 };
        }

        // Initialize a list to store the positions of the target word.
        // Each position is represented as a tuple containing the row, column, and direction.
        // This list is used to count the number of X-MAS appearances.
        List<(int, int, int)> positions = new List<(int, int, int)>();

        // Initialize the count of target word appearances.
        int count = 0;

        // Iterate through each row of the word search.
        for (int i = 0; i < wordSearch.Length; i++)
        {
            // Iterate through each column of the word search.
            for (int j = 0; j < wordSearch[i].Length; j++)
            {
                // Check if the current cell contains the first letter of the target word.
                if (wordSearch[i][j] == targetWord[0])
                {
                    // Check all directions to search for the target word.
                    for (int k = 0; k < dx.Length; k++)
                    {
                        // Initialize variables to track the current position in the word search.
                        int x = i + dx[k];
                        int y = j + dy[k];
                        int index = 1;

                        // Check if the current direction is valid.
                        while (x >= 0 && x < wordSearch.Length && y >= 0 && y < wordSearch[i].Length && index < targetWord.Length)
                        {
                            // Check if the current cell contains the next letter of the target word.
                            if (wordSearch[x][y] == targetWord[index])
                            {
                                // Move to the next cell in the current direction.
                                x += dx[k];
                                y += dy[k];
                                index++;
                            }
                            else
                            {
                                // Break the loop if the current direction is invalid.
                                break;
                            }
                        }

                        // Increment the count if the target word is found in the current direction.
                        if (index == targetWord.Length)
                        {
                            if (isX)
                            {
                                // Add the position of center letter and the direction to a list.
                                // This list is used to count the number of X-MAS appearances.
                                // Find the center letter
                                int centerX = i + dx[k] * (targetWord.Length / 2);
                                int centerY = j + dy[k] * (targetWord.Length / 2);
                                positions.Add((centerX, centerY, k));
                            }
                            else
                                count++;
                        }
                    }
                }
            }
        }

        // Count the number of X-MAS appearances if the target word is in the shape of an X.
        if (isX)
        {
            // Iterate through each position in the list.
            foreach ((int x, int y, int direction) in positions)
            {
                // find another MAS in the opposite direction
                foreach ((int x2, int y2, int direction2) in positions)
                {
                    // Check if the center positions are the same and the directions are opposite.
                    if ((x, y) == (x2, y2) && direction != direction2)
                    {
                        // Increment the count if the X-MAS shape is found.
                        count++;
                    }
                }
            }
            // Divide the count by 2 to avoid double counting.
            count /= 2;
        }

        // Return the total count of target word appearances.
        return count;
    }
}