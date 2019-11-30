using System.Collections.Generic;
using System.Linq;

namespace Harjtyo_Huffman
{
    class Message
    {
        public string InputString { get; private set; }

        public Message(string msg)
        {
            InputString = msg;
        }

        // function iterates through the encoded string
        // if s[i]=='1' then move to node->right 
        // if s[i]=='0' then move to node->left 
        // if leaf node append the node->data to our output string 
        public string Decode(HuffmanTreeNode root, string encodedStr)
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
        public string Encode(string str, Dictionary<char, string> codeTable)
        {
            string encodedMsg = "";

            // chars in string
            foreach (var c in str)
            {
                codeTable.TryGetValue(c, out string bin);
                encodedMsg += bin;
            }

            return encodedMsg;
        }

        public Dictionary<char, int> GetFrequencies(string str)
        {
            // frequency for every char
            Dictionary<char, int> freqs = new Dictionary<char, int>();

            // loop string
            foreach (var item in str)
            {
                // new char found
                if (!freqs.ContainsKey(item))
                {
                    freqs.Add(item, 0);
                }
                freqs[item]++; // add to char counter
            }

            return OrderedDictionary(freqs);
        }

        //  Sort Dictionary, asc order
        private static Dictionary<char, int> OrderedDictionary(Dictionary<char, int> dict)
        {
            Dictionary<char, int> orderedDict = new Dictionary<char, int>();

            foreach (var item in dict.OrderBy(i => i.Value))
            {
                orderedDict.Add(item.Key, item.Value);
            }
            return orderedDict;
        }

    }
}
