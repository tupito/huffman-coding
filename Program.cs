using System;
using System.Collections.Generic;
using System.Linq;

namespace Harjtyo_Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "MISSISSIPPIS";

            // get chars and frequencies, order by frequency asc
            Dictionary<char, int> freqs = getFrequencies(str);

            // create a hoffman tree
            CreateHoffmanTree(freqs);

            // TODO:
            // PUU - VALMIS
            //  -	Encode the message into a binary
            //  -   Decode the message from binary back to text

            // You can use String variables to store binary numbers as arrangements of the characters 1 and 0. Don’t worry about actual bit manipulation unless you really want to. 
        }

        static void CreateHoffmanTree(Dictionary<char, int> freqs)
        {

            // https://www.geeksforgeeks.org/huffman-coding-greedy-algo-3/

            // HuffmanQueue
            List<HuffmanTreeNode> queue = new List<HuffmanTreeNode>();

            foreach (var item in freqs)
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

                // new node to the queue
                queue.Add(f);

                // sort queue, has to be in asc order
                queue.Sort();
            }
            // Create code table for message
            PrintCode(root, "");
        }

        static void PrintCode(HuffmanTreeNode root, string s)
        {
            if (root.left == null && root.right == null && Char.IsLetter(root.c) )
            {
                // c is the character in the node
                Console.WriteLine(root.c + ":" + s);
                return;
            }

            //left 0, right 1
            PrintCode(root.left, s + "0");
            PrintCode(root.right, s + "1");
        }


        static Dictionary<char,int> getFrequencies(string str)
        {
            Dictionary<char, int> freqs = new Dictionary<char, int>();

            foreach (var item in str)
            {
                if (!freqs.ContainsKey(item)) {
                    freqs.Add(item, 0);
                }
                freqs[item]++;
            }

            return orderedDictionary(freqs);
        }

        static Dictionary<char,int> orderedDictionary(Dictionary<char, int> dict)
        {
            Dictionary<char, int> orderedDict = new Dictionary<char, int>();
            foreach (var item in dict.OrderBy(i => i.Value ))
            {
                orderedDict.Add(item.Key, item.Value);
            }

            return orderedDict;
        }
    }
}
