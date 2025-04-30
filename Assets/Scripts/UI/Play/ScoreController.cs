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
    
    public TimingManager timingManager;
    // public CharacterAnimation characterAnimation;
    // public TestTimingManager testTimingManager;
    // public JudgementEffect judgementEffect;
    public NumberImage ScoreNumberImage;
    public NumberImage ComboNumberImage;
    // public DeadGauge deadGauge;
    
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
        //Execute
        if (timingManager.HitQueue.Count > 0)
        {
            while (timingManager.HitQueue.Count > 0)
            {
                HitResult hitResult = timingManager.HitQueue.Dequeue();
                if (hitResult == HitResult.None)
                    continue;
                ScoreUpdate(hitResult);
                if (DeadGauge == 0)
                {
                    dokoh.System.ScoreManager.Score = currentScore;
                    dokoh.System.ScoreManager.Hit = Hit;
                    dokoh.System.ScoreManager.Combo = MaxCombo;
                    dokoh.System.ScoreManager.Perfect = PerfectHit;
                    dokoh.System.ScoreManager.Good = GoodHit;
                    dokoh.System.ScoreManager.Bad = BadHit;
                    dokoh.System.SceneManager.LoadScene(SceneDataType.Result);
                }
                // characterAnimation.UpdateAnimator(ComboHit);
                ScoreNumberImage.UpdateDisplay(currentScore);
                ComboNumberImage.UpdateDisplay(ComboHit);
                // deadGauge.DeadGaugeUpdate(DeadGauge);

            }
        }

        //test
        // if (testTimingManager.HitQueue.Count > 0)
        // {
        //     while (testTimingManager.HitQueue.Count > 0)
        //     {
        //         TestEnum hitResult = testTimingManager.HitQueue.Dequeue();
        //         if (hitResult == TestEnum.None)
        //             continue;
        //         ScoreUpdate(hitResult);
        //         if (DeadGauge == 0)
        //         {
        //             dokoh.System.ScoreManager.Score = currentScore;
        //             dokoh.System.ScoreManager.Hit = Hit;
        //             dokoh.System.ScoreManager.Combo = MaxCombo;
        //             dokoh.System.ScoreManager.Perfect = PerfectHit;
        //             dokoh.System.ScoreManager.Good = GoodHit;
        //             dokoh.System.ScoreManager.Bad = BadHit;
        //             dokoh.System.SceneManager.LoadScene(SceneDataType.Result);
        //         }
        //         characterAnimation.UpdateAnimator(ComboHit);
        //         ScoreNumberImage.UpdateDisplay(currentScore);
        //         ComboNumberImage.UpdateDisplay(ComboHit);
        //         deadGauge.DeadGaugeUpdate(DeadGauge);

        //     }
        // }
    }

    //test
    // private void ScoreUpdate(TestEnum hitResult)
    // {
    //     StartCoroutine(judgementEffect.EffectUpdate(hitResult));
        
    //     // if (hitResult == HitResult.Bad)
    //     if (hitResult == TestEnum.Bad)
    //     {
    //         DeadGauge -= deadGaugeAmount;
    //         if (DeadGauge < 0)
    //             return;
    //         if (ComboHit > MaxCombo)
    //             MaxCombo = ComboHit;
    //         ComboHit = 0;
    //     }
    //     else if (hitResult == TestEnum.GoodBig || hitResult == TestEnum.GoodSmall)
    //     {
    //         ScoreCalculation(_judgementData.GoodComboScore, _judgementData.GoodScore);
    //         GoodHit++;
    //     }
    //     else if (hitResult == TestEnum.PerfectSmall || hitResult == TestEnum.PerfectBig)
    //     {
    //        ScoreCalculation(_judgementData.GreatComboScore, _judgementData.GreatScore);
    //        PerfectHit++;
    //     }
    // }
    //execute
    private void ScoreUpdate(HitResult hitResult)
    {
        // StartCoroutine(judgementEffect.EffectUpdate(hitResult));
        
        if (hitResult == HitResult.Bad)
        {
            DeadGauge -= deadGaugeAmount;
            if (DeadGauge < 0)
                return;
            if (ComboHit > MaxCombo)
                MaxCombo = ComboHit;
            ComboHit = 0;
        }
        else if (hitResult == HitResult.SmallGood || hitResult == HitResult.BigGood)
        {
            ScoreCalculation(_judgementData.GoodComboScore, _judgementData.GoodScore);
            GoodHit++;
        }
        else if (hitResult == HitResult.SmallPerfect || hitResult == HitResult.BigPerfect)
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
