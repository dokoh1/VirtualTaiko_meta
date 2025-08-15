
using System;
using UnityEngine;

public class ScoreData
{
    public int score { get; private set; }
    public int combo {get; private set;}
    public int hit { get; private set; }
    public int maxCombo { get; private set; }
    public int perfectHit { get; private set; }
    public int goodHit { get; private set; }
    public int badHit { get; private set; }
    //게임 밸런스 관련 상수들
    private const float MAX_DEAD_GAUGE = 350f;
    private const float INITIAL_DEAD_GAUGE = 350f;
    private const float DEAD_GAUGE_CHANGE_AMOUNT = 10f;
    public float deadGuage { get; private set; } = INITIAL_DEAD_GAUGE;

    public bool isDead => deadGuage <= 0;

    public void ApplyHit(HitResult result, JudgementData data)
    {
        switch (result)
        {
            case HitResult.Bad:
                badHit++;
                combo = 0;
                deadGuage = MathF.Max(deadGuage - DEAD_GAUGE_CHANGE_AMOUNT, 0f);
                break;
            case HitResult.SmallGood:
            case HitResult.BigGood:
                goodHit++;
                AddScore(data.goodScore, data.goodComboScore);
                break;
            case HitResult.SmallPerfect:
            case HitResult.BigPerfect:
                perfectHit++;
                AddScore(data.greatScore, data.greatComboScore);
                break;
        }

        hit++;
    }

    private void AddScore(int baseScore, int comboScore)
    {
        combo++;
        maxCombo = Mathf.Max(maxCombo, combo);
        score += (combo % 10 == 0) ? comboScore : baseScore;
        deadGuage = Mathf.Min(deadGuage + DEAD_GAUGE_CHANGE_AMOUNT, MAX_DEAD_GAUGE);
    }
}
