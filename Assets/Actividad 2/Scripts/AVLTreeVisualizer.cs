using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AVLTreeVisualizer : MonoBehaviour
{
    public GameObject textPrefab; 
    public GameObject linePrefab; 
    public AVLTree tree; 
    public Canvas canvas; 

    private Dictionary<AVLTree.Node, GameObject> nodeObjects = new Dictionary<AVLTree.Node, GameObject>();

    private void Start()
    {
        if (tree != null && tree.Root != null)
        {
            VisualizeTree(tree.Root, 0, 0, 200);
        }
        else
        {
            Debug.LogError("AVLTree o Root no asignado.");
        }
    }

    private void VisualizeTree(AVLTree.Node node, float x, float y, float offsetX)
    {
        if (node == null) return;

        GameObject nodeText = Instantiate(textPrefab, canvas.transform);
        nodeText.GetComponent<TextMeshProUGUI>().text = node.Value.ToString();

        RectTransform rectTransform = nodeText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
        nodeObjects[node] = nodeText;

        if (node.Left != null)
        {
            Vector2 leftPos = new Vector2(x - offsetX, y - 100);
            DrawUILine(new Vector2(x, y), leftPos);
            VisualizeTree(node.Left, leftPos.x, leftPos.y, offsetX / 2);
        }

        if (node.Right != null)
        {
            Vector2 rightPos = new Vector2(x + offsetX, y - 100);
            DrawUILine(new Vector2(x, y), rightPos);
            VisualizeTree(node.Right, rightPos.x, rightPos.y, offsetX / 2);
        }
    }

    private void DrawUILine(Vector2 start, Vector2 end)
    {
        GameObject line = Instantiate(linePrefab, canvas.transform); 
        RectTransform rectTransform = line.GetComponent<RectTransform>();

        Vector2 dir = (end - start).normalized; 
        float distance = Vector2.Distance(start, end);

        rectTransform.sizeDelta = new Vector2(distance, 2f); 
        rectTransform.anchoredPosition = start + dir * distance * 0.5f;
        rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }
}
