/*  Problem Description:

--- Day 9: Disk Fragmenter ---
Another push of the button leaves you in the familiar hallways of some friendly amphipods! 
Good thing you each somehow got your own personal mini submarine. 
The Historians jet away in search of the Chief, mostly by driving directly into walls.

While The Historians quickly figure out how to pilot these things, you notice an amphipod in the corner struggling with his computer. 
He's trying to make more contiguous free space by compacting all of the files, but his program isn't working; you offer to help.

He shows you the disk map (your puzzle input) he's already generated. For example:

2333133121414131402
The disk map uses a dense format to represent the layout of files and free space on the disk. 
The digits alternate between indicating the length of a file and the length of free space.

So, a disk map like 12345 would represent:
- a one-block file,
- two blocks of free space, 
- a three-block file, 
- four blocks of free space, 
- and then a five-block file. 
 
A disk map like 90909 would represent three nine-block files in a row (with no free space between them).

Each file on disk also has an ID number based on the order of the files as they appear before they are rearranged, starting with ID 0. 
So, the disk map 12345 has three files: a one-block file with ID 0, a three-block file with ID 1, and a five-block file with ID 2. 
Using one character for each block where digits are the file ID and . is free space, the disk map 12345 represents these individual blocks:

0..111....22222
The first example above, 2333133121414131402, represents these individual blocks:

00...111...2...333.44.5555.6666.777.888899
The amphipod would like to move file blocks one at a time from the end of the disk to the leftmost free space block 
(until there are no gaps remaining between file blocks). 
For the disk map 12345, the process looks like this:

0..111....22222
02.111....2222.
022111....222..
0221112...22...
02211122..2....
022111222......
The first example requires a few more steps:

00...111...2...333.44.5555.6666.777.888899
009..111...2...333.44.5555.6666.777.88889.
0099.111...2...333.44.5555.6666.777.8888..
00998111...2...333.44.5555.6666.777.888...
009981118..2...333.44.5555.6666.777.88....
0099811188.2...333.44.5555.6666.777.8.....
009981118882...333.44.5555.6666.777.......
0099811188827..333.44.5555.6666.77........
00998111888277.333.44.5555.6666.7.........
009981118882777333.44.5555.6666...........
009981118882777333644.5555.666............
00998111888277733364465555.66.............
0099811188827773336446555566..............
The final step of this file-compacting process is to update the filesystem checksum. 
To calculate the checksum, add up the result of multiplying each of these blocks' position with the file ID number it contains. 
The leftmost block is in position 0. If a block contains free space, skip it instead.

Continuing the first example, the first few blocks' position multiplied by its file ID number are 
0 * 0 = 0, 1 * 0 = 0, 2 * 9 = 18, 3 * 9 = 27, 4 * 8 = 32, and so on. 
In this example, the checksum is the sum of these, 1928.

Compact the amphipod's hard drive using the process he requested. What is the resulting filesystem checksum?

Resume:
The disk map uses a dense format to represent the layout of files and free space on the disk.
The digits alternate between indicating the length of a file and the length of free space.
Each file on disk also has an ID number based on the order of the files as they appear before they are rearranged, starting with ID 0.
The amphipod would like to move file blocks one at a time from the end of the disk to the leftmost free space block,
 until there are no gaps remaining between file blocks.
The final step of this file-compacting process is to update the filesystem checksum. 
To calculate the checksum, add up the result of multiplying each of these blocks' position with the file ID number it contains.
The leftmost block is in position 0. If a block contains free space, skip it instead.

Resolution steps:
1. Read the input from the file. It contains the disk map.
2. Decode the disk map to list of blocks. 
3. Compact the file blocks: move file blocks to the leftmost free space block, until there are no gaps remaining between file blocks.
4. Calculate the checksum by multiplying the position of the block with the file ID number.
5. Print the checksum.

--- Part Two ---
Upon completion, two things immediately become clear. First, the disk definitely has a lot more contiguous free space, just like the amphipod hoped. Second, the computer is running much more slowly! Maybe introducing all of that file system fragmentation was a bad idea?

The eager amphipod already has a new plan: rather than move individual blocks, he'd like to try compacting the files on his disk by moving whole files instead.

This time, attempt to move whole files to the leftmost span of free space blocks that could fit the file. Attempt to move each file exactly once in order of decreasing file ID number starting with the file with the highest file ID number. If there is no span of free space to the left of a file that is large enough to fit the file, the file does not move.

The first example from above now proceeds differently:

00...111...2...333.44.5555.6666.777.888899
0099.111...2...333.44.5555.6666.777.8888..
0099.1117772...333.44.5555.6666.....8888..
0099.111777244.333....5555.6666.....8888..
00992111777.44.333....5555.6666.....8888..
The process of updating the filesystem checksum is the same; now, this example's checksum would be 2858.

Start over, now compacting the amphipod's hard drive using this new method instead. What is the resulting filesystem checksum?

Resume:
The amphipod would like to move whole files to the leftmost span of free space blocks that could fit the file.
Attempt to move each file exactly once in order of decreasing file ID number starting with the file with the highest file ID number.
If there is no span of free space to the left of a file that is large enough to fit the file, the file does not move.
The process of updating the filesystem checksum is the same.

Resolution steps:
1. Read the input from the file.
2. Decode the disk map to list of blocks and list of all compound blocks.
3. Compact the file blocks: move complete files to the leftmost span of free space blocks that could fit the file.
4. Obtain the list of single blocks from the list of all compound blocks.
5. Calculate the checksum by multiplying the position of the block with the file ID number.
6. Print the checksum.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    /// <summary>
    /// Decode the disk map to list of blocks
    /// </summary>
    /// <param name="diskMap">Disk map, the representation of the layout of files and free space on the disk.</param>
    /// <returns>List of single blocks and list of all compound blocks.</returns>
    private static (List<int>, List<(int, int)>) DecodeDiskMap(string diskMap)
    {
        // Create a List of blocks. Each file block is represented by its ID and free space blocks by -1.
        List<int> blocks = new List<int>();
        // Create a list to store all the blocks, containing the id (or -1 if it is a free space block) and the length of the block
        List<(int, int)> allBlocks = new List<(int, int)>();

        // id is used to assign an ID to each file block
        int id = 0;
        // Loop through each character in the disk map
        for (int i = 0; i < diskMap.Length; i++)
        {
            // Get the length of the block
            int length = int.Parse(diskMap[i].ToString());

            // If the index is even, means that the character represents a file block: assign an ID to the block
            // If the index is odd, means that the character represents a free space block: assign -1 to the block
            int blockId = (i % 2 == 0) ? id : -1;
            // Add to the list of all blocks one block with the id (or -1 if it is a free space block) and the length of the block
            allBlocks.Add((blockId, length));
            // Append each file block to the list of blocks
            for (int j = 0; j < length; j++)
                // The block is represented by its ID (or -1 if it is a free space block)
                blocks.Add(blockId);
            // If the index is even, means that the character represents a file block
            if (blockId >= 0)
                // Increment the ID
                id++;
        }
        // return the list of single blocks and the list of all compound blocks
        return (blocks, allBlocks);
    }

    /// <summary>
    /// Display the disk blocks.
    /// </summary>
    /// <param name="blocks">List of blocks.</param>
    private static void DisplayDiskBlocks(List<int> blocks)
    {
        // Create a string representation of the disk blocks, concatenating the representation of each block
        // Free space blocks are represented by a dot, and file blocks are represented by their ID (last digit)
        string blockString = string.Join("", blocks.Select(b => b == -1 ? "." : b.ToString().Last().ToString()));
        Console.WriteLine("Disk blocks: " + blockString);
    }

    /// <summary>
    /// Move file blocks to the leftmost free space block, until there are no gaps remaining between file blocks.
    /// </summary>
    /// <param name="blocks">List of blocks.</param>
    private static void CompactFileSingleBlocks(List<int> blocks)
    {
        // leftmostBlock is the index of the leftmost block to inspect
        int leftmostBlock = 0;
        // rightmostBlock is the index of the rightmost block to inspect
        int rightmostBlock = blocks.Count - 1;

        // Loop through the blocks
        while (leftmostBlock < rightmostBlock)
        {
            // Found a free space block
            if (blocks[leftmostBlock] == -1)
            {
                // Get the id of the rightmost block
                int id = blocks[rightmostBlock];

                // If the rightmost block is a free space block, no need to move it
                if (id == -1) {
                    // Just move the rightmost block to left
                    rightmostBlock--;
                    // And repeat the process with the same leftmost block
                    continue;
                }

                // Replace the rightmost block with a free space block
                blocks[rightmostBlock] = -1;
                // Move the rightmost block to left
                rightmostBlock--;
                // Replace the leftmost block with the id of the rightmost block
                blocks[leftmostBlock] = id;

                //Console.WriteLine(blocks);
            }
            // Move to the next block
            leftmostBlock++;
        }
    }

    /// <summary>
    /// Transform a list of all compound blocks to a list of single blocks.
    /// </summary>
    /// <param name="allBlocks">List of all compound blocks.</param>
    /// <returns>List of single blocks.</returns>
    private static List<int> GetBlocksFromListOfAllBlocks(List<(int, int)> allBlocks)
    {
        // Create a list of single blocks
        List<int> blocks = new List<int>();
        // Loop through the list of all compound blocks
        foreach (var block in allBlocks)
            // Loop through the length of the compound block
            for (int j = 0; j < block.Item2; j++)
                // Append each block to the list of single blocks
                blocks.Add(block.Item1);
        // Return the list of single blocks
        return blocks;
    }

    /// <summary>
    /// Move whole files to the leftmost span of free space blocks that could fit the file.
    /// </summary>
    /// <param name="allblocks">List of all compound blocks.</param>
    private static void CompactFileCompoundBlocks(List<(int, int)> allblocks)
    {
        // leftmostBlock is the index of the leftmost block to inspect. Start from the beginning of the list.
        int leftmostBlock = 0;
        // Loop through the list of all compound blocks
        while (leftmostBlock < allblocks.Count)
        {
            // Get the id of the leftmost block and its length
            int idMain = allblocks[leftmostBlock].Item1;
            int lengthMain = allblocks[leftmostBlock].Item2;

            // Found a free space block (with id -1)
            if (idMain == -1)
            {
                // rightmostBlock is the index of the rightmost block to inspect. Start from the end of the list.
                int rightmostBlock = allblocks.Count - 1;
                // Loop through the list of all compound blocks after the leftmost block, from right to left
                while (rightmostBlock > leftmostBlock)
                {
                    // Get the id of the rightmost block and its length
                    int idOther = allblocks[rightmostBlock].Item1;
                    int lengthOther = allblocks[rightmostBlock].Item2;

                    // If the rightmost block is a free space block, no need to move it.
                    // If the rightmost block is a file block, but its length is greater than the leftmost block, it cannot be moved.
                    if (idOther == -1 || lengthOther > lengthMain)
                        // So move to the previous block
                        rightmostBlock--;
                    // If the rightmost block is a file block, and its length is less than or equal to the leftmost block, it can be moved.
                    else 
                    {
                        // Move the rightmost block to the leftmost block
                        allblocks[leftmostBlock] = (idOther, lengthOther);
                        // Replace the rightmost block with a free space block of same length
                        allblocks[rightmostBlock] = (-1, lengthOther);
                        // If the length of the rightmost block is less than the length of the leftmost block, leftover free space
                        if (lengthOther < lengthMain)
                            // Insert a new free space block after the leftmost block
                            allblocks.Insert(leftmostBlock + 1, (-1, lengthMain - lengthOther));
                        // Break the loop, to get the next free space block
                        break;
                    }
                }
            }
            // Move to the next block, to find the next free space block
            leftmostBlock++;
        }
    }


    /// <summary>
    ///     Calculate the checksum by adding up the result of multiplying each of these blocks' position with the file ID number it contains.
    /// </summary>
    /// <param name="blocks">List of blocks.</param>
    /// <returns>Checksum.</returns>
    private static long CalculateChecksum(List<int> blocks)
    {
        // Initialize the checksum to 0
        long checksum = 0;
        // Loop through the blocks
        for (int i = 0; i < blocks.Count; i++)
        {
            // Ignore the free space blocks
            if (blocks[i] != -1)
                // Add to the checksum the result of multiplying the position of the block with the file ID number
                checksum += i * blocks[i];
        }
        // Return the checksum
        return checksum;
    }

    /// <summary>
    /// Get the disk map, display the disk blocks, compact the file blocks, and calculate the checksum.
    /// </summary>
    /// <param name="diskMap">Disk map.</param>
    /// <param name="display">Display the disk map informations, or just the checksum?</param>
    private static void OptimizeDiskMap(string diskMap, bool display = true)
    {
        // Optional: Display the initial disk map
        if (display) Console.WriteLine("Initial disk map: " + diskMap);
        // Decode the disk map to disk blocks, file blocks, and free space blocks
        var (blocks, allBlocks) = DecodeDiskMap(diskMap);
        // Optional: Display the disk blocks
        if (display) DisplayDiskBlocks(blocks);
        // Compact the file blocks: move file blocks to the leftmost free space block, until there are no gaps remaining between file blocks
        CompactFileSingleBlocks(blocks);
        // Optional: Display the disk blocks after compacting the file blocks
        if (display) DisplayDiskBlocks(blocks);
        // Calculate the checksum, and display it
        Console.WriteLine("Checksum - Part One: " + CalculateChecksum(blocks));

        // Perform the second part of the puzzle
        // Compact the file blocks: move whole files to the leftmost span of free space blocks that could fit the file
        CompactFileCompoundBlocks(allBlocks);
        // Update the list of single blocks from the list of all compound blocks
        blocks = GetBlocksFromListOfAllBlocks(allBlocks);
        // Optional: Display the disk blocks after compacting the file blocks
        if (display) DisplayDiskBlocks(blocks);
        // Calculate the second checksum, and display it
        Console.WriteLine("Checksum - Part Two: " + CalculateChecksum(blocks));

        // Optional: Display one empty line
        if (display) Console.WriteLine();
    }

    static void Main()
    {
        // Test the DecodeDiskMap method with the simplest sample input
        OptimizeDiskMap("12345", true);

        // Test the DecodeDiskMap method with the sample input
        OptimizeDiskMap("2333133121414131402", true);

        //return;

        // Execute the solution
        Console.WriteLine("Puzzle solution:");
        OptimizeDiskMap(File.ReadAllText("input.txt"), false);
    }
}
