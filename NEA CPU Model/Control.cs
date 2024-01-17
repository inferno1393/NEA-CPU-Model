using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class Control
    {
        private List<string> instructions;
        public Control(List<string> instructions)
        {
            this.instructions = instructions;
        }

        // creates instance of processor and send in the data fetched/received
        public void Flow(List<string> instructions)
        {
            Processor processor = new Processor();
            foreach (var instruction in instructions)
            {
                string opcode = String.Concat(instruction[0] + instruction[1] + instruction[2] + instruction[3] + instruction[4]);
                
                if(opcode == "00000")
                {
                    goto Exit;
                }
            }
        Exit:
            MessageBox.Show("HALT instruction found");
        }

        // fetches data needed from RAM
        private void Fetch()
        {

        }
    }
}

