using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Xml.XPath;

namespace NEA_CPU_Model
{
    internal class Processor : AbstractProcessor
    {
        // dictionaries
        // uses a dictionary to implement an associative array to store the register values
        private Dictionary<string, int> registers = new Dictionary<string, int> { };

        // creates an instance of the cache class
        static public Cache cache = new Cache();

        // uses a dictionary to store the opcodes that can take <value>
        private Dictionary<string, int> ValueOpcodes = new Dictionary<string, int>
        {
            { "STR", 0},
            { "ADD", 2},
            { "SUB", 2},
            { "MOV", 1},
            { "CMP", 1},
            { "AND", 2},
            { "ORR", 2},
            { "EOR", 2},
            { "MVN", 1},
            { "LSL", 2},
            { "LSR", 2},
        };

        // uses a dictionary to store the opcodes that access a register
        private Dictionary<string, int> multipleOperandOpcodes = new Dictionary<string, int>
        {
            { "ADD", 1},
            { "SUB", 1},
            { "AND", 1},
            { "ORR", 1},
            { "EOR", 1},
            { "LSL", 1},
            { "LSR", 1}
        };

        // attributes
        // shows how many instructions have been executed
        static public int cycleCounter = 0;

        // controls which instruction is executed
        private int programCounter = 0;

        // controls if the end of the program has been met or if an error has occured
        private bool repeat = true;

        // string value to store result of comparisons for branching
        private string comparisonResult = string.Empty;

        // string value to store format of any <value> operands
        private string valueFormat = string.Empty;

        // constructor
        public Processor()
        {

        }

        // splits the instructions into each instruction and then into opcode and operand
        // then controls the CPU components in executing the instruction
        public override void FlowOfInstructions(List<string> instructions, RAM RAM, bool loop)
        {
            repeat = true; // sets a boolean variable that will be set to false if the end of the code or an error is found
            // code needs to execute all instructions at once
            if (loop)
            {
                while (programCounter < instructions.Count && repeat) // while not reached end of code/error not encountered
                {
                    SplitInstruction(instructions, RAM); // call to process the instructions
                }

                // reset cycle values
                programCounter = 0;
                cycleCounter = 0;
            }
            // execute only the next instruction
            else
            {
                if(programCounter < instructions.Count && repeat) // if not reached end of code/error not encountered
                {
                    SplitInstruction(instructions, RAM); // call to process the instructions

                    if(programCounter == instructions.Count - 1)
                    {
                        // reset cycle values for next cycle
                        programCounter = 0;
                        cycleCounter = 0;
                    }
                }
                else
                {
                    // reset cycle values for next cycle
                    programCounter = 0;
                    cycleCounter = 0;
                }
            }
        }

        // controls the program counter during execution and splits the instruction
        protected override void SplitInstruction(List<string> instructions, RAM RAM)
        {
            string instruction = instructions[programCounter]; // gets current instruction
            programCounter++; // increment program counter for next cycle
            cycleCounter++; // increment cycle counter to show how many instructions have been executed

            if (!instruction.Contains(':')) // checks if a colon is present the instruction is a label
            {
                // splits the instruction into opcode and operand
                string opcode = Parser.GetOpcode(instruction);
                string operand = Parser.GetOperand(instruction);

                // splits the operand into the individual operands
                string[] values = operand.Split(',');

                // decides which format any <value> is and does error checking for empty RAM addresses
                if (ValueOpcodes.ContainsKey(opcode))
                {
                    int valuePosition = ValueOpcodes[opcode];

                    if (values[valuePosition].Contains('#')) // if the value in the operand is a hardcoded value
                    {
                        values[valuePosition] = values[valuePosition].Replace("#", "");
                        valueFormat = "#";
                    }
                    else if (values[valuePosition].Contains('R')) // if the value in the operand is a register address
                    {
                        values[valuePosition] = values[valuePosition].Replace("R", "");
                        valueFormat = "R";
                        if (!registers.ContainsKey(values[valuePosition]))
                        {
                            MessageBox.Show($"Register address empty at line {programCounter}");
                            repeat = false; // an error has occurred so stop execution
                        }
                    }
                    else // else the value in the operand is a RAM address
                    {
                        valueFormat = "A";
                        if (FetchData(values[valuePosition], RAM) == -1)
                        {
                            MessageBox.Show($"RAM address empty at line {programCounter}");
                            repeat = false; // an error has occurred so stop execution
                        }
                    }
                }

                // checks for the opcodes that contain multiple operands
                // checks that the second of the three operands (which is always a register) is not empty
                if (multipleOperandOpcodes.ContainsKey(opcode))
                {
                    if (values[1].Contains('R')) // checks if the user has put in the register with a label
                    {
                        values[1] = values[1].Replace("R", "");
                    }

                    if (!registers.ContainsKey(values[1]))
                    {
                        MessageBox.Show($"Register address emptyat line {programCounter}");
                        repeat = false; // an error has occurred so stop execution
                    }
                }

                // updates specific purpose registers on the interface
                Program.model.cirText.Text = opcode;
                Program.model.programCounterText.Text = programCounter.ToString();

                if (repeat) // checks an error has not occurred
                {
                    // call point for the decoding of the instruction
                    DecodeInstruction(opcode, values, RAM, instructions);
                }

            }
        }

