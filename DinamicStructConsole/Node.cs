using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinamicStructConsole
{
    public class Stack<T>
    {
        private Node<T> top;

        public Stack()
        {
            top = null;
        }

        public void Push(T elem)
        {
            Node<T> newNode = new Node<T>(elem);
            newNode.Next = top;
            top = newNode;
            Console.WriteLine($"Pushed: {elem}");
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty. Pop operation cannot be performed.");
                return default(T);
            }
            T value = top.Data;
            top = top.Next;
            Console.WriteLine($"Popped: {value}");
            return value;
        }

        public T Top()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty. Top operation cannot be performed.");
                return default(T);
            }
            Console.WriteLine($"Top element: {top.Data}");
            return top.Data;
        }

        public bool IsEmpty()
        {
            bool empty = top == null;
            Console.WriteLine($"Is stack empty? {empty}");
            return empty;
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty.");
                return;
            }

            Console.WriteLine("Stack elements:");
            Node<T> current = top;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}
