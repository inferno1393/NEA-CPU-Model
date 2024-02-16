using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NEA_CPU_Model
{
    internal class Cache: AbstractMemory
    {
        // uses a dictionary to implement an associative array to store the cache values
        Dictionary<string, int> cacheMemory = new Dictionary<string, int>() { };

        // uses a dictionary to implement an associative array to tell which addresses have been in cache the longest
        Dictionary<string, int> timestamps = new Dictionary<string, int>() { };

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
            Program.model.mbrText.Text = cacheMemory[address].ToString();

            return cacheMemory[address]; // returns data
        }

        // stores the data given in the given address
        public override void StoreData(string address, int data)
        {
            if (cacheMemory.Count == capacity) // checks if cache is full
            {
                string addr = FindOldest();

                // cache is full so remove the address that has been in cache the longest
                timestamps.Remove(addr);
                cacheMemory.Remove(addr);
            }

            if (IsAddressEmpty(address)) // checks if address is empty to avoid updating an existing key
            {
                cacheMemory.Add(address, data); // address does not already exists so add the address
                timestamps.Add(address, Processor.cycleCounter); // adds the new address to the queue
            }
            else
            {
                cacheMemory[address] = data; // address already exsits so update the address
                timestamps[address] = Processor.cycleCounter; // updates the age counter to reflect the update time
            }
            

            // updates specific purpose registers
            Program.model.marText.Text = address;
            Program.model.mbrText.Text = data.ToString();

            UpdateInterface(); // updates interface to show changes
        }

        // returns the address of the oldest address in cache
        private string FindOldest()
        {
            // creates dictionaries containing the keys and values currently in cache
            Dictionary<string, int>.KeyCollection keys = timestamps.Keys;
            Dictionary<string, int>.ValueCollection values = timestamps.Values;

            // creates a string array to hold the unsorted values
            string[] unsortedValues = new string[values.Count];

            int j = 0; // initializes an iteration value
            foreach (var value in values) // adds the values to the string array
            {
                unsortedValues[j] = value.ToString();
                j++;
            }

            // creates a string array and equals it to the sorted unSorted array
            string[] sortedValues = SortArray(unsortedValues, 0, unsortedValues.Length - 1);


            string address = string.Empty; // creates an empty string to store the matching address 

            // carries out a linear search to find the matching address to the oldest timestamp value
            foreach(var key in keys)
            {
                if (timestamps[key].ToString() == sortedValues.First())
                {
                    return key; // matching address found, so return it
                }
            }

            return "No matching address found"; // returns an error for no address found
        }

        // checks if the given address is empty
        protected override bool IsAddressEmpty(string address)
        {
            if (cacheMemory.ContainsKey(address)) // checks if key is in dictionary
            {
                return false; // the address is not empty
            }
            return true; // the address is empty
        }

        // clears the current instance of cache
        public void Clear()
        {
            cacheMemory = new Dictionary<string, int>() { }; // resets the cache memory
            timestamps = new Dictionary<string, int>() { }; // resets the age queue
        }

        // orders the addresses in cache in ascending numerical order and the displays them to the interface
        private void UpdateInterface()
        {
            // creates a dictionary containing the keys currently in cache
            Dictionary<string, int>.KeyCollection keys = cacheMemory.Keys;

            // sorts keys into numerical order for visbility
            //var sortedKeys = keys.OrderBy(key => Convert.ToInt32(key));
            string[] unSortedKeys = new string[keys.Count];
            int j = 0;
            foreach(var key in keys)
            {
                unSortedKeys[j] = key;
                j++;
            }

            string[] sortedKeys = SortArray(unSortedKeys, 0, unSortedKeys.Length - 1);

            // updates interface to show the new order
            int i = 0; // sets an iteration value to 0

            foreach (var key in sortedKeys)
            {
                if (i <= capacity-1)
                {
                    if (i < Model.cacheData.Count())
                    {
                        // adds the address and data to the interface
                        Model.cacheData[i].Text = cacheMemory[key.ToString()].ToString();
                        Model.cacheAddress[i].Text = key.ToString();
                    }
                }
                i++; // increments the iteration value for next cycle
            }
        }

        // recursively calls until the array is split into individual values
        // and combines and sorts arrays until one combined, sorted array is returned to call point
        private string[] SortArray(string[] unSortedArray, int leftValue, int rightValue)
        {
            if (leftValue < rightValue) // checks if the array has been split to individual values
            {
                int middleValue = leftValue + (rightValue - leftValue) / 2; // finds the midpoint of the array
                SortArray(unSortedArray, leftValue, middleValue); // splits the left side down to individual values
                SortArray(unSortedArray, middleValue + 1, rightValue); // splits the right side down to individual values
                MergeArray(unSortedArray, leftValue, middleValue, rightValue); // combines and sorts the current array
            }
            return unSortedArray; // the current array will now be sorted
        }

        // combines and sorts the given array
        private void MergeArray(string[] unSortedArray, int leftValue, int middleValue, int rightValue)
        {
            int leftArrayLength = middleValue - leftValue + 1; // finds the length of the left
            int rightArrayLength = rightValue - middleValue; // finds the length of the right
            string[] leftArray = new string[leftArrayLength]; // creates the left array
            string[] rightArray = new string[rightArrayLength]; // creates the right array

            // intiailizes iteration values
            int i = 0;
            int j = 0;

            // adds the values to the left array
            for (i = 0; i < leftArrayLength; ++i)
            {
                leftArray[i] = unSortedArray[leftValue + i];
            }

            // adds the values to the right array
            for (j = 0; j < rightArrayLength; ++j)
            {
                rightArray[j] = unSortedArray[middleValue + 1 + j];
            }

            // reses the iteration values
            i = 0;
            j = 0;

            // combines the left array and right array
            while (i < leftArrayLength && j < rightArrayLength)
            {
                if (Convert.ToInt32(leftArray[i]) <= Convert.ToInt32(rightArray[j]))
                {
                    unSortedArray[leftValue++] = leftArray[i++];
                }
                else
                {
                    unSortedArray[leftValue++] = rightArray[j++];
                }
            }

            // inserts the left array into the sorted array
            while (i < leftArrayLength)
            {
                unSortedArray[leftValue++] = leftArray[i++];
            }

            // inserts the right array into the sorted array
            while (j < rightArrayLength)
            {
                unSortedArray[leftValue++] = rightArray[j++];
            }
        }
    }
}