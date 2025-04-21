using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TimingManager timingManager;
    public Drums drums;
    public void Start()
    {
        timingManager = FindFirstObjectByType<TimingManager>();
    }

    void Update()
    {
        if (drums.dataSet == DrumDataType.RightFace)
        {
            HitResult result = timingManager.CheckTiming();

            switch (result)
            {
                case HitResult.Perfect:
                    Debug.Log("Perfect Hit!");
                    break;
                case HitResult.Good:
                    Debug.Log("Good Hit!");
                    break;
                case HitResult.Bad:
                    Debug.Log("Bad Hit!");
                    break;
                case HitResult.Miss:
                    Debug.Log("Miss...");
                    break;
            }
        }
    }
}
