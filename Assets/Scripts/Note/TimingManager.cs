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
    public List<GameObject>BoxNoteList = new List<GameObject>();
    public Transform Center;
    public RectTransform[] TimingRect;
    Vector2[] timingBoxs;
    void Start()
    {
        timingBoxs = new Vector2[TimingRect.Length];

        for (int i = 0; i < TimingRect.Length; i++)
        {
            float left = TimingRect[i].localPosition.x - TimingRect[i].rect.width / 2f;
            float right = TimingRect[i].localPosition.x + TimingRect[i].rect.width / 2f;

            timingBoxs[i] = new Vector2(left, right);
        }
    }
    public HitResult CheckTiming()
    {
        if (BoxNoteList.Count == 0) return HitResult.Miss;

        GameObject note = BoxNoteList[0];
        float noteX = note.transform.localPosition.x;

        // Perfect
        float perfectLeft = TimingRect[2].localPosition.x - TimingRect[2].rect.width / 2f;
        float perfectRight = TimingRect[2].localPosition.x + TimingRect[2].rect.width / 2f;

        // Good
        float goodLeft = TimingRect[1].localPosition.x - TimingRect[1].rect.width / 2f;
        float goodRight = TimingRect[3].localPosition.x + TimingRect[3].rect.width / 2f;

        // Bad
        float badLeft = TimingRect[0].localPosition.x - TimingRect[0].rect.width / 2f;
        float badRight = TimingRect[4].localPosition.x + TimingRect[4].rect.width / 2f;

        HitResult result = HitResult.Miss;

        if (noteX >= perfectLeft && noteX <= perfectRight)
        {
            result = HitResult.Perfect;
        }
        else if (noteX >= goodLeft && noteX <= goodRight)
        {
            result = HitResult.Good;
        }
        else if (noteX >= badLeft && noteX <= badRight)
        {
            result = HitResult.Bad;
        }

        Debug.Log(result);

        if (result != HitResult.Miss)
        {
            Destroy(note);
            BoxNoteList.RemoveAt(0);
        }

        return result;
    }
}