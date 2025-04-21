using UnityEngine;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    public int currentScore;
    public TestDrumInput testDrumInput;
    public int ComboHit;
    public int DeadGauge;
    public int MaxDeadGauge;
    private int deadGaugeAmount;
    private JudgementData _judgementData;
    
    private void Start()
    {
        currentScore = 0;
        DeadGauge = 200;
        MaxDeadGauge = 200;
        deadGaugeAmount = 50;
        _judgementData = new();
    }

    private void Update()
    {
        ScoreUpdate();
    }
    
    private void ScoreUpdate()
    {
        if (testDrumInput.judgementData == JudgementDataType.Bad)
        {
            DeadGauge -= deadGaugeAmount;
            if (DeadGauge < 0)
                return;
            ComboHit = 0;
        }
        else if (testDrumInput.judgementData == JudgementDataType.Good)
            ScoreCalculation(_judgementData.GoodComboScore, _judgementData.GoodScore);
        else if (testDrumInput.judgementData == JudgementDataType.Great)
           ScoreCalculation(_judgementData.GreatComboScore, _judgementData.GreatScore);
    }

    void ScoreCalculation(int comboScore, int score)
    {
        ComboHit++;
        if (ComboHit % 10 == 0)
            currentScore += comboScore;
        else
            currentScore += score;
        if (DeadGauge < MaxDeadGauge)
            DeadGauge += deadGaugeAmount;
    }
}
