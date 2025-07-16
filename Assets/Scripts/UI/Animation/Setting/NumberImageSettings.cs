using UnityEngine;

[CreateAssetMenu(fileName = "NumberImageSettings", menuName = "UI/Number Image Settings")]
public class NumberImageSettings : ScriptableObject
{
    [Header("Animation Settings")] 
    public float sizeAmount;
    public float duration;
    public Vector2 baseDigitSize;
}
