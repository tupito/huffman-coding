using System;
using System.Collections.Generic;
using System.Linq;

namespace Harjtyo_Huffman
{
    class Program
    {

        /*
            Write a program to implement Huffman coding and decoding. It should do the following:
            - Accept a text message, possibly more than one line
            - Create a Huffman tree for this message                ok
            - Create a code table                                   ok
            - Encode the message into a binary
            - Decode the message from binary back to text 

            You can use String variables to store binary numbers as arrangements of the characters 1 and 0. Don’t worry about actual bit manipulation unless you really want to. 
         */

        // code table for message
        public Dictionary<char, string> codeTable = new Dictionary<char, string>();

        static void Main(string[] args)
        {
            string str = "MISSISSIPPIS";

            // get chars and frequencies, order by frequency asc
            Dictionary<char, int> freqs = getFrequencies(str);

            Program p = new Program();

            p.CreateHuffmanTree(freqs);

            Encode(str, p.codeTable);


            // create a hoffman tree
            //CreateHuffmanTree(freqs);

            //todo: Encode the message into a binary

            //todo: Decode the message from binary back to text

        }

        // encodes message to binary form (really a string)
        static void Encode(string str, Dictionary<char, string> codeTable)
        {
            string encodedMsg = "";

            //chars in string
            foreach (var c in str)
            {
                codeTable.TryGetValue(c, out string bin);
                encodedMsg += bin + " ";
            }

            Console.WriteLine(encodedMsg);
        }

        public void CreateHuffmanTree(Dictionary<char, int> freqs)
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

            // Code table for message
            PrintCodes(root, "");
            StoreCodes(root, "");
        }

        static void PrintCodes(HuffmanTreeNode root, string s)
        {

            if (root.left == null && root.right == null && Char.IsLetter(root.c) )
            {
                // c is the character in the node
                Console.WriteLine(root.c + ":" + s);
                return;
            }

            //left 0, right 1
            PrintCodes(root.left, s + "0");
            PrintCodes(root.right, s + "1");
        }

        public void StoreCodes(HuffmanTreeNode root, string s)
        {

            if (root.left == null && root.right == null && Char.IsLetter(root.c))
            {
                // codetable char
                if (!codeTable.ContainsKey(root.c))
                {
                    codeTable.Add(root.c, s);
                }
                codeTable[root.c] = s;
                return;
            }

            //left 0, right 1
            StoreCodes(root.left, s + "0");
            StoreCodes(root.right, s + "1");
        }


        static Dictionary<char,int> getFrequencies(string str)
        {
            // frequency for every char
            Dictionary<char, int> freqs = new Dictionary<char, int>();

            // loop string
            foreach (var item in str)
            {
                // new char found
                if (!freqs.ContainsKey(item)) {
                    freqs.Add(item, 0);
                }
                freqs[item]++; // add to char counter
            }

            return orderedDictionary(freqs);
        }

        static Dictionary<char,int> orderedDictionary(Dictionary<char, int> dict)
        {
            Dictionary<char, int> orderedDict = new Dictionary<char, int>();
            
            //  Dictionary to ascending order
            foreach (var item in dict.OrderBy(i => i.Value ))
            {
                orderedDict.Add(item.Key, item.Value);
            }

            return orderedDict;
        }
    }
}
