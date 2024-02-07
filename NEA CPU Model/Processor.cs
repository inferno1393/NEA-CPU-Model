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
        public int programCounter = 0;
        public bool repeat = true;
        public string temp = string.Empty;

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
                programCounter = 0;
                while (programCounter < instructions.Count && repeat)
                {
                    Execute(instructions, RAM);
                }
            }
            // execute only the next instruction
            else
            {
                if(programCounter < instructions.Count && repeat)
                {
                    Execute(instructions, RAM);
                }
                else
                {
                    programCounter = 0;
                    // exit program as end reached
                }
            }
        }

        // controls the program counter during execution and splits the instruction
        private void Execute(List<string> instructions, RAM RAM)
        {
            string instruction = instructions[programCounter];
            programCounter++;
            if (instruction.Contains(':'))
            {
                // instruction is a label so should be ignored
            }
            else
            {
                string opcode = Parser.GetOpcode(instruction);
                Program.model.cirText.Text = opcode;
                string operand = Parser.GetOperand(instruction);
                string[] values = operand.Split(',');

                Decode(opcode, values, RAM, instructions);
                Program.model.programCounterText.Text = programCounter.ToString();
            }
        }

        // decodes the instruction given and calls the appropriate subroutine to execute it
        private void Decode(string opcode, string[] values, RAM RAM, List<string> instructions)
        {
            // follow appropriate steps based on opcode
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
            if (RAM.ReturnData(values[1]) != -1)
            {
                registers[values[0]] = RAM.ReturnData(values[1]);
                UpdateInterface(values[0], registers[values[0]]);
            }
            else
            {
                MessageBox.Show($"Attempted to access empty RAM address in line {programCounter}");
                repeat = false;
            }
        }

        // store the value in the 1st operand into the 2nd operand
        private void STR(string[] values, RAM RAM)
        {
            if (values[0].Contains('#'))
            {
                values[0] = values[0].Replace("#", "");
                RAM.StoreData(values[1], Convert.ToInt32(values[0]));
            }
            else
            {
                if (registers.ContainsKey(values[0]))
                {
                    RAM.StoreData(values[1], registers[values[0]]);
                }
                else
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
            if (values[2].Contains('#'))
            {
                values[2] = values[2].Replace("#", "");
                if (registers.ContainsKey(values[1]))
                {
                    result = registers[values[1]] + Convert.ToInt32(values[2]);
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R'))
            {
                values[2] = values[2].Replace("R", "");
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2]))
                {
                    result = registers[values[1]] + registers[values[2]];
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else
            {
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1)
                {
                    result = registers[values[1]] + RAM.ReturnData(values[2]);
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
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
            if (values[2].Contains('#'))
            {
                values[2] = values[2].Replace("#", "");
                if (registers.ContainsKey(values[1]))
                {
                    result = Convert.ToInt32(values[2]) - registers[values[1]];
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R'))
            {
                values[2] = values[2].Replace("R", "");
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2]))
                {
                    result = registers[values[2]] - registers[values[1]];
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else
            {
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1)
                {
                    result = RAM.ReturnData(values[2]) - registers[values[1]];
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // copies the value in the 2nd operand into the 1st operand
        private void MOV(string[] values, RAM RAM)
        {
            if (values[1].Contains('#'))
            {
                values[1] = values[1].Replace("#", "");
                if (registers.ContainsKey(values[0]))
                {
                    registers[values[0]] = Convert.ToInt32(values[1]);
                }
                else
                {
                    registers.Add(values[0], Convert.ToInt32(values[1]));
                }
            }
            else if (values[1].Contains('R'))
            {
                values[1] = values[1].Replace("R", "");
                if (registers.ContainsKey(values[0]) && registers.ContainsKey(values[1]))
                {
                    registers[values[0]] = Convert.ToInt32(values[1]);
                }
                else if (registers.ContainsKey(values[1]))
                {
                    registers.Add(values[0], Convert.ToInt32(values[1]));
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else
            {
                if (registers.ContainsKey(values[0]) && RAM.ReturnData(values[1]) != -1)
                {
                    registers[values[0]] = RAM.ReturnData(values[1]);
                }
                else if (RAM.ReturnData(values[1]) != -1)
                {
                    registers.Add(values[0], RAM.ReturnData(values[1]));
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // compares the value in the 1st operand with the 2nd operand
        private void CMP(string[] values, RAM RAM)
        {
            if (values[1].Contains('#'))
            {
                values[1] = values[1].Replace("#", "");
                if (registers.ContainsKey(values[0]))
                {
                    if (registers[values[0]] == Convert.ToInt32(values[1]))
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
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[1].Contains('R'))
            {
                values[1] = values[1].Replace("R", "");
                if (registers.ContainsKey(values[0]) && registers.ContainsKey(values[1]))
                {
                    if (registers[values[0]] == registers[values[1]])
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
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else
            {
                if (registers.ContainsKey(values[0]) && RAM.ReturnData(values[1]) != -1)
                {
                    if (registers[values[0]] == RAM.ReturnData(values[1]))
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
                else
                {
                    MessageBox.Show($"Attempted to access empty register/ RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        // always branches to the label given by the operand
        private void B(string[] values, RAM RAM, List<string> instructions)
        {
            if (Parser.labels.ContainsKey(values[0]))
            {
                programCounter = Parser.labels[values[0]];
            }
            else
            {
                MessageBox.Show("Label not found");
                repeat = false;
            }
        }

        // branches to the label given by the operand if the given condition was met by the last comparison
        private void Bcondition(string[] values, RAM RAM, List<string> instructions, string condition)
        {
            if (temp == condition)
            {
                
            }
        }

        // Bitwise and the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void AND(string[] values, RAM RAM)
        {

        }

        // Bitwise or the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void ORR(string[] values, RAM RAM)
        {

        }

        // Bitwise xor the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
        private void EOR(string[] values, RAM RAM)
        {

        }

        // Bitwise not the value in the 2nd operand and stores it in the 1st operand
        private void MVN(string[] values, RAM RAM)
        {

        }

        // Bitwise left shift the value in the 2nd operand by the 3rd operand and stores it in the 1st operand
        private void LSL(string[] values, RAM RAM)
        {
            int result = 0;
            if (values[2].Contains('#'))
            {
                values[2] = values[2].Replace("#", "");
                if (registers.ContainsKey(values[1]))
                {
                    result = registers[values[1]] * (2 * Convert.ToInt32(values[2]));
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R'))
            {
                values[2] = values[2].Replace("R", "");
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2]))
                {
                    result = registers[values[1]] * (2 * registers[values[2]]);
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else
            {
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1)
                {
                    result = registers[values[1]] * (2 * RAM.ReturnData(values[2]));
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
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
            if (values[2].Contains('#'))
            {
                values[2] = values[2].Replace("#", "");
                if (registers.ContainsKey(values[1]))
                {
                    result = registers[values[1]] / (2 * Convert.ToInt32(values[2]));
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else if (values[2].Contains('R'))
            {
                values[2] = values[2].Replace("R", "");
                if (registers.ContainsKey(values[1]) && registers.ContainsKey(values[2]))
                {
                    result = registers[values[1]] / (2 * registers[values[2]]);
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register in line {programCounter}");
                    repeat = false;
                }
            }
            else
            {
                if (registers.ContainsKey(values[1]) && RAM.ReturnData(values[2]) != -1)
                {
                    result = registers[values[1]] / (2 * RAM.ReturnData(values[2]));
                    registers[values[0]] = result;
                    UpdateInterface(values[0], registers[values[0]]);
                    Program.model.accumulatorText.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show($"Attempted to access empty register/RAM address in line {programCounter}");
                    repeat = false;
                }
            }
        }

        private void UpdateInterface(string register, int data)
        {
            int reg = Convert.ToInt32(register);
            if  (reg >= 0 && reg <= 3)
            {
                Model.registersData[reg].Text = data.ToString();
            }
            else
            {
                MessageBox.Show("Register out of range");
            }
        }
    }
}