public interface IEffectManager
{
    void OnHit(HitResult result, ScoreData scoreData);
    void UpdateUI(ScoreData scoreData);
}
