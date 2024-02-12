using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace NEA_CPU_Model
{
    internal class Processor : AbstractProcessor
    {
        // uses a dictionary to implement an associative array to store the register values
        private Dictionary<string, int> registers = new Dictionary<string, int> { };

        static public Cache cache = new Cache();

        // attributes
        // shows how many instructions have been executed
        static public int cycleCounter = 0;

        // controls which instruction is executed
        private int programCounter = 0;

        // controls if the end of the program has been met or if an error has occured
        private bool repeat = true;

        // temporary value for comparisons/branching
        private string temp = string.Empty;

        // constructor
        public Processor()
        {

        }

        // splits the instructions into each instruction and then into opcode and operand
        // then controls the CPU components in executing the instruction
        public override void FlowOfInstructions(List<string> instructions, RAM RAM, bool loop)
        {
            repeat = true;
            // code needs to execute all instructions at once
            if (loop)
            {
                // reset cycle values
                programCounter = 0;
                cycleCounter = 0;
                while (programCounter < instructions.Count && repeat) // while not reached end of code/error not encountered
                {
                    SplitInstruction(instructions, RAM); // call to process the instructions
                }
            }
            // execute only the next instruction
            else
            {
                if(programCounter < instructions.Count && repeat) // if not reached end of code/error not encountered
                {
                    SplitInstruction(instructions, RAM); // call to process the instructions
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
            programCounter++; // increments program counter for next cycle
            cycleCounter++; // increments cycle counter to show how many instructions have been executed

            if (!instruction.Contains(':')) // checks if a colon is present the instruction is a label
            {
                // splits the instruction into opcode and operand
                string opcode = Parser.GetOpcode(instruction);
                string operand = Parser.GetOperand(instruction);

                // splits the operand into the individual operands
                string[] values = operand.Split(',');


                // updates specific purpose registers on the interface
                Program.model.cirText.Text = opcode;
                Program.model.programCounterText.Text = programCounter.ToString();

                DecodeInstruction(opcode, values, RAM, instructions); // call point for the decoding of the instruction
            }
            // else a colon is present so it should ignore it
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
                    B(values, RAM, instructions);
                    break;

                case "B<EQ>":
                    Bcondition(values, RAM, instructions, "EQ");
                    break;

                case "B<NE>":
                    Bcondition(values, RAM, instructions, "NE");
                    break;

                case "B<GT>":
                    Bcondition(values, RAM, instructions, "GT");
                    break;

                case "B<LT>":
                    Bcondition(values, RAM, instructions, "LT");
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

                default: // in case an empty instruction was detected, go to next instruction
                    break;
            }
        }

        // load the value in the 2nd operand into the 1st operand
        private void LDR(string[] values, RAM RAM)
        {
            if (FetchData(values[1], RAM) != -1) // checks that the accessed address is not empty
            {
                if (values[0].Contains('R')) // checks if the user has put in the register with a label
                {
                    values[0] = values[0].Replace("R", "");
                }
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
            if (values[0].Contains('#')) // if the value in the 1st operand is a hard value
            {
                values[0] = values[0].Replace("#", ""); // removes the # from the operand
                WriteData(values[1], Convert.ToInt32(values[0]), RAM); // stores the data in the appropriate RAM address
            }
            else // else the value in the 1st operand is a register
            {
                values[0] = values[0].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[0])) // checks that the accessed register is not empty
                {
                    WriteData(values[1], registers[values[0]], RAM); // stores the data in the appropriate RAM address
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // adds the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void ADD(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            if (values[1].Contains('R')) // checks if the user has put in the register with a label
            {
                values[1] = values[1].Replace("R", "");
            }

            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = registers[values[1]] + Convert.ToInt32(values[2]); // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R')) // or if the value in the 3rd operand is a register
            {
                values[2] = values[2].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2])) // checks that the accessed registers are not empty
                {
                    result = registers[values[1]] + registers[values[2]]; // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (registers.ContainsKey(values[1]) && FetchData(values[2], RAM) != -1) // checks that the accessed register and RAM address is not empty
                {
                    result = registers[values[1]] + FetchData(values[2], RAM); // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // subtract the value in the 3rd operand from the 2nd operand and stores it in the 1st operand
        private void SUB(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            if (values[1].Contains('R')) // checks if the user has put in the register with a label
            {
                values[1] = values[1].Replace("R", "");
            }

            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = Convert.ToInt32(values[2]) - registers[values[1]]; // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R')) // or if the value in the 3rd operand is a register
            {
                values[2] = values[2].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2])) // checks that the accessed registers are not empty
                {
                    result = registers[values[2]] - registers[values[1]]; // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (registers.ContainsKey(values[1]) && FetchData(values[2], RAM) != -1) // checks that the accessed register and RAM address is not empty
                {
                    result = FetchData(values[2], RAM) - registers[values[1]]; // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // copies the value in the 2nd operand into the 1st operand
        private void MOV(string[] values, RAM RAM)
        {
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }

            if (values[1].Contains('#')) // if the value in the 2nd operand is a hard value
            {
                values[1] = values[1].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[0])) // if the address already has a value replace it
                {
                    registers[values[0]] = Convert.ToInt32(values[1]);
                }
                else // else add the address and its value to the dictionary
                {
                    registers.Add(values[0], Convert.ToInt32(values[1]));
                }
            }
            else if (values[1].Contains('R')) // else if the value in the 2nd operand is a register
            {
                values[1] = values[1].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[0]) && registers.ContainsKey(values[1]))// checks that the accessed registers are not empty
                {
                    registers[values[0]] = Convert.ToInt32(values[1]); // if the address already has a value replace it
                }
                else if (registers.ContainsKey(values[1]))
                {
                    registers.Add(values[0], Convert.ToInt32(values[1])); // else add the address and its value to the dictionar
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 2nd operand is a RAM address
            {
                if (registers.ContainsKey(values[0]) && FetchData(values[1], RAM) != -1)// checks that the accessed register and RAM address is not empty
                {
                    registers[values[0]] = FetchData(values[1], RAM); // if the address already has a value replace it
                }
                else if (FetchData(values[1], RAM) != -1)
                {
                    registers.Add(values[0], FetchData(values[1], RAM)); // else add the address and its value to the dictionary
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // compares the value in the 1st operand with the 2nd operand
        private void CMP(string[] values, RAM RAM)
        {
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            
            if (values[1].Contains('#')) // if the value in the 2nd operand is a hard value
            {
                values[1] = values[1].Replace("#", ""); // removes the R from the operand
                if (registers.ContainsKey(values[0])) // checks that the accessed register is not empty
                {
                    if (registers[values[0]] == Convert.ToInt32(values[1])) // records the result of the comparison in the temp variable
                    {
                        temp = "EQ";
                    }
                    if (registers[values[0]] != Convert.ToInt32(values[1]))
                    {
                        temp = "NE";
                    }
                    else if (registers[values[0]] > Convert.ToInt32(values[1]))
                    {
                        temp = "GT";
                    }
                    else if (registers[values[0]] < Convert.ToInt32(values[1]))
                    {
                        temp = "LT";
                    }
                }
                else  // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[1].Contains('R')) // else if the value in the 2nd operand is a register
            {
                values[1] = values[1].Replace("R", "");
                if (registers.ContainsKey(values[0]) && registers.ContainsKey(values[1])) // checks that the accessed registers are not empty
                {
                    if (registers[values[0]] == registers[values[1]]) // records the result of the comparison in the temp variable
                    {
                        temp = "EQ";
                    }
                    if (registers[values[0]] != registers[values[1]])
                    {
                        temp = "NE";
                    }
                    else if (registers[values[0]] > registers[values[1]])
                    {
                        temp = "GT";
                    }
                    else if (registers[values[0]] < registers[values[1]])
                    {
                        temp = "LT";
                    }
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else  // else the value in the 2nd operand is a RAM address
            {
                if (registers.ContainsKey(values[0]) && FetchData(values[1], RAM) != -1) // checks that the accessed register and RAM address are not empty
                {
                    if (registers[values[0]] == FetchData(values[1], RAM)) // records the result of the comparison in the temp variable
                    {
                        temp = "EQ";
                    }
                    if (registers[values[0]] != FetchData(values[1], RAM))
                    {
                        temp = "NE";
                    }
                    else if (registers[values[0]] > FetchData(values[1], RAM))
                    {
                        temp = "GT";
                    }
                    else if (registers[values[0]] < FetchData(values[1], RAM))
                    {
                        temp = "LT";
                    }
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/ RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // always branches to the label given by the operand
        private void B(string[] values, RAM RAM, List<string> instructions)
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

        // branches to the label given by the operand if the given condition was met by the last comparison
        private void Bcondition(string[] values, RAM RAM, List<string> instructions, string condition)
        {
            // checks if the string stored by thelast comparison met the given condition
            if (temp == condition)
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
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            if (values[1].Contains('R')) // checks if the user has put in the register with a label
            {
                values[1] = values[1].Replace("R", "");
            }

            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = registers[values[1]] & Convert.ToInt32(values[2]); // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R')) // or if the value in the 3rd operand is a register
            {
                values[2] = values[2].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2])) // checks that the accessed registers are not empty
                {
                    result = registers[values[1]] & registers[values[2]]; // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (registers.ContainsKey(values[1]) && FetchData(values[2], RAM) != -1) // checks that the accessed register and RAM address are not empty
                {
                    result = registers[values[1]] & FetchData(values[2], RAM); // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // Bitwise or the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void ORR(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            if (values[1].Contains('R')) // checks if the user has put in the register with a label
            {
                values[1] = values[1].Replace("R", "");
            }

            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = registers[values[1]] | Convert.ToInt32(values[2]); // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R')) // or if the value in the 3rd operand is a register
            {
                values[2] = values[2].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2])) // checks that the accessed registers are not empty
                {
                    result = registers[values[1]] | registers[values[2]]; // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (registers.ContainsKey(values[1]) && FetchData(values[2], RAM) != -1) // checks that the accessed register and RAM address are not empty
                {
                    result = registers[values[1]] | FetchData(values[2], RAM); // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // Bitwise xor the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void EOR(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            if (values[1].Contains('R')) // checks if the user has put in the register with a label
            {
                values[1] = values[1].Replace("R", "");
            }

            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = registers[values[1]] ^ Convert.ToInt32(values[2]); // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R')) // or if the value in the 3rd operand is a register
            {
                values[2] = values[2].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2])) // checks that the accessed registers are not empty
                {
                    result = registers[values[1]] ^ registers[values[2]]; // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (registers.ContainsKey(values[1]) && FetchData(values[2], RAM) != -1) // checks that the accessed register and RAM address are not empty
                {
                    result = registers[values[1]] ^ FetchData(values[2], RAM); // calcultes the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // Bitwise not the value in the 2nd operand and stores it in the 1st operand
        private void MVN(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            
            if (values[1].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[1] = values[1].Replace("#", ""); // removes the # from the operand

                result = NotLogic(Convert.ToInt32(values[1])); // calculates the result

                // updates the interface
                if (registers.ContainsKey(values[0])) // address exists, so update current data
                {
                    registers[values[0]] = result; // stores the result in the appropriate 
                }
                else // address doesn't exist, so need to add address
                {
                    registers.Add(values[0], result); // stores the result in the appropriate register
                }

                UpdateInterface(values[0], registers[values[0]]);
                Program.model.accumulatorText.Text = result.ToString();
            }
            else if (values[1].Contains('R')) // or if the value in the 3rd operand is a register
            {
                values[1] = values[1].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = NotLogic(registers[values[1]]); // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (FetchData(values[1], RAM) != -1) // checks that the accessed RAM address is not empty
                {
                    NotLogic(FetchData(values[1], RAM)); // calculates the result
                    registers[values[0]] = result; // stores the result in the appropriate register

                    // updates the interface
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // Bitwise left shift the value in the 2nd operand by the 3rd operand and stores it in the 1st operand
        private void LSL(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            if (values[1].Contains('R')) // checks if the user has put in the register with a label
            {
                values[1] = values[0].Replace("R", "");
            }

            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", "");
                if (registers.ContainsKey(values[1]))
                {
                    int calculation = registers[values[1]]; // creates a temporary value to carry out the calculation on
                    for (int i = 0; i < Convert.ToInt32(values[2]); i++)
                    {
                        calculation *= 2;
                    }
                    result = calculation;
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R')) // else if the value in the 3rd operand is a register
            {
                values[2] = values[2].Replace("R", "");
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2]))
                {
                    result = registers[values[1]];
                    for (int i = 0; i < registers[values[2]]; i++)
                    {
                        result *= 2;
                    }
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (registers.ContainsKey(values[1]) && FetchData(values[2], RAM) != -1)
                {
                    result = registers[values[1]];
                    for (int i = 0; i < FetchData(values[2], RAM); i++)
                    {
                        result *= 2;
                    }
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // Bitwise right shift the value in the 2nd operand by the 3rd operand and stores it in the 1st operand
        private void LSR(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[0].Contains('R')) // checks if the user has put in the register with a label
            {
                values[0] = values[0].Replace("R", "");
            }
            if (values[1].Contains('R')) // checks if the user has put in the register with a label
            {
                values[1] = values[0].Replace("R", "");
            }

            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", "");
                if (registers.ContainsKey(values[1]))
                {
                    result = registers[values[1]];
                    for (int i = 0; i < Convert.ToInt32(values[2]); i++)
                    {
                        result /= 2;
                    }
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R')) // else if the value in the 3rd operand is a register
            {
                values[2] = values[2].Replace("R", "");
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2]))
                {
                    result = registers[values[1]];
                    for (int i = 0; i < registers[values[2]]; i++)
                    {
                        result /= 2;
                    }
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else // else the value in the 3rd operand is a RAM address
            {
                if (registers.ContainsKey(values[1]) && FetchData(values[2], RAM) != -1)
                {
                    result = registers[values[1]];
                    for (int i = 0; i < FetchData(values[2], RAM); i++)
                    {
                        result *= 2;
                    }
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else // address is empty, exit out
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // does the bitwise logic
        private int NotLogic(int value)
        {
            string result = string.Empty;

            // converts the input to binary as this is necessary to carry out bitwise operations
            string operand = denToBin(value);

            for (int i = 0; i < operand.Length; i++)
            {
                if (operand[i] == 1) // if input is 1, output is 0
                {
                    result += '0';
                }
                else // else output is 0
                {
                    result += '1';
                }
            }

            return binToDen(result); // converts output to denary and returns to call point
        }

        // converts input to binary
        private string denToBin(int value)
        {
            string result = " ";
            result = findBinary(value).ToString(); // calls recursive convertor
            return result;
        }

        // converts input to denary
        private int binToDen(string value)
        {
            int result = 0;
            result = findDecimal(value); // calls recursive convertor
            return result;
        }

        // finds the given binary number for a denary input using recursion
        private int findBinary(int denaryNumber)
        {
            if (denaryNumber == 0) // if values is 0, return 0 (aka if the number has been reduced to 0, end)
            {
                return 0;
            }
            else // else add the value mod 2 to the recursive call to calculate the next digit
            {
                return (denaryNumber % 2 + 10 * findBinary(denaryNumber / 2));
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