using UnityEngine;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    // public TestDrumInput testDrumInput;
    
    private int currentScore;
    
    private int ComboHit;
    private int Hit;
    private int MaxCombo;
    
    private int PerfectHit;
    private int GoodHit;
    private int BadHit;
   
    private float DeadGauge;
    private float MaxDeadGauge;
    private float deadGaugeAmount;
    
    private JudgementData _judgementData;
    
    public PlayerController playerController;
    public NumberImage ScoreNumberImage;
    public NumberImage ComboNumberImage;
    public DeadGauge deadGauge;
    
    private void OnEnable()
    {
        currentScore = 0;
        ComboHit = 0;
        Hit = 0;
        MaxCombo = 0;
        PerfectHit = 0;
        GoodHit = 0;
        BadHit = 0;
        DeadGauge = 350;
        MaxDeadGauge = 350;
        deadGaugeAmount = 50;
        _judgementData = new();
        ScoreNumberImage.UpdateDisplay(currentScore);
    }

    private void Update()
    {
        if (playerController.HitQueue.Count > 0)
        {
            ScoreUpdate();
            if (DeadGauge == 0)
            {
                dokoh.System.ScoreManager.Score = currentScore;
                dokoh.System.ScoreManager.Hit = Hit;
                dokoh.System.ScoreManager.Combo = MaxCombo;
                dokoh.System.ScoreManager.Perfect = PerfectHit;
                dokoh.System.ScoreManager.Good = GoodHit;
                dokoh.System.ScoreManager.Bad = BadHit;
                dokoh.System.SceneManager.LoadScene(SceneDataType.Score);
            }
            ScoreNumberImage.UpdateDisplay(currentScore);
            ComboNumberImage.UpdateDisplay(ComboHit);
            deadGauge.DeadGaugeUpdate(DeadGauge);

        }
    }
    
    private void ScoreUpdate()
    {
        HitResult hitResult = playerController.HitQueue.Dequeue();
        if (hitResult == HitResult.Bad)
        {
            DeadGauge -= deadGaugeAmount;
            if (DeadGauge < 0)
                return;
            if (ComboHit > MaxCombo)
                MaxCombo = ComboHit;
            ComboHit = 0;
        }
        else if (hitResult == HitResult.Good)
        {
            ScoreCalculation(_judgementData.GoodComboScore, _judgementData.GoodScore);
            GoodHit++;
        }
        else if (hitResult == HitResult.Perfect)
        {
           ScoreCalculation(_judgementData.GreatComboScore, _judgementData.GreatScore);
           PerfectHit++;
        }
    }

    void ScoreCalculation(int comboScore, int score)
    {
        ComboHit++;
        Hit++;
        if (ComboHit % 10 == 0)
            currentScore += comboScore;
        else
            currentScore += score;
        if (DeadGauge < MaxDeadGauge)
            DeadGauge += deadGaugeAmount;
    }
}
