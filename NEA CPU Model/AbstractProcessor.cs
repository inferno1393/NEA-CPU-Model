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
        public virtual void Flow(List<string> instructions, RAM RAM, bool loop)
        {
            // is overriden by the child class
        }
    }

}
