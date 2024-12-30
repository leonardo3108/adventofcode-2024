/*  Problem Description:

--- Day 23: LAN Party ---
As The Historians wander around a secure area at Easter Bunny HQ, you come across posters for a LAN party scheduled for today! 
Maybe you can find it; you connect to a nearby datalink port and download a map of the local network (your puzzle input).

The network map provides a list of every connection between two computers. For example:

kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn
Each line of text in the network map represents a single connection; 
the line kh-tc represents a connection between the computer named kh and the computer named tc. 
Connections aren't directional; tc-kh would mean exactly the same thing.

LAN parties typically involve multiplayer games, so maybe you can locate it by finding groups of connected computers. 
Start by looking for sets of three computers where each computer in the set is connected to the other two computers.

In this example, there are 12 such sets of three inter-connected computers:

aq,cg,yn
aq,vc,wq
co,de,ka
co,de,ta
co,ka,ta
de,ka,ta
kh,qp,ub
qp,td,wh
tb,vc,wq
tc,td,wh
td,wh,yn
ub,vc,wq
If the Chief Historian is here, and he's at the LAN party, it would be best to know that right away. 
You're pretty sure his computer's name starts with t, so consider only sets of three computers where at least one computer's name starts with t. 
That narrows the list down to 7 sets of three inter-connected computers:

co,de,ta
co,ka,ta
de,ka,ta
qp,td,wh
tb,vc,wq
tc,td,wh
td,wh,yn
Find all the sets of three inter-connected computers. How many contain at least one computer with a name that starts with t?

Resume:
You are given a list of connections between computers. Each connection is represented by a string of two computer names separated by a hyphen.
You need to find all the sets of three inter-connected computers. How many contain at least one computer with a name that starts with t?

Resolution steps:
1. Parse the input and create a dictionary of connections.
2. Traverse the dictionary and find all the sets of three inter-connected computers. 
3. Sort the computers and add them to a set to avoid duplicates.
3. Count the triads with at least one computer with a name starting with 't'.
4. Print the number of sets found.


--- Part Two ---
There are still way too many results to go through them all. You'll have to find the LAN party another way and go there yourself.

Since it doesn't seem like any employees are around, you figure they must all be at the LAN party. 
If that's true, the LAN party will be the largest set of computers that are all connected to each other. 
That is, for each computer at the LAN party, that computer will have a connection to every other computer at the LAN party.

In the above example, the largest set of computers that are all connected to each other is made up of co, de, ka, and ta. 
Each computer in this set has a connection to every other computer in the set:

ka-co
ta-co
de-co
ta-ka
de-ta
ka-de
The LAN party posters say that the password to get into the LAN party is the name of every computer at the LAN party, sorted alphabetically, 
then joined together with commas. (The people running the LAN party are clearly a bunch of nerds.) 
In this example, the password would be co,de,ka,ta.

What is the password to get into the LAN party?

Resume:
You need to find the largest set of computers that are all connected to each other. This set will be the LAN party.
The password to get into the LAN party is the sorted list of computers joined together with commas.

Reasoning:
Routine to find the triads generalized to find n-tuples of connected computers (dimensions).
So, we start with 3-tuples and increase the dimensions until we find just one set of connected computers.
Then, we print the password to get into the LAN party.
*/

using System.Diagnostics;

