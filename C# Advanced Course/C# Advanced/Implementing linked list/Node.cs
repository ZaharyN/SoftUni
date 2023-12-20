using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLinkedList
{
    public class Node
    {
        private int value;
        public Node(int value)
        {
            this.value = value; 
        }

        public Node Previous { get; set; }
        public Node Next { get; set; }
        public int Value {
            get
            {
                return this.value;
            }
            protected set
            {
                this.value = value;
            }
        }
    }
}
