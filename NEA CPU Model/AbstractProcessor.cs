using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class AbstractProcessor
    {
        // controls the flow of instructions/data around the CPU
        public virtual void FlowOfInstructions(List<string> instructions, RAM RAM, bool loop)
        {
            // is overridden by the child class
            // controls whether the code needs to execute all instructions or one instruction
            // uses loop to decide whether to all instructions or only the next one
            // resets the program counter at end of execution
        }

        // controls the program counter during execution and splits the instruction
        protected virtual void DecodeInstruction(List<string> instructions, RAM RAM)
        {
            // is overridden by the child class
            // updates program counter, program counter + CIR on interface
            // splits the instruction into opcode and operand (and then into operands)
            // then calls decode to run the instruction
        }

        // decodes the instruction given and calls the appropriate subroutine to execute it
        protected virtual void ExecuteInstruction(string opcode, string[] values, RAM RAM, List<string> instructions)
        {
            // is overridden by the child class
            // contains switch-case which calls the appropriate subroutine for the opcode and gives it the data
        }
    }
}