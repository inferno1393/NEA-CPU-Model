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
        public virtual int ReturnData(int address)
        {
            return 0;
            // is overriden by the child class
        }

        // stores the data given in the given address
        public virtual void StoreData(int address, int data)
        {
            // is overriden by the child class
        }
    }
}
