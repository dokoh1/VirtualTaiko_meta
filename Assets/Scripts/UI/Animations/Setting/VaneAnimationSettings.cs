using UnityEngine;

[CreateAssetMenu(fileName = "VaneAnimationSettings", menuName = "UI/Vane Animation Settings")]
public class VaneAnimationSettings : ScriptableObject
{
    [Header("Rotation Settings")] 
    public float rotationDuration;
    public Vector3 roationAngle;

    [Header("Movement Settings")] 
    public Vector2 moveOffset;
    public float moveDuration;
    public float moveAppendInterval;
}
