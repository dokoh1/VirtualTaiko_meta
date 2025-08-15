using UnityEngine;
using UnityEngine.Serialization;

public class PlayController : MonoBehaviour
{
    public TimingManager timingManager;
    public NoteManager1 noteManager1;
    
    [SerializeField]
    private JudgementData _judgementData;

    private ScoreData _scoreData;
    
    private void OnEnable()
    {
        _scoreData = new ScoreData();
    }
    
    private void Update()
    {
        if (noteManager1.isMusicEnded || _scoreData.isDead)
        {
            GameEvents.TriggerOnGameOver(_scoreData);
            this.enabled = false;
            // gameOverHandler.EndGame(_scoreData);
            return;
        }

        while (timingManager.HitQueue.Count > 0)
        {
            var hit = timingManager.HitQueue.Dequeue();
            if (hit == HitResult.None)
                continue;

            int oldCombo = _scoreData.combo;
            int oldScore = _scoreData.score;
            float oldGauge = _scoreData.deadGuage;
            
            _scoreData.ApplyHit(hit, _judgementData);

            GameEvents.TriggerOnNoteHit(hit, _scoreData);

            if (oldScore != _scoreData.score)
                GameEvents.TriggerOnScoreUpdated(_scoreData.score);
            if (oldCombo != _scoreData.combo)
                GameEvents.TriggerOnComboUpdated(_scoreData.combo);
            if (oldGauge != _scoreData.deadGuage)
                GameEvents.TriggerOnDeadGauge(_scoreData.deadGuage);
        }

    }
}
