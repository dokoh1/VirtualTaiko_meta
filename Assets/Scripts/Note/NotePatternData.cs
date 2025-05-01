[System.Serializable]
public class NoteInfo
{
    public float spawnTime;
    public NoteType noteType;

    public NoteInfo(float spawnTime, NoteType noteType)
    {
        this.spawnTime = spawnTime;
        this.noteType = noteType;
    }
}