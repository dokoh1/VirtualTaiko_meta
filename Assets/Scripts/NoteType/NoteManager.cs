using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    public List<TjaNote> notes = new();

    [Header("판정 시간 허용 범위 (초)")]
    public float perfectThreshold = 0.05f;
    public float goodThreshold = 0.10f;
    public float missThreshold = 0.15f;

    private void Awake()
    {
        // 싱글톤 할당
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    /// 타격 시도 → 판정
    public void TryJudgeHit(float hitTime, HitPosition inputPosition)
    {
        foreach (var note in notes)
        {
            if (note.judged)
                continue;

            float timeDiff = Mathf.Abs(note.time - hitTime);

            // 너무 오래된 노트는 무시
            if (timeDiff > missThreshold)
                continue;

            // 위치 일치 여부
            if (note.requiredHit != inputPosition)
                continue;

            // 판정 처리
            JudgeResult result = JudgeResult.Miss;

            if (timeDiff <= perfectThreshold)
                result = JudgeResult.Perfect;
            else if (timeDiff <= goodThreshold)
                result = JudgeResult.Good;

            note.judged = true;

            Debug.Log($"판정: {result}");

            // 점수, 콤보, 이펙트 처리 등 필요 시 여기에
            return;
        }

        // 일치하는 노트를 못 찾은 경우
        Debug.Log("Miss! 타격 가능한 노트 없음");
    }
}

public enum JudgeResult
{
    Perfect,
    Good,
    Miss
}