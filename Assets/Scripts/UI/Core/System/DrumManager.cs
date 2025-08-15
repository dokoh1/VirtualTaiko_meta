
using System.Collections.Generic;
using UnityEngine;

public class DrumManager : MonoBehaviour
{
    public Queue<DrumDataType> dataSet = new();

    public void AddQueue(DrumDataType data)
    {
        dataSet.Enqueue(data);
    }

    public DrumDataType UseQueue()
    {
        if (dataSet.Count > 0)
            return dataSet.Dequeue();
        else
            return DrumDataType.NotHit;
    }
}
