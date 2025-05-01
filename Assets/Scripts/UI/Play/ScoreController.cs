using UnityEngine;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    // public TestDrumInput testDrumInput;
    
    public AudioClip Combo_10;
    public AudioClip Combo_30;
    public AudioClip Combo_50;
    public AudioClip Combo_100;
    public AudioClip Combo_200;
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
    public FireAnimation fireAnimation;
    public GoblinAnimation goblinAnimation;
    public CharacterAnimation characterAnimation;
    // public TestTimingManager testTimingManager;
    public JudgementEffect judgementEffect;
    public NumberImage ScoreNumberImage;
    public NumberImage ComboNumberImage;
    public DeadGauge deadGauge;
    public FireworkAnimation fireworkAnimation;
    public CloudAnimation cloudAnimation;
    public NoteManager1 noteManager1;
    
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
        deadGaugeAmount = 10;
        _judgementData = new();
        ScoreNumberImage.UpdateDisplay(currentScore);
    }

    private void EndGame()
    {
        dokoh.System.ScoreManager.Score = currentScore;
        dokoh.System.ScoreManager.Hit = Hit;
        dokoh.System.ScoreManager.Combo = MaxCombo;
        dokoh.System.ScoreManager.Perfect = PerfectHit;
        dokoh.System.ScoreManager.Good = GoodHit;
        dokoh.System.ScoreManager.Bad = BadHit;
        dokoh.System.SceneManager.LoadScene(SceneDataType.Result);
    }
    private void Update()
    {
        if (noteManager1.isMusicEnded)
            EndGame();

        // EXECUTE
        if (timingManager.HitQueue.Count > 0)
        {
            while (timingManager.HitQueue.Count > 0)
            {
                HitResult hitResult = timingManager.HitQueue.Dequeue();
                if (hitResult == HitResult.None)
                    continue;
                ScoreUpdate(hitResult);
                if (DeadGauge == 0)
                    EndGame();
            }
        }
        fireAnimation.SetIsFire(ComboHit);
        characterAnimation.UpdateAnimator(ComboHit);
        ScoreNumberImage.UpdateDisplay(currentScore);
        ComboNumberImage.UpdateDisplay(ComboHit);
        deadGauge.DeadGaugeUpdate(DeadGauge);
    }
    
    private void ScoreUpdate(HitResult hitResult)
    {
        StartCoroutine(judgementEffect.EffectUpdate(hitResult));
        
        // if (hitResult == HitResult.Bad)
        if (hitResult == HitResult.Bad)
        {
            BadHit++;
            DeadGauge -= deadGaugeAmount;
            if (DeadGauge < 0)
                return;
            if (ComboHit > MaxCombo)
                MaxCombo = ComboHit;
            ComboHit = 0;
        }
        //test 
        // else if (hitResult == TestEnum.SmallGood || hitResult == TestEnum.SmallBig)
        else if (hitResult == HitResult.BigGood || hitResult == HitResult.SmallGood)
        {
            ScoreCalculation(_judgementData.GoodComboScore, _judgementData.GoodScore);
            GoodHit++;
        }

        //test 
        // else if (hitResult == TestEnum.PerfectSmall || hitResult == TestEnum.PerfectBig)

        else if (hitResult == HitResult.SmallPerfect || hitResult == HitResult.BigPerfect)
        {
           ScoreCalculation(_judgementData.GreatComboScore, _judgementData.GreatScore);
           PerfectHit++;
        }
        if (ComboHit == 10)
            dokoh.System.AudioManager.PlaySFX(Combo_10);
        if (ComboHit == 30)
            dokoh.System.AudioManager.PlaySFX(Combo_30);
        if (ComboHit == 50)
            dokoh.System.AudioManager.PlaySFX(Combo_50);
        if (ComboHit == 100)
            dokoh.System.AudioManager.PlaySFX(Combo_100);
        if (ComboHit == 200)
            dokoh.System.AudioManager.PlaySFX(Combo_200);

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
