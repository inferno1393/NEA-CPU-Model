﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    internal class AbstractProcessor
    {
        // controls the flow of instructions/data around the CPU
        public virtual void Flow(List<string> instructions)
        {
            // is overriden by the child class
        }

        // fetches data from the address given
        protected virtual int Fetch(string address)
        {
            return 0;
            // is overriden by the child class
        }


        // writes the data given into the address given
        protected virtual void WriteToMemory(string address, int data)
        {
            // is oveerriden by the child class
        }
    }

}