using UnityEngine;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    public TimingManager timingManager;
    public NoteManager1 noteManager1;
    
    [SerializeField]
    private JudgementData _judgementData;

    private ScoreData _scoreData;
 
    public EffectManager effectManager;
    public AudioFeedback audioFeedback;
    public GameOverHandler gameOverHandler;
    
    private void OnEnable()
    {
        _scoreData = new ScoreData();
    }
    
    private void Update()
    {
        if (noteManager1.isMusicEnded || _scoreData.isDead)
        {
            gameOverHandler.EndGame(_scoreData);
            return;
        }

        while (timingManager.HitQueue.Count > 0)
        {
            var hit = timingManager.HitQueue.Dequeue();
            if (hit == HitResult.None)
                continue;
            
            _scoreData.ApplyHit(hit, _judgementData);
            effectManager.OnHit(hit, _scoreData);
            audioFeedback.PlayComboSound(_scoreData.combo);
        }
        effectManager.UpdateUI(_scoreData);
    }
}
