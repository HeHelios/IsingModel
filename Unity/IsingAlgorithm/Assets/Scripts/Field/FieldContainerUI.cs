using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FieldContainerUI : WindowObject {
    private const int INDENT = 30;
    private const int OUTLINE_THICKNESS = 2;
    private const int MAX_TEXT_LENGTH = 3;

    private Rect windowRect;

    [SerializeField] private int windowWidth;
    [SerializeField] private int textFieldHeight;
    [SerializeField] private GUISkin skin;
    private GUIStyle textStyle;

    private string widthText = "";
    private string heightText = "";

    public override void OpenWindow() {
        base.OpenWindow();
        SetSize();
    }

    private void SetSize() {
        int windowHeight = INDENT + 3 * textFieldHeight + 4 * OUTLINE_THICKNESS;
        windowRect = new Rect(Screen.width/2 - windowHeight/2, Screen.height/2 - windowHeight/2, windowWidth, windowHeight);
    }

    private void InitializeComponents() {
        textStyle = new GUIStyle();
        textStyle.fontSize = 20;
        textStyle.richText = true;
        textStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void Start() => InitializeComponents();
    

    private void OnGUI() {
        GUI.skin = skin;

        if(isOpened) windowRect = GUI.Window(3, windowRect, ContainerWindow, "Field settings");
    }

    private void ContainerWindow(int id) {
        GUI.Label(new Rect(OUTLINE_THICKNESS, INDENT, windowRect.width/3, textFieldHeight), "<color=white>Width:</color>", textStyle);
        widthText = GUI.TextField(new Rect(OUTLINE_THICKNESS + windowRect.width/3, INDENT, 2*windowRect.width/3 - INDENT, textFieldHeight), widthText, MAX_TEXT_LENGTH);
        
        GUI.Label(new Rect(OUTLINE_THICKNESS, INDENT + textFieldHeight + OUTLINE_THICKNESS, windowRect.width/3, textFieldHeight), "<color=white>Height:</color>", textStyle);
        heightText = GUI.TextField(new Rect(OUTLINE_THICKNESS + windowRect.width/3, INDENT + textFieldHeight + OUTLINE_THICKNESS, 2*windowRect.width/3 - INDENT, textFieldHeight), heightText, MAX_TEXT_LENGTH);

        if (GUI.Button(new Rect(OUTLINE_THICKNESS, INDENT + 2*(textFieldHeight + OUTLINE_THICKNESS), windowRect.width - 2 * OUTLINE_THICKNESS, textFieldHeight), "Generate!")) {
            CloseWindow();

            int fieldWidth = 0, fieldHeight = 0;
            if (!Int32.TryParse(widthText, out fieldWidth)) {
                Debug.LogError("You must put the number into the text field");
                return;
            }

            if (!Int32.TryParse(heightText, out fieldHeight)) {
                Debug.LogError("You must put the number into the text field");
                return;
            }

            Debug.Log($"Width: {fieldWidth.ToString()}; Height: {fieldHeight.ToString()}");

            ScriptsContainer.fieldContainer.GenerateField(fieldWidth, fieldHeight);
            ScriptsContainer.fieldGUI.OpenWindow();
            ScriptsContainer.settingsGUI.OpenWindow();
            ScriptsContainer.isingAlgorithm.StartModelling();
        }

        GUI.DragWindow();
    }
}
