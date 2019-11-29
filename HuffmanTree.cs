using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harjtyo_Huffman
{
    class HuffmanTree
    {
        public Dictionary<char, string> CodeTable { get; private set; }

        public HuffmanTreeNode RootNode { get; private set; }

        private List<HuffmanTreeNode> queue;

        public HuffmanTree(){}

        public void CreateTree(Dictionary<char, int> charFreqs)
        {
            // queue for nodes will be kept in asc order
            queue = new List<HuffmanTreeNode>();
            
            // Huffman code table will be populated with chars and corresponding binaries counted from Huffman tree
            CodeTable = new Dictionary<char, string>();

            Console.WriteLine("\nHuffman tree:");

            foreach (var item in charFreqs)
            {
                // create nodes based on chars and frequencies
                HuffmanTreeNode node = new HuffmanTreeNode(item.Key, item.Value);
                node.left = null;
                node.right = null;
                queue.Add(node);
            }

            HuffmanTreeNode root = null;

            // queue handling
            while (queue.Count > 1)
            {
                // 1st node with smallest frequency to extract
                HuffmanTreeNode x = queue.First();
                queue.RemoveAt(0);

                // 2nd node with smallest frequency y to extract
                HuffmanTreeNode y = queue.First();
                queue.RemoveAt(0);

                // new node, daddy for the two extracted nodes (sum of the 2 smallest)
                HuffmanTreeNode f = new HuffmanTreeNode('-', x.data + y.data);

                // 1st extracted node as left child
                f.left = x;

                // 2nd extracted node as right child
                f.right = y;

                // new node is now root node
                root = f;

                RootNode = root; // for decoding

                // new node to the queue
                queue.Add(f);

                // sort queue, has to be in asc order
                queue.Sort();
            }

            StoreCodes(root, ""); // to dict
        }

        public void StoreCodes(HuffmanTreeNode root, string s)
        {

            if (root.left == null && root.right == null && Char.IsLetter(root.c))
            {
                // codetable char
                if (!CodeTable.ContainsKey(root.c))
                {
                    CodeTable.Add(root.c, s);
                }
                CodeTable[root.c] = s;
                Console.WriteLine("\t\t" + root.c + ":" + s);
                return;
            }

            //left 0, right 1
            StoreCodes(root.left, s + "0");
            StoreCodes(root.right, s + "1");
        }
    }
}
