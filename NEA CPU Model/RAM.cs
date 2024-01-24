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
            if (IsAddressEmpty(address))
            {
                return -1;
            }
            return memory[address];
        }

        // stores the data given in the given address
        public override void StoreData(int address, int data)
        {
            if (IsAddressEmpty(address))
            {
                memory.Add(address, data);
            }
            else
            {
                memory[address] = data;
            }
            UpdateInterface(address, data);
        }

        protected override bool IsAddressEmpty(int address)
        {
            if (memory.ContainsKey(address))
            {
                return false;
            }
            return true;
        }

        private void UpdateInterface(int address, int data)
        {
            Program.model.RAM_Address.Text = address.ToString();
            Program.model.RAM_Data.Text = data.ToString();
        }
    }
}
