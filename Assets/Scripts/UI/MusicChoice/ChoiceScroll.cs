using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace dokoh
{
    public class ChoiceScroll : MonoBehaviour
    {
        private ChoiceData activeChoice;

        [FormerlySerializedAs("choiceList")] [SerializeField]
        private List<ChoiceData> choices = new();

        [SerializeField] private TestDrumInput testDrumInput;
        public Drums drums;
        
        private ChoiceAnimationData _animData;
        public Animator _animator;
        private readonly int HasHIsChoice = Animator.StringToHash("IsChoice");

        private readonly float _moveDistance = 85f;
        private readonly float _moveDuration = 0.5f;
        private readonly float _arrowDistance = 15f;
        private bool _isScrolling;
        private Vector2 _startDownPos;
        private Vector2 _startUpPos;
        private bool _isChanged;
        
        private Dictionary<Image, Tween> activeTweens = new Dictionary<Image, Tween>();
        private Sequence activeSequences;
        private void OnEnable()
        {
            foreach (var ActiveImage in choices[3].ActiveImages)
            {
                Tween t = ActiveImage.DOFade(0.3f, 1f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutQuad)
                    .From(1f);
                activeTweens[ActiveImage] = t;
            }
            ArrowData arrowData = choices[3].ArrowData;
            
            _startDownPos = arrowData.DownArrowRect.anchoredPosition;
            _startUpPos = arrowData.UpArrowRect.anchoredPosition;
            ArrowAnimation();
            _isChanged = false;
        }

        private void ArrowAnimation()
        {
            ArrowData arrowData = choices[3].ArrowData;
            activeSequences = DOTween.Sequence();
            activeSequences.Append(arrowData.DownArrow
                .DOFade(0.1f, 2f)
                .From(1f));
            activeSequences.Join(arrowData.DownArrowRect
                .DOAnchorPosY(arrowData.DownArrowRect.anchoredPosition.y - _arrowDistance, 2f)
                .SetEase(Ease.Linear)
                .From(_startDownPos));
            activeSequences.Join(arrowData.UpArrow
                .DOFade(0.1f, 2f)
                .From(1f));
            activeSequences.Join(arrowData.UpArrowRect
                .DOAnchorPosY(arrowData.UpArrowRect.anchoredPosition.y + _arrowDistance, 2f)
                .SetEase(Ease.Linear)
                .From(_startUpPos));
            activeSequences.SetLoops(-1);
        }
        
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
            activeChoice = choices[3];
           // Execute Code
            // 곡 위로
            // if (drums.dataSet == DrumDataType.RightFace)
            //     ScrollUp();
            // // 곡 아래로
            // else if (drums.dataSet == DrumDataType.LeftFace)
            //     ScrollDown();
            // // 곡 선택
            // else if (drums.dataSet == DrumDataType.DobletFace)
            //     DoChoice();
            
            if (testDrumInput.testInputType == DrumDataType.RightFace && !_isChanged)
                ScrollUp();
            // 곡 아래로
            else if (testDrumInput.testInputType == DrumDataType.LeftFace && !_isChanged)
                ScrollDown();
            // 곡 선택
            else if (testDrumInput.testInputType == DrumDataType.DobletFace && !_isChanged)
                DoChoice();
        }

        private void DoChoice()
        {
            _isChanged = true;
            InitAnimations(activeChoice);
            Sequence seq = DOTween.Sequence();
            Image[] ActiveImages = choices[3].ActiveImages;
            foreach (var ActiveImage in ActiveImages)
            {
                seq.Join(ActiveImage.DOFade(1f, 0.1f)
                    .From(0f)
                    .SetLoops(4, LoopType.Yoyo)
                    .SetEase(Ease.InOutQuad)); 
            }

            seq.InsertCallback(0f, () =>
            {
                _animator.SetBool(HasHIsChoice, true);
            });

            seq.AppendCallback(() =>
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
            });
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
                    InitAnimations(choices[i]);
                    choices[i].ArrowData.Arrow.SetActive(false);
                    choices[i].ActiveFrame.SetActive(false);
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

            // choices[4].ActiveFrame.SetActive(false);

            var bottomChoice = GetBottomChoice();
            var topChoice = GetTopChoice();
            
            float bottomChoiceRectY = bottomChoice.CardTrans.anchoredPosition.y - _moveDistance;
            
            topChoice.CardTrans.anchoredPosition =
                new Vector2(topChoice.CardTrans.anchoredPosition.x, bottomChoiceRectY);
            
            choices.Remove(topChoice);
            choices.Add(topChoice);

            choices[6].Text.text = choices[1].Text.text;
            choices[6].ChoiceType = choices[1].ChoiceType;
            choices[3].ActiveFrame.SetActive(true);
            ActiveFrameAnimation(choices[3]);
            choices[3].ArrowData.Arrow.SetActive(true);
            ArrowAnimation();
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
                    InitAnimations(choices[i]);
                    choices[i].ArrowData.Arrow.SetActive(false);
                    choices[i].ActiveFrame.SetActive(false);
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


            var bottomChoice = GetBottomChoice();
            var topChoice = GetTopChoice();
            float topChoiceRectY = topChoice.CardTrans.anchoredPosition.y + _moveDistance;
            bottomChoice.CardTrans.anchoredPosition =
                new Vector2(bottomChoice.CardTrans.anchoredPosition.x, topChoiceRectY);
            choices.Remove(bottomChoice);
            choices.Insert(0, bottomChoice);

            choices[0].Text.text = choices[5].Text.text;
            choices[0].ChoiceType = choices[5].ChoiceType;
            choices[3].ActiveFrame.SetActive(true);
            ActiveFrameAnimation(choices[3]);
            choices[3].ArrowData.Arrow.SetActive(true);
            ArrowAnimation();
            _isScrolling = false;
        }

        private void ActiveFrameAnimation(ChoiceData choice)
        {
            foreach (var ActiveImage in choice.ActiveImages)
            {
                Tween t = ActiveImage.DOFade(0.3f, 1f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutQuad)
                    .From(1f);
                activeTweens[ActiveImage] = t;
            }
            choice.ArrowData.Arrow.SetActive(true);
        }
        private void InitAnimations(ChoiceData choiceData)
        {
            foreach (var ActiveImage in choiceData.ActiveImages)
            {
                if (activeTweens.TryGetValue(ActiveImage, out var tween))
                {
                    tween.Kill();
                    var color = ActiveImage.color;
                    color.a = 1f;
                    ActiveImage.color = color;
                }
            }
            activeSequences.Kill();
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
                    top = choice;
            }
            return top;
        }

        private ChoiceData GetBottomChoice()
        {
            ChoiceData bottom = choices[0];
            foreach (var choice in choices)
            {
                if (choice.CardTrans.anchoredPosition.y < bottom.CardTrans.anchoredPosition.y)
                    bottom = choice;
            }
            return bottom;
        }
    }
}