class Program
{
    static void Main()
    {
        // Sample input
        string[] lines =
        {
            "kh-tc",
            "qp-kh",
            "de-cg",
            "ka-co",
            "yn-aq",
            "qp-ub",
            "cg-tb",
            "vc-aq",
            "tb-ka",
            "wh-tc",
            "yn-cg",
            "kh-ub",
            "ta-co",
            "de-co",
            "tc-td",
            "tb-wq",
            "wh-td",
            "ta-ka",
            "td-qp",
            "aq-cg",
            "wq-ub",
            "ub-vc",
            "de-ta",
            "wq-aq",
            "wq-vc",
            "wh-yn",
            "ka-de",
            "kh-ta",
            "co-tc",
            "wh-qp",
            "tb-vc",
            "td-yn"
        };

        // Input from the file
        lines = File.ReadAllLines("input.txt");
        //Console.WriteLine("Input: " + string.Join(", ", lines) + ".\n");

        // Parse the input and create a dictionary of connections
        var connections = new Dictionary<string, List<string>>();
        foreach (string line in lines)
        {
            string[] pair = line.Split('-');
            string computer1 = pair[0];
            string computer2 = pair[1];
            AddConnection(connections, computer1, computer2);
            AddConnection(connections, computer2, computer1);
        }

        /* // Debug - Print connections
        Console.WriteLine("Connections from:");
        foreach ((string computer, var others) in connections)
            Console.WriteLine("\t" + computer + ": " + string.Join(", ", others) + ".");
        */

        // Part One - Find all the sets of three inter-connected computers that contain at least one computer with a name that starts with t
        // Dimensions of the n-tuple = 3: triads
        int dimensions = 3;
        var triads = EstablishLocalAreaNetworks(connections, dimensions, 0);

        // Count the triads with at least one computer with a name starting with 't'
        int count = 0;
        foreach (var triad in triads)
        {
            if (triad[0][0] == 't' || triad[1][0] == 't' || triad[2][0] == 't')
                count++;
        }

        // Print the number of sets found
        Console.WriteLine("Count of triads of inter-connected computers that contain at least one computer with a name that starts with t: " + count);

        // Part Two - Find the largest set of computers that are all connected to each other
        // Dimensions of the n-tuple = 4, 5, 6, ... until we find just one set of connected computers
        List<string[]> lans = null;
        while (count > 1) {
            lans = EstablishLocalAreaNetworks(connections, ++dimensions, 0);
            count = lans.Count;
            Console.WriteLine($"{dimensions}-LANs found: " + count + ".");
        }
        Debug.Assert(lans != null, "No LANs found.");
        // Print the password to get into the LAN party
        Console.WriteLine("The password to get into the LAN party: " + string.Join(",", lans.First()) + ".");
    }

