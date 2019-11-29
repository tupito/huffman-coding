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
            - Create a Huffman tree for this message
            - Create a code table
            - Encode the message into a binary
            - Decode the message from binary back to text 

            You can use String variables to store binary numbers as arrangements of the characters 1 and 0. Don’t worry about actual bit manipulation unless you really want to. 
         */

        static void Main(string[] args)
        {
            string inputStr = "MISSISSIPPIS";
            Console.WriteLine("Input:\t\t " + inputStr);

            // get chars and frequencies from input, order by frequency asc
            Dictionary<char, int> charFreqs = getFrequencies(inputStr);
            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.CreateTree(charFreqs);

            // encode, CodeTable has characters and corresponding binary-values (as a string)
            string encodedStr = Encode(inputStr, huffmanTree.CodeTable);
            Console.WriteLine("\nEncoded: \t" + encodedStr);

            // decode
            string decodedStr = Decode(huffmanTree.RootNode, encodedStr);
            Console.WriteLine("\nDecoded: \t" + decodedStr);
        }

        // function iterates through the encoded string
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
                if (encodedStr[i] == '0')  // move to left
                    curr = curr.left;
                else
                    curr = curr.right; // move to right

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

        //  Dictionary to ascending order
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
