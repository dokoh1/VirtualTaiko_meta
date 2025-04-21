using System.Collections.Generic;
using JetBrains.Annotations;
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
    private float perfectRange = 0.5f;
    private float goodRange = 1f;
    private float badRange = 3f;

    public HitResult CheckTiming()
    {
        if (BoxNoteList.Count == 0) return HitResult.Miss;

        GameObject note = BoxNoteList[0];
        float noteX = note.transform.position.x; // ⬅ 월드 좌표
        float centerX = Center.position.x;        // ⬅ 월드 좌표
        float distance = Mathf.Abs(noteX - centerX);
        // Debug.Log("distance " + distance);
        HitResult result;
        if (distance <= perfectRange) 
        {   
            result = HitResult.Perfect;
        }
        else if (distance <= goodRange) 
        {

            result = HitResult.Good;
        }
        
        else if (distance <= badRange)
        {
            result = HitResult.Bad;
        } 
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