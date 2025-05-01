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
    public NoteMap noteMap;
    
    public GameObject SmallDon;
    public GameObject BigDon;
    public GameObject SmallKa;
    public GameObject BigKa;

    private GameObject currentNote;
    public Transform noteAppearLocation;
    private TimingManager timingManager;
    public int bpm;
    public AudioClip PlayMusic;
    double currentTime = 0d;
    
    private float startTime;
    public float noteSpawnOffset;

    private void Start()
    {
        timingManager = GetComponent<TimingManager>();
        dokoh.System.AudioManager.PlayBGMOne(PlayMusic);
        // music.Play();
        startTime = Time.time;
    }

    void Update()
    {
        if (noteMap == null || noteMap.notes == null)
            return;

        if (timingManager.BoxNoteList.Count >= noteMap.notes.Count)
            return;

        int noteIndex = timingManager.BoxNoteList.Count;
        Noteinfo nextNote = noteMap.notes[noteIndex];

        if (dokoh.System.AudioManager.bgmSource.time + noteSpawnOffset  >= nextNote.spawntime)
        {
            SpawnNote();
        }
    }


    private int nextNoteIndex = 0;

    private void SpawnNote()
    {
        if (noteMap == null || noteMap.notes == null || nextNoteIndex >= noteMap.notes.Count)
            return;

        float currentTime = Time.time - startTime; // 음악 시작 후 흐른 시간

        // 노트가 나타나야 할 시점인지 확인
        while (nextNoteIndex < noteMap.notes.Count && noteMap.notes[nextNoteIndex].spawntime <= currentTime + noteSpawnOffset)
        {
            Noteinfo noteInfo = noteMap.notes[nextNoteIndex];
            GameObject prefab = null;

            switch (noteInfo.notetype)
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
                GameObject t_note = Instantiate(prefab, noteAppearLocation);

                NoteData noteData = t_note.AddComponent<NoteData>();
                noteData.noteType = noteInfo.notetype;

                timingManager.BoxNoteList.Add(t_note);
            }

            nextNoteIndex++;
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
                if (hitKey == KeyCode.S || hitKey == KeyCode.K) // 왼쪽 면 타격
                {
                    isHit = true;
                }

                break;

            case NoteType.smallBlue:
                if (hitKey == KeyCode.L || hitKey == KeyCode.A) // 오른쪽 사이드 타격
                {
                    isHit = true;
                }

                break;

            case NoteType.bigRed:
                if (hitKey == KeyCode.S && hitKey == KeyCode.K) // 양쪽 안쪽 면 타격
                {
                    isHit = true;
                }

                break;

            case NoteType.bigBlue:
                if (hitKey == KeyCode.A && hitKey == KeyCode.L) // 양쪽 사이드 타격
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