using System;
using System.Collections.Generic;

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
            // input str
            Message message = new Message("MISSISSIPPIS");
            Console.WriteLine("Input:\t\t " + message.InputString);

            // get chars and frequencies from input, order by frequency asc
            Dictionary<char, int> charFrequencies = message.GetFrequencies(message.InputString);

            // create Huffman tree
            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.CreateTree(charFrequencies);

            // encode, CodeTable has characters and corresponding binary-values (as a string)
            string encodedStr = message.Encode(message.InputString, huffmanTree.CodeTable);
            Console.WriteLine("\nEncoded: \t" + encodedStr);

            // decode
            string decodedStr = message.Decode(huffmanTree.RootNode, encodedStr);
            Console.WriteLine("\nDecoded: \t" + decodedStr);
        }
    }
}
