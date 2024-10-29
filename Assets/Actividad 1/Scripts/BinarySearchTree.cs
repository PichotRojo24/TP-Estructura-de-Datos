using System;
using UnityEngine;

public class BinarySearchTree : MonoBehaviour
{
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public Node Root; 

    void Start()
    {
        int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };
        foreach (int value in myArray)
        {
            Insert(value);
        }
        InOrderTraversal(Root);

        PreOrderTraversal(Root);

        PostOrderTraversal(Root);

        int depth = MaxDepth(Root);
    }

    public void Insert(int value)
    {
        Root = InsertRecursively(Root, value);
    }

    private Node InsertRecursively(Node root, int value)
    {
        if (root == null)
        {
            root = new Node(value);
            return root;
        }

        if (value < root.Value)
            root.Left = InsertRecursively(root.Left, value);
        else if (value > root.Value)
            root.Right = InsertRecursively(root.Right, value);

        return root;
    }

    public void InOrderTraversal(Node node)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left);
            InOrderTraversal(node.Right);
        }
    }

    public void PreOrderTraversal(Node node)
    {
        if (node != null)
        {
            PreOrderTraversal(node.Left);
            PreOrderTraversal(node.Right);
        }
    }

    public void PostOrderTraversal(Node node)
    {
        if (node != null)
        {
            PostOrderTraversal(node.Left);
            PostOrderTraversal(node.Right);
        }
    }

    public int MaxDepth(Node node)
    {
        if (node == null)
            return 0;

        int leftDepth = MaxDepth(node.Left);
        int rightDepth = MaxDepth(node.Right);

        return Math.Max(leftDepth, rightDepth) + 1;
    }
}
