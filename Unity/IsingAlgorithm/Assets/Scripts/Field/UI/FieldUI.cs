using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : WindowObject {
    private const int MAX_FREQUENCY = 200;
    private const int MIN_FREQUENCY = 1;

    private Field field;

    [Header("Size settings")]
    [SerializeField] private float windowHeightRatio;
    [SerializeField] private int headerHeight;
    [SerializeField] private int buttonHeight;

    [Header("UI settings")]
    #region Color settings
    [SerializeField] private Color plusSpinColor;
    [SerializeField] private Color minusSpinColor;
    private Color startWindowColor;
    #endregion

    #region Calculated size settings
    private int boxWidth;
    private int boxHeight;
    private int cellSize;
    #endregion

    [SerializeField] private Transform fieldWindow;
    [SerializeField] private GameObject cellObject; 

    [SerializeField] private bool isShowing = false;

    private List<List<GameObject>> fieldCells;

    private void GetField() => field = transform.GetComponent<FieldContainer>().GetField();

    private void Start() {
        fieldCells = new List<List<GameObject>>();
        startWindowColor = fieldWindow.GetComponent<Image>().color;
        fieldWindow.GetComponent<Image>().color -= new Color(0, 0, 0, startWindowColor.a);

        ScriptsContainer.isingAlgorithm.OnFrequencyReached += UpdateField;
    }

    private void UpdateInterfaceScale() {
        int desirableHeight = (int)((float)Screen.height / windowHeightRatio);

        if ((desirableHeight / field.GetSize().y) * field.GetSize().x < Screen.width) 
            cellSize = desirableHeight / field.GetSize().y;
        else 
            cellSize = Screen.width / field.GetSize().x;

        boxHeight = headerHeight + cellSize * field.GetSize().y + buttonHeight;
        boxWidth = cellSize * field.GetSize().x;
    }

    public override void OpenWindow() {
        base.OpenWindow();

        isShowing = true;
        GetField();
        UpdateInterfaceScale();
        Debug.Log($"Width: {boxWidth.ToString()}; Height: {boxHeight.ToString()}");
        
        fieldWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(boxWidth, boxHeight);
        fieldWindow.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize, cellSize);
        fieldWindow.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, headerHeight);
        fieldWindow.GetComponent<Image>().color = startWindowColor;

        for (int x = 0; x < field.GetSize().x; ++x) {
            List<GameObject> cellRow = new List<GameObject>();
            for (int y = 0; y < field.GetSize().y; ++y) {
                GameObject createdCell = Instantiate(cellObject, Vector3.zero, Quaternion.identity);
                createdCell.transform.SetParent(fieldWindow);
                createdCell.name = $"Cell ({x}, {y})";
                cellRow.Add(createdCell);
            }

            fieldCells.Add(cellRow);
        }
    }

    public override void CloseWindow() {
        base.CloseWindow();

        isShowing = false;
        for (int x = 0; x < field.GetSize().x; ++x) {
            for (int y = 0; y < field.GetSize().y; ++y) {
                Destroy(fieldCells[x][y]);
            }
        }

        fieldCells = new List<List<GameObject>>();
        fieldWindow.GetComponent<Image>().color -= new Color(0, 0, 0, fieldWindow.GetComponent<Image>().color.a);
    }

    private void UpdateField() {
        for (int x = 0; x < field.GetSize().x; ++x) {
            for (int y = 0; y < field.GetSize().y; ++y) {
                fieldCells[x][y].GetComponent<Image>().color = (field.GetCell(x, y) == 1)? plusSpinColor : minusSpinColor;
            }
        }
    }
}
