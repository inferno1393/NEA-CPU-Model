using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class AbstractMemory
    {
        // returns the data of the address being accessed
        public virtual int ReturnData(string address)
        {
            return 0;
            // returns the data stored in the given address
            // is overriden by the child class
        }

        // stores the data given in the given address
        public virtual void StoreData(string address, int data)
        {
            // stores the given data in the given address
            // is overriden by the child class
        }

        // returns if an address is empty
        protected virtual bool IsAddressEmpty(string address)
        {
            // checks if the given address contains data
            return false;
            // is overriden by the child class
        }
    }
}