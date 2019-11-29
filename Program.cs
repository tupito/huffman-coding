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

        public HuffmanTreeNode top;

        static void Main(string[] args)
        {
            string inputStr = "MISSISSIPPIS";
            Console.WriteLine("Input:\t\t " + inputStr);

            // get chars and frequencies, order by frequency asc
            Dictionary<char, int> freqs = getFrequencies(inputStr);

            Program p = new Program();

            p.CreateHuffmanTreeAndStoreCodes(freqs);

            string encodedStr = Encode(inputStr, p.codeTable);
            Console.WriteLine("\nEncoded: \t" + encodedStr);

            string decodedStr = Decode(p.top, encodedStr);
            Console.WriteLine("\nDecoded: \t" + decodedStr);
        }

        // function iterates through the encoded string s 
        // if s[i]=='1' then move to node->right 
        // if s[i]=='0' then move to node->left 
        // if leaf node append the node->data to our output string 
        static string Decode(HuffmanTreeNode root, string encodedStr)
        {
            string ans = "";
            HuffmanTreeNode curr = root;

            // loop encoded str
            for (int i = 0; i < encodedStr.Length; i++)
            {
                if (encodedStr[i] == '0')
                    curr = curr.left;
                else
                    curr = curr.right;

                // reached leaf node
                if (curr.left == null && curr.right == null)
                {
                    ans += curr.c;
                    curr = root;
                }
            }
            return ans;
        }

        // encodes message to binary form (really a string)
        static string Encode(string str, Dictionary<char, string> codeTable)
        {
            string encodedMsg = "";

            //chars in string
            foreach (var c in str)
            {
                codeTable.TryGetValue(c, out string bin);
                encodedMsg += bin;
            }

            return encodedMsg;
        }

        public void CreateHuffmanTreeAndStoreCodes(Dictionary<char, int> freqs)
        {

            // https://www.geeksforgeeks.org/huffman-coding-greedy-algo-3/

            // HuffmanQueue
            List<HuffmanTreeNode> queue = new List<HuffmanTreeNode>();

            Console.WriteLine("\nHuffman tree:");

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

                
                top = root; // FOR DECODE

                // new node to the queue
                queue.Add(f);

                // sort queue, has to be in asc order
                queue.Sort();
            }

            // Code table for message
            PrintCodes(root, ""); // to console 
            StoreCodes(root, ""); // to dict
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
