
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
        return dataSet.Dequeue();
    }
}
