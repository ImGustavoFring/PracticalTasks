using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class TreeNode
    {
        public char Value;
        public TreeNode Left, Right;

        public TreeNode(char value)
        {
            Value = value;
            Left = Right = null;
        }
    }

    public static class TreeSort
    {
        public static string Sort(string text)
        {
            TreeNode root = null;

            foreach (char c in text)
            {
                root = Insert(root, c);
            }

            List<char> sortedList = new List<char>();
            InOrderTraversal(root, sortedList);

            return new string(sortedList.ToArray());
        }

        private static TreeNode Insert(TreeNode node, char value)
        {
            if (node == null)
            {
                return new TreeNode(value);
            }

            if (value < node.Value)
            {
                node.Left = Insert(node.Left, value);
            }
            else
            {
                node.Right = Insert(node.Right, value);
            }

            return node;
        }

        private static void InOrderTraversal(TreeNode node, List<char> sortedList)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, sortedList);
                sortedList.Add(node.Value);
                InOrderTraversal(node.Right, sortedList);
            }
        }
    }
}
