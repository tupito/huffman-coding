using System;
using System.Collections.Generic;
using System.Text;

namespace Harjtyo_Huffman
{
    class HuffmanTreeNode : IComparable<HuffmanTreeNode>
    {
        public int Frequency { get; set; }
        public char Char { get; set; }

        public HuffmanTreeNode Left { get; set; }
        public HuffmanTreeNode Right { get; set; }

        public HuffmanTreeNode (char c, int data)
        {
            Char = c;
            Frequency = data;
        }

        public int CompareTo(HuffmanTreeNode other)
        {
            // compare frequencies
            return this.Frequency.CompareTo(other.Frequency);
        }
    }
}
