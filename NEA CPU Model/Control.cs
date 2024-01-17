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

        public void Execute(List<string> instructions)
        {
            foreach (var instruction in instructions)
            {
                string opcode = String.Concat(instruction[0] + instruction[1] + instruction[2] + instruction[3] + instruction[4]);
                switch (opcode)
                {
                    case "10001":
                        break;
                    case "10000":
                        break;
                    case "01111":
                        break;
                    case "01110":
                        break;
                    case "01101":
                        break;
                    case "01100":
                        break;
                    case "01011":
                        break;
                    case "01010":
                        break;
                    case "01001":
                        break;
                    case "01000":
                        break;
                    case "00111":
                        break;
                    case "00110":
                        break;
                    case "00101":
                        break;
                    case "00100":
                        break;
                    case "00011":
                        break;
                    case "00010":
                        break;
                    case "00001":
                        break;
                    case "00000":
                        goto Exit;
                    default:
                        break;
                }
            }
        Exit:
            MessageBox.Show("HALT instruction found");
        }
    }
}

