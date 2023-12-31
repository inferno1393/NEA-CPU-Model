﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NEA_CPU_Model
{
    public class QueueArray<T>
    {
        private int capacity;
        private T[] Queue;
        private int front;
        private int rear;
        private int count;
        public int Count { get { return count; } }

        // constructor
        public QueueArray() : this(capacity: 100)
        {
        }

        public QueueArray(int capacity)
        {
            Queue = new T[capacity];
            this.capacity = capacity;
            front = 0;
            rear = -1;
            count = 0;
        }

        // adds a value to the queue
        public void Enqueue(T s)
        {
            if (!Full())
            {
                rear = (rear + 1) % capacity;
                Queue[rear] = s;
                count++;
            }
        }

        // removes a value from the queue
        public T Dequeue()
        {
            if (!Empty())
            {
                T r = Queue[front];
                Queue[front] = default(T);
                front = (front + 1) % capacity;
                count--;
                return r;
            }
            else
            {
                throw new InvalidOperationException("Invalid operation. The queue is empty.");
            }
        }

        // returns if the stack is empty
        public bool Empty()
        {
            return count == 0;
        }

        // returns if the stack is full
        public bool Full()
        {
            return count == capacity;
        }
    }
}


