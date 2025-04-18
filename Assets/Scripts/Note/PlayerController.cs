using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TimingManager timingManager;
    public void Start()
    {
         timingManager = FindObjectOfType<TimingManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timingManager.CheckTiming();
        }
    }
}
