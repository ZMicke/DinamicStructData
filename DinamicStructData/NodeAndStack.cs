using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DinamicStructData
{
    internal class NodeAndStack
    {
        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

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
                MessageBox.Show($"Pushed: {elem}");
            }

            public T Pop()
            {
                if (IsEmpty())
                {
                    MessageBox.Show("Stack is empty. Pop operation cannot be performed.");
                    return default(T);
                }
                T value = top.Data;
                top = top.Next;
                MessageBox.Show($"Popped: {value}");
                return value;
            }

            public T Top()
            {
                if (IsEmpty())
                {
                    MessageBox.Show("Stack is empty. Top operation cannot be performed.");
                    return default(T);
                }
                MessageBox.Show($"Top element: {top.Data}");
                return top.Data;
            }

            public bool IsEmpty()
            {
                bool empty = top == null;
                MessageBox.Show($"Is stack empty? {empty}");
                return empty;
            }

            public void Print(System.Windows.Controls.TextBlock outputTextBlock)
            {
                if (IsEmpty())
                {
                    MessageBox.Show("Stack is empty.");
                    return;
                }

                string result = "Stack elements:\n";
                Node<T> current = top;
                while (current != null)
                {
                    result += current.Data + "\n";
                    current = current.Next;
                }
                MessageBox.Show(result);
            }
        }
    }
}
