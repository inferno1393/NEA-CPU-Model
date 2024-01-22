using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class Processor: AbstractProcessor
    {
        List<string> instructions = new List<string>();
        
        public Processor(List<string> instructions)                                             
        {
            instructions = this.instructions;
        }

        public override void Flow(List<string> instructions)
        {
            foreach (var instruction in instructions)
            {
                string opcode = Parser.GetOpcode(instruction);

                if (opcode == "HALT")
                {
                    goto Exit;
                }
            }
        Exit:
            ; // HALT instruction found, end of execution so will return back to call point
        }

        protected override int Fetch(string address)
        {
            return 0;
        }
    }
}