    /// <summary>
    ///     Establishes local area networks of connected computers with a given dimension.
    /// </summary>
    /// <param name="connections">The dictionary of connections between computers.</param>
    /// <param name="dimension">The dimension of the n-tuple of connected computers to find.</param>
    /// <param name="debugLevel">The debug level: 0 - no debug, 1 - Completed LANs, 2 - LANs to try, 3 - Adjusts in LANs, 4 - Details.</param>
    /// <returns>The list of n-tuples of connected computers.</returns>
    private static List<string[]> EstablishLocalAreaNetworks(Dictionary<string, List<string>> connections, int dimension = 3, int debugLevel = 0)
    {
        // The list of n-tuples of connected computers (LANs) to return. Keeped as a set of simple strings to avoid duplicates.
        var lans = new HashSet<string>();
        // Index to traverse the computers (list of keys in the dictionary, or list of connected computers that are values in the dictionary)
        int index = 0;
        // The actual list of computers to check. The first computer is always the key in the dictionary, the others come from the dictionary values.
        var lan = new List<string>();
        // The next computer to check
        string? nextComputer;
        // The list of other computers connected by the dictionary key (dictionary value)
        List<string> others = null;
        // Sort the computers from the dictionary keys, so we can traverse them in order of the computer name
        List<string> keyComputers = connections.Keys.ToList();
        keyComputers.Sort();
        // Iterate until all the computers combinations are checked
        while (true)
        {
            // list of computers to check is empty
            if (lan.Count == 0)
            {
                // Get the next computer from the dictionary keys. If the index is out of range, we are done.
                if (index == keyComputers.Count)
                    break;
                nextComputer = keyComputers.ElementAt(index);
                // Get the list of other computers connected by the dictionary key
                others = connections[nextComputer];
                // If the list of other computers are greater or equal to the dimension - 1, the LAN is possible
                if (others.Count >= dimension - 1)
                {
                    // Start the LAN with the key computer
                    lan.Add(keyComputers.ElementAt(index));
                    if (debugLevel > 1) Console.WriteLine("Starting LAN with " + lan[0] + ". Other computers: " + string.Join(", ", others) + ".");
                    // Reset the index to traverse the other computers
                    index = 0;
                }
                // If the list of other computers are less than the dimension - 1, it is not possible to form a LAN
                else
                {
                    // Move to the next key computer, and try again
                    index++;
                    continue;
                }
            }
            // Get the next computer from the list of other computers
            Debug.Assert(others != null, $"{lan.First()} has no connections.");
            if (debugLevel > 3) Console.Write("\tIndex: " + index + ".");
            nextComputer = others[index];
            if (debugLevel > 3) Console.Write(" Next computer: " + nextComputer);
            // Check if the next computer is connected to all the other computers in the LAN. If one misses, mark the next computer as null.
            foreach (string connectedComputer in lan)
                if (!connections[nextComputer].Contains(connectedComputer) || !connections[connectedComputer].Contains(nextComputer))
                {
                    nextComputer = null;
                    if (debugLevel > 3) Console.WriteLine("... no.");
                    break;
                }
            // If the next computer is connected to all the other computers in the LAN
            if (nextComputer != null)
            {
                // Add the next computer to the LAN
                if (debugLevel > 3) Console.WriteLine("... yes.");
                lan.Add(nextComputer);
                // If the LAN is completed
                if (lan.Count == dimension)
                {
                    // Sort the computers in the LAN and try to add it to the list of LANs
                    var addLan = new List<string>(lan);
                    addLan.Sort();
                    var strLan = string.Join(",", addLan);
                    if (debugLevel > 0) Console.WriteLine($"LAN {strLan} completed! ({lans.Count + 1})");
                    lans.Add(strLan);
                    // Remove the last computer from the LAN, to try another combination and keep trying
                    lan.Remove(nextComputer);
                    if (debugLevel > 1) Console.WriteLine("Now LAN with: " + string.Join(", ", lan));
                }
                else if (debugLevel > 2) Console.WriteLine("LAN updated: " + string.Join(", ", lan) + ".");
            }
            // Move to the next computer in the list of other computers
            index++;
            // If the index is out of range, the list of other computers ended and LAN is not possible. Try another combination.
            while (index == others.Count && lan.Count > 0)
            {
                // Remove the last computer from the LAN
                string last = lan[lan.Count - 1];
                lan.RemoveAt(lan.Count - 1);
                // If the LAN is empty
                if (lan.Count == 0)
                {
                    // Move to the next key computer
                    index = keyComputers.IndexOf(last) + 1;
                    if (debugLevel > 1) Console.WriteLine("Others computers ended. New LAN, index (key): " + index + ".\n");
                }
                // If the LAN is not empty
                else
                {
                    // Move to the next computer in the list of other computers
                    index = others.IndexOf(last) + 1;
                    if (debugLevel > 2) Console.WriteLine("Others computers ended. LAN updated: " + string.Join(", ", lan) + ", index: " + index + ".");
                }
            }
        }
        // Return the list of LANs. Each LAN, initially a simple string, is splitted into an array of strings. The HashSet is converted to a List.
        return lans.ToList().ConvertAll(lan => lan.Split(',').ToArray());
    }

    /// <summary>
    ///    Adds a connection between two computers to the dictionary of connections.
    /// </summary>
    /// <param name="connections">The dictionary of connections between computers.</param>
    /// <param name="fromComputer">Computer from which the connection starts.</param>
    /// <param name="toComputer">Computer to which the connection goes.</param>
    /// <remarks>
    ///     It must be called twice for each connection, as the connections are not directional.
    /// </remarks> 

    private static void AddConnection(Dictionary<string, List<string>> connections, string fromComputer, string toComputer)
    {
        // Origin computer is not in the dictionary
        if (!connections.ContainsKey(fromComputer))
            // Add the origin computer to the dictionary with an empty list of connections
            connections[fromComputer] = new List<string>();
        // Add the destination computer to the list of connections of the origin computer
        connections[fromComputer].Add(toComputer);
    }
}
