using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTest : MonoBehaviour
{
    private void Start()
    {
        BinarySearchTree bst = new BinarySearchTree();
        int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };

        foreach (int value in myArray)
        {
            bst.Insert(value);
        }

        bst.InOrderTraversal(bst.Root);

        bst.PreOrderTraversal(bst.Root);

        bst.PostOrderTraversal(bst.Root);

        int depth = bst.MaxDepth(bst.Root);
    }
}

