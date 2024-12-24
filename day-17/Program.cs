/*  Problem Description:

--- Day 17: Chronospatial Computer ---
The Historians push the button on their strange device, but this time, you all just feel like you're falling.

"Situation critical", the device announces in a familiar voice. "Bootstrapping process failed. Initializing debugger...."

The small handheld device suddenly unfolds into an entire computer! The Historians look around nervously before one of them tosses it to you.

This seems to be a 3-bit computer: its program is a list of 3-bit numbers (0 through 7), like 0,1,2,3. The computer also has three registers named A, B, and C, but these registers aren't limited to 3 bits and can instead hold any integer.

The computer knows eight instructions, each identified by a 3-bit number (called the instruction's opcode). Each instruction also reads the 3-bit number after it as an input; this is called its operand.

A number called the instruction pointer identifies the position in the program from which the next opcode will be read; it starts at 0, pointing at the first 3-bit number in the program. Except for jump instructions, the instruction pointer increases by 2 after each instruction is processed (to move past the instruction's opcode and its operand). If the computer tries to read an opcode past the end of the program, it instead halts.

So, the program 0,1,2,3 would run the instruction whose opcode is 0 and pass it the operand 1, then run the instruction having opcode 2 and pass it the operand 3, then halt.

There are two types of operands; each instruction specifies the type of its operand. The value of a literal operand is the operand itself. For example, the value of the literal operand 7 is the number 7. The value of a combo operand can be found as follows:

Combo operands 0 through 3 represent literal values 0 through 3.
Combo operand 4 represents the value of register A.
Combo operand 5 represents the value of register B.
Combo operand 6 represents the value of register C.
Combo operand 7 is reserved and will not appear in valid programs.
The eight instructions are as follows:

The adv instruction (opcode 0) performs division. The numerator is the value in the A register. The denominator is found by raising 2 to the power of the instruction's combo operand. (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.) The result of the division operation is truncated to an integer and then written to the A register.

The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.

The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value to the B register.

The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand; if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.

The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)

The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value. (If a program outputs multiple values, they are separated by commas.)

The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register. (The numerator is still read from the A register.)

The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register. (The numerator is still read from the A register.)

Here are some examples of instruction operation:

If register C contains 9, the program 2,6 would set register B to 1.
If register A contains 10, the program 5,0,5,1,5,4 would output 0,1,2.
If register A contains 2024, the program 0,1,5,4,3,0 would output 4,2,5,6,7,7,7,7,3,1,0 and leave 0 in register A.
If register B contains 29, the program 1,7 would set register B to 26.
If register B contains 2024 and register C contains 43690, the program 4,0 would set register B to 44354.
The Historians' strange device has finished initializing its debugger and is displaying some information about the program it is trying to run (your puzzle input). For example:

Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0
Your first task is to determine what the program is trying to output. To do this, initialize the registers to the given values, then run the given program, collecting any output produced by out instructions. (Always join the values produced by out instructions with commas.) After the above program halts, its final output will be 4,6,3,5,6,3,5,2,1,0.

Using the information provided by the debugger, initialize the registers to the given values, then run the program. Once it halts, what do you get if you use commas to join the values it output into a single string?


Resume and resolution:
The program is a list of 3-bit numbers (0 through 7), like 0,1,2,3. 
The first number is the opcode and the second number is the operand, and so on.
The computer has three registers named A, B, and C.
The computer knows eight instructions, each identified by a 3-bit number (called the instruction's opcode).
    - adv: performs division.
    - bxl: calculates the bitwise XOR of register B and the instruction's literal operand.
    - bst: calculates the value of its combo operand modulo 8.
    - jnz: jumps if the A register is not zero.
    - bxc: calculates the bitwise XOR of register B and register C.
    - out: outputs the value of its combo operand modulo 8.
    - bdv: works exactly like the adv instruction except that the result is stored in the B register.
    - cdv: works exactly like the adv instruction except that the result is stored in the C register.
The program has two types of operands: literal and combo.
    - The value of a literal operand is the operand itself.
    - The value of a combo operand (4, 5 or 6) represent the value of registers A, B and C respectively.
The instruction pointer:
    - identifies the position in the program from which the next opcode will be read.
    - increases by 2 after each instruction is processed.
The program is trying to output a list of values.
The program halts when the instruction pointer is past the end of the program.

Resolution steps:
    - Parse the input data.
    - Implement the instructions.
    - Run the program.
    - Collect the output values.
    - Join the output values with commas.
    

-- Part Two ---
Digging deeper in the device's manual, you discover the problem: this program is supposed to output another copy of the program! 
Unfortunately, the value in register A seems to have been corrupted. 
You'll need to find a new value to which you can initialize register A 
so that the program's output instructions produce an exact copy of the program itself.

For example:

Register A: 2024
Register B: 0
Register C: 0

Program: 0,3,5,4,3,0
This program outputs a copy of itself if register A is instead initialized to 117440. 
(The original initial value of register A, 2024, is ignored.)

What is the lowest positive initial value for register A that causes the program to output a copy of itself?

Resoning:
Run the program with different values of register A until the output is the same as the program itself, 
    or at least the first x values are the same.
Start with 0 and increment the value by 1. Try to find a pattern in the output values.
Found a pattern in values 158166717 and 292384445. Then used the first as the starting point and the difference as the step.
Then found the value 164278899142333 as the solution.
*/

class Program
{
    static void Main()
    {
        // input test sample for part one
        string[] input = new string[]
        {
            "Register A: 729",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,1,5,4,3,0"
        };

        // input test sample for part two
        input = new string[]
        {
            "Register A: 2024",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,3,5,4,3,0"
        };

        // input from file
        input = File.ReadAllText("input.txt").Split('\n');
        // structures to hold the registers values and the program
        Dictionary<char, long> registers = new Dictionary<char, long>();
        int[] program;
        // parse the input data
        foreach (var line in input)
        {
            // parse the registers values
            if (line.StartsWith("Register"))
            {
                string[] parts = line.Split(':');
                char key = parts[0].Split(' ')[1][0];
                var value = int.Parse(parts[1]);
                registers[key] = value;
            }
            // parse the program
            else if (line.StartsWith("Program"))
            {
                program = line.Split(' ')[1].Split(',').Select(int.Parse).ToArray();
                // run the program (part one)
                List<int> output = RunProgram(program, new Dictionary<char, long>(registers), null, false);
                // print the output values
                string outputString = string.Join(",", output);
                Console.WriteLine("Part One output: " + outputString);
                
                //return;
                Console.WriteLine("\n");
                
                // part two: try to find the lowest positive initial value for register A that causes the program to output a copy of itself
                // start with 0 and increment the value by 1
                long actualValue = 0;
                long previousValue = 0;
                long step = 1;
                string programString = string.Join(",", program);
                Console.WriteLine("Part Two: trying values to get output " + programString);
                // found a pattern in values 158166717 and 292384445
                actualValue = 158166717;
                step = 292384445 - 158166717;
                do  // loop
                {
                    // generate a copy of the registers and run the program
                    Dictionary<char, long> registersCopy = new Dictionary<char, long>(registers);
                    registersCopy['A'] = actualValue;
                    output = RunProgram(program, registersCopy, program, false);
                    // verify the output values, print the values if hit a certain number of values
                    outputString = string.Join(",", output);
                    if (output.Count >= 15)
                        Console.WriteLine("\t" + actualValue + " > " + outputString + " vs " + programString);
                    //if (output.Count == 10) return;

                    // increment the value of register A by the step value
                    previousValue = actualValue;
                    actualValue += step;
                }
                // stops if the output is the same as the program itself
                while (outputString != programString);
                // print the value of register A that produces the output equal to the program
                Console.WriteLine("\nFound:  " + previousValue + " > " + outputString + " vs " + programString);
            }
        }
    }

