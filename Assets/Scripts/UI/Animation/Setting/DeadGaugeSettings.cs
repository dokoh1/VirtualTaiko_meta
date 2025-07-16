using UnityEngine;

[CreateAssetMenu(fileName = "DeadGaugeSettings", menuName = "UI/Dead Gauge Settings")]
public class DeadGaugeSettings : ScriptableObject
{
    [Header("Gauge Settings")] 
    public float maxGauge;
    public float initFirstGauge;
    public float initSecondGauge;
    public float animationDuration;
}