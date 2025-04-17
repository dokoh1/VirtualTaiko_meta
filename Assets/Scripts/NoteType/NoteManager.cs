using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;

    public List<TjaNote> notes = new();
    private int currentIndex = 0;

    public float perfectWindow = 0.08f;
    public float goodWindow = 0.15f;

    private int combo = 0;
    private int _totalScore = 0;
    public int TotalScore => _totalScore;

    public event Action<int> OnScoreChanged;

    // 연타 관련
    private bool isRolling = false;
    private float rollEndTime = 0f;
    private float lastRollHitTime = -1f;
    private int rollHitCount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        float timeNow = Time.time;

        // 연타 상태인데 시간이 초과되면 종료
        if (isRolling && timeNow > rollEndTime + 0.2f)
        {
            isRolling = false;
            Debug.Log("🛑 연타 종료됨 (시간 초과)");
        }
    }

    public void TryJudgeHit(float hitTime, HitPosition hitPos)
    {
        // 연타 중이면 연타 판정 시도
        if (isRolling)
        {
            float interval = lastRollHitTime < 0 ? 0f : hitTime - lastRollHitTime;

            if (lastRollHitTime < 0 || (interval >= 0.3f && interval <= 0.4f))
            {
                lastRollHitTime = hitTime;
                rollHitCount++;
                AddScore(300); // 연타는 퍼펙트 판정으로 점수만 추가
                Debug.Log($"🥁 연타 퍼펙트! 누적 타격 수: {rollHitCount}, 점수: {_totalScore}");
            }

            return;
        }

        // 일반 노트 판정
        if (currentIndex >= notes.Count)
            return;

        TjaNote note = notes[currentIndex];
        float delta = Mathf.Abs(hitTime - note.time);

        // 연타 시작 노트라면
        if (note.type == NoteType.YellowRollStart)
        {
            isRolling = true;
            rollEndTime = FindRollEndTime(currentIndex);
            lastRollHitTime = -1f;
            rollHitCount = 0;
            Debug.Log("▶️ 연타 시작!");
            currentIndex++;
            return;
        }

        // 연타 종료 노트는 자동으로 넘김
        if (note.type == NoteType.YellowRollEnd)
        {
            currentIndex++;
            return;
        }

        // 타격 유형이 일치하는지 검사 (추후 확장 가능)
        // if (!IsCorrectHitType(note.type, hitPos)) return;

        if (delta <= perfectWindow)
        {
            HandleJudge(JudgeResult.Perfect);
            currentIndex++;
        }
        else if (delta <= goodWindow)
        {
            HandleJudge(JudgeResult.Good);
            currentIndex++;
        }
        else if (hitTime > note.time + goodWindow)
        {
            HandleJudge(JudgeResult.Miss);
            currentIndex++;
        }
    }

    private void HandleJudge(JudgeResult result)
    {
        switch (result)
        {
            case JudgeResult.Perfect:
                AddScore(300);
                combo++;
                Debug.Log("🎯 판정: Perfect");
                break;
            case JudgeResult.Good:
                AddScore(100);
                combo++;
                Debug.Log("👌 판정: Good");
                break;
            case JudgeResult.Miss:
                combo = 0;
                Debug.Log("❌ 판정: Miss");
                break;
        }

        // 콤보 UI, 이펙트 등 여기서 처리 가능
    }

    private void AddScore(int amount)
    {
        _totalScore += amount;
        OnScoreChanged?.Invoke(_totalScore);
        Debug.Log($"💯 점수 +{amount}, 현재 점수: {_totalScore}");
    }

    private float FindRollEndTime(int startIndex)
    {
        for (int i = startIndex + 1; i < notes.Count; i++)
        {
            if (notes[i].type == NoteType.YellowRollEnd)
            {
                return notes[i].time;
            }
        }

        return notes[startIndex].time + 2.0f; // fallback: 2초 연타
    }
}

public enum JudgeResult
{
    Perfect,
    Good,
    Miss
}