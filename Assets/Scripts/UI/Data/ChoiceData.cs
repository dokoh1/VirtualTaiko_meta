using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class ChoiceData
{
    public ChoiceType ChoiceType;
    [Header("Transform")]
    public RectTransform CardTrans;
    public RectTransform TopTrans;
    public RectTransform CenterTrans;
    public RectTransform BottomTrans;
    public RectTransform InTrans;
    public RectTransform CrownTrans;
    public RectTransform BadgeTrans;
    
    [Header("Display Elements")]
    public GameObject ActiveFrame;
    public Text Text;
    public Image[] ActiveImages;
    
    [Header("Arrow Elements")]
    public ArrowData ArrowData;
}
