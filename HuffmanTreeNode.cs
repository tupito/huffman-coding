using System;
using System.Collections.Generic;
using System.Text;

namespace Harjtyo_Huffman
{
    class HuffmanTreeNode : IComparable<HuffmanTreeNode>
    {
        public int data;
        public char c;

         
        public HuffmanTreeNode left;
        public HuffmanTreeNode right;

        public HuffmanTreeNode (char c, int data)
        {
            this.c = c;
            this.data = data;
        }

        public int CompareTo(HuffmanTreeNode other)
        {
            // compare frequencies
            return this.data.CompareTo(other.data);
        }
    }
}
