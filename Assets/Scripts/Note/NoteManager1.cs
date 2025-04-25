using System;
using UnityEngine;

public enum NoteType
{
    smallRed,
    smallBlue,
    bigRed,
    bigBlue,
}

public class NoteManager1 : MonoBehaviour
{
    public int bpm;
    double currentTime = 0d;
    public Transform noteAppearLocation;
    public GameObject SmallDon;
    public GameObject BigDon;
    public GameObject SmallKa;
    public GameObject BigKa;
    TimingManager timingManager;

    private void Start()
    {
        timingManager = GetComponent<TimingManager>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject noteToSpawn = null;

            // 노트 타입에 맞는 프리팹을 할당
            switch (noteType)
            {
                case NoteType.smallRed:
                    noteToSpawn = SmallDon; // 작은 빨간 원
                    break;
                case NoteType.smallBlue:
                    noteToSpawn = SmallKa; // 작은 파란 원
                    break;
                case NoteType.bigRed:
                    noteToSpawn = BigDon; // 큰 빨간 원
                    break;
                case NoteType.bigBlue:
                    noteToSpawn = BigKa; // 큰 파란 원
                    break;
            }

            // 프리팹을 인스턴스화
            if (noteToSpawn != null)
            {
                GameObject t_note = Instantiate(noteToSpawn, noteAppearLocation.position, Quaternion.identity);
                t_note.transform.SetParent(noteAppearLocation);
                timingManager.BoxNoteList.Add(t_note);
            }

            currentTime -= 60d / bpm;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            timingManager.MissNote(other.gameObject);
        }
    }

    public NoteType noteType;

    public void CheckHit(KeyCode hitKey)
    {
        bool isHit = false;
        switch (noteType)
        {
            case NoteType.smallRed:
                if (hitKey == KeyCode.S && hitKey == KeyCode.K) // 왼쪽 면 타격
                {
                    isHit = true;
                }

                break;

            case NoteType.smallBlue:
                if (hitKey == KeyCode.L && hitKey == KeyCode.A) // 오른쪽 사이드 타격
                {
                    isHit = true;
                }

                break;

            case NoteType.bigRed:
                if (hitKey == KeyCode.S || hitKey == KeyCode.K) // 양쪽 안쪽 면 타격
                {
                    isHit = true;
                }

                break;

            case NoteType.bigBlue:
                if (hitKey == KeyCode.A || hitKey == KeyCode.L) // 양쪽 사이드 타격
                {
                    isHit = true;
                }

                break;

        }

        if (isHit)
        {
            HitResult result = timingManager.CheckTiming();
        }
        else
        {
            HitResult result = HitResult.Bad;
        }
    }
}
