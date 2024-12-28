/*  Problem Description:

--- Day 19: Linen Layout ---
Today, The Historians take you up to the hot springs on Gear Island! Very suspiciously, absolutely nothing goes wrong as they begin their careful search of the vast field of helixes.

Could this finally be your chance to visit the onsen next door? Only one way to find out.

After a brief conversation with the reception staff at the onsen front desk, you discover that you don't have the right kind of money to pay the admission fee. However, before you can leave, the staff get your attention. Apparently, they've heard about how you helped at the hot springs, and they're willing to make a deal: if you can simply help them arrange their towels, they'll let you in for free!

Every towel at this onsen is marked with a pattern of colored stripes. There are only a few patterns, but for any particular pattern, the staff can get you as many towels with that pattern as you need. Each stripe can be white (w), blue (u), black (b), red (r), or green (g). So, a towel with the pattern ggr would have a green stripe, a green stripe, and then a red stripe, in that order. (You can't reverse a pattern by flipping a towel upside-down, as that would cause the onsen logo to face the wrong way.)

The Official Onsen Branding Expert has produced a list of designs - each a long sequence of stripe colors - that they would like to be able to display. You can use any towels you want, but all of the towels' stripes must exactly match the desired design. So, to display the design rgrgr, you could use two rg towels and then an r towel, an rgr towel and then a gr towel, or even a single massive rgrgr towel (assuming such towel patterns were actually available).

To start, collect together all of the available towel patterns and the list of desired designs (your puzzle input). For example:

r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb
The first line indicates the available towel patterns; in this example, the onsen has unlimited towels with a single red stripe (r), unlimited towels with a white stripe and then a red stripe (wr), and so on.

After the blank line, the remaining lines each describe a design the onsen would like to be able to display. In this example, the first design (brwrr) indicates that the onsen would like to be able to display a black stripe, a red stripe, a white stripe, and then two red stripes, in that order.

Not all designs will be possible with the available towels. In the above example, the designs are possible or impossible as follows:

brwrr can be made with a br towel, then a wr towel, and then finally an r towel.
bggr can be made with a b towel, two g towels, and then an r towel.
gbbr can be made with a gb towel and then a br towel.
rrbgbr can be made with r, rb, g, and br.
ubwu is impossible.
bwurrg can be made with bwu, r, r, and g.
brgr can be made with br, g, and r.
bbrgwb is impossible.
In this example, 6 of the eight designs are possible with the available towel patterns.

To get into the onsen as soon as possible, consult your list of towel patterns and desired designs carefully. How many designs are possible?

Resume:
- The onsen has unlimited towels with a single red stripe (r), unlimited towels with a white stripe and then a red stripe (wr), and so on.
- The remaining lines each describe a design the onsen would like to be able to display.
- Not all designs will be possible with the available towels.
- How many designs are possible?

Resolution steps:
- Read the input file and parse the available towel patterns and the list of desired designs.
- In each design, find all possible occurrences of the towel patterns.
- Using each possible occurrence of the towel patterns, build links between the positions of the patterns in the designs (a graph).
- Clean the graph by removing the positions with no links.
- Count the possible designs: the designs with links in the graph.
- Print the count of possible designs.


--- Part Two ---
The staff don't really like some of the towel arrangements you came up with. 
To avoid an endless cycle of towel rearrangement, maybe you should just give them every possible option.

Here are all of the different ways the above example's designs can be made:

brwrr can be made in two different ways: b, r, wr, r or br, wr, r.

bggr can only be made with b, g, g, and r.

gbbr can be made 4 different ways:

g, b, b, r
g, b, br
gb, b, r
gb, br
rrbgbr can be made 6 different ways:

r, r, b, g, b, r
r, r, b, g, br
r, r, b, gb, r
r, rb, g, b, r
r, rb, g, br
r, rb, gb, r
bwurrg can only be made with bwu, r, r, and g.

brgr can be made in two different ways: b, r, g, r or br, g, r.

ubwu and bbrgwb are still impossible.

Adding up all of the ways the towels in this example could be arranged into the desired designs yields 16 (2 + 1 + 4 + 6 + 1 + 2).

They'll let you into the onsen as soon as you have the list. What do you get if you add up the number of different ways you could make each design?

Resume:
- Calculate the number of different ways you could make each design.
- Add up the number of different ways you could make each design.
- Print the result.

Resolution steps:
- Percurring all the ways in the graph is a complex task, we just need to count the different ways.
- We go backwards from the end to the start, counting the ways to reach the end (the last position), and storing the count in a dictionary.
- The last position has only one way
- Each position has next positions. Each next position already has a count. So, the count of the current position is the sum of the counts of the next positions.
- The total count is count of the first position.
- Sum the total count of all designs.
- Print the total count of different ways you could make each design.
*/

