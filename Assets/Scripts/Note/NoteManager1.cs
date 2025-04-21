using System;
using UnityEngine;

public class NoteManager1 : MonoBehaviour
{
    public int bpm;
    double currentTime = 0d;
    public Transform noteAppearLocation;
    public GameObject notePrefab;
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
            GameObject t_note = Instantiate(notePrefab, noteAppearLocation.position, Quaternion.identity);
            t_note.transform.SetParent(noteAppearLocation);
            timingManager.BoxNoteList.Add(t_note);
            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            timingManager.BoxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
