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
    public TimingManager timingManager;

    private void Start()
    {
        timingManager = GetComponent<TimingManager>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            SpawnNote();
            currentTime -= 60d / bpm;
        }
    }

    private void SpawnNote()
    {
        int random = UnityEngine.Random.Range(0, 4);
        GameObject prefab = null;

        switch ((NoteType)random)
        {
            case NoteType.smallRed:
                prefab = SmallDon;
                break;
            case NoteType.smallBlue:
                prefab = SmallKa;
                break;
            case NoteType.bigRed:
                prefab = BigDon;
                break;
            case NoteType.bigBlue:
                prefab = BigKa;
                break;
        }

        if (prefab != null)
        {
            GameObject t_note = Instantiate(prefab, noteAppearLocation.position, Quaternion.identity);
            t_note.transform.SetParent(noteAppearLocation);

            // 여기서 NoteType 저장
            NoteData noteData = t_note.AddComponent<NoteData>();
            noteData.noteType = (NoteType)random;

            timingManager.BoxNoteList.Add(t_note);
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
        Debug.Log("CheckHit called with key: " + hitKey);
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