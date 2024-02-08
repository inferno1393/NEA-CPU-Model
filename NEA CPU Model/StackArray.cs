using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace NEA_CPU_Model
{
    public class StackArray<T>
    {
        // attributes
        private int capacity;
        private T[] arrayStack;
        private int top;

        // allows read-only access to count
        public int Count { get { return top + 1; } }

        // constructor
        public StackArray() : this(capacity: 100)
        {
        }

        // initializes attributes
        public StackArray(int capacity)
        {
            arrayStack = new T[capacity];
            this.capacity = capacity;
            top = -1;
        }

        // adds a value to the stack
        public void Push(T s)
        {
            top++;
            arrayStack[top] = s;
        }

        // removes a value from the stack
        public T Pop()
        {
            T r = arrayStack[top];
            top--;
            return r;
        }

        // returns the top value of the stack
        public T Peek()
        {
            return arrayStack[top];
        }

        // clears the stack
        public void Clear()
        {
            top = -1;
        }

        // returns if the stack is empty
        public bool Empty()
        {
            return top == -1;
        }

        // returns if the stack is full
        public bool Full()
        {
            return (top + 1) == capacity;
        }

    }
}

