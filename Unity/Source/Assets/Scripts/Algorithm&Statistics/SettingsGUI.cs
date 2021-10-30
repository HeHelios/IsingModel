using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsGUI : WindowObject {
    private const int INDENT = 25;

    [Header("Limits settings")]
    [SerializeField] private Vector2 temperatureLimits;
    [SerializeField] private Vector2 MagneticFieldLimits;

    [Header("Window settings")]
    [SerializeField] private GUISkin skin;
    [SerializeField] private Vector2 startCoordinates;
    [SerializeField] private int windowWidth;
    [SerializeField] private int sliderHeight = 20;
    [SerializeField] private int outlineThickness = 5;
    private Rect windowRect;
    private GUIStyle textStyle;

    private void Start() {
        windowRect = new Rect(startCoordinates.x, startCoordinates.y, windowWidth, (sliderHeight + outlineThickness) * 4 + INDENT);

        textStyle = new GUIStyle();
        textStyle.richText = true;
        textStyle.alignment = TextAnchor.MiddleLeft;
        textStyle.fontSize = 20;
    }

    private void OnGUI() {
        GUI.skin = skin;
        if(isOpened) windowRect = GUI.Window(1, windowRect, SettingsWindow, "Settings");
    }

    private void SettingsWindow(int id) {
        GUI.Label(new Rect(outlineThickness, INDENT + outlineThickness, windowRect.width - 2 * outlineThickness, sliderHeight), $"<color=white>Temperature. <b>T = {ScriptsContainer.isingAlgorithm.temperature.ToString()}</b></color>", textStyle);
        ScriptsContainer.isingAlgorithm.temperature = GUI.HorizontalSlider(
            new Rect(outlineThickness, INDENT + 2*outlineThickness + sliderHeight, windowRect.width - 2 * outlineThickness, sliderHeight), 
            ScriptsContainer.isingAlgorithm.temperature,
            temperatureLimits.x, temperatureLimits.y);

        GUI.Label(new Rect(outlineThickness, INDENT + 3*outlineThickness + 2*sliderHeight, windowRect.width - 2 * outlineThickness, sliderHeight), $"<color=white>Magnetic field. <b>M = {ScriptsContainer.isingAlgorithm.magneticField.ToString()}</b></color>", textStyle);
        ScriptsContainer.isingAlgorithm.magneticField = GUI.HorizontalSlider(
            new Rect(outlineThickness, INDENT + 4*outlineThickness + 3*sliderHeight, windowRect.width - 2 * outlineThickness, sliderHeight), 
            ScriptsContainer.isingAlgorithm.magneticField,
            MagneticFieldLimits.x, MagneticFieldLimits.y);

        GUI.DragWindow();
    }
}