        // decodes the instruction given and calls the appropriate subroutine to execute it
        protected override void DecodeInstruction(string opcode, string[] values, RAM RAM, List<string> instructions)
        {
            // calls the appropriate subroutine and passes in the necessary values
            switch (opcode)
            {
                case "LDR":
                    LDR(values, RAM);
                    break;

                case "STR":
                    STR(values, RAM);
                    break;

                case "ADD":
                    ADD(values, RAM);
                    break;

                case "SUB":
                    SUB(values, RAM);
                    break;

                case "MOV":
                    MOV(values, RAM);
                    break;

                case "CMP":
                    CMP(values, RAM);
                    break;

                case "B":
                    Branch(values, RAM, instructions, "NA");
                    break;

                case "B<EQ>":
                    Branch(values, RAM, instructions, "EQ");
                    break;

                case "B<NE>":
                    Branch(values, RAM, instructions, "NE");
                    break;

                case "B<GT>":
                    Branch(values, RAM, instructions, "GT");
                    break;

                case "B<LT>":
                    Branch(values, RAM, instructions, "LT");
                    break;

                case "AND":
                    AND(values, RAM);
                    break;

                case "ORR":
                    ORR(values, RAM);
                    break;

                case "EOR":
                    EOR(values, RAM);
                    break;

                case "MVN":
                    MVN(values, RAM);
                    break;

                case "LSL":
                    LSL(values, RAM);
                    break;

                case "LSR":
                    LSR(values, RAM);
                    break;

                // end of code execution, exit
                case "HALT":
                    repeat = false;
                    break;

                default: // in case of an empty instruction or a label, go to next instruction
                    break;
            }
        }

        // load the value in the 2nd operand into the 1st operand
        private void LDR(string[] values, RAM RAM)
        {
            if (FetchData(values[1], RAM) != -1) // checks that the accessed address is not empty
            {
                registers[values[0]] = FetchData(values[1], RAM); // sets the appropriate register to the value fetched from RAM
                UpdateInterface(values[0], registers[values[0]]); // updates the interface accordingly
            }
            else // address is empty, exit out
            {
                MessageBox.Show($"Attempted to access empty RAM address in line {programCounter}");
                repeat = false;
            }
        }

        // store the value in the 1st operand into the 2nd operand
        private void STR(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 0, RAM); // fetches the value to process
            WriteData(values[1], calculationValue, RAM); // stores the data appropriately
        }