    /// <summary>
    /// Print the registers values, for debug.
    /// </summary>
    /// <param name="registers">Structure with the registers values.</param>
    private static void DisplayRegisters(Dictionary<char, long> registers)
    {
        foreach (var register in registers)
            Console.Write($"{register.Key}: {register.Value} ");
        Console.WriteLine();
    }

    /// <summary>
    /// Run the program and return the output values.
    /// </summary>
    /// <param name="program">The program to run.</param>
    /// <param name="registers">The initial registers values.</param>
    /// <param name="testOutput">The expected output values (for part two), or null, for just running the program (part one).</param>
    /// <param name="debug">true to print debug information.</param>
    /// <returns>The output values.</returns>
    private static List<int> RunProgram(int[] program, Dictionary<char, long> registers, int[] testOutput = null, bool debug = false)
    {
        // list to hold the output values
        List<int> output = new List<int>();
        // if debug, print the registers values and the program
        if (debug)
        {
            Console.Write("Registers: ");
            DisplayRegisters(registers);
            Console.Write("Program: ");
            foreach (int code in program)
                Console.Write(code + " ");
            Console.WriteLine();
            Console.WriteLine("Running program...");
        }
        // array with the mneumonic for the opcodes, for debug
        string[] mneumonic = new string[] { "adv", "bxl", "bst", "jnz", "bxc", "out", "bdv", "cdv" };
        // loop through the program
        for (int i = 0; i < program.Length; i += 2)
        {
            // get the opcode and the operand
            int opcode = program[i];
            int operand = program[i + 1];
            // if debug, print the opcode (with mneumonic), operand and the registers values
            if (debug)
            {
                Console.Write($"{opcode}({mneumonic[opcode]}) {operand}  - Registers:");
                DisplayRegisters(registers);
            }
            // switch through the opcodes
            switch (opcode)
            {
                // adv: performs division (A /= 2^operand)
                case 0:
                    registers['A'] /= (long)Math.Pow(2, Combo(operand, registers));
                    break;
                // bxl: calculates the bitwise XOR of register B and the instruction's literal operand
                case 1:
                    registers['B'] ^= operand;
                    break;
                // bst: calculates the value of its combo operand modulo 8
                case 2:
                    registers['B'] = Combo(operand, registers) % 8;
                    break;
                // jnz: jumps if the A register is not zero
                case 3:
                    if (registers['A'] != 0)
                        i = operand - 2;
                    break;
                // bxc: calculates the bitwise XOR of register B and register C
                case 4:
                    registers['B'] ^= registers['C'];
                    break;
                // out: outputs the value of its combo operand modulo 8
                // in case of check mode, stop if the output is different from the expected output
                case 5:
                    output.Add((int) (Combo(operand, registers) % 8));
                    if (testOutput != null && output.Last() != testOutput[output.Count - 1])
                        return output;
                    break;
                // bdv: works exactly like the adv instruction except that the result is stored in the B register
                case 6:
                    registers['B'] = registers['A'] / (long)Math.Pow(2, Combo(operand, registers));
                    break;
                // cdv: works exactly like the adv instruction except that the result is stored in the C register
                case 7:
                    registers['C'] = registers['A'] / (long)Math.Pow(2, Combo(operand, registers));
                    break;
            }
        }
        // return the output values
        return output;
    }

    /// <summary>
    /// Get the value of a combo operand.
    /// </summary>
    /// <param name="operand">The operand value.</param>
    /// <param name="registers">The registers values.</param>
    /// <returns>The value of the operand.</returns>
    /// <remarks>
    /// Combo operands 0 through 3 represent literal values 0 through 3.
    /// Combo operand 4 to 6 represent the value of register A, B and C respectively.
    /// Combo operand 7 is reserved and will not appear in valid programs.
    /// </remarks>
    private static long Combo(int operand, Dictionary<char, long> registers)
    {
        return operand < 4 ? operand : operand < 7 ? registers[(char)('A' + operand - 4)]: 0;
    }
}