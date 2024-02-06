using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class Processor : AbstractProcessor
    {
        // uses a dictionary to implement an associative array to store the register values
        private Dictionary<string, int> registers = new Dictionary<string, int> { };
        public int programCounter = 0;
        public bool repeat = true;

        // constructor
        public Processor()
        {

        }

        // splits the instructions into each instruction and then into opcode and operand
        // then controls the CPU components in executing the instruction
        public override void Flow(List<string> instructions, RAM RAM, bool loop)
        {
            // code needs to execute all instructions at once
            if (loop)
            {
                programCounter = 0;
                while (programCounter < instructions.Count && repeat)
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
            }
            // execute only the next instruction
            else
            {
                if(programCounter < instructions.Count && repeat)
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
                else
                {
                    // exit program as end reached
                }
            }
        }
        


        private void Decode(string opcode, string[] values, RAM RAM, List<string> instructions)
        {
            int result = 0;
            string temp = string.Empty;
            // follow appropriate steps based on opcode
            switch (opcode)
            {
                // load the value in the 2nd operand into the 1st operand
                case "LDR":
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
                    break;

                // store the value in the 1st operand into the 2nd operand
                case "STR":
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
                    break;

                // adds the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
                case "ADD":
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
                    break;

                // subtract the value in the 3rd operand from the 2nd operand and stores it in the 1st operand
                case "SUB":                 
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
                    break;

                // copies the value in the 2nd operand into the 1st operand
                case "MOV":
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
                        else if(RAM.ReturnData(values[1]) != -1)
                        {
                            registers.Add(values[0], RAM.ReturnData(values[1]));
                        }
                        else
                        {
                            MessageBox.Show($"Attempted to access empty RAM address in line {programCounter}");
                            repeat = false;
                        }
                    }
                    break;

                // compares the value in the 1st operand with the 2nd operand
                case "CMP":
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
                            if(registers[values[0]] == registers[values[1]])
                            {
                                temp = "EQ";
                            }
                            if(registers[values[0]] != registers[values[1]])
                            {
                                temp = "NE";
                            }
                            else if (registers[values[0]] > registers[values[1]])
                            {
                                temp = "GT";
                            }
                            else if(registers[values[0]] < registers[values[1]])
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
                    break;

                // always branches to the label given by the operand
                case "B":
                    string inst = string.Empty;
                    for (int i = 0; i < instructions.Count; i++)
                    {
                        inst = Parser.GetOperand(instructions[i]).Replace(":", "");
                        if (inst == values[0])
                        {
                            programCounter = i;
                        }                        
                    }
                        break;

                // branches to the label given by the operand if the last comparison was EQ
                case "B<EQ>":
                    if (temp == "EQ")
                    {
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            if (Parser.GetOperand(instructions[i]) == values[0])
                            {
                                programCounter = i;
                                break;
                            }
                        }
                    }
                    break;

                // branches to the label given by the operand if the last comparison was NE
                case "B<NE>":
                    if (temp == "NE")
                    {
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            if (Parser.GetOperand(instructions[i]) == values[0])
                            {
                                programCounter = i;
                                break;
                            }
                        }
                    }
                    break;

                // branches to the label given by the operand if the last comparison was GT
                case "B<GT>":
                    if (temp == "GT")
                    {
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            if (Parser.GetOperand(instructions[i]) == values[0])
                            {
                                programCounter = i;
                                break;
                            }
                        }
                    }
                    break;

                // branches to the label given by the operand if the last comparison was LT
                case "B<LT>":
                    if (temp == "LT")
                    {
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            if (Parser.GetOperand(instructions[i]) == values[0])
                            {
                                programCounter = i;
                                break;
                            }
                        }
                    }
                    break;

                // Bitwise and the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
                case "AND":
                   
                    break;

                // Bitwise or the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
                case "ORR":
                    break;

                // Bitwise xor the value in the 3rd operand with the 2nd operand and stores it in the 1st operand
                case "EOR":
                    break;

                // Bitwise not the value in the 2nd operand and stores it in the 1st operand
                case "MVN":
                    break;

                // Bitwise left shift the value in the 2nd operand by the 3rd operand and stores it in the 1st operand
                case "LSL":
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
                    break;

                // Bitwise right shift the value in the 2nd operand by the 3rd operand and stores it in the 1st operand
                case "LSR":
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
                    break;

                // end of code execution, exit
                case "HALT":
                    repeat = false;
                    break;
            }
        }
        private void UpdateInterface(string register, int data)
        {
            switch (register)
            {
                case "0":
                    Program.model.RData0.Text = data.ToString();
                    break;
                case "1":
                    Program.model.RData1.Text = data.ToString();
                    break;
                case "2":
                    Program.model.RData2.Text = data.ToString();
                    break;
                case "3":
                    Program.model.RData3.Text = data.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}