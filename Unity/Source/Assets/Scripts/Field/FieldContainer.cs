using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldContainer : MonoBehaviour {
    private Field field;
    
    #region Changeable size settings
    [Header("Field size settings")]
    [SerializeField] private int fieldWidth;
    [SerializeField] private int fieldHeight;
    #endregion

    public void GenerateField(int fieldWidth, int fieldHeight) => field = new Field(fieldWidth, fieldHeight); 
    public Field GetField() => field;
}
