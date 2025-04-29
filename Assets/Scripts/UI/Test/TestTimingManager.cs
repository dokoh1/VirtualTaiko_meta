using System.Collections.Generic;
using UnityEngine;

public enum TestEnum
{
    PerfectSmall,
    PerfectBig,
    GoodBig,
    GoodSmall,
    Bad,
    None
}
public class TestTimingManager : MonoBehaviour
{
    public Queue<TestEnum> HitQueue = new();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HitQueue.Enqueue(TestEnum.PerfectSmall);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            HitQueue.Enqueue(TestEnum.PerfectBig);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            HitQueue.Enqueue(TestEnum.GoodBig);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            HitQueue.Enqueue(TestEnum.GoodSmall);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            HitQueue.Enqueue(TestEnum.Bad);
        }
    }
}
