using System.Collections.Generic;
using UnityEngine;

public enum HitResult
{
    BigPerfect,
    SmallPerfect,
    BigGood,
    SmallGood,
    Bad,
    None,
}

public class TimingManager : MonoBehaviour
{
    public List<GameObject> BoxNoteList = new List<GameObject>();
    public Transform Center;
    public Queue<HitResult> HitQueue = new Queue<HitResult>();
    
    [Header("판정 거리 기준 (중심 기준 거리)")]
    private float perfectRange = 0.35f;
    private float goodRange = 0.7f;
    private float badRange = 1.05f;

    // ReSharper disable Unity.PerformanceAnalysis
    public HitResult CheckTiming()
    {
        if (BoxNoteList.Count == 0) return HitResult.Bad;

        GameObject closestNote = null;
        float closestDistance = float.MaxValue;
        float centerX = Center.position.x;
    
        foreach (var note in BoxNoteList)
        {
            float noteX = note.transform.position.x;
            float distance = Mathf.Abs(noteX - centerX);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNote = note;
            }
        }

        if (closestNote == null) return HitResult.Bad;

        float noteXPos = closestNote.transform.position.x;
        float distanceFromCenter = Mathf.Abs(noteXPos - centerX);

        NoteData noteData = closestNote.GetComponent<NoteData>();

        bool isBig = noteData.noteType == NoteType.bigRed || noteData.noteType == NoteType.bigBlue;

        HitResult result;

        if (distanceFromCenter <= perfectRange)
            result = isBig ? HitResult.BigPerfect : HitResult.SmallPerfect;
        else if (distanceFromCenter <= goodRange)
            result = isBig ? HitResult.BigGood : HitResult.SmallGood;
        else if (distanceFromCenter <= badRange)
            result = HitResult.Bad;
        else
        {
            result = HitResult.None;
            return result;
        }

        HitQueue.Enqueue(result);
        BoxNoteList.Remove(closestNote);
        Destroy(closestNote);
        Debug.Log(result);

        return result;
    }


    public void MissNote(GameObject note)
    {
        if (BoxNoteList.Contains(note))
        {
            HitResult result = HitResult.Bad;
            HitQueue.Enqueue(result);
            BoxNoteList.Remove(note);
            Destroy(note);
            Debug.Log(result);
        }
    }
    public void ProcessResult(HitResult result)
    {
        bool isCheck = false;
        // 예시 처리
        switch (result)
        {
            case HitResult.SmallPerfect:
                isCheck = true;
                break;
            case HitResult.BigPerfect:    
                isCheck = true;
                break;
            case HitResult.BigGood:
                isCheck = true;
                break;
            case HitResult.SmallGood:
                isCheck = true;
                break;
            case HitResult.Bad:
                isCheck = true;
                break;
            case HitResult.None:
                isCheck = true;
                break;
        }
        Debug.Log(result);
    }
}
