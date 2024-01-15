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
            for (int i = 0; i < instructions.Count; i++)
            {

            }
        }
    }
}
