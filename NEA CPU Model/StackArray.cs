using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_CPU_Model
{
    public class StackArray<T> // uses a generic array to model a stack
    {
        // attributes
        private int capacity;
        private T[] arrayStack;
        private int top;
        public int Count { get { return top + 1; } }

        // constructor
        public StackArray() : this(capacity: 100)
        {
        }

        // sets attributes on creation
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

        // remove a value from the stack
        public T Pop()
        {
            T r = arrayStack[top];
            top--;
            return r;
        }

        // returns the first value in the stack
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