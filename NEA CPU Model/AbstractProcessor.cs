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
        public virtual void Flow(string instructions, RAM RAM)
        {
            // is overriden by the child class
        }
    }

}
