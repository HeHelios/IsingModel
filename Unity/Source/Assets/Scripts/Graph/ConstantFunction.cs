using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFunction : MonoBehaviour {
    [SerializeField] private UIGraphNormalizer graphNormalizer;
    [SerializeField] private UIGridRenderer gridRenderer;
    [SerializeField] private UILineRenderer lineRenderer;

    public void UpdateValue(float value) {
        float normalizedValue = gridRenderer.GetGridSize().y * (value - graphNormalizer.bottomBoundY)/(graphNormalizer.upperBoundY - graphNormalizer.bottomBoundY);
        lineRenderer.points[0] = new Vector2(lineRenderer.points[0].x, normalizedValue);
        lineRenderer.points[1] = new Vector2(lineRenderer.points[1].x, normalizedValue);
        lineRenderer.UpdateGraph();
    }
}