class Program
{
    static void Main()
    {
        // input data (sample)
        string[] lines = new string[]
        {
            "r, wr, b, g, bwu, rb, gb, br",
            "",
            "brwrr",
            "bggr",
            "gbbr",
            "rrbgbr",
            "ubwu",
            "bwurrg",
            "brgr",
            "bbrgwb"
        };
        // input data from file
        lines = File.ReadAllLines("input.txt");

        // store the available towel patterns in a set
        HashSet<string> towelPatterns = new HashSet<string>();

        // read the available towel patterns. first lines (until empty line) are the available towel patterns
        // track the middle line to separate the available towel patterns from the desired designs
        int middleLine = 0;
        int maxPatternSize = 0;
        // loop until the empty line
        while (lines[middleLine] != "")
        {
            // split the patterns by comma and space
            string[] patterns = lines[middleLine].Split(", ");
            // add the patterns to the towel patterns set
            foreach (string pattern in patterns) 
            {
                towelPatterns.Add(pattern);
                maxPatternSize = Math.Max(maxPatternSize, pattern.Length);                
            }
            middleLine++;
        }
        //Console.WriteLine("Towel patterns: " + string.Join(", ", towelPatterns) + ". Max pattern size: " + maxPatternSize + ".");

        // parse the remaining lines to explore the towel designs
        string[] designs = new string[lines.Length - middleLine - 1];
        for (int i = 0; i < designs.Length; i++)
            designs[i] = lines[middleLine + 1 + i];

        // structures to store the links between the positions of the patterns in the designs - direct and reverse
        Dictionary<(string, int), List<int>> nextPositions = new Dictionary<(string, int), List<int>>();
        Dictionary<(string, int), List<int>> previousPositions = new Dictionary<(string, int), List<int>>();
        foreach (string design in designs)
        {
            // store the links between the positions of the patterns in the designs - direct and reverse
            // iterate over the towel patterns
            foreach (string pattern in towelPatterns)
            {
                // Find the first occurrence of the pattern in the design
                int previousPosition = design.IndexOf(pattern);
                int patternSize = pattern.Length;
                while (previousPosition >= 0)
                {
                    // calculate the position after the pattern
                    int nextPosition = previousPosition + patternSize;

                    // store the links between the positions of the patterns in the designs - direct
                    if (!nextPositions.ContainsKey((design, previousPosition)))
                        nextPositions[(design, previousPosition)] = new List<int>();
                    nextPositions[(design, previousPosition)].Add(nextPosition);
                    // store the links between the positions of the patterns in the designs - reverse
                    if (!previousPositions.ContainsKey((design, nextPosition)))
                        previousPositions[(design, nextPosition)] = new List<int>();
                    previousPositions[(design, nextPosition)].Add(previousPosition);

                    // find the next occurrence of the pattern in the design
                    previousPosition = design.IndexOf(pattern, previousPosition + 1);
                }
            }

            // remove nodes with no previous or next positions - clean the graph
            bool removed = false;
            do {
                removed = false;
                // remove nodes with no previous positions
                for (int baseIndex = 1; baseIndex < design.Length; baseIndex++)
                {   
                    if (!previousPositions.ContainsKey((design, baseIndex)))
                    {
                        // remove links from the next positions
                        for (int otherIndex = baseIndex + 1; otherIndex < design.Length; otherIndex++)
                            if (previousPositions.ContainsKey((design, otherIndex)) && previousPositions[(design, otherIndex)].Contains(baseIndex))
                            {
                                if (previousPositions[(design, otherIndex)].Count <= 1)
                                    previousPositions.Remove((design, otherIndex));
                                else
                                    previousPositions[(design, otherIndex)].Remove(baseIndex);
                                removed = true;
                            }
                        // remove the node
                        nextPositions.Remove((design, baseIndex));
                    }
                }
                // remove nodes with no next positions
                for (int baseIndex = 1; baseIndex < design.Length; baseIndex++)
                {   
                    if (!nextPositions.ContainsKey((design, baseIndex)))
                    {
                        // remove links 
                        for (int otherIndex = 0; otherIndex < baseIndex; otherIndex++)
                            if (nextPositions.ContainsKey((design, otherIndex)) && nextPositions[(design, otherIndex)].Contains(baseIndex))
                            {
                                if (nextPositions[(design, otherIndex)].Count <= 1)
                                    nextPositions.Remove((design, otherIndex));
                                else
                                    nextPositions[(design, otherIndex)].Remove(baseIndex);
                                removed = true;
                            }
                        // remove the node
                        previousPositions.Remove((design, baseIndex));
                    }
                }
            } while (removed);
        }
        // Part One - Count the possible designs
        // After cleaning the graph, impossible designs stay with no links. So, just count the designs with links.
        int countPossibles = 0;
        foreach (string design in designs)
        {
            bool possible = false;
            for (int index = 0; index < design.Length; index++)
            {
                if (previousPositions.ContainsKey((design, index)) || nextPositions.ContainsKey((design, index)))
                {
                    possible = true;    
                    //Console.Write("Design " + design + " at index " + index + ", ");
                    //Console.Write(nextPositions.ContainsKey((design, index))? "next: {" + string.Join(", ", nextPositions[(design, index)]) + "}, ": "no next positions, ");
                    //Console.WriteLine(previousPositions.ContainsKey((design, index))? "previous: {" + string.Join(", ", previousPositions[(design, index)]) + "}.": "no previous positions.");
                }
            }
            if (possible)
                countPossibles++;
        }
        Console.WriteLine("Possible designs: " + countPossibles + ".");

        // Part Two - Different ways you could make each design
        // We need to count the different ways you could make each design, and sum the results. So, for each design...
        // Percurring all the ways in the graph is a complex task, we just need to count the different ways.
        // We go backwards from the end to the start, counting the ways to reach the end (the last position), and storing the count in a dictionary.
        // The last position has only one way to reach the end: count[end] = 1. 
        // Each position has next positions. Each next position already has a count. So, the count of the current position is the sum of the counts of the next positions.
        // The total count is count of the first position.
        
        // Sum the total count of all designs.
        long totalWays = 0;
        // iterate over the designs
        foreach (string design in designs)
        {   
            // store the count of the positions of each design in a dictionary
            Dictionary<(string, int), long> countPositions = new Dictionary<(string, int), long>();
            // total count of each position
            long count = 0;
            // iterate over the positions of the design, from the end to the start
            for (int index = design.Length - 1; index >= 0; index--)
            {
                // start with zero count
                count = 0;
                // iterate over the next positions
                if (nextPositions.ContainsKey((design, index)))
                    foreach (int nextIndex in nextPositions[(design, index)])
                    {
                        // reach the end: the count is one way
                        if (nextIndex == design.Length)
                            count++;
                        // reach the next position: count is the count of the next position
                        else
                            count += countPositions[(design, nextIndex)];
                    }
                // store the count of the position, that is the sum of the counts of the next positions for the current position
                countPositions[(design, index)] = count;
                //Console.WriteLine("Index: " + index + ". Count: " + count + ".");
            }
            //Console.WriteLine("Design: " + design + ", ways: " + count + ".");
            // sum the total count of all designs. The count of each design is the count of the first position, that was the last calculated.
            totalWays += count;
        }
        // print the total count of different ways you could make each design
        Console.WriteLine("Different ways you could make each design: " + totalWays + ".");
    }
}