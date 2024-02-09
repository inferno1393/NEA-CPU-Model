﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NEA_CPU_Model
{
    internal class Cache: AbstractMemory
    {
        // uses a dictionary to implement an associative array to store the cache values
        Dictionary<string, int> cacheMemory = new Dictionary<string, int>() { };

        // uses a queue to tell which addresses have been in cache the longest
        QueueArray<string> ageQueue = new QueueArray<string>();

        // attributes
        public int capacity = 4;

        // returns the data of the address being accessed
        public override int ReturnData(string address)
        {
            if (IsAddressEmpty(address)) // checks if address is empty to avoid accessing a null address
            {
                return -1;
            }
            // updates specific purpose registers
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = cacheMemory[address].ToString();

            return cacheMemory[address]; // returns data
        }

        // stores the data given in the given address
        public override void StoreData(string address, int data)
        {
            if (ageQueue.Count == capacity)
            {
                string addr = ageQueue.Dequeue();
                cacheMemory.Remove(addr);
            }
            if (IsAddressEmpty(address)) // checks if address is empty to avoid updating an existing key
            {
                cacheMemory.Add(address, data); // address does not already exists so add the address
                ageQueue.Enqueue(address); // adds the new address to the queue
            }
            else
            {
                cacheMemory[address] = data; // address already exsits so update the address
                //ageQueue.Remove(address);
                //ageQueue.Enqueue(address);
            }
            

            // updates specific purpose registers
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = data.ToString();

            UpdateInterface(); // updates interface to show changes
        }

        // checks if the given address is empty
        protected override bool IsAddressEmpty(string address)
        {
            if (cacheMemory.ContainsKey(address)) // checks if key is in dictionary
            {
                return false;
            }
            return true;
        }

        // clears the current instance of cache
        public void Clear()
        {
            cacheMemory = new Dictionary<string, int>() { }; // resets the cache memory
            ageQueue = new QueueArray<string>(); // resets the age queue
        }

        // orders the addresses in cache in ascending numerical order and the displays them to the interface
        private void UpdateInterface()
        {
            // creates a dictionary containing the keys currently in cache
            Dictionary<string, int>.KeyCollection keys = cacheMemory.Keys;

            // sorts keys into numerical order for visbility
            var sortedKeys = keys.OrderBy(key => Convert.ToInt32(key));

            // updates interface to show the new order
            int i = 0; // sets an iteration value to 0

            foreach (var key in sortedKeys)
            {
                if (i <= capacity-1)
                {
                    if (i < Model.cacheData.Count())
                    {   
                        Model.cacheData[i].Text = cacheMemory[key.ToString()].ToString();
                        Model.cacheAddress[i].Text = key.ToString();
                    }
                }
                i++; // increments the iteration value for next cycle
            }
        }
    }
}
