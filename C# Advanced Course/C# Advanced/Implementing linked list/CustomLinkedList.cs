using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLinkedList
{
    public class CustomLinkedList
    {
        private int count;
        public CustomLinkedList()
        {
            Count = 0;
        }
        public Node Head { get; set; }
        public Node Tail { get; set; }
        public int Count
        {
            get => count;
            private set { count = value; }
        }

        public void AddFirst(int element)
        {
            Node newHead = new Node(element);

            if (Count == 0)
            {
                Head = Tail = newHead;
            }
            else
            {
                Node oldHead = Head;
                Head = newHead;
                newHead.Next = oldHead;
                oldHead.Previous = Head;
            }
            Count++;
        }
        public void AddLast(int element)
        {
            Node newTail = new Node(element);

            if (Count == 0)
            {
                Head = Tail = newTail;
            }
            else
            {
                Node oldTail = Tail;
                Tail = newTail;

                newTail.Previous = oldTail;
                oldTail.Next = Tail;
            }
            Count++;
        }
        public int RemoveFirst()
        {
            int returnValue;

            if (Count == 0)
            {
                throw new InvalidOperationException("Linked list is empty!");
            }

            returnValue = Head.Value;
            if (Count == 1)
            {
                Head = Tail = null;
                return returnValue;
            }

            Node newHead = Head.Next;
            Head.Next = null;
            Head = newHead;
            newHead.Previous = null;

            return returnValue;
        }
        public int RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Linked list is empty!");
            }
            int returnValue = Tail.Value;
            if (Count == 1)
            {
                Head = Tail = null;
                return returnValue;
            }

            Node newTail = Tail.Previous;
            Tail.Previous = null;
            Tail = newTail;
            newTail.Next = null;

            return returnValue;
        }
        public void ForEach(Action<int> action)
        {
            Node currentNode = Head;
            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = currentNode.Next;
            }

        }
        public int[] ToArray()
        {
            int[] array = new int[Count];
            int counter = 0;
            Node currentNode = Head;

            while (currentNode != null)
            {
                array[counter++] = currentNode.Value;
                currentNode = currentNode.Next;
            }

            return array;
        }
    }
}
