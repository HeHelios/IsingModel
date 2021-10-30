using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {
    private float totalEnergy;
    private float totalMagnetization;
    private int width;
    private int height;

    private float averageEnergy;
    private float averageMagnetization;
    private int totalFlips;
    
    private Dictionary<Vector2Int, int> fieldCells;

    public Field(int width, int height) {
        this.width = width;
        this.height = height;
        this.fieldCells = new Dictionary<Vector2Int, int>();

        this.totalEnergy = -((width + 1) * height + width * (height + 1));
        this.totalMagnetization = width * height;

        this.averageEnergy = this.totalEnergy;
        this.averageMagnetization = this.totalMagnetization;
        this.totalFlips = 1;

        for (int x = 0; x < width; ++x) {
            for (int y = 0; y < height; ++y) {
                this.fieldCells.Add(new Vector2Int(x, y), 1);
            }
        }
    }

    #region Accessors
    public Vector2Int GetSize() => new Vector2Int(width, height);
    public float GetTotalEnergy() => totalEnergy;
    public float GetAverageEnergy() => averageEnergy;
    public float GetTotalMagnetization() => totalMagnetization;
    public float GetAverageMagnetization() => averageMagnetization;
    public int GetCell(int x, int y) {
        if (x >= width) x = 0;
        if (y >= height) y = 0;
        if (x < 0) x = width - 1;
        if (y < 0) y = height - 1;

        return fieldCells[new Vector2Int(x, y)];
    }
    #endregion

    #region Event handler
    private void SetCell(int x, int y, int value) {
        if (value != 1 && value != (-1)) {
            Debug.LogError("Value must be either 1 or -1");
            return;
        }

        fieldCells[new Vector2Int(x, y)] = value;
    }
    private int GetSumNeighbours(int x, int y) => GetCell(x - 1, y) + GetCell(x + 1, y) + GetCell(x, y - 1) + GetCell(x, y + 1);
    
    public float FlipEnergy(int x, int y, float magneticField) => 2 * GetCell(x, y) * GetSumNeighbours(x, y) - 2 * magneticField * GetCell(x, y);
    public void FlipSpin(int x, int y, float magneticField) {
        totalEnergy += FlipEnergy(x, y, magneticField);
        totalMagnetization -= 2 * GetCell(x, y);

        averageEnergy = ((float)totalFlips * averageEnergy + totalEnergy)/((float)totalFlips + 1f);
        averageMagnetization = ((float)totalFlips * averageMagnetization + totalMagnetization)/((float)totalFlips + 1);

        ++totalFlips;
        SetCell(x, y, -GetCell(x, y));
    }
    #endregion
}
