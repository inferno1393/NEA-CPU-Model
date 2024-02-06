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

        // constructor
        public Processor()
        {

        }

        // splits the instructions into each instruction and then into opcode and operand
        // then controls the CPU components in executing the instruction
        public override void Flow(List<string> instructions, RAM RAM, bool loop)
        {
            // splits the instruction into opcode and operand
            // then splits the operand into each value

            // code needs to execute all instructions at once
            if (loop)
            {
                while (programCounter < instructions.Count)
                {
                    string instruction = instructions[programCounter];
                    programCounter++;
                    string opcode = Parser.GetOpcode(instruction);
                    string operand = Parser.GetOperand(instruction);
                    string[] values = operand.Split(',');

                    Decode(opcode, values, RAM);
                    Program.model.programCounterText.Text = programCounter.ToString();
                }
            }
            // execute only the next instruction
            else
            {
                string instruction = instructions[programCounter];
                programCounter++;
                string opcode = Parser.GetOpcode(instruction);
                string operand = Parser.GetOperand(instruction);
                string[] values = operand.Split(',');

                Decode(opcode, values, RAM);
                Program.model.programCounterText.Text = programCounter.ToString();
            }

        }

        private void Decode(string opcode, string[] values, RAM RAM)
        {
            // follow appropriate steps based on opcode
            switch (opcode)
            {
                case "LDR":
                    if (RAM.ReturnData(values[1]) == -1)
                    {
                        registers[values[0]] = RAM.ReturnData(values[1]);
                        UpdateInterface(values[0], registers[values[0]]);
                    }
                    else
                    {
                        MessageBox.Show($"Attempted to access empty RAM address in line {programCounter}"); // not showing the message box
                        goto Exit;
                    }
                    break;
                case "STR":
                    // uses the raw data if a hashtag is present
                    if (values[0].Contains('#'))
                    {
                        values[0] = values[0].Replace('#' , ' ');
                        RAM.StoreData(values[1], Convert.ToInt32(values[0]));
                    }
                    else
                    {
                        RAM.StoreData(values[1], registers[values[0]]);
                    }
                    
                    break;
                case "ADD":
                    break;
                case "SUB":
                    break;
                case "MOV":
                    break;
                case "CMP":
                    break;
                case "B":
                    programCounter = 0;
                    break;
                case "AND":
                    break;
                case "ORR":
                    break;
                case "EOR":
                    break;
                case "MVN":
                    break;
                case "LSL":
                    break;
                case "LSR":
                    break;
                case "HALT":
                    goto Exit;
            }
        Exit:
            ; // HALT instruction found or error found, end of execution so will return back to call point
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
