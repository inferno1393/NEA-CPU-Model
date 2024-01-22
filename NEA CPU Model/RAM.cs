using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class RAM: AbstractMemory
    {
        // uses a dictionary to implement an associative array to store the RAM values
        private Dictionary<int, int> memory = new Dictionary<int, int> { };

        // constructor 
        public RAM()
        {
            
        }

        // returns the data of the address being accessed
        public override int ReturnData(int address)
        {
            return memory[address];
        }


        // stores the data given in the given address
        public override void StoreData(int address, int data)
        {
            memory[address] = data;
        }
    }
}
