using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGraphNormalizer : MonoBehaviour {
    [SerializeField] private UILineRenderer lineRenderer;
    [SerializeField] private UIGridRenderer gridRenderer;
    [SerializeField] private RectTransform graphRectTransform;

    [Space]

    [SerializeField] public float bottomBoundX, upperBoundX;
    [SerializeField] public float bottomBoundY, upperBoundY;
    [SerializeField] private Vector2 offsetX, offsetY;

    [Space]

    [SerializeField] private int textSize;
    [SerializeField] private Font textFont;
    [SerializeField] private Color textColor; 

    private List<Vector2> pointsOnGraph;
    private List<GameObject> notationObjectsX, notationObjectsY;

    private void Start() {
        pointsOnGraph = new List<Vector2>();
        notationObjectsX = new List<GameObject>();
        notationObjectsY = new List<GameObject>();

        CreateNotationX();
        CreateNotationY();
        UpdateTextNotationX();
        UpdateTextNotationY();
        
        UpdateBounds(bottomBoundX, upperBoundX, bottomBoundY, upperBoundY);
    }

    public void RemoveAllPoints() => pointsOnGraph = new List<Vector2>();

    private Vector2 GetNormalizedPoint (Vector2 point) {
        float newX = (float)gridRenderer.GetGridSize().x * (point.x - bottomBoundX) / (upperBoundX - bottomBoundX);
        float newY = (float)gridRenderer.GetGridSize().y * (point.y - bottomBoundY) / (upperBoundY - bottomBoundY);

        return new Vector2(newX, newY);
    }

    public void Add(float x, float y, bool addToArray) {
        if(addToArray) pointsOnGraph.Add(new Vector2(x, y));

        lineRenderer.AddPointToGraph(GetNormalizedPoint(new Vector2(x, y)));
    }

    public void AddAndResize(float x, float y, bool addToArray) {
        Add(x, y, addToArray);
        UpdateBounds(Mathf.Min(x, bottomBoundX), Mathf.Max(x, upperBoundX), Mathf.Min(y, bottomBoundY), Mathf.Max(y, upperBoundY));
    }

    public void UpdateBounds(float new_bottomBoundX, float new_upperBoundX, float new_bottomBoundY, float new_upperBoundY) {
        lineRenderer.RemoveAllPoints();
        bottomBoundX = new_bottomBoundX;
        upperBoundX = new_upperBoundX;
        bottomBoundY = new_bottomBoundY;
        upperBoundY = new_upperBoundY;

        for (int i = 0; i < pointsOnGraph.Count; ++i) 
            Add(pointsOnGraph[i].x, pointsOnGraph[i].y, false);

        UpdateTextNotationX();
        UpdateTextNotationY();
    }

    private void SetNotationObjectProperties (ref GameObject notationObject) {
        notationObject.transform.SetParent(graphRectTransform, false);
        notationObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        notationObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

        TextMeshProUGUI textComponent = notationObject.AddComponent<TextMeshProUGUI>();
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.text = "X";
        textComponent.fontStyle = FontStyles.Bold;
        textComponent.fontSize = textSize;
    }

    private void CreateNotationX() {
        for (int x = 0; x <= gridRenderer.GetGridSize().x; ++x) {
            float ratio = (float)x / (float)gridRenderer.GetGridSize().x;

            GameObject notationObject = new GameObject($"X notation No. {x + 1}", typeof(RectTransform));
            SetNotationObjectProperties(ref notationObject);

            notationObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(ratio * graphRectTransform.rect.width, 0) + offsetX;
            notationObjectsX.Add(notationObject);
        }
    }

    private void UpdateTextNotationX() {
        for (int x = 0; x <= gridRenderer.GetGridSize().x; ++x) {
            float ratio = (float)x / (float)gridRenderer.GetGridSize().x;
            float xValue = bottomBoundX + ratio * (upperBoundX - bottomBoundX);

            notationObjectsX[x].GetComponent<TextMeshProUGUI>().text = "" + (int)xValue;
        }
    }

    private void CreateNotationY() {
        for (int y = 0; y <= gridRenderer.GetGridSize().y; ++y) {
            float ratio = (float)y / (float)gridRenderer.GetGridSize().y;
            
            GameObject notationObject = new GameObject($"X notation No. {y + 1}", typeof(RectTransform));
            SetNotationObjectProperties(ref notationObject);

            notationObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, ratio * graphRectTransform.rect.height) + offsetY;
            notationObjectsY.Add(notationObject);
        }
    }

    private void UpdateTextNotationY() {
        for (int y = 0; y <= gridRenderer.GetGridSize().y; ++y) {
            float ratio = (float)y / (float)gridRenderer.GetGridSize().y;
            float yValue = bottomBoundY + ratio * (upperBoundY - bottomBoundY);

            notationObjectsY[y].GetComponent<TextMeshProUGUI>().text = "" + (int)yValue;
        }
    }
}
