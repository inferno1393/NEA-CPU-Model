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
            if (IsAddressEmpty(address))
            {
                return -1;
            }
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = memory[address].ToString();
            return memory[address];
        }

        // stores the data given in the given address
        public override void StoreData(string address, int data)
        {
            if (IsAddressEmpty(address))
            {
                memory.Add(address, data);
            }
            else
            {
                memory[address] = data;
            }
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = data.ToString();
            UpdateInterface(address, data);
        }

        protected override bool IsAddressEmpty(string address)
        {
            if (memory.ContainsKey(address))
            {
                return false;
            }
            return true;
        }

        // adds the address/data that just got changed to the interface
        private void UpdateInterface(string address, int data)
        {
            switch (address)
            {
                case "0":
                    Program.model.Data0.Text = data.ToString();
                    break;
                case "1":
                    Program.model.Data1.Text = data.ToString();
                    break;
                case "2":
                    Program.model.Data2.Text = data.ToString();
                    break;
                case "3":
                    Program.model.Data3.Text = data.ToString();
                    break;
                case "4":
                    Program.model.Data4.Text = data.ToString();
                    break;
                case "5":
                    Program.model.Data5.Text = data.ToString();
                    break;
                case "6":
                    Program.model.Data6.Text = data.ToString();
                    break;
                case "7":
                    Program.model.Data7.Text = data.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}
