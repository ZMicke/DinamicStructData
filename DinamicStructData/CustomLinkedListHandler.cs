using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DinamicStructWPF
{
    public class CustomLinkedList<T>
    {
        public class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node head;

        // Add an element to the front of the list
        public void AddToFront(T data)
        {
            Node newNode = new Node(data);
            newNode.Next = head;
            head = newNode;
        }

        // 1. Reverse the list
        public void Reverse()
        {
            Node prev = null;
            Node current = head;
            Node next = null;

            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            head = prev;
        }

        // 2. Move the last element to the front
        public void MoveLastToFirst()
        {
            if (head == null || head.Next == null) return;

            Node prev = null;
            Node current = head;

            while (current.Next != null)
            {
                prev = current;
                current = current.Next;
            }

            if (prev != null)
            {
                prev.Next = null;
                current.Next = head;
                head = current;
            }
        }

        // 2. Move the first element to the end
        public void MoveFirstToLast()
        {
            if (head == null || head.Next == null) return;

            Node first = head;
            head = head.Next;
            first.Next = null;

            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = first;
        }

        // 3. Count distinct elements
        public int CountDistinct()
        {
            HashSet<T> elements = new HashSet<T>();
            Node current = head;

            while (current != null)
            {
                elements.Add(current.Data);
                current = current.Next;
            }

            return elements.Count;
        }

        // 4. Remove non-unique elements
        public void RemoveNonUnique()
        {
            Dictionary<T, int> elementCounts = new Dictionary<T, int>();
            Node current = head;

            // Count occurrences
            while (current != null)
            {
                if (elementCounts.ContainsKey(current.Data))
                    elementCounts[current.Data]++;
                else
                    elementCounts[current.Data] = 1;

                current = current.Next;
            }

            // Remove nodes with count > 1
            Node dummy = new Node(default(T))
            {
                Next = head
            };
            Node prev = dummy;
            current = head;

            while (current != null)
            {
                if (elementCounts[current.Data] > 1)
                {
                    prev.Next = current.Next;
                }
                else
                {
                    prev = current;
                }

                current = current.Next;
            }

            head = dummy.Next;
        }

        // 5. Insert another list after the first occurrence of a value
        public void InsertListAfterFirstOccurrence(CustomLinkedList<T> list, T value)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Data.Equals(value))
                {
                    Node next = current.Next;
                    current.Next = list.head;

                    Node tail = list.head;
                    while (tail != null && tail.Next != null)
                    {
                        tail = tail.Next;
                    }

                    if (tail != null)
                    {
                        tail.Next = next;
                    }

                    break;
                }

                current = current.Next;
            }
        }

        // 6. Insert an element in a sorted list
        public void InsertSorted(T data)
        {
            Node newNode = new Node(data);

            if (head == null || Comparer<T>.Default.Compare(head.Data, data) >= 0)
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            Node current = head;
            while (current.Next != null && Comparer<T>.Default.Compare(current.Next.Data, data) < 0)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        // 7. Remove all occurrences of a value
        public void RemoveAll(T value)
        {
            Node dummy = new Node(default(T))
            {
                Next = head
            };
            Node current = dummy;

            while (current.Next != null)
            {
                if (current.Next.Data.Equals(value))
                {
                    current.Next = current.Next.Next;
                }
                else
                {
                    current = current.Next;
                }
            }

            head = dummy.Next;
        }

        // 8. Insert an element before the first occurrence of another value
        public void InsertBefore(T newValue, T existingValue)
        {
            Node dummy = new Node(default(T))
            {
                Next = head
            };
            Node prev = dummy;
            Node current = head;

            while (current != null)
            {
                if (current.Data.Equals(existingValue))
                {
                    Node newNode = new Node(newValue)
                    {
                        Next = current
                    };
                    prev.Next = newNode;
                    break;
                }

                prev = current;
                current = current.Next;
            }

            head = dummy.Next;
        }

        // 9. Append another list to this list
        public void AppendList(CustomLinkedList<T> list)
        {
            if (head == null)
            {
                head = list.head;
                return;
            }

            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = list.head;
        }

        // 10. Split the list into two by the first occurrence of a value
        public void SplitByFirstOccurrence(T value, out CustomLinkedList<T> list1, out CustomLinkedList<T> list2)
        {
            list1 = new CustomLinkedList<T>();
            list2 = new CustomLinkedList<T>();

            Node current = head;
            bool found = false;

            while (current != null)
            {
                if (!found && current.Data.Equals(value))
                {
                    found = true;
                    list2.head = current.Next;
                    current.Next = null;
                }

                if (!found)
                {
                    list1.AddToFront(current.Data);
                }

                current = current.Next;
            }

            // Reverse list1 to preserve original order
            list1.Reverse();
        }

        // 11. Double the list
        public void DoubleList()
        {
            if (head == null) return;

            Node current = head;
            Node tail = null;

            while (current != null)
            {
                AddToFront(current.Data);
                current = current.Next;
            }

            Reverse();
        }

        // 12. Swap two elements
        public void SwapElements(T elem1, T elem2)
        {
            if (head == null || elem1.Equals(elem2)) return;

            Node prev1 = null, prev2 = null, node1 = null, node2 = null;
            Node current = head;

            while (current != null)
            {
                if (current.Data.Equals(elem1))
                {
                    node1 = current;
                }
                else if (current.Data.Equals(elem2))
                {
                    node2 = current;
                }

                if (node1 == null) prev1 = current;
                if (node2 == null) prev2 = current;

                current = current.Next;
            }

            if (node1 != null && node2 != null)
            {
                if (prev1 != null) prev1.Next = node2;
                else head = node2;

                if (prev2 != null) prev2.Next = node1;
                else head = node1;

                Node temp = node1.Next;
                node1.Next = node2.Next;
                node2.Next = temp;
            }
        }

        // Display the list in a TextBlock (for WPF integration)
        public void PrintList(TextBlock outputTextBlock)
        {
            Node current = head;
            outputTextBlock.Text = "Список элементов:\n";

            while (current != null)
            {
                outputTextBlock.Text += current.Data + " -> ";
                current = current.Next;
            }

            outputTextBlock.Text += "null\n";
        }
    }
}
