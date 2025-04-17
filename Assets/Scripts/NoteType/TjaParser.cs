using System;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    SmallRed,
    SmallBlue,
    BigRed,
    BigBlue,
    YellowRollStart,
    YellowRollEnd
}

public enum HitPosition
{
    LeftFace,
    RightFace,
    LeftSide,
    Rightside,
    NotPlayed
}

[Serializable]
public class TjaNote
{
    public float time;
    public NoteType type;
    public List<HitPosition> requiredhits = new();
    public bool judged = false;
    public HitPosition requiredHit;
}

public class TjaParser : MonoBehaviour
{
    public TextAsset tjaFile;
    public List<TjaNote> notes = new List<TjaNote>();

    public float bpm = 120f;
    public float offset = 0f;

    private float totalTime = 0f;

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
            else if (line.StartsWith("OFFSET:"))
            {
                if (float.TryParse(line.Substring(7), out float parsedOffset))
                {
                    offset = parsedOffset;
                    totalTime = offset;
                }
            }
            else if (line.StartsWith("#START"))
            {
                isParsing = true;
                continue;
            }
            else if (line.StartsWith("#END"))
            {
                isParsing = false;
                break;
            }

            if (!isParsing || string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue;

            // 한 마디 단위 처리
            float barDuration = (4f * 60f) / bpm;
            int noteCount = line.Length;
            float noteInterval = barDuration / noteCount;

            for (int i = 0; i < noteCount; i++)
            {
                char c = line[i];
                NoteType? type = CharToNoteType(c);

                if (type.HasValue)
                {
                    notes.Add(new TjaNote
                    {
                        time = totalTime + (i * noteInterval),
                        type = type.Value
                    });
                }
            }

            totalTime += barDuration;
        }

        Debug.Log($"총 {notes.Count}개의 노트가 로드되었습니다.");
    }

    NoteType? CharToNoteType(char c)
    {
        return c switch
        {
            '1' => NoteType.SmallRed,
            '2' => NoteType.SmallBlue,
            '3' => NoteType.BigRed,
            '4' => NoteType.BigBlue,
            '5' => NoteType.YellowRollStart,
            '8' => NoteType.YellowRollEnd,
            _   => null
        };
    }
}