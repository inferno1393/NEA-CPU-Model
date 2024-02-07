using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class RAM : AbstractMemory
    {
        // uses a dictionary to implement an associative array to store the RAM values
        private Dictionary<string, int> memory = new Dictionary<string, int> { };

        // constructor
        public RAM()
        {

        }

        // returns the data of the address being accessed
        public override int ReturnData(string address)
        {
            if (IsAddressEmpty(address)) // checks if address is empty to avoid accessing a null address
            {
                return -1;
            }
            // updates specific purpose registers
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = memory[address].ToString();
            return memory[address]; // returns data
        }

        // stores the data given in the given address
        public override void StoreData(string address, int data)
        {
            if (IsAddressEmpty(address)) // checks if address is empty to avoid updating an existing key
            {
                memory.Add(address, data);
            }
            else
            {
                memory[address] = data;
            }
            // updates specific purpose registers
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = data.ToString();
            UpdateInterface(address, data); // updates interface to show changes
        }

        protected override bool IsAddressEmpty(string address)
        {
            if (memory.ContainsKey(address)) // checks if key is in dictionary
            {
                return false;
            }
            return true;
        }

        // adds the address/data that just got changed to the interface
        private void UpdateInterface(string address, int data)
        {
            int addr = Convert.ToInt32(address);
            if (addr >= Model.ramIndex && addr <= (Model.ramIndex + Model.ramData.Count())) // address is within the range of available addresses
            {
                Model.ramData[addr - Model.ramIndex].Text = data.ToString();
            }
            else // address is not within range
            {
                // nothing to do
            }
        }
    }
}