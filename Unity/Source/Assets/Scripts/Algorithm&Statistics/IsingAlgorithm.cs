using System.Collections;
using UnityEngine;
using System;

public class IsingAlgorithm : MonoBehaviour {
    public event Action OnFrequencyReached;
    private Field field;
    private Coroutine currentPlayingCoroutine;
    [SerializeField] private UIGraphNormalizer graphNormalizer_energy; 
    [SerializeField] private ConstantFunction averageEnergy;
    [SerializeField] private UIGraphNormalizer graphNormalizer_magnetization;
    [SerializeField] private ConstantFunction averageMagnetization;

    public float temperature;
    public float magneticField;
    [SerializeField] private float delayBetweenIterations = 0.01f;

    #region Start settings
    private float startTemperature;
    private float startMagneticField;
    #endregion

    [SerializeField] private bool isRunning;
    private float frequency = 100;
    [SerializeField] private int currentNumberOfIterations;

    private void Start() {
        startTemperature = temperature;
        startMagneticField = magneticField;
    }

    private void FixedUpdate() {
        if (isRunning) MonteCarlo(field, temperature, magneticField);
    }

    public void StartModelling() {
        field = ScriptsContainer.fieldContainer.GetField();
        isRunning = true;
    } 
    public void StopModelling() {
        temperature = startTemperature;
        magneticField = startMagneticField;
        currentNumberOfIterations = 0;
        
        isRunning = false;
    }

    public void StopModellingCoroutine() {
        if (currentPlayingCoroutine != null) {
            StopCoroutine(currentPlayingCoroutine);
        }
    }

    private void VisualizeAlgorithm() {
        if (currentNumberOfIterations % frequency == 0) {
            graphNormalizer_energy.AddAndResize(currentNumberOfIterations, field.GetTotalEnergy(), true);
            graphNormalizer_magnetization.AddAndResize(currentNumberOfIterations, field.GetTotalMagnetization(), true);
            averageEnergy.UpdateValue(field.GetAverageEnergy());
            averageMagnetization.UpdateValue(field.GetAverageMagnetization());
            OnFrequencyReached?.Invoke();
        }
        
        currentNumberOfIterations++;
    }

    private void MonteCarlo(Field field, float temperature, float magneticField) {
        int currentX = UnityEngine.Random.Range(0, field.GetSize().x);
        int currentY = UnityEngine.Random.Range(0, field.GetSize().y);

        float energyChange = field.FlipEnergy(currentX, currentY, magneticField);

        if (energyChange < 0) 
            field.FlipSpin(currentX, currentY, magneticField);
        else if (UnityEngine.Random.Range(0f, 1f) <= Mathf.Exp(-energyChange / temperature)) 
            field.FlipSpin(currentX, currentY, magneticField);

        VisualizeAlgorithm();
    }
}
