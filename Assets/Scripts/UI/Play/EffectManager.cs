using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectManager : MonoBehaviour, IEffectManager
{
    public FireAnimation fireAnimation;
    public CharacterAnimation characterAnimation;
    public GoblinAnimation goblinAnimation;
    public FireworkAnimation fireworkAnimation;
    public CloudAnimation cloudAnimation;
    public JudgementEffect judgementEffect;

    public NumberImage scoreNumberImage;
    public NumberImage comboNumberImage;
    public DeadGauge deadGauge;

    public void OnHit(HitResult result, ScoreData scoreData)
    {
        StartCoroutine(judgementEffect.EffectUpdate(result));
        if (scoreData.hit == 30 || scoreData.hit == 60)
        {
            goblinAnimation.CreateGoblin();
            if (scoreData.hit == 60)
                cloudAnimation.ChangeImage();
        }
        if (scoreData.combo % 10 == 0 && scoreData.combo != 0)
            fireworkAnimation.DoFireWork();
    }

    public void UpdateUI(ScoreData scoreData)
    {
        fireAnimation.SetIsFire(scoreData.combo);
        characterAnimation.UpdateAnimator(scoreData.combo);
        scoreNumberImage.UpdateDisplay(scoreData.score);
        comboNumberImage.UpdateDisplay(scoreData.combo);
        deadGauge.DeadGaugeUpdate(scoreData.deadGuage);
    }
}
