using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FieldGUI : WindowObject {
    private const int INDENT = 2;

    private Field field;

    [Header("Size settings")]
    [SerializeField] private float windowHeightRatio;
    [SerializeField] private int headerHeight;
    [SerializeField] private int buttonHeight;
    private Rect windowRect;

    #region Calculated size settings
    private int boxWidth;
    private int boxHeight;
    private int cellSize;
    #endregion

    [Header("UI settings")]
    #region Color settings
    [SerializeField] private Color plusSpinColor;
    [SerializeField] private Color minusSpinColor;
    #endregion
    [SerializeField] private GUISkin skin;
    [SerializeField] private Texture2D whiteTexture; 

    #region Button settings
    [Header("Button settings")]
    [SerializeField] private string buttonTextOnResume = "Resume";
    [SerializeField] private string buttonTextOnStop = "Stop";
    private bool buttonIsInResumeMode;
    private string buttonText;
    private Action buttonAction;
    #endregion

    private void UpdateButtonInformation() {
        buttonText = buttonTextOnStop;
        buttonAction = ScriptsContainer.isingAlgorithm.StopModellingCoroutine;
        buttonIsInResumeMode = false;
    }

    public override void OpenWindow() {
        base.OpenWindow();

        GetField();
        UpdateInterfaceScale();
        SetStartWindowSize();
        UpdateButtonInformation();
    }

    private void GetField() => field = transform.GetComponent<FieldContainer>().GetField();

    private void UpdateInterfaceScale() {
        int desirableHeight = (int)((float)Screen.height / windowHeightRatio);

        if ((desirableHeight / field.GetSize().y) * field.GetSize().x < Screen.width) 
            cellSize = desirableHeight / field.GetSize().y;
        else 
            cellSize = Screen.width / field.GetSize().x;

        boxHeight = headerHeight + cellSize * field.GetSize().y + buttonHeight;
        boxWidth = cellSize * field.GetSize().x;
    }

    private void SetStartWindowSize() => windowRect = new Rect(Screen.width/2 - boxWidth/2, Screen.height/2 - boxHeight/2, boxWidth, boxHeight);

    private void UpdateFieldInformation() {
        for (int x = 0; x < field.GetSize().x; ++x) {
            for (int y = 0; y < field.GetSize().y; ++y) {
                Color colorToHighlight = (field.GetCell(x, y) == 1)? plusSpinColor : minusSpinColor;
                GUI.color = colorToHighlight;
                GUI.DrawTexture(new Rect(x * cellSize + INDENT, headerHeight + y * cellSize + INDENT, cellSize - 2 * INDENT, cellSize - 2 * INDENT), whiteTexture);
                GUI.color = Color.white;
            }
        }
    }

    private void ChangeButtonMode() {
        buttonIsInResumeMode = !buttonIsInResumeMode;
        buttonText = buttonIsInResumeMode? buttonTextOnResume : buttonTextOnStop;
        buttonAction = buttonIsInResumeMode? ScriptsContainer.isingAlgorithm.StartModelling : (Action)ScriptsContainer.isingAlgorithm.StopModellingCoroutine;
    }

    private void FieldWindow(int id) {
        for (int x = 0; x < field.GetSize().x; ++x) {
            for (int y = 0; y < field.GetSize().y; ++y) {
                GUI.Box(new Rect(x * cellSize, headerHeight + y * cellSize, cellSize, cellSize), string.Empty);
            }
        }
        
        if (GUI.Button(new Rect(0, windowRect.height - buttonHeight, windowRect.width, buttonHeight), buttonText)) {
            buttonAction?.Invoke();
            ChangeButtonMode();
        }

        UpdateFieldInformation();
        
        GUI.DragWindow();
    }

    private void OnGUI() {
        GUI.skin = skin;
        
        if(isOpened) windowRect = GUI.Window(0, windowRect, FieldWindow, "Field");
    }
}
