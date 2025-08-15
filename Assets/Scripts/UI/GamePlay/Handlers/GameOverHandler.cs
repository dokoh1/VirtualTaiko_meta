
using UnityEngine;

public class GameOverHandler : MonoBehaviour, IGameOverHandler
{
    public void EndGame(ScoreData data)
    {
        Single.System.ScoreManager.Score = data.score;
        Single.System.ScoreManager.Hit = data.hit;
        Single.System.ScoreManager.Combo = data.maxCombo;
        Single.System.ScoreManager.Perfect = data.perfectHit;
        Single.System.ScoreManager.Good = data.goodHit;
        Single.System.ScoreManager.Bad = data.badHit;
        Single.System.SceneManager.LoadScene(SceneDataType.Result);
    }
}
