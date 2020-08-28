using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic {
    
    [SerializeField] private Vector2Int gridSize = new Vector2Int(1, 1);
    [SerializeField] private float thickness = 10f;

    public Vector2Int GetGridSize() => gridSize;

    private float width, height, cellWidth, cellHeight;

    private void CreateAndAdd(ref VertexHelper vertexHelper, float x, float y) {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;
        vertex.position = new Vector3(x, y);
        vertexHelper.AddVert(vertex);
    }

    protected override void OnPopulateMesh(VertexHelper vertexHelper) {
        vertexHelper.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        cellWidth = width / (float)gridSize.x;
        cellHeight = height / (float)gridSize.y;

        int counter = 0;
        for (int y = 0; y < gridSize.y; ++y) {
            for (int x = 0; x < gridSize.x; ++x) {
                DrawElement(x, y, counter, vertexHelper);
                ++counter;
            }
        }
    }

    private void DrawElement(int x, int y, int index, VertexHelper vertexHelper) {
        float xPosition = cellWidth * x;
        float yPosition = cellHeight * y;

        CreateAndAdd(ref vertexHelper, xPosition, yPosition);
        CreateAndAdd(ref vertexHelper, xPosition, yPosition + cellHeight);
        CreateAndAdd(ref vertexHelper, xPosition + cellWidth, yPosition + cellHeight);
        CreateAndAdd(ref vertexHelper, xPosition + cellWidth, yPosition);

        float distance = thickness / Mathf.Sqrt(2f);

        CreateAndAdd(ref vertexHelper, xPosition + distance, yPosition + distance);
        CreateAndAdd(ref vertexHelper, xPosition + distance, yPosition + (cellHeight - distance));
        CreateAndAdd(ref vertexHelper, xPosition + (cellWidth - distance), yPosition + (cellHeight - distance));
        CreateAndAdd(ref vertexHelper, xPosition + (cellWidth - distance), yPosition + distance);

        int offset = index * 8;

        //Left edge
        vertexHelper.AddTriangle(offset + 0, offset + 1, offset + 5);
        vertexHelper.AddTriangle(offset + 5, offset + 4, offset + 0);

        //Top edge
        vertexHelper.AddTriangle(offset + 1, offset + 2, offset + 6);
        vertexHelper.AddTriangle(offset + 6, offset + 5, offset + 1);

        //Right edge
        vertexHelper.AddTriangle(offset + 2, offset + 3, offset + 7);
        vertexHelper.AddTriangle(offset + 7, offset + 6, offset + 2);

        //Bottom edge
        vertexHelper.AddTriangle(offset + 3, offset + 0, offset + 4);
        vertexHelper.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}
