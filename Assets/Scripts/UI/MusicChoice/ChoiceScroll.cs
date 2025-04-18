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
        private ChoiceData activeChoice;

        [FormerlySerializedAs("choiceList")] [SerializeField]
        private List<ChoiceData> choices = new();

        [SerializeField] private TestDrumInput testDrumInput;
        
        private ChoiceAnimationData _animData;

        private readonly float _moveDistance = 85f;
        private readonly float _activeDistance = 120f;
        private readonly float _moveDuration = 0.5f;
        private bool _isScrolling;
        
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
            public float ActiveDistance;
        }
        
        void Start()
        {
            activeChoice = choices[3];
        }
        
        void Update()
        {
            // 곡 선택
            activeChoice = choices[3];
            if (testDrumInput.testType == DrumDataType.RightFace)
                DoChoice();
            // 곡 아래로
            else if (testDrumInput.testType == DrumDataType.LeftFace)
                ScrollDown();
            // 곡 위로
            else if (testDrumInput.testType == DrumDataType.LeftSide)
                ScrollUp();
        }

        private void DoChoice()
        {
            if (activeChoice.ChoiceType == ChoiceType.Music1)
                dokoh.System.SceneManager.LoadScene(SceneDataType.Music1);
            else if (activeChoice.ChoiceType == ChoiceType.Music2)
                dokoh.System.SceneManager.LoadScene(SceneDataType.Music2);
            else if (activeChoice.ChoiceType == ChoiceType.Music3)
                dokoh.System.SceneManager.LoadScene(SceneDataType.Music3);
            else if (activeChoice.ChoiceType == ChoiceType.BackToMenu)
                dokoh.System.SceneManager.LoadScene(SceneDataType.Start);
            else if (activeChoice.ChoiceType == ChoiceType.RandomMusic)
            {
                int rand = Random.Range(0, 3);
                if (rand == 0)
                    dokoh.System.SceneManager.LoadScene(SceneDataType.Music1);
                else if (rand == 1)
                    dokoh.System.SceneManager.LoadScene(SceneDataType.Music2);
                else if (rand == 2)
                    dokoh.System.SceneManager.LoadScene(SceneDataType.Music3);
            }
                
        }
        private void ScrollDown()
        {
            if (_isScrolling)
                return;

            StopAllCoroutines();
            StartCoroutine(ScrollDownProcess());
        }

        private void ScrollUp()
        {
            if (_isScrolling)
                return;
            
            StopAllCoroutines();
            StartCoroutine(ScrollUpProcess());
        }

        private IEnumerator ScrollUpProcess()
        {
            _isScrolling = true;

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < choices.Count; i++)
            {
                if (i == 4)
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
                        BadgeSize = new Vector2(30, 30),
                        ActiveDistance = 120
                    };
                    ApplyAnimation(sequence, choices[i], data);
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
                        BadgeSize = new Vector2(20, 20),
                        ActiveDistance = 120
                    };
                    ApplyAnimation(sequence, choices[i], data);
                }
                else
                {
                    sequence.Join(choices[i].CardTrans
                        .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y + _moveDistance, _moveDuration)
                        .SetEase(Ease.InOutQuad));
                }
            }

            bool finished = false;
            sequence.OnComplete(() => finished = true);
            yield return new WaitUntil(() => finished);

            choices[5].ActiveFrame.SetActive(true);
            choices[4].ActiveFrame.SetActive(false);

            var bottomChoice = GetBottomChoice();
            var topChoice = GetTopChoice();
            
            float bottomChoiceRectY = bottomChoice.CardTrans.anchoredPosition.y - _moveDistance;
            
            topChoice.CardTrans.anchoredPosition =
                new Vector2(topChoice.CardTrans.anchoredPosition.x, bottomChoiceRectY);
            
            choices.Remove(topChoice);
            choices.Add(topChoice);

            choices[6].Text.text = choices[1].Text.text;
            choices[6].ChoiceType = choices[1].ChoiceType;
            _isScrolling = false;
        }
        
        private IEnumerator ScrollDownProcess()
        {
            _isScrolling = true;

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
                        BadgeSize = new Vector2(30, 30),
                        ActiveDistance = -120
                    };
                    ApplyAnimation(sequence, choices[i], data);
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
                        BadgeSize = new Vector2(20, 20),
                        ActiveDistance = -120
                    };
                    ApplyAnimation(sequence, choices[i], data);
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
            _isScrolling = false;
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
                .DOAnchorPosY(choice.CardTrans.anchoredPosition.y + data.ActiveDistance, _moveDuration)
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
