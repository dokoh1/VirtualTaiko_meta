using System;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    SmallRed,
    SmallBlue,
    BigRed,
    BigBlue,
    None
}

public enum HitPosition
{
    LeftInside,
    LeftSide,
    RightInside,
    Rightside,
}

[Serializable]
public class TjaNote
{
    public float time;
    public NoteType type;
    public bool judged;
    public HitPosition requiredHit;
}

public class TjaParser : MonoBehaviour
{
    public TextAsset tjaFile; // Inspector에서 넣는 .txt 또는 .tja

    public List<TjaNote> notes = new List<TjaNote>();

    private float bpm = 120f;
    private float offset = 0f;

    void Start()
    {
        if (tjaFile != null)
        {
            ParseTja(tjaFile.text);
        }
        else
        {
            Debug.LogError("TJA 파일이 비어있습니다.");
        }
    }

    void ParseTja(string text)
    {
        string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        bool isParsing = false;
        int noteIndex = 0;

        foreach (var rawLine in lines)
        {
            string line = rawLine.Trim();

            if (line.StartsWith("BPM:"))
            {
                if (float.TryParse(line.Substring(4), out float parsedBpm))
                {
                    bpm = parsedBpm;
                }
            }

            if (line.StartsWith("OFFSET:"))
            {
                if (float.TryParse(line.Substring(7), out float parsedOffset))
                {
                    offset = parsedOffset;
                }
            }

            if (line.StartsWith("#START"))
            {
                isParsing = true;
                noteIndex = 0;
                continue;
            }

            if (line.StartsWith("#END"))
            {
                isParsing = false;
                break;
            }

            if (!isParsing || string.IsNullOrEmpty(line))
                continue;

            foreach (char c in line)
            {
                float beatDuration = 60f / bpm;
                float noteTime = offset + noteIndex * beatDuration;

                NoteType noteType = CharToNoteType(c);

                if (noteType != NoteType.None)
                {
                    notes.Add(new TjaNote
                    {
                        time = noteTime,
                        type = noteType
                    });
                }

                noteIndex++;
            }
        }

        Debug.Log($"총 {notes.Count}개의 노트가 로드되었습니다.");
    }

    NoteType CharToNoteType(char c)
    {
        return c switch
        {
            '1' => NoteType.SmallRed,
            '2' => NoteType.SmallBlue,
            '3' => NoteType.BigRed,
            '4' => NoteType.BigBlue,
            _   => NoteType.None
        };
    }
    
}
