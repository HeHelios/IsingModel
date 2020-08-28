using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour {
    [Header("Color settings")]
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color startModellingColor;
    [SerializeField] private Color stopModellingColor;

    [Header("Button objects settings")]
    [SerializeField] private GameObject startModellingButton;
    [SerializeField] private GameObject stopModellingButton;

    [Header("Other scripts")]
    [SerializeField] private ScriptsContainer scriptsContainer;

    public void UpdateButtonsColor() {
        startModellingButton.GetComponent<Graphic>().color =
            (ScriptsContainer.fieldUI.IsOpened() || ScriptsContainer.fieldContainerUI.IsOpened())? inactiveColor : startModellingColor;
        stopModellingButton.GetComponent<Graphic>().color =
            (ScriptsContainer.fieldUI.IsOpened())? stopModellingColor : inactiveColor;
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(0.01f);

        scriptsContainer.ShowEnergyGraph(false);
        scriptsContainer.ShowMagnetizationGraph(false);
        UpdateButtonsColor();
    }

    public void StartModelling() {
        if(ScriptsContainer.fieldContainerUI.IsOpened() || ScriptsContainer.fieldUI.IsOpened())
            return;

        ScriptsContainer.fieldContainerUI.OpenWindow();
    }

    public void StopModelling() {
        if (!ScriptsContainer.fieldUI.IsOpened())
            return;

        ScriptsContainer.isingAlgorithm.StopModelling();
        ScriptsContainer.fieldContainerUI.CloseWindow();
        ScriptsContainer.fieldUI.CloseWindow();
        ScriptsContainer.settingsGUI.CloseWindow();
        ScriptsContainer.fieldContainerUI.CloseWindow();

        scriptsContainer.ShowEnergyGraph(false);
        scriptsContainer.ShowMagnetizationGraph(false);
        scriptsContainer.HidePointsOnEnergyGraph();
        scriptsContainer.HidePointsOnMagnetizationGraph();
    }

    public void ExitProgram() => Application.Quit();
}
