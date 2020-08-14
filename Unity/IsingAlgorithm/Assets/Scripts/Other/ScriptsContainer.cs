using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsContainer : MonoBehaviour
{
    public static FieldGUI fieldGUI;
    public static SettingsGUI settingsGUI;
    public static FieldContainerUI fieldContainerUI;
    public static Panel panel;
    public static IsingAlgorithm isingAlgorithm;
    public static FieldContainer fieldContainer;

    private void Start() {
        fieldGUI = transform.GetComponent<FieldGUI>();
        fieldContainer = transform.GetComponent<FieldContainer>();
        fieldContainerUI = transform.GetComponent<FieldContainerUI>();
        settingsGUI = transform.GetComponent<SettingsGUI>();
        panel = transform.GetComponent<Panel>();
        isingAlgorithm = transform.GetComponent<IsingAlgorithm>();
    }
}
