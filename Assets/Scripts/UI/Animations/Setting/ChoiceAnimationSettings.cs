using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceAnimationSettings", menuName = "UI/Choice Animation Settings")]
public class ChoiceAnimationSettings : ScriptableObject
{
    [Header("Scroll Settings")] 
    public float moveDistance;
    public float moveDuration;
    public float arrowDistance;
    
    [Header("Active Image Fade Settings")]
    public float activeImageFadeDuration;
    public float activeImageMinFade;
    public float activeImageMaxFade;

    [Header("Active Card Data")] 
    [SerializeField]
    public ChoiceCardSettings activeCard;
    
    [Header("Not Active Card Data")] 
    [SerializeField]
    public ChoiceCardSettings notActiveCard;
}