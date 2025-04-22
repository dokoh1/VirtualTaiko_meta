using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TimingManager timingManager;
    // public Drums drums;
    public void Start()
    {
        timingManager = FindFirstObjectByType<TimingManager>();
        StartCoroutine(HitUpdate());
    }

    IEnumerator HitUpdate()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HitResult result = timingManager.CheckTiming();
                
                // switch (result)
                // {
                //     case HitResult.Perfect:
                //         Debug.Log("Perfect Hit!");
                //         break;
                //     case HitResult.Good:
                //         Debug.Log("Good Hit!");
                //         break;
                //     case HitResult.Bad:
                //         Debug.Log("Bad Hit!");
                //         break;
                //     case HitResult.Miss:
                //         Debug.Log("Miss...");
                //         break;
                // }
                
                yield return new WaitForSeconds(0.001f);
            }
            else
            {
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}
    // void Update()
    // {
    //     if (drums.dataSet == DrumDataType.RightFace)
    //     {
    //         HitResult result = timingManager.CheckTiming();
    //
    //         switch (result)
    //         {
    //             case HitResult.Perfect:
    //                 Debug.Log("Perfect Hit!");
    //                 break;
    //             case HitResult.Good:
    //                 Debug.Log("Good Hit!");
    //                 break;
    //             case HitResult.Bad:
    //                 Debug.Log("Bad Hit!");
    //                 break;
    //             case HitResult.Miss:
    //                 Debug.Log("Miss...");
    //                 break;
    //         }
    //     }
    // }
