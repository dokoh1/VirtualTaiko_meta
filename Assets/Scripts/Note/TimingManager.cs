using System.Collections.Generic;
using UnityEngine;

public enum HitResult
{
    Perfect,
    Good,
    Bad,
    Miss
}

public class TimingManager : MonoBehaviour
{
    public List<GameObject> BoxNoteList = new List<GameObject>();
    public Transform Center;

    [Header("판정 거리 기준 (중심 기준 거리)")]
    public float perfectRange = 20f;
    public float goodRange = 40f;
    public float badRange = 60f;

    public HitResult CheckTiming()
    {
        if (BoxNoteList.Count == 0) return HitResult.Miss;

        GameObject note = BoxNoteList[0];
        float noteX = note.transform.position.x; // ⬅ 월드 좌표
        float centerX = Center.position.x;        // ⬅ 월드 좌표
        float distance = Mathf.Abs(noteX - centerX);

        HitResult result;
        if (distance <= perfectRange) result = HitResult.Perfect;
        else if (distance <= goodRange) result = HitResult.Good;
        else if (distance <= badRange) result = HitResult.Bad;
        else result = HitResult.Miss;

        Debug.Log($"판정: {result} (Distance: {distance:F1})");

        if (result != HitResult.Miss)
        {
            Destroy(note);
            BoxNoteList.RemoveAt(0);
        }
        // else
        // {
        //     Destroy(note);
        //     BoxNoteList.RemoveAt(0);
        // }

        return result;
    }

}