using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeVisualizer : MonoBehaviour
{
    public GameObject textPrefab; 
    public BinarySearchTree tree; 
    public Canvas canvas; 

    private Dictionary<BinarySearchTree.Node, GameObject> nodeObjects = new Dictionary<BinarySearchTree.Node, GameObject>();

    private void Start()
    {
        if (tree != null && tree.Root != null)
        {
            VisualizeTree(tree.Root, 0, 0, 200); 
        }
        else
        {
            Debug.LogError("BinarySearchTree o Root no asignado.");
        }
    }

    private void VisualizeTree(BinarySearchTree.Node node, float x, float y, float offsetX)
    {
        if (node == null) return;
        GameObject nodeText = Instantiate(textPrefab, canvas.transform);
        nodeText.GetComponent<TextMeshProUGUI>().text = node.Value.ToString();

        RectTransform rectTransform = nodeText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
        nodeObjects[node] = nodeText;

        if (node.Left != null)
        {
            DrawLine(new Vector2(x, y), new Vector2(x - offsetX, y - 100));
            VisualizeTree(node.Left, x - offsetX, y - 100, offsetX / 2);
        }

        if (node.Right != null)
        {
            DrawLine(new Vector2(x, y), new Vector2(x + offsetX, y - 100));
            VisualizeTree(node.Right, x + offsetX, y - 100, offsetX / 2);
        }
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        GameObject line = new GameObject("Line", typeof(LineRenderer));
        line.transform.SetParent(canvas.transform);
        LineRenderer lr = line.GetComponent<LineRenderer>();

        lr.startWidth = 2f;
        lr.endWidth = 2f;
        lr.positionCount = 2;
        lr.useWorldSpace = false;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.black;
        lr.endColor = Color.black;
    }
}
