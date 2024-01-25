using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class Processor: AbstractProcessor
    {
        // uses a dictionary to implement an associative array to store the register values
        private Dictionary<int, int> registers = new Dictionary<int, int> { };


        // constructor
        public Processor()                                             
        {
            
        }

        // splits the instructions into each instruction and then into opcode and operand
        // then controls the CPU components in executing the instruction
        public override void Flow(string instruction, RAM RAM)
        {
            // splits the instruction into opcode and operand
            // then splits the operand into each value
            string opcode = Parser.GetOpcode(instruction);
            string operand = Parser.GetOperand(instruction);
            string[] values = operand.Split(',');

            // if instruction is HALT stop execution
            switch (opcode)
            {
                case "LDR":
                    registers[operand[0]] = RAM.ReturnData(operand[1]);
                    break;
                case "STR":
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

        // fetches data from the address given
        protected override int Fetch(int address, RAM RAM)
        {
            return RAM.ReturnData(address);
        }

        // writes the data given into the address given
        protected override void WriteToMemory(int address, int data, RAM RAM)
        {
            RAM.StoreData(address, data);
        }
    }
}
