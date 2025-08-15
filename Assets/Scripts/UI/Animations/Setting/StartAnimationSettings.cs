using UnityEngine;

[CreateAssetMenu(fileName = "StartAnimationSettings", menuName = "UI/Start Animation Settings")]
public class StartAnimationSettings : ScriptableObject
{
    [Header("General Animation Settings")] 
    public float moveTime;
    public float slowMoveTime;

    [Header("Light & Guide Fade Settings")]
    public float minFade;
    public float maxFade;

    [Header("Object Move Settings")] 
    public float objectMoveAmount;

    [Header("Element Animation Settings (Crystals, Flowers, Leafs, YutStick, Bell")]
    public Vector2 initPosition;
    public Vector2 initSize;
    public Vector2 elementSize;
    public float elementMoveAmount;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
}
