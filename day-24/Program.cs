/*  Problem Description:

--- Day 24: Crossed Wires ---
You and The Historians arrive at the edge of a large grove somewhere in the jungle. After the last incident, the Elves installed a small device that monitors the fruit. While The Historians search the grove, one of them asks if you can take a look at the monitoring device; apparently, it's been malfunctioning recently.

The device seems to be trying to produce a number through some boolean logic gates. Each gate has two inputs and one output. The gates all operate on values that are either true (1) or false (0).

AND gates output 1 if both inputs are 1; if either input is 0, these gates output 0.
OR gates output 1 if one or both inputs is 1; if both inputs are 0, these gates output 0.
XOR gates output 1 if the inputs are different; if the inputs are the same, these gates output 0.
Gates wait until both inputs are received before producing output; wires can carry 0, 1 or no value at all. There are no loops; once a gate has determined its output, the output will not change until the whole system is reset. Each wire is connected to at most one gate output, but can be connected to many gate inputs.

Rather than risk getting shocked while tinkering with the live system, you write down all of the gate connections and initial wire values (your puzzle input) so you can consider them in relative safety. For example:

x00: 1
x01: 1
x02: 1
y00: 0
y01: 1
y02: 0

x00 AND y00 -> z00
x01 XOR y01 -> z01
x02 OR y02 -> z02
Because gates wait for input, some wires need to start with a value (as inputs to the entire system). The first section specifies these values. For example, x00: 1 means that the wire named x00 starts with the value 1 (as if a gate is already outputting that value onto that wire).

The second section lists all of the gates and the wires connected to them. For example, x00 AND y00 -> z00 describes an instance of an AND gate which has wires x00 and y00 connected to its inputs and which will write its output to wire z00.

In this example, simulating these gates eventually causes 0 to appear on wire z00, 0 to appear on wire z01, and 1 to appear on wire z02.

Ultimately, the system is trying to produce a number by combining the bits on all wires starting with z. z00 is the least significant bit, then z01, then z02, and so on.

In this example, the three output bits form the binary number 100 which is equal to the decimal number 4.

Here's a larger example:

x00: 1
x01: 0
x02: 1
x03: 1
x04: 0
y00: 1
y01: 1
y02: 1
y03: 1
y04: 1

ntg XOR fgs -> mjb
y02 OR x01 -> tnw
kwq OR kpj -> z05
x00 OR x03 -> fst
tgd XOR rvg -> z01
vdt OR tnw -> bfw
bfw AND frj -> z10
ffh OR nrd -> bqk
y00 AND y03 -> djm
y03 OR y00 -> psh
bqk OR frj -> z08
tnw OR fst -> frj
gnj AND tgd -> z11
bfw XOR mjb -> z00
x03 OR x00 -> vdt
gnj AND wpb -> z02
x04 AND y00 -> kjc
djm OR pbm -> qhw
nrd AND vdt -> hwm
kjc AND fst -> rvg
y04 OR y02 -> fgs
y01 AND x02 -> pbm
ntg OR kjc -> kwq
psh XOR fgs -> tgd
qhw XOR tgd -> z09
pbm OR djm -> kpj
x03 XOR y03 -> ffh
x00 XOR y04 -> ntg
bfw OR bqk -> z06
nrd XOR fgs -> wpb
frj XOR qhw -> z04
bqk OR frj -> z07
y03 OR x01 -> nrd
hwm AND bqk -> z03
tgd XOR rvg -> z12
tnw OR pbm -> gnj
After waiting for values on all wires starting with z, the wires in this system have the following values:

bfw: 1
bqk: 1
djm: 1
ffh: 0
fgs: 1
frj: 1
fst: 1
gnj: 1
hwm: 1
kjc: 0
kpj: 1
kwq: 0
mjb: 1
nrd: 1
ntg: 0
pbm: 1
psh: 1
qhw: 1
rvg: 0
tgd: 0
tnw: 1
vdt: 1
wpb: 0
z00: 0
z01: 0
z02: 0
z03: 1
z04: 0
z05: 1
z06: 1
z07: 1
z08: 1
z09: 1
z10: 1
z11: 0
z12: 0
Combining the bits from all wires starting with z produces the binary number 0011111101000. Converting this number to decimal produces 2024.

Simulate the system of gates and wires. What decimal number does it output on the wires starting with z?

Resume:
The problem is to simulate the system of gates and wires and find the decimal number output on the wires starting with z.

Resolution steps:
1. Read the input file and parse the input.
2. Create a dictionary to store the wire values.
3. Create a dictionary to store the gates.
4. Create a function to evaluate the gate.
5. Create a function to evaluate the wire value.
6. Evaluate the wire values.
7. Convert the binary number to decimal number.
8. Print the decimal number.


--- Part Two ---
After inspecting the monitoring device more closely, you determine that the system you're simulating is trying to add two binary numbers.

Specifically, it is treating the bits on wires starting with x as one binary number, 
treating the bits on wires starting with y as a second binary number, and then attempting to add those two numbers together. 
The output of this operation is produced as a binary number on the wires starting with z. 
(In all three cases, wire 00 is the least significant bit, then 01, then 02, and so on.)

The initial values for the wires in your puzzle input represent just one instance of a pair of numbers that sum to the wrong value. 
Ultimately, any two binary numbers provided as input should be handled correctly. 
That is, for any combination of bits on wires starting with x and wires starting with y, 
the sum of the two numbers those bits represent should be produced as a binary number on the wires starting with z.

For example, if you have an addition system with four x wires, four y wires, and five z wires, 
you should be able to supply any four-bit number on the x wires, any four-bit number on the y numbers, 
and eventually find the sum of those two numbers as a five-bit number on the z wires. 
One of the many ways you could provide numbers to such a system 
would be to pass 11 on the x wires (1011 in binary) and 13 on the y wires (1101 in binary):

x00: 1
x01: 1
x02: 0
x03: 1
y00: 1
y01: 0
y02: 1
y03: 1
If the system were working correctly, then after all gates are finished processing, you should find 24 (11+13) on the z wires as the five-bit binary number 11000:

z00: 0
z01: 0
z02: 0
z03: 1
z04: 1
Unfortunately, your actual system needs to add numbers with many more bits and therefore has many more wires.

Based on forensic analysis of scuff marks and scratches on the device, you can tell that there are exactly four pairs of gates whose output wires have been swapped. (A gate can only be in at most one such pair; no gate's output was swapped multiple times.)

For example, the system below is supposed to find the bitwise AND of the six-bit number on x00 through x05 and the six-bit number on y00 through y05 and then write the result as a six-bit number on z00 through z05:

x00: 0
x01: 1
x02: 0
x03: 1
x04: 0
x05: 1
y00: 0
y01: 0
y02: 1
y03: 1
y04: 0
y05: 1

x00 AND y00 -> z05
x01 AND y01 -> z02
x02 AND y02 -> z01
x03 AND y03 -> z03
x04 AND y04 -> z04
x05 AND y05 -> z00
However, in this example, two pairs of gates have had their output wires swapped, causing the system to produce wrong answers. The first pair of gates with swapped outputs is x00 AND y00 -> z05 and x05 AND y05 -> z00; the second pair of gates is x01 AND y01 -> z02 and x02 AND y02 -> z01. Correcting these two swaps results in this system that works as intended for any set of initial values on wires that start with x or y:

x00 AND y00 -> z00
x01 AND y01 -> z01
x02 AND y02 -> z02
x03 AND y03 -> z03
x04 AND y04 -> z04
x05 AND y05 -> z05
In this example, two pairs of gates have outputs that are involved in a swap. By sorting their output wires' names and joining them with commas, the list of wires involved in swaps is z00,z01,z02,z05.

Of course, your actual system is much more complex than this, and the gates that need their outputs swapped could be anywhere, not just attached to a wire starting with z. If you were to determine that you need to swap output wires aaa with eee, ooo with z99, bbb with ccc, and aoc with z24, your answer would be aaa,aoc,bbb,ccc,eee,ooo,z24,z99.

Your system of gates and wires has four pairs of gates which need their output wires swapped - eight wires in total. Determine which four pairs of gates need their outputs swapped so that your system correctly performs addition; what do you get if you sort the names of the eight wires involved in a swap and then join those names with commas?
*/

using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

class Program
{
    static void Main()
    {
        // Simpler input for testing
        string[] lines =
        {
            "x00: 1",
            "x01: 1",
            "x02: 1",
            "y00: 0",
            "y01: 1",
            "y02: 0",
            "",
            "x00 AND y00 -> z00",
            "x01 XOR y01 -> z01",
            "x02 OR y02 -> z02"
        };

        // Larger input sample
        lines = new string[]
        {
            "x00: 1",
            "x01: 0",
            "x02: 1",
            "x03: 1",
            "x04: 0",
            "y00: 1",
            "y01: 1",
            "y02: 1",
            "y03: 1",
            "y04: 1",
            "",
            "ntg XOR fgs -> mjb",
            "y02 OR x01 -> tnw",
            "kwq OR kpj -> z05",
            "x00 OR x03 -> fst",
            "tgd XOR rvg -> z01",
            "vdt OR tnw -> bfw",
            "bfw AND frj -> z10",
            "ffh OR nrd -> bqk",
            "y00 AND y03 -> djm",
            "y03 OR y00 -> psh",
            "bqk OR frj -> z08",
            "tnw OR fst -> frj",
            "gnj AND tgd -> z11",
            "bfw XOR mjb -> z00",
            "x03 OR x00 -> vdt",
            "gnj AND wpb -> z02",
            "x04 AND y00 -> kjc",
            "djm OR pbm -> qhw",
            "nrd AND vdt -> hwm",
            "kjc AND fst -> rvg",
            "y04 OR y02 -> fgs",
            "y01 AND x02 -> pbm",
            "ntg OR kjc -> kwq",
            "psh XOR fgs -> tgd",
            "qhw XOR tgd -> z09",
            "pbm OR djm -> kpj",
            "x03 XOR y03 -> ffh",
            "x00 XOR y04 -> ntg",
            "bfw OR bqk -> z06",
            "nrd XOR fgs -> wpb",
            "frj XOR qhw -> z04",
            "bqk OR frj -> z07",
            "y03 OR x01 -> nrd",
            "hwm AND bqk -> z03",
            "tgd XOR rvg -> z12",
            "tnw OR pbm -> gnj"
        };

        // Read the input file
        lines = File.ReadAllLines("input.txt");

        Dictionary<string, int> wireValues = new Dictionary<string, int>();
        Dictionary<string, (string op, string g1, string g2)> gates = new Dictionary<string, (string, string, string)>();

        // Parse the input and store the wire values and gates
        foreach (string line in lines)
        {
            if (line.Contains(":"))
            {
                string[] parts = line.Split(":");
                string wire = parts[0].Trim();
                int value = int.Parse(parts[1].Trim());
                wireValues[wire] = value;
            }
            else if (line.Contains("->"))
            {
                string[] parts = line.Split("->");
                string output = parts[1].Trim();
                parts = parts[0].Trim().Split();
                gates[output] = (parts[1].Trim(), parts[0].Trim(), parts[2].Trim());
            }
        }

        var originalWireValues = new Dictionary<string, int>(wireValues);

        (ulong z, var sum, var errors) = EvaluateGatesAndCompute(wireValues, gates);
        Console.WriteLine($"The decimal number output on the wires starting with z is {z}.");
        Console.WriteLine($"{errors.Count} errors at z-gates: {string.Join(", ", errors)}.\n");

        // Part 2
        /*
        (z28,rwv) = (khf XOR ngm) vs (khf AND ngm) = 0 vs 1
        //(z29,jtw) = (ctt XOR nqr) vs (ctt AND nqr) = 0 vs 1
        (z30,ppm) = (dtf XOR pgr) vs (dtf AND pgr) = 1 vs 0
        (qjc,qfs) = (x05 AND y05) vs (x05 XOR y05) = 0 vs 1
        (skn,hcc) = (cgn AND cjh) vs (cgn XOR cjh) = 0 vs 1
        (qcw,hqc) = (x24 XOR y24) vs (x24 AND y24) = 1 vs 0

        */
        List<string> swapList = new List<string>();
        SwapGates("qjc", "qfs", gates, swapList);
        SwapGates("skn", "hcc", gates, swapList);
        SwapGates("qcw", "hqc", gates, swapList);
        SwapGates("z30", "ppm", gates, swapList);
        Console.WriteLine();

        wireValues = new Dictionary<string, int>(originalWireValues);
        (z, sum, errors) = EvaluateGatesAndCompute(wireValues, gates);
        if (errors.Count == 0)
            Console.WriteLine("Zero erros at z-gates.");
        else
            Console.WriteLine($"{errors.Count} errors at z-gates: {string.Join(", ", errors)}.\n");
        //hcc,hqc,ppm,qcw,qfs,qjc,skn,z30

        swapList.Sort();
        Console.WriteLine($"The names of the eight wires involved in a swap are: {string.Join(",", swapList)}.");
    }

    private static void SwapGates(string gate1, string gate2, Dictionary<string, (string op, string g1, string g2)> gates, List<string> swapList)
    {
        Console.WriteLine($"Swapping gates {gate1} and {gate2}.");
        var tempGate = gates[gate1];
        gates[gate1] = gates[gate2];
        gates[gate2] = tempGate;
        swapList.Add(gate1);
        swapList.Add(gate2);
    }

    private static string SortGateInputs(Dictionary<string, (string op, string g1, string g2)> gates, string targetGate)
    {
        (string g1, string g2) = (gates[targetGate].g1, gates[targetGate].g2);
        if (g1.CompareTo(g2) > 0)
            (g1, g2) = (gates[targetGate].g2, gates[targetGate].g1);
        return $"{g1} {gates[targetGate].op} {g2}";
    }

    private static (ulong, string, List<string>) EvaluateGatesAndCompute(Dictionary<string, int> wireValues, 
                                                                         Dictionary<string, (string op, string g1, string g2)> gates,
                                                                         string originalSum = "",
                                                                         bool debug = false)
    {
        // Evaluate all the wire values
        foreach (string wire in gates.Keys)
            EvaluateWire(wire, gates, wireValues);

        // Z Gates: compute and print the gate names, the binary number and the decimal number
        (ulong z, string zb, string gateNames, int z1, int z2) = Decode('z', wireValues);
        if (debug)
        {
            Console.WriteLine("Binary values from gates " + gateNames + ": " + zb);
            Console.WriteLine($"Decimal number: {z}");
        }

        // X Gates: compute and print the gate names, the binary number and the decimal number
        (ulong x, string xb, gateNames, int x1, int x2) = Decode('x', wireValues);
        if (debug)
        {
            Console.WriteLine("\nBinary values from gates " + gateNames + ": " + xb);
            Console.WriteLine($"Decimal number: {x}");
        }

        // Y Gates: compute and print the gate names, the binary number and the decimal number
        (ulong y, string yb, gateNames, int y1, int y2) = Decode('y', wireValues);
        if (debug)
        {
            Console.WriteLine("\nBinary values from gates " + gateNames + ": " + yb);
            Console.WriteLine($"Decimal number: {y}");
        }

        ulong s = originalSum == ""? x + y : Convert.ToUInt64(originalSum, 2);
        int s1 = originalSum == ""? x1 + y1: Convert.ToInt32(originalSum.Substring(0, originalSum.Length - 30), 2);
        int s2 = originalSum == ""? x2 + y2: Convert.ToInt32(originalSum.Substring(originalSum.Length - 30, 30), 2);
        string sb = Convert2binary(s1, s2);
        if (debug) Console.WriteLine($"\nx + y: {s} ({sb})");
        int d1 = s1 ^ z1, d2 = s2 ^ z2;
        ulong d = (ulong)Math.Pow(2, 30) * (ulong)d1 + (ulong)d2;
        string db = Convert2binary(d1, d2);
        int errorCount = db.Count(x => x == '1');
        if (debug) Console.WriteLine($"z XOR (x + y) = {d} ({db}) - {errorCount} errors.");

        Console.WriteLine("X-gates: 0" + xb);
        Console.WriteLine("Y-gates: 0" + yb);
        Console.WriteLine("Z-gates: " + zb);
        Console.WriteLine("x + y:   " + sb);
        Console.WriteLine("z^(x+y): " + Convert2binary(d1, d2));

        List<string> errors = new List<string>();
        for (int i = 0; i < db.Length; i++)
            if (db[db.Length - i - 1] == '1')
                errors.Add($"z{i.ToString().PadLeft(2, '0')}");
        return (z, sb, errors);
    }

    private static string Convert2binary(int part1, int part2)
    {
        // Pad the 30 bits binary number with zeros
        return Convert.ToString((int) part1, 2).PadLeft(16, '0') + Convert.ToString((int) part2, 2).PadLeft(30, '0');
    }

    /// <summary>
    ///    Decode the binary number from the wire values
    ///    The binary number is obtained by concatenating the bits of the selected gates in the order they appear in the dictionary
    /// </summary>
    /// <param name="prefix">The prefix of the gate names</param>
    /// <param name="wireValues">The dictionary of wire values</param>
    /// <returns>The decimal number, the binary number, and the list of gate names</returns>
    private static (ulong, string, string, int, int) Decode(char prefix, Dictionary<string, int> wireValues)
    {
        // Select the z gates
        var zValues = wireValues.Where(x => x.Key.StartsWith(prefix)).OrderBy(x => x.Key);

        // Convert the binary wire values to a decimal number
        ulong decimalNumber = 0;
        int part1 = 0, part2 = 0;
        // Show the binary number and the list of z gates
        StringBuilder binaryNumber = new StringBuilder();
        StringBuilder gateNames = new StringBuilder();

        // Iterate the z gates in reverse order, to get the most significant bit first        
        for (int i = zValues.Count() - 1; i >= 0; i--)
        {
            // Get the gate name and the bit value
            string gate = zValues.ElementAt(i).Key;
            int bit = zValues.ElementAt(i).Value;
            // Validate the gate name and the bit value
            Debug.Assert(bit == 0 || bit == 1, "Values of bits must be 0 or 1");
            Debug.Assert(gate.StartsWith(prefix), $"Gate name must start with {prefix}");
            Debug.Assert(int.Parse(gate.Substring(1, gate.Length - 1)) == i, "Gate names must follow a sequence");
            // Build the decimal number by multiplying by 2 and adding the actual bit value
            decimalNumber = decimalNumber * 2 + (ulong)bit;
            // Build the binary number and the list of z gates
            binaryNumber.Append(bit);
            gateNames.Append(gate).Append("+");

            if (i >= 30)
            {
                part1 = part1 * 2 + bit;
            }
            else if (i < 30)
            {
                part2 = part2 * 2 + bit;
            }
        }
        gateNames.Remove(gateNames.Length - 1, 1);
        return (decimalNumber, binaryNumber.ToString(), gateNames.ToString(), part1, part2);
    }

    /// <summary>
    ///     Evaluate the gate operation
    /// </summary>
    /// <param name="operation">The operation to evaluate: AND, OR, or XOR</param>
    /// <param name="left">The left gate</param>
    /// <param name="right">The right gate</param>
    /// <param name="gates">The dictionary of gates</param>
    /// <param name="wireValues">The dictionary of wire values</param>
    /// <returns>The result of the operation, 0 or 1</returns>
    /// <remarks>
    ///   The left and right gates are obtained recursively by evaluating the wire values
    ///  </remarks>
    private static int EvaluateGate(string operation, string left, string right, 
                                    Dictionary<string, (string op, string g1, string g2)> gates, 
                                    Dictionary<string, int> wireValues)
    {
        int leftValue = EvaluateWire(left, gates, wireValues);
        int rightValue = EvaluateWire(right, gates, wireValues);
        return operation switch
        {
            "AND" => leftValue & rightValue,
            "OR" => leftValue | rightValue,
            "XOR" => leftValue ^ rightValue,
            _ => 0
        };
    }

    /// <summary>
    ///     Evaluate the wire value
    /// </summary>
    /// <param name="wire">The wire to evaluate</param>
    /// <param name="gates">The dictionary of gates</param>
    /// <param name="wireValues">The dictionary of wire values</param>
    /// <returns>The value of the wire, 0 or 1</returns>
    /// <remarks>
    ///  The wire value is obtained from the wire values dictionary, if it is not present, it is evaluated from the gate, recursively.
    ///  </remarks>
    private static int EvaluateWire(string wire, 
                                    Dictionary<string, (string op, string g1, string g2)> gates, 
                                    Dictionary<string, int> wireValues)
    {
        if (!wireValues.ContainsKey(wire))
            wireValues[wire] = EvaluateGate(gates[wire].op, gates[wire].g1, gates[wire].g2, gates, wireValues);
        return wireValues[wire];
    }
}