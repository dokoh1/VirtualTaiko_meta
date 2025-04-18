using System.Collections.Generic;
using UnityEngine;

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
            float left = TimingRect[i].position.x - TimingRect[i].rect.width / 2f;
            float right = TimingRect[i].position.x + TimingRect[i].rect.width / 2f;
            timingBoxs[i] = new Vector2(left, right);
        }
    }


    public void CheckTiming()
    {
        for (int i = 0; i < BoxNoteList.Count; i++)
        {
            float t_notePosX = BoxNoteList[i].transform.position.x;

            for (int x = 0; x < TimingRect.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    Debug.Log("Hit " + x);
                    return;
                }
            }
        }
        Debug.Log("Miss");
    }
}