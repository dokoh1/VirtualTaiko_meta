using UnityEngine;

public class ResultView  : MonoBehaviour
{
    [SerializeField] private NumberImage perfect;
    [SerializeField] private NumberImage good;
    [SerializeField] private NumberImage bad;
    [SerializeField] private NumberImage hit;
    [SerializeField] private NumberImage combo;
    [SerializeField] private NumberImage score;
    [SerializeField] private AudioClip resultBackground;

    public void Initialize(ScoreData data)
    {
        perfect.UpdateDisplay(data.perfectHit);
        good.UpdateDisplay(data.goodHit);
        bad.UpdateDisplay(data.badHit);
        hit.UpdateDisplay(data.hit);
        combo.UpdateDisplay(data.maxCombo);
        score.UpdateDisplay(data.score);
        Single.System.AudioManager.PlayBGM(resultBackground);
    }
}
