using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NEA_CPU_Model
{
    public class CircularQueue<T>
    {
        // attributes
        private int capacity;
        private T[] QueueArray;
        private int front;
        private int rear;
        private int count;

        // allows read-only access to count
        public int Count { get { return count; } }

        // constructor
        public CircularQueue() : this(capacity: 10)
        {
        }

        // initializes attributes
        public CircularQueue(int capacity)
        {
            QueueArray = new T[capacity];
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
                QueueArray[rear] = s;
                count++;
            }
        }

        // removes a value from the queue
        public T Dequeue()
        {
            if (!Empty())
            {
                T r = QueueArray[front];
                QueueArray[front] = default(T);
                front = (front + 1) % capacity;
                count--;
                return r;
            }
            else
            {
                throw new InvalidOperationException("Invalid operation. The queue is empty.");
            }
        }

        // returns if the queue is empty
        public bool Empty()
        {
            return count == 0;
        }

        // returns if the queue is full
        public bool Full()
        {
            return count == capacity;
        }
    }
}
