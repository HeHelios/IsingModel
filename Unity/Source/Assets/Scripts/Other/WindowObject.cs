using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowObject : MonoBehaviour {
    protected bool isOpened;

    public bool IsOpened() => isOpened;

    public virtual void OpenWindow() {
        if (isOpened) {
            Debug.LogWarning("This window is already opened!");
            return;
        }
        
        isOpened = true;
        ScriptsContainer.panel.UpdateButtonsColor();
    }

    public virtual void CloseWindow() {
        if (!isOpened) {
            Debug.LogWarning("This window is already closed");
            return;
        }

        isOpened = false;
        ScriptsContainer.panel.UpdateButtonsColor();
    }
}
