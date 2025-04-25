using System;
using UnityEngine;
using UnityEngine.UI;

namespace dokoh
{
    [Serializable]
    public class ChoiceData
    {
        public ChoiceType ChoiceType;
        public RectTransform CardTrans;
        public RectTransform TopTrans;
        public RectTransform CenterTrans;
        public RectTransform BottomTrans;
        public RectTransform InTrans;
        public RectTransform CrownTrans;
        public RectTransform BadgeTrans;
        public GameObject ActiveFrame;
        public Text Text;
        public Image[] ActiveImages;
    }
}