using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class Cache: AbstractMemory
    {
        // uses a dictionary to implement an associative array to store the cache values
        Dictionary<string, int> cache = new Dictionary<string, int>() { };

        // uses a queue to tell which addresses have been in cache the longest
        QueueArray<string> queue = new QueueArray<string>();

        // attributes
        public int capacity = 8;

        // returns the data of the address being accessed
        public override int ReturnData(string address)
        {
            if (IsAddressEmpty(address)) // checks if address is empty to avoid accessing a null address
            {
                return -1;
            }
            // updates specific purpose registers
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = cache[address].ToString();
            return cache[address]; // returns data
        }

        // stores the data given in the given address
        public override void StoreData(string address, int data)
        {
            if (IsAddressEmpty(address)) // checks if address is empty to avoid updating an existing key
            {
                cache.Add(address, data);
            }
            else
            {
                cache[address] = data;
            }
            // updates specific purpose registers
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = data.ToString();
            UpdateInterface(address, data); // updates interface to show changes
        }

        // checks if the given address is empty
        protected override bool IsAddressEmpty(string address)
        {
            if (cache.ContainsKey(address)) // checks if key is in dictionary
            {
                return false;
            }
            return true;
        }

        public void Clear()
        {
            cache = new Dictionary<string, int>() { };
        }

        private void UpdateInterface(string address, int data)
        {

        }

    }
}
