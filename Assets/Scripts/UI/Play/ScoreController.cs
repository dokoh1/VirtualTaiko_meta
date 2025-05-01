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
    
    // public TimingManager timingManager;
    public FireAnimation fireAnimation;
    public GoblinAnimation goblinAnimation;
    public CharacterAnimation characterAnimation;
    public TestTimingManager testTimingManager;
    public JudgementEffect judgementEffect;
    public NumberImage ScoreNumberImage;
    public NumberImage ComboNumberImage;
    public DeadGauge deadGauge;
    public FireworkAnimation fireworkAnimation;
    public CloudAnimation cloudAnimation;
    
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
        // while (timingManager.HitQueue.Count > 0)
        if (testTimingManager.HitQueue.Count > 0)
        {
            while (testTimingManager.HitQueue.Count > 0)
            {
                TestEnum hitResult = testTimingManager.HitQueue.Dequeue();
                if (hitResult == TestEnum.None)
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
                fireAnimation.SetIsFire(ComboHit);
                characterAnimation.UpdateAnimator(ComboHit);
                ScoreNumberImage.UpdateDisplay(currentScore);
                ComboNumberImage.UpdateDisplay(ComboHit);
                deadGauge.DeadGaugeUpdate(DeadGauge);

            }
        }
    }
    
    private void ScoreUpdate(TestEnum hitResult)
    {
        StartCoroutine(judgementEffect.EffectUpdate(hitResult));
        
        // if (hitResult == HitResult.Bad)
        if (hitResult == TestEnum.Bad)
        {
            BadHit++;
            DeadGauge -= deadGaugeAmount;
            if (DeadGauge < 0)
                return;
            if (ComboHit > MaxCombo)
                MaxCombo = ComboHit;
            ComboHit = 0;
        }
        // else if (hitResult == HitResult.Good)
        // else if (hitResult == TestEnum.GoodBig || hitResult == TestEnum.GoodSmall)
        else if (hitResult == TestEnum.GoodBig)
        {
            ScoreCalculation(_judgementData.GoodComboScore, _judgementData.GoodScore);
            GoodHit++;
        }
        // else if (hitResult == HitResult.Perfect)
        // else if (hitResult == TestEnum.PerfectSmall || hitResult == TestEnum.PerfectBig)
        else if (hitResult == TestEnum.PerfectSmall)
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
        if (Hit == 30)
            goblinAnimation.CreateGoblin();
        else if (Hit == 60)
        {
            goblinAnimation.CreateGoblin();
            cloudAnimation.ChangeImage();
        }
        if (ComboHit % 30 == 0 && ComboHit != 0)
            fireworkAnimation.DoFireWork();
    }
}
