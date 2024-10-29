using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AVLTreeVisualizer : MonoBehaviour
{
    public GameObject textPrefab; // Prefab de texto para los nodos
    public GameObject linePrefab; // Prefab de l�nea (debe ser un objeto UI con un componente Image)
    public AVLTree tree; // Referencia al script del �rbol AVL
    public Canvas canvas; // Canvas donde se mostrar�n los nodos

    private Dictionary<AVLTree.Node, GameObject> nodeObjects = new Dictionary<AVLTree.Node, GameObject>();

    private void Start()
    {
        if (tree != null && tree.Root != null)
        {
            // Centrar el �rbol en el Canvas
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            float centerX = canvasRect.rect.width / 2;
            VisualizeTree(tree.Root, centerX, 300, centerX / 2); // Ajustar la posici�n y el offset
        }
        else
        {
            Debug.LogError("AVLTree o Root no asignado.");
        }
    }

    private void VisualizeTree(AVLTree.Node node, float x, float y, float offsetX)
    {
        if (node == null) return;

        // Crear y configurar el texto para el nodo
        GameObject nodeText = Instantiate(textPrefab, canvas.transform);
        nodeText.GetComponent<TextMeshProUGUI>().text = node.Value.ToString();

        // Ajustar la posici�n usando anchoredPosition para trabajar en el espacio del Canvas
        RectTransform rectTransform = nodeText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
        nodeObjects[node] = nodeText;

        // Dibujar las conexiones y llamar recursivamente a los hijos
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

    // M�todo para dibujar una l�nea en el espacio del Canvas usando un objeto de UI
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

    // Funciones para realizar rotaciones en el �rbol
    public void RotateLeft()
    {
        if (tree.Root != null)
        {
            tree.Root = tree.RotateLeft(tree.Root);
            RedrawTree();
        }
    }

    public void RotateRight()
    {
        if (tree.Root != null)
        {
            tree.Root = tree.RotateRight(tree.Root);
            RedrawTree();
        }
    }

    // M�todo para limpiar y redibujar el �rbol despu�s de una rotaci�n
    private void RedrawTree()
    {
        foreach (var obj in nodeObjects.Values)
        {
            Destroy(obj);
        }
        nodeObjects.Clear();

        // Centrar nuevamente el �rbol despu�s de redibujar
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float centerX = canvasRect.rect.width / 2;
        VisualizeTree(tree.Root, centerX, 300, centerX / 2);
    }
}
