﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;

namespace NEA_CPU_Model
{
    public class Parser
    {
        // Attributes
        private StackArray<string> splitInstructions;
        private List<string> instructions;

        // uses a dictionary to implement an associative array to store the location of labels
        static public Dictionary<string, int> labels = new Dictionary<string, int> { };

        // creates a dictionary of the valid Opcodes and the format their Operand should be in
        static private Dictionary<string, string> instructionSet = new()
        {
            { "LDR", "Rd, <memory ref>" },
            { "STR", "Rd, <memory ref>" },
            { "ADD", "Rd, Rn, <memory ref>" },
            { "SUB", "Rd, Rn, <memory ref>" },
            { "MOV", "Rd, <memory ref>" },
            { "CMP", "Rd, <memory ref>" },
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
            { "HALT", "" }
        };

        // constructor
        public Parser(List<string> instructions)
        {
            // sets the instructions list attribute to equal the passed in list
            this.instructions = instructions;

            // initializes splitInstructions and sets the capacity of splitInstructions to be double the length of the list
            // because each instruction will be split into opcode and operand
            this.splitInstructions = new StackArray<string>(instructions.Count * 2);
        }

        // returns a string showing the validity of the instructions
        public string ParseInstructions(List<string> instructions)
        {
            int count = instructions.Count; // initializes an iteration value that can be decremented to remove instructions to ignore
            // splits the instructions in the queue into Opcode and Operand
            for (int i = 0; i < instructions.Count; i++)
            {
                string instruction = instructions[i];

                if (instruction.Contains(':')) // checks if the current instruction is a label
                {
                    // instruction is a label so should be added to labels array
                    // it is added to labels array so branch instructions can send to the correct location
                    string operand = instruction.Replace(":", "");
                    if (labels.ContainsKey(operand))
                    {
                        labels[operand] = i;
                    }
                    else
                    {
                        labels.Add(operand, i);
                    }
                    count--;
                }
                else if(instruction == "" || instruction == " ") // checks if the current instruction is blank
                {
                    // the instruction should be ignored
                    count--;
                }
                else // push the opcode and operand onto the stack
                {
                    splitInstructions.Push(GetOpcode(instruction));
                    splitInstructions.Push(GetOperand(instruction));
                }
            }

            if (count > 0) // checks that an instruction has been entered (that is not a label)
            {
                splitInstructions.Pop(); // pops off the operand of the HALT instruction

                // checks last instruction is a HALT (since program must halt)
                if (splitInstructions.Pop() != "HALT")
                {
                    return "Invalid, no HALT command";
                }

                // checks instructions are valid
                for (int i = count; i > 1; i--) // ends at 1 to avoid emptying the stack 
                {
                    if (!CheckInstruction(splitInstructions.Pop(), splitInstructions.Pop()))
                    {
                        return $"Invalid, incorrect instruction at line {i - 1}";
                    }
                }

                // else instructions are valid
                return "Valid";
            }

            return "Invalid, no instructions were entered";
        }
        private bool CheckInstruction(string Operand, string Opcode)
        {
            // if the Opcode is invalid, the instruction is invalid
            if (OpcodeFormat(Opcode) == "invalid")
            {
                return false;
            }
            // if the Operand is invalid, the instruction is invalid
            else if (OperandFormat(Operand) == "invalid")
            {
                return false;
            }
            // if the Operand is not in the correct format for the Opcode, the instruction is invalid
            else if (OpcodeFormat(Opcode) != OperandFormat(Operand))
            {
                return false;
            }

            // else the instruction is valid
            return true;
        }

        // returns the format the Operand is in by counting the commas present
        private string OperandFormat(string Operand)
        {
            int count = 0;

            // counts commas in the Operand string
            for (int i = 0; i < Operand.Length; i++)
            {
                if (Operand[i] == ',')
                {
                    count++;
                }
            }

            // if the Operand is blank then the format is blank and that is returned instead
            if (Operand == "")
            {
                return "";
            }
            // returns the Operand format based on the number of commas counted
            else
            {
                switch (count)
                {
                    case 0:
                        return "<label>";
                    case 1:
                        return "Rd, <memory ref>";
                    case 2:
                        return "Rd, Rn, <memory ref>";
                    default:
                        return "invalid";
                }
            }
        }

        // uses a dictionary to return the format the operand should be in
        private string OpcodeFormat(string Opcode)
        {
            // if the Opcode is a valid key
            // return the matching value
            if (instructionSet.ContainsKey(Opcode))
            {
                return instructionSet[Opcode];
            }
            // else return invalid Opcode
            else
            {
                return "invalid";
            }
        }

        // splits the operand from the instruction
        public static string GetOperand(string instruction)
        {
            string operand = string.Empty;

            // if the Opcode is HALT, the operand will be blank
            if (GetOpcode(instruction) == "HALT")
            {
                return "";
            }
            else if(GetOpcode(instruction) == " " || GetOpcode(instruction) == "") // checks for blank instructions
            {
                return "";
            }
            // if the Opcode starts with B, it is a branch instruction
            else if (GetOpcode(instruction)[0] == 'B')
            {
                // checks if the branch has a condition or not
                // if the next character in the instruction is not a < then it is an always branch instruction
                if (instruction[1] != '<') 
                {
                    for (int i = 0; i < instruction.Length-1; i++)
                    {
                        // the opcode is 1 long, so add the remaining characters to the operand
                        operand += instruction[i + 1]; 
                    }
                }
                else // else the character is a branch condition
                {
                    for (int i = 0; i < instruction.Length-5; i++)
                    {
                        // the opcode is 5 long, so add the remaining characters to the operand
                        operand += instruction[i + 5];
                    }
                }
            }
            // else the instruction is standard 3 long Opcode and it can be handled as standard
            else
            {
                for (int i = 0; i < instruction.Length - 3; i++)
                {
                    // the opcode is 3 long, so add the remaining characters to the operand
                    operand += instruction[i + 3];
                }
            }

            return operand;
        }

        // splits the opcode from the operand (but doesn't verify valid opcode)
        public static string GetOpcode(string instruction)
        {
            // if the instruction is just HALT, then the opcode is just HALT
            if (instruction == "HALT")
            {
                return "HALT";
            }
            // if the instruction is blank, return a blank opcode
            else if(instruction == " " || instruction == "")
            {
                return "";
            }
            // if the Opcode starts with B, it is a branch instruction
            else if (instruction[0] == 'B')
            {
                // checks if the branch has a condition or not
                if (instruction[1] != '<')
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