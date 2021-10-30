using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsContainer : MonoBehaviour
{
    public static FieldGUI fieldGUI;
    public static FieldUI fieldUI;
    public static SettingsGUI settingsGUI;
    public static FieldContainerUI fieldContainerUI;
    public static Panel panel;
    public static IsingAlgorithm isingAlgorithm;
    public static FieldContainer fieldContainer;

    [SerializeField] private GameObject EnergyGraph;
    [SerializeField] private GameObject MagnetizationGraph; 
    [SerializeField] private UILineRenderer energyLineRenderer;
    [SerializeField] private UILineRenderer magnetizationLineRenderer;
    [SerializeField] private UIGraphNormalizer energyGraphNormalizer;
    [SerializeField] private UIGraphNormalizer magnetizationGraphNormalizer;

    public void ShowEnergyGraph(bool value) => EnergyGraph.SetActive(value);
    public void ShowMagnetizationGraph(bool value) => MagnetizationGraph.SetActive(value);

    private void HidePointsOnGraph(UIGraphNormalizer normalizer, UILineRenderer lineRenderer) {
        lineRenderer.RemoveAllPoints();
        normalizer.RemoveAllPoints();
    }

    public void HidePointsOnEnergyGraph() => HidePointsOnGraph(energyGraphNormalizer, energyLineRenderer);
    public void HidePointsOnMagnetizationGraph() => HidePointsOnGraph(magnetizationGraphNormalizer, magnetizationLineRenderer);

    private void Start() {
        fieldGUI = transform.GetComponent<FieldGUI>();
        fieldUI = transform.GetComponent<FieldUI>();
        fieldContainer = transform.GetComponent<FieldContainer>();
        fieldContainerUI = transform.GetComponent<FieldContainerUI>();
        settingsGUI = transform.GetComponent<SettingsGUI>();
        panel = transform.GetComponent<Panel>();
        isingAlgorithm = transform.GetComponent<IsingAlgorithm>();
    }
}
