using UnityEngine;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    // public TestDrumInput testDrumInput;
    
    private int currentScore;
    private int ComboHit;
   
    private int DeadGauge;
    private int MaxDeadGauge;
    private int deadGaugeAmount;
    
    private JudgementData _judgementData;
    
    public TimingManager timingManager;
    public NumberImage ScoreNumberImage;
    public NumberImage ComboNumberImage;
    
    private void Start()
    {
        currentScore = 0;
        ComboHit = 0;
        DeadGauge = 200;
        MaxDeadGauge = 200;
        deadGaugeAmount = 50;
        _judgementData = new();
        ScoreNumberImage.UpdateDisplay(currentScore);
    }

    private void Update()
    {
        if (timingManager.HitQueue.Count > 0)
        {
            ScoreUpdate();
            ScoreNumberImage.UpdateDisplay(currentScore);
            ComboNumberImage.UpdateDisplay(ComboHit);
        }
    }
    
    private void ScoreUpdate()
    {
        HitResult hitResult = timingManager.HitQueue.Dequeue();
        if (hitResult == HitResult.Bad)
        {
            DeadGauge -= deadGaugeAmount;
            if (DeadGauge < 0)
                return;
            ComboHit = 0;
        }
        else if (hitResult == HitResult.Good)
            ScoreCalculation(_judgementData.GoodComboScore, _judgementData.GoodScore);
        else if (hitResult == HitResult.Perfect)
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
