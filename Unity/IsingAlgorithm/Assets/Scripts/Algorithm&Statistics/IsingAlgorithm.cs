using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsingAlgorithm : MonoBehaviour {
    private Field field;
    private Coroutine currentPlayingCoroutine;

    public float temperature;
    public float magneticField;
    [SerializeField] private float delayBetweenIterations = 0.01f;

    #region Start settings
    private float startTemperature;
    private float startMagneticField;
    #endregion

    private void Start() {
        startTemperature = temperature;
        startMagneticField = magneticField;
    }

    public void StartModelling() {
        field = ScriptsContainer.fieldContainer.GetField();
        currentPlayingCoroutine = StartCoroutine(MakeIteration(delayBetweenIterations));
    } 
    public void StopModelling() {
        if (currentPlayingCoroutine != null) {
            StopCoroutine(currentPlayingCoroutine);
        }

        temperature = startTemperature;
        magneticField = startMagneticField;
    }

    public void StopModellingCoroutine() {
        if (currentPlayingCoroutine != null) {
            StopCoroutine(currentPlayingCoroutine);
        }
    }

    private void MonteCarlo(Field field, float temperature, float magneticField) {
        int currentX = Random.Range(0, field.GetSize().x);
        int currentY = Random.Range(0, field.GetSize().y);

        float energyChange = field.FlipEnergy(currentX, currentY, magneticField);

        if (energyChange < 0) 
            field.FlipSpin(currentX, currentY, magneticField);
        else if (Random.Range(0f, 1f) <= Mathf.Exp(-energyChange / temperature)) 
            field.FlipSpin(currentX, currentY, magneticField);
    }

    private IEnumerator MakeIteration(float delay) {
        yield return new WaitForSeconds(delay);
        MonteCarlo(field, temperature, magneticField);
        yield return currentPlayingCoroutine = StartCoroutine(MakeIteration(delay));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            StartModelling();
        }
    }
}
