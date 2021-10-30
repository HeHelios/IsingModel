using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UILineRenderer : Graphic {
    [SerializeField] private UIGridRenderer gridRenderer;
    [SerializeField] public List<Vector2> points;
    [SerializeField] private float thickness = 10f;

    private float width, height, unitWidth, unitHeight;

    public void RemoveAllPoints() {
        points = new List<Vector2>();
        UpdateGeometry();
    }

    private void CreateAndAdd(ref VertexHelper vertexHelper, float x, float y) {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;
        vertex.position = new Vector3(x, y);
        vertexHelper.AddVert(vertex);
    }

    public void UpdateGraph() => UpdateGeometry();

    protected override void OnPopulateMesh(VertexHelper vertexHelper) {
        vertexHelper.Clear();

        if (points.Count < 2 || gridRenderer == null) return;

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / (float)gridRenderer.GetGridSize().x;
        unitHeight = height / (float)gridRenderer.GetGridSize().y;

        for (int i = 0; i < points.Count - 1; ++i) {
            int index = i * 4;
            DrawVerticesForPoint(points[i], points[i+1], index, vertexHelper);
        }
    }

    private void DrawVerticesForPoint(Vector2 point1, Vector2 point2, int index, VertexHelper vertexHelper) {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        float distance = Mathf.Sqrt((point1.x - point2.x) * (point1.x - point2.x) + (point1.y - point2.y) * (point1.y - point2.y));
        float XDisplacement = ((point2.y - point1.y)/distance) * thickness / 2f;
        float YDisplacement = ((point2.x - point1.x)/distance) * thickness / 2f;

        CreateAndAdd(ref vertexHelper, unitWidth * point1.x + XDisplacement, unitHeight * point1.y - YDisplacement);
        CreateAndAdd(ref vertexHelper, unitWidth * point1.x - XDisplacement, unitHeight * point1.y + YDisplacement);
        CreateAndAdd(ref vertexHelper, unitWidth * point2.x + XDisplacement, unitHeight * point2.y - YDisplacement);
        CreateAndAdd(ref vertexHelper, unitWidth * point2.x - XDisplacement, unitHeight * point2.y + YDisplacement);

        vertexHelper.AddTriangle(index + 1, index + 0, index + 2);
        vertexHelper.AddTriangle(index + 2, index + 3, index + 1);
    }

    private delegate float Function(float argument);
    private void DrawFunction(Function function, float startX, float endX, int numberOfPoints) {
        if (startX >= endX) {
            Debug.LogError("Start value of x must be more than the end one");
            return;
        }

        RemoveAllPoints();
        float deltaX = (endX - startX)/(float)numberOfPoints;

        for (int i = 0; i < numberOfPoints; ++i) {
            points.Add(new Vector2(startX + (float)i * deltaX, function(startX + (float)i * deltaX)));
        }

        UpdateGeometry();
    }

    public void AddPointToGraph(Vector2 point) {
        points.Add(point);
        UpdateGeometry();
    }
}
