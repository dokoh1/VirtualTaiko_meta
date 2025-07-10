using UnityEngine;

public class Result  : MonoBehaviour
{
    [SerializeField] private NumberImage perfect;
    [SerializeField] private NumberImage good;
    [SerializeField] private NumberImage bad;
    [SerializeField] private NumberImage hit;
    [SerializeField] private NumberImage combo;
    [SerializeField] private NumberImage score;
    [SerializeField] private AudioClip resultBackground;

    private void OnEnable()
    {
        perfect.UpdateDisplay(Single.System.ScoreManager.Perfect);
        good.UpdateDisplay(Single.System.ScoreManager.Good);
        bad.UpdateDisplay(Single.System.ScoreManager.Bad);
        hit.UpdateDisplay(Single.System.ScoreManager.Hit);
        combo.UpdateDisplay(Single.System.ScoreManager.Combo);
        score.UpdateDisplay(Single.System.ScoreManager.Score);
        Single.System.AudioManager.PlayBGM(resultBackground);
    }
}
