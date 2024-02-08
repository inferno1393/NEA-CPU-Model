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

        // attributes
        private int programCounter = 0; // controls which instruction is executed
        private bool repeat = true; // controls if the end of the program has been met (or an error has occured)
        private string temp = string.Empty; // temporary value for comparisons/branching
        private int[] binary = { 128, 64, 32, 16, 8, 4, 2, 1 }; // int array for 8 bit binary conversion

        // constructor
        public Processor()
        {

        }

        // splits the instructions into each instruction and then into opcode and operand
        // then controls the CPU components in executing the instruction
        public override void Flow(List<string> instructions, RAM RAM, bool loop)
        {
            repeat = true;
            // code needs to execute all instructions at once
            if (loop)
            {
                programCounter = 0; // sets program counter to start of instructions
                while (programCounter < instructions.Count && repeat) // while not reached end of code/error not encountered
                {
                    Execute(instructions, RAM); // call to process the instructions
                }
            }
            // execute only the next instruction
            else
            {
                if(programCounter < instructions.Count && repeat) // while not reached end of code/error not encountered
                {
                    Execute(instructions, RAM); // call to process the instructions
                }
                else
                {
                    programCounter = 0; // sets program counter to start of instructions ready for next cycle
                    // exit program as end reached
                }
            }
        }

        // controls the program counter during execution and splits the instruction
        protected override void Execute(List<string> instructions, RAM RAM)
        {
            string instruction = instructions[programCounter]; // gets current instruction
            programCounter++; // increments program counter for next cycle
            if (instruction.Contains(':')) // since a colon is present the instruction is a label
            {
                // so should be ignored
            }
            else
            {
                // splits the instruction into opcode and operand
                string opcode = Parser.GetOpcode(instruction);
                string operand = Parser.GetOperand(instruction);

                // splits the operand into the individual operands
                string[] values = operand.Split(',');

                // updates specific purpose registers on the interface
                Program.model.cirText.Text = opcode;
                Program.model.programCounterText.Text = programCounter.ToString();

                DecodeInstruction(opcode, values, RAM, instructions); // call point for the actual execution of the instruction
                
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
            }
        }

        // load the value in the 2nd operand into the 1st operand
        private void LDR(string[] values, RAM RAM)
        {
            if (RAM.ReturnData(values[1]) != -1) // checks that the accessed address is not empty
            {
                registers[values[0]] = RAM.ReturnData(values[1]); // sets the appropriate register to the value fetched from RAM
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
                RAM.StoreData(values[1], Convert.ToInt32(values[0])); // stores the data in the appropriate RAM address
            }
            else // else the value in the 1st operand is a register
            {
                values[0] = values[0].Replace("R", ""); // removes the R from the operand
                if (registers.ContainsKey(values[0])) // checks that the accessed register is not empty
                {
                    RAM.StoreData(values[1], registers[values[0]]); // stores the data in the appropriate RAM address
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
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1) // checks that the accessed register and RAM address is not empty
                {
                    result = registers[values[1]] + RAM.ReturnData(values[2]); // calculates the result
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
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1) // checks that the accessed register and RAM address is not empty
                {
                    result = RAM.ReturnData(values[2]) - registers[values[1]]; // calculates the result
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
                if (registers.ContainsKey(values[0]) && RAM.ReturnData(values[1]) != -1)// checks that the accessed register and RAM address is not empty
                {
                    registers[values[0]] = RAM.ReturnData(values[1]); // if the address already has a value replace it
                }
                else if (RAM.ReturnData(values[1]) != -1)
                {
                    registers.Add(values[0], RAM.ReturnData(values[1])); // else add the address and its value to the dictionary
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
                if (registers.ContainsKey(values[0]) && RAM.ReturnData(values[1]) != -1) // checks that the accessed register and RAM address are not empty
                {
                    if (registers[values[0]] == RAM.ReturnData(values[1])) // records the result of the comparison in the temp variable
                    {
                        temp = "EQ";
                    }
                    if (registers[values[0]] != RAM.ReturnData(values[1]))
                    {
                        temp = "NE";
                    }
                    else if (registers[values[0]] > RAM.ReturnData(values[1]))
                    {
                        temp = "GT";
                    }
                    else if (registers[values[0]] < RAM.ReturnData(values[1]))
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
            if (temp == condition) // checks if the last comparison met the given condition
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
            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = BinaryLogic(registers[values[1]], Convert.ToInt32(values[2]), "AND"); // calcultes the result
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
                    result = BinaryLogic(registers[values[1]], registers[values[2]], "AND"); // calcultes the result
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
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1) // checks that the accessed register and RAM address are not empty
                {
                    result = BinaryLogic(registers[values[1]], RAM.ReturnData(values[2]), "AND"); // calcultes the result
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
            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = BinaryLogic(registers[values[1]], Convert.ToInt32(values[2]), "OR"); // calcultes the result
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
                    result = BinaryLogic(registers[values[1]], registers[values[2]], "OR"); // calcultes the result
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
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1) // checks that the accessed register and RAM address are not empty
                {
                    result = BinaryLogic(registers[values[1]], RAM.ReturnData(values[2]), "OR"); // calcultes the result
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
            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", ""); // removes the # from the operand
                if (registers.ContainsKey(values[1])) // checks that the accessed register is not empty
                {
                    result = BinaryLogic(registers[values[1]], Convert.ToInt32(values[2]), "EOR"); // calcultes the result
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
                    result = BinaryLogic(registers[values[1]], registers[values[2]], "EOR"); // calcultes the result
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
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1) // checks that the accessed register and RAM address are not empty
                {
                    result = BinaryLogic(registers[values[1]], RAM.ReturnData(values[2]), "EOR"); // calcultes the result
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
            if (values[1].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[1] = values[1].Replace("#", ""); // removes the # from the operand

                result = BinaryLogic(Convert.ToInt32(values[1]), 0, "MVN"); // calculates the result
                // updates the interface

                if (registers.ContainsKey(values[0])) // address exists, so update current data
                {
                    
                    registers[values[0]] = result; // stores the result in the appropriate register

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
                    result = BinaryLogic(registers[values[1]], 0, "MVN"); // calculates the result
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
                if (RAM.ReturnData(values[1]) != -1) // checks that the accessed RAM address is not empty
                {
                    BinaryLogic(RAM.ReturnData(values[1]), 0, "MVN"); // calculates the result
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
            if (values[2].Contains('#')) // if the value in the 3rd operand is a hard value
            {
                values[2] = values[2].Replace("#", "");
                if (registers.ContainsKey(values[1]))
                {
                    result = registers[values[1]];
                    for (int i = 0; i < Convert.ToInt32(values[2]); i++)
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
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1)
                {
                    result = registers[values[1]];
                    for (int i = 0; i < RAM.ReturnData(values[2]); i++)
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
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1)
                {
                    result = registers[values[1]];
                    for (int i = 0; i < RAM.ReturnData(values[2]); i++)
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
        private int BinaryLogic(int value1, int value2, string opcode)
        {
            string result = string.Empty;
            // converts the inputs to binary as this is necessary to carry out bitwise operations
            string operand1 = denToBin(value1);
            string operand2 = denToBin(value2);

            if (opcode == "AND")
            {
                for (int i = 0; i < operand2.Length; i++) // loops through for each digit in the binary representation of the operand
                {
                    if (operand1[i] == '1' && operand2[i] == '1') // if both inputs are 1, output is 1
                    {
                        result += '1';
                    }
                    else // else output is 0
                    {
                        result += '0';
                    }
                }
            }
            else if (opcode == "OR")
            {
                for (int i = 0; i < operand2.Length; i++) // loops through for each digit in the binary representation of the operand
                {
                    if (operand1[i] == '1' || operand2[i] == '1') // if either inputs are 1, output is 1
                    {
                        result += '1';
                    }
                    else // else output is 0
                    {
                        result += '0';
                    }
                }
            }
            else if (opcode == "EOR")
            {
                for (int i = 0; i < operand2.Length; i++) // loops through for each digit in the binary representation of the operand
                {
                    if ((operand1[i] == '1' || operand2[i] == '1') && !(operand1[i] == '1' && operand2[i] == '1')) // if only 1 of the inputs is 1, output is 1
                    {
                        result += '1';
                    }
                    else // else output is 0
                    {
                        result += '0';
                    }
                }
            }
            else // must be a not opcode
            {
                for (int i = 0; i < operand1.Length; i++) // loops through for each digit in the binary representation of the operand
                {
                    if (operand1[i] == 1) // if input is 1, output is 0
                    {
                        result += '0';
                    }
                    else // else output is 0
                    {
                        result += '1';
                    }
                }
            }

            return binToDen(result); // converts output to denary and returns to call point
        }

        // converts input to binary
        private string denToBin(int v)
        {
            string r = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                if (v >= binary[i])
                {
                    r += '1';
                    v -= binary[i];
                }
            }
            return r;
        }
        // converts input to denary
        private int binToDen(string v)
        {

            int r = 0;
            for (int i = 0; i < 8; i++)
            {
                if (Convert.ToInt32(v) >= binary[i])
                {
                    r += binary[i];
                    v = (Convert.ToInt32(v) - binary[i]).ToString();
                }
            }
            return r;
        }

        // clears the current instance of memory
        public void Clear()
        {
            registers = new Dictionary<string, int>(); // resets registers back to an empty dictionary
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