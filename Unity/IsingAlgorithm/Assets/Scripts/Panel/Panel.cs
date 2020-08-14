using System.Collections;
using System.Collections.Generic;
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

    public void UpdateButtonsColor() {
        startModellingButton.GetComponent<Graphic>().color =
            (ScriptsContainer.fieldGUI.IsOpened() || ScriptsContainer.fieldContainerUI.IsOpened())? inactiveColor : startModellingColor;
        stopModellingButton.GetComponent<Graphic>().color =
            (ScriptsContainer.fieldGUI.IsOpened())? stopModellingColor : inactiveColor;
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(0.01f);
        UpdateButtonsColor();
    }

    public void StartModelling() {
        if(ScriptsContainer.fieldContainerUI.IsOpened() || ScriptsContainer.fieldGUI.IsOpened())
            return;

        ScriptsContainer.fieldContainerUI.OpenWindow();
    }

    public void StopModelling() {
        if (!ScriptsContainer.fieldGUI.IsOpened())
            return;

        ScriptsContainer.isingAlgorithm.StopModelling();
        ScriptsContainer.fieldContainerUI.CloseWindow();
        ScriptsContainer.fieldGUI.CloseWindow();
        ScriptsContainer.settingsGUI.CloseWindow();
        ScriptsContainer.fieldContainerUI.CloseWindow();
    }

    public void ExitProgram() => Application.Quit();
}
