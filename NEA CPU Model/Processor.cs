using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class Processor
    {
        private List<string> instructions;
        public Processor(List<string> instructions)
        {
            this.instructions = instructions;
        }

        public void Execute(List<string> instructions)
        {
        foreach(var instruction in instructions)
            {
                string opcode = GetOpcode(instruction);
                switch (opcode)
                {
                    case "10001":
                        break;
                    case "10000":
                        break;
                    case "01111":
                        break;
                    case "01110":
                        break;
                    case "01101":
                        break;
                    case "01100":
                        break;
                    case "01011":
                        break;
                    case "01010":
                        break;
                    case "01001":
                        break;
                    case "01000":
                        break;
                    case "00111":
                        break;
                    case "00110":
                        break;
                    case "00101":
                        break;
                    case "00100":
                        break;
                    case "00011":
                        break;
                    case "00010":
                        break;
                    case "00001":
                        break;
                    case "00000":
                        goto Exit;
                    default:
                        break;
                }
            }
        Exit:
            MessageBox.Show("HALT instruction found");
        }


        // splits the operand from the instruction
        static string GetOperand(string instruction)
        {
            string operand = string.Empty;
            // if the Opcode is HALT, the operand will be blank
            if (GetOpcode(instruction) == "HALT")
            {
                return " ";
            }
            // if the Opcode starts with B, it is a branch instruction
            else if (GetOpcode(instruction)[0] == 'B')
            {
                // checks if the branch has a condition or not
                if (instruction[1] == ' ')
                {
                    for (int i = 0; i < instruction.Length - 3; i++)
                    {
                        operand += instruction[i + 1];
                    }
                }
                else
                {
                    for (int i = 0; i < instruction.Length - 3; i++)
                    {
                        operand += instruction[i + 5];
                    }
                }
            }
            // else the instruction is standard 3 long Opcode and it can be handled as standard
            else
            {
                for (int i = 0; i < instruction.Length - 3; i++)
                {
                    operand += instruction[i + 3];
                }
            }

            return operand;
        }

        // splits the opcode from the operand (but doesn't verify valid opcode)
        static string GetOpcode(string instruction)
        {
            // if the instruction is just HALT, then the opcode is just HALT
            if (instruction == "HALT")
            {
                return "HALT";
            }
            // if the Opcode starts with B, it is a branch instruction
            else if (instruction[0] == 'B')
            {
                // checks if the branch has a condition or not
                if (instruction[1] == ' ')
                {
                    return "B";
                }
                else
                {
                    return string.Concat(instruction[0], instruction[1], instruction[2], instruction[3], instruction[4]);
                }
            }
            // else the instruction is standard 3 long Opcode and it can be handled as standard
            else
            {
                return string.Concat(instruction[0], instruction[1], instruction[2]);
            }
        }
    }
}
