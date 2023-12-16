using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    public class Parser
    {
        // Attributes
        private StackArray<string> splitInstructions;
        private QueueArray<string> instructions;

        // constructor
        public Parser(QueueArray<string> instructions, StackArray<string> splitInstructions)
        {
            this.splitInstructions = splitInstructions;
            this.instructions = instructions;
        }

        // returns a string showing the validity of the instructions
        public string ParseInstructions(QueueArray<string> instructions, StackArray<string> splitInstructions)
        {
            // splits the instructions in the queue into Opcode and Operand
            for (int i = 0; i < instructions.Count; i++)
            {
                string instruction = instructions.Dequeue();
                splitInstructions.Push(GetOpcode(instruction));
                splitInstructions.Push(GetOperand(instruction));

                instructions.Enqueue(instruction);
            }

            splitInstructions.Pop(); // pops of the operand of the HALT instruction

            // checks last instruction is a HALT (since program must halt)
            if (splitInstructions.Pop() != "HALT")
            {
                return "Invalid, no HALT command";
            }

            // checks instructions are valid
            for (int i = 0; i < splitInstructions.Count; i++)
            {
                if (!CheckInstruction(splitInstructions.Pop(), splitInstructions.Pop()))
                {
                    return "Invalid, incorrect instruction";
                }
            }

            // else the instructions are valid
            return "Valid";
        }

        static bool CheckInstruction(string Operand, string Opcode)
        {
            // if the Opcode is invalid, the instruction is invalid
            if (OpcodeFormat(Opcode) == "invalid")
            {
                return false;
            }
            // if the Operand is invalid, the instruction is invalid
            else if (OperandFormat(Operand) == "invalid")
            {

            }
            // if the Operand is not in the correct format for the Opcode, the instruction is invalid
            else if (OpcodeFormat(Opcode) != OperandFormat(Operand))
            {
                return false;
            }

            // else the instruction is valid
            return true;
        }

        static string OperandFormat(string Operand)
        {
            // returns the format the Operand is in by counting the commans present
            string format = string.Empty;
            int count = 0;

            for (int i = 0; i < Operand.Length; i++)
            {
                if (Operand[i] == ',')
                {
                    count++;
                }
            }

            if (Operand == " ")
            {
                format = " ";
            }
            else
            {
                switch (count)
                {
                    case 0:
                        format = "<label>";
                        break;
                    case 1:
                        format = "Rd, <memory ref>";
                        break;
                    case 2:
                        format = "Rd, Rn, <memory ref>";
                        break;
                    default:
                        format = "invalid";
                        break;
                }
            }

            return format;
        }

        static string OpcodeFormat(string Opcode)
        {
            // uses a dictionary to return the format the operand should be in
            string format = string.Empty;

            Dictionary<string, string> instructionSet = new()
            {
                { "LDR", "Rd, <memory ref>" },
                { "STR", "Rd, <memory ref>" },
                { "ADD", "Rd, Rn, <memory ref>" },
                { "SUB", "Rd, Rn, <memory ref>" },
                { "MOV", "Rd, <memory ref>" },
                { "CMP", "Rd, <memory ref> " },
                { "B", "<label>"},
                { "B<EQ>", "<label>" },
                { "B<NE>", "<label>" },
                { "B<GT>", "<label>" },
                { "B<LT>", "<label>" },
                { "AND", "Rd, Rn, <memory ref>" },
                { "ORR", "Rd, Rn, <memory ref>" },
                { "EOR", "Rd, Rn, <memory ref>" },
                { "MVN", "Rd, <memory ref>" },
                { "LSL", "Rd, Rn, <memory ref>" },
                { "LSR", "Rd, Rn, <memory ref>" },
            };

            if (instructionSet.ContainsKey(Opcode))
            {
                format = instructionSet[Opcode];
            }
            else
            {
                format = "invalid";
            }

            return format;
        }

        static string GetOperand(string instruction)
        {
            string operand = string.Empty;
            // splits the operand from the instruction
            if (GetOpcode(instruction) == "HALT")
            {
                operand = " ";
            }
            else if (GetOpcode(instruction)[0] == 'B')
            {
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
            else
            {
                for (int i = 0; i < instruction.Length - 3; i++)
                {
                    operand += instruction[i + 3];
                }
            }

            return operand;
        }

        static string GetOpcode(string instruction)
        {
            string opcode = string.Empty;
            // splits the opcode from the operand (but doesn't verify valid opcode)
            if (instruction == "HALT")
            {
                opcode = "HALT";
            }
            else if (instruction[0] == 'B')
            {
                if (instruction[1] == ' ')
                {
                    opcode = "B";
                }
                else
                {
                    opcode = String.Concat(instruction[0], instruction[1], instruction[2], instruction[3], instruction[4]);
                }
            }
            else
            {
                opcode = String.Concat(instruction[0], instruction[1], instruction[2]);
            }
            return opcode;
        }

    }
}