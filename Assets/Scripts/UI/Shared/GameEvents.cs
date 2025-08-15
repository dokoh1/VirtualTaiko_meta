using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<HitResult, ScoreData> OnNoteHit;
    public static void TriggerOnNoteHit(HitResult result, ScoreData data) => OnNoteHit?.Invoke(result, data);

    public static event Action<int> OnComboUpdated;
    public static void TriggerOnComboUpdated(int combo) => OnComboUpdated?.Invoke(combo);

    public static event Action<int> OnScoreUpdated;
    public static void TriggerOnScoreUpdated(int score) => OnScoreUpdated?.Invoke(score);

    public static event Action<ScoreData> OnGameOver;
    public static void TriggerOnGameOver(ScoreData data) => OnGameOver?.Invoke(data);

    public static event Action<float> OnDeadGauge;
    public static void TriggerOnDeadGauge(float value) => OnDeadGauge?.Invoke(value);
}
