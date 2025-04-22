using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject smallRedPrefab;
    public GameObject smallBluePrefab;
    public GameObject bigRedPrefab;
    public GameObject bigBluePrefab;
    public GameObject rollStartPrefab;

    public Transform spawnPoint;
    public float scrollSpeed = 5f;
    public float spawnAheadTime = 2.0f;

    private List<TjaNote> noteList;
    private int nextIndex = 0;
    private float musicStartTime;
    private bool started = false;

    public void Initialize(List<TjaNote> notes)
    {
        noteList = notes;
    }

    public void StartMusic()
    {
        musicStartTime = Time.time;
        started = true;
    }

    void Update()
    {
        if (!started || noteList == null || nextIndex >= noteList.Count)
            return;

        float elapsed = Time.time - musicStartTime;

        while (nextIndex < noteList.Count && noteList[nextIndex].time <= elapsed + spawnAheadTime)
        {
            SpawnNote(noteList[nextIndex]);
            nextIndex++;
        }
    }

    void SpawnNote(TjaNote noteData)
    {
        GameObject prefab = GetPrefab(noteData.type);
        if (prefab == null) return;

        GameObject note = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        note.GetComponent<NoteMovement>().Initialize(noteData.time, musicStartTime, scrollSpeed);
    }

    GameObject GetPrefab(NoteType type)
    {
        return type switch
        {
            NoteType.SmallRed => smallRedPrefab,
            NoteType.SmallBlue => smallBluePrefab,
            NoteType.BigRed => bigRedPrefab,
            NoteType.BigBlue => bigBluePrefab,
            NoteType.YellowRollStart => rollStartPrefab,
            _ => null
        };
    }
}
