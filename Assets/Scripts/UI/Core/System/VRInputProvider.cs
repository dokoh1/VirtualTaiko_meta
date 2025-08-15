using UnityEngine;

public class VRInputProvider : MonoBehaviour, IInputProvider
{
    public DrumDataType GetDrumInput()
    {
        return Single.System.DrumManager.UseQueue();
    }
}