        // adds the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void ADD(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 2, RAM); // fetches the value to process
            int result = registers[values[1]] + calculationValue; // calculates the result

            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();
        }

        // subtract the value in the 3rd operand from the 2nd operand and stores it in the 1st operand
        private void SUB(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 2, RAM); // fetches the value to process
            int result = calculationValue - registers[values[1]]; // calculates the result

            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();
        }

        // copies the value in the 2nd operand into the 1st operand
        private void MOV(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 1, RAM); // fetches the value to process
            registers[values[0]] = calculationValue; // stores the value in the right register
            UpdateInterface(values[0], calculationValue); // updates the interface to show the new register update
        }

        // compares the value in the 1st operand with the 2nd operand
        private void CMP(string[] values, RAM RAM)
        {
            int comparisonValue = findCalculationValue(values, 1, RAM); // fetches the value to compare too

            // records the result of the comparison in the appropriate variable
            if (registers[values[0]] == comparisonValue)
            {
                comparisonResult = "EQ";
            }
            if (registers[values[0]] != comparisonValue)
            {
                comparisonResult = "NE";
            }
            else if (registers[values[0]] > comparisonValue)
            {
                comparisonResult = "GT";
            }
            else if (registers[values[0]] < comparisonValue)
            {
                comparisonResult = "LT";
            }
        }

        // branches to the label given by the operand if the given condition was met by the last comparison
        private void Branch(string[] values, RAM RAM, List<string> instructions, string condition)
        {
            // checks if the string stored by the last comparison met the given condition (or if it should always branch)
            if (comparisonResult == condition || condition == "NA")
            {
                if (Parser.labels.ContainsKey(values[0])) // checks the label exists
                {
                    programCounter = Parser.labels[values[0]]; // sets the program counter to the line containing the label
                }
                else // label doesn't exist so exit out
                {
                    MessageBox.Show("Label not found");
                    repeat = false;
                }
            }
        }

        // Bitwise and the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void AND(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 2, RAM); // fetches the value to process
            int result = registers[values[1]] & calculationValue; // calculates the result

            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();
        }

        // Bitwise or the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void ORR(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 2, RAM); // fetches the value to process
            int result = registers[values[1]] | calculationValue; // calculates the result

            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();
        }

        // Bitwise xor the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void EOR(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 2, RAM); // fetches the value to process
            int result = registers[values[1]] ^ calculationValue; // calculates the result
            
            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();
        }

        // Bitwise not the value in the 2nd operand and stores it in the 1st operand
        private void MVN(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 1, RAM); // fetches the value to process
            int result = NotLogic(calculationValue); // calculates the result
           
            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();
        }

        // Bitwise left shift the value in the 2nd operand by the 3rd operand and stores it in the 1st operand
        private void LSL(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 2, RAM); // fetches the value to process
            int result = LogicalShift(registers[values[1]], calculationValue, "*"); // calculates the result

            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();

        }

        // Bitwise right shift the value in the 2nd operand by the 3rd operand and stores it in the 1st operand
        private void LSR(string[] values, RAM RAM)
        {
            int calculationValue = findCalculationValue(values, 2, RAM); // fetches the value to process
            int result = LogicalShift(registers[values[1]], calculationValue, "/"); // calculates the result

            registers[values[0]] = result; // stores the result in the appropriate register

            // updates the interface
            UpdateInterface(values[0], registers[values[0]]);
            Program.model.accumulatorText.Text = result.ToString();

        }

        // does the bitwise logic
        private int NotLogic(int value)
        {
            string result = string.Empty;

            // converts the input to binary as this is necessary to carry out bitwise operations
            string operand = decToBin(value);

            for (int i = 0; i < operand.Length; i++)
            {
                if (operand[i] == '1') // if input is 1, output is 0
                {
                    result += '0';
                }
                else // else value is 0
                {
                    result += '1';
                }
            }

            return binToDec(result); // converts output to decimal and returns to call point
        }

        // does the Logical Shifting
        private int LogicalShift(int value, int shift, string Operator)
        {
            int calculationValue = 0; // creates a temporary value to carry out the calculation on
            if (Operator == "*") // if the call point is a left shift
            {
                calculationValue = value;
                for (int i = 0; i < shift; i++) // shifts the value a number of times equal to shift
                {
                    calculationValue *= 2;
                }
            }
            else // else the call point is a right shift
            {
                calculationValue = value;
                for (int i = 0; i < shift; i++) // shifts the value a number of times equal to shift
                {
                    calculationValue /= 2;
                }
            }
            return calculationValue; // returns the result
        }

        // converts input to binary
        private string decToBin(int value)
        {
            string result = " ";
            result = findBinary(value).ToString(); // calls recursive convertor
            return result;
        }

        // converts input to decimal
        private int binToDec(string value)
        {
            int result = 0;
            result = findDecimal(value); // calls recursive convertor
            return result;
        }

        // finds the given binary number for a decimal input using recursion
        private int findBinary(int decimalNumber)
        {
            if (decimalNumber == 0) // if values is 0, return 0 (aka if the number has been reduced to 0, end)
            {
                return 0;
            }
            else // else add the value mod 2 to the recursive call to calculate the next digit
            {
                return (decimalNumber % 2 + 10 * findBinary(decimalNumber / 2));
            }
        }

        // finds the given decimal number for a binary input using recursion
        private int findDecimal(string binaryNumber, int i=0)
        {
            int length = binaryNumber.Length; // stores number of digits in binary
            if (i == length-1) // if the end of the string has been reached, remove the last digit
            {
                return binaryNumber[i] - '0';
            }
            else // else left shift the value by the addition of the length and the recursive call
            {
                return (binaryNumber[i] - '0') << (length-i-1) + findDecimal(binaryNumber, i +1); // << means perform a left shift
            }
        }

        // returns the operand from the value format for the given position
        private int findCalculationValue(string[] values, int position, RAM RAM)
        {
            int calculationValue = 0;
            if(valueFormat == "#") // returns if the value is a hard value
            {
                calculationValue = Convert.ToInt32(values[position]);
            }
            else if(valueFormat == "R") // returns if the value is a register
            {
                calculationValue = registers[values[position]];
            }
            else // else the value is a RAM address
            {
                calculationValue = FetchData(values[position], RAM);
            }
            return calculationValue;
        }

        // fetches data from RAM
        private int FetchData(string address, RAM RAM)
        {
            if(cache.ReturnData(address) != -1) // if the address is stored in cache, return the value stored in cache at the given address
            {
                return cache.ReturnData(address);
            }
            return RAM.ReturnData(address); // else return the value stored in RAM at the given address
        }

        // writes data to RAM
        private void WriteData(string address, int data, RAM RAM)
        {
            RAM.StoreData(address, data); // store the given data in the given address in RAM
            cache.StoreData(address, data); // also store the given data in the given address in cache
        }

        // clears the current instance of registers and cache
        public void Clear()
        {
            registers = new Dictionary<string, int>(); // resets registers back to an empty dictionary
            cache.Clear(); // resets cache
        }

        // adds the address/data that just got changed to the interface
        private void UpdateInterface(string register, int data)
        {
            int reg = Convert.ToInt32(register);

            // if register is within the range of available registers
            if (reg >= Model.registerIndex && reg <= (Model.registerIndex + Model.registersData.Count()))
            {
                Model.registersData[reg - Model.registerIndex].Text = data.ToString();
            }
            // else address is not within range so do nothing
        }
    }
}