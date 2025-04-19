using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Serialization;

namespace dokoh
{
    public class ChoiceScroll : MonoBehaviour
    {
        public GameObject activeChoice;

        [FormerlySerializedAs("choiceList")] [SerializeField]
        private List<ChoiceData> choices = new();

        [SerializeField] private TestDrumInput testDrumInput;
        
        private ChoiceAnimationData _animData;

        private float _moveDistance = 85f;
        private float _ActiveDistance = 120f;
        private float _moveDuration = 0.5f;
        private bool isScrolling = false;
        
        private struct ChoiceAnimationData
        {
            public float CenterHeight;
            public float TopPosY;
            public float TopHeight;
            public float BottomHeight;
            public float BottomPosY;
            public float InHeight;
            public Vector2 CrownSize;
            public Vector2 BadgeSize;
        }

        void Start()
        {

        }

        void Update()
        {
            // 곡 선택
            if (testDrumInput.testType == DrumDataType.RightFace)
            {

            }
            // 곡 아래로
            else if (testDrumInput.testType == DrumDataType.LeftFace)
            {
                ScrollDown();
            }
            // 곡 위로
            else if (testDrumInput.testType == DrumDataType.LeftSide)
            {

            }
        }

        private void ScrollDown()
        {
            if (isScrolling)
                return;

            StopAllCoroutines();
            StartCoroutine(Process());
        }

        private IEnumerator Process()
        {
            isScrolling = true;

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < choices.Count; i++)
            {
                if (i == 2)
                {
                    var data = new ChoiceAnimationData
                    {
                        CenterHeight = 100,
                        TopPosY = +20,
                        TopHeight = 20,
                        BottomHeight = 20,
                        BottomPosY = -20,
                        InHeight = 110,
                        CrownSize = new Vector2(30, 30),
                        BadgeSize = new Vector2(30, 30)
                    };
                    ApplyAnimation(sequence, choices[i], data);
                    // //Center
                    // sequence.Join(choices[i].CenterTrans
                    //     .DOSizeDelta(
                    //         new Vector2(choices[i].CenterTrans.sizeDelta.x, choices[i].CenterTrans.sizeDelta.y + 30),
                    //         _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // //Top
                    // sequence.Join(choices[i].TopTrans
                    //     .DOSizeDelta(new Vector2(choices[i].TopTrans.sizeDelta.x, choices[i].TopTrans.sizeDelta.y + 10),
                    //         _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // sequence.Join(choices[i].TopTrans
                    //     .DOAnchorPosY(choices[i].TopTrans.anchoredPosition.y + 20, _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    //
                    // //Bottom
                    // sequence.Join(choices[i].BottomTrans
                    //     .DOSizeDelta(new Vector2(choices[i].BottomTrans.sizeDelta.x, 20), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // sequence.Join(choices[i].BottomTrans
                    //     .DOAnchorPosY(choices[i].BottomTrans.anchoredPosition.y - 20, _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // //In
                    // sequence.Join(choices[i].InTrans
                    //     .DOSizeDelta(new Vector2(choices[i].InTrans.sizeDelta.x, 110), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // //Position
                    // sequence.Join(choices[i].CardTrans
                    //     .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y - _ActiveDistance, _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // //Decoration
                    // sequence.Join(choices[i].CrownTrans
                    //     .DOSizeDelta(new Vector2(30, 30), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // sequence.Join(choices[i].BadgeTrans
                    //     .DOSizeDelta(new Vector2(30, 30), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));

                }
                else if (i == 3)
                {
                    var data = new ChoiceAnimationData
                    {
                        CenterHeight = 70,
                        TopPosY = -20,
                        TopHeight = 10,
                        BottomHeight = 10,
                        BottomPosY = +20,
                        InHeight = 60,
                        CrownSize = new Vector2(20, 20),
                        BadgeSize = new Vector2(20, 20)
                    };
                    ApplyAnimation(sequence, choices[i], data);
                    //Center
                    // sequence.Join(choices[i].CenterTrans
                    //     .DOSizeDelta(new Vector2(choices[i].CenterTrans.sizeDelta.x, 70), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    //
                    // //Top
                    // sequence.Join(choices[i].TopTrans
                    //     .DOSizeDelta(new Vector2(choices[i].TopTrans.sizeDelta.x, 10), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // sequence.Join(choices[i].TopTrans
                    //     .DOAnchorPosY(choices[i].TopTrans.anchoredPosition.y - 20, _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    //
                    // //Bottom
                    // sequence.Join(choices[i].BottomTrans
                    //     .DOSizeDelta(new Vector2(choices[i].BottomTrans.sizeDelta.x, 10), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // sequence.Join(choices[i].BottomTrans
                    //     .DOAnchorPosY(choices[i].BottomTrans.anchoredPosition.y + 20, _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // //In
                    // sequence.Join(choices[i].InTrans
                    //     .DOSizeDelta(new Vector2(choices[i].InTrans.sizeDelta.x, 60), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // //Position
                    // sequence.Join(choices[i].CardTrans
                    //     .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y - _ActiveDistance, _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // //Decoration
                    // sequence.Join(choices[i].CrownTrans
                    //     .DOSizeDelta(new Vector2(20, 20), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                    // sequence.Join(choices[i].BadgeTrans
                    //     .DOSizeDelta(new Vector2(20, 20), _moveDuration)
                    //     .SetEase(Ease.InOutQuad));
                }
                else
                {
                    sequence.Join(choices[i].CardTrans
                        .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y - _moveDistance, _moveDuration)
                        .SetEase(Ease.InOutQuad));
                }
            }

            bool finished = false;
            sequence.OnComplete(() => finished = true);
            yield return new WaitUntil(() => finished);

            choices[3].ActiveFrame.SetActive(true);
            choices[4].ActiveFrame.SetActive(false);

            var bottomChoice = GetBottomChoice();
            var topChoice = GetTopChoice();
            float topChoiceRectY = topChoice.CardTrans.anchoredPosition.y + _moveDistance;
            bottomChoice.CardTrans.anchoredPosition =
                new Vector2(bottomChoice.CardTrans.anchoredPosition.x, topChoiceRectY);
            choices.Remove(bottomChoice);
            choices.Insert(0, bottomChoice);

            choices[0].Text.text = choices[5].Text.text;
            choices[0].ChoiceType = choices[5].ChoiceType;
            isScrolling = false;
        }
        
        private void ApplyAnimation(Sequence sequence, ChoiceData choice, ChoiceAnimationData data)
        {
            sequence.Join(choice.CenterTrans
                .DOSizeDelta(new Vector2(choice.CenterTrans.sizeDelta.x, data.CenterHeight), _moveDuration)
                .SetEase(Ease.InOutQuad));

            sequence.Join(choice.TopTrans
                .DOAnchorPosY(choice.TopTrans.anchoredPosition.y + data.TopPosY, _moveDuration)
                .SetEase(Ease.InOutQuad));
            sequence.Join(choice.TopTrans
                .DOSizeDelta(new Vector2(choice.TopTrans.sizeDelta.x, data.TopHeight), _moveDuration)
                .SetEase(Ease.InOutQuad));

            sequence.Join(choice.BottomTrans
                .DOSizeDelta(new Vector2(choice.BottomTrans.sizeDelta.x, data.BottomHeight), _moveDuration)
                .SetEase(Ease.InOutQuad));
            sequence.Join(choice.BottomTrans
                .DOAnchorPosY(choice.BottomTrans.anchoredPosition.y + data.BottomPosY, _moveDuration)
                .SetEase(Ease.InOutQuad));

            sequence.Join(choice.InTrans
                .DOSizeDelta(new Vector2(choice.InTrans.sizeDelta.x, data.InHeight), _moveDuration)
                .SetEase(Ease.InOutQuad));

            sequence.Join(choice.CardTrans
                .DOAnchorPosY(choice.CardTrans.anchoredPosition.y - _ActiveDistance, _moveDuration)
                .SetEase(Ease.InOutQuad));

            sequence.Join(choice.CrownTrans
                .DOSizeDelta(data.CrownSize, _moveDuration)
                .SetEase(Ease.InOutQuad));
            sequence.Join(choice.BadgeTrans
                .DOSizeDelta(data.BadgeSize, _moveDuration)
                .SetEase(Ease.InOutQuad));
        }

        private ChoiceData GetTopChoice()
        {
            ChoiceData top = choices[0];
            
            foreach (var choice in choices)
            {
                if (choice.CardTrans.anchoredPosition.y > top.CardTrans.anchoredPosition.y)
                {
                    top = choice;
                }
            }
            return top;
        }

        private ChoiceData GetBottomChoice()
        {
            ChoiceData bottom = choices[0];
            foreach (var choice in choices)
            {
                if (choice.CardTrans.anchoredPosition.y < bottom.CardTrans.anchoredPosition.y)
                {
                    bottom = choice;
                }
            }
            return bottom;
        }
    }
}
