using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChoiceView : MonoBehaviour
{
    // AudioClip
    public AudioClip ChooseMusic;
    public AudioClip UpScrollMusic;
    public AudioClip DownScrollMusic;
    
    // 사용자 입력을 Presenter에게 알리기 위한 이벤트
    public event Action OnScrollUpRequested;
    public event Action OnScrollDownRequested;
    public event Action OnChoiceMadeRequested;
    
    // UI
    [FormerlySerializedAs("choiceList")] [SerializeField]
    private List<ChoiceData> choices = new();
    
    public ChoiceAnimationSettings _settings;

    public Animator _animator;
    private readonly int HasHIsChoice = Animator.StringToHash("IsChoice");
    private bool _isAnimating = false;
    private ChoiceData _activeChoice;
    private Vector2 _startDownPos;
    private Vector2 _startUpPos;

    private Dictionary<Image, Tween> activeTweens = new Dictionary<Image, Tween>();

    private Sequence activeSequences;
    
    void Update()
    {
        if (_isAnimating) return;
        
        DrumDataType drumDataType = InputManager.Instance.GetInput();

        if (drumDataType == DrumDataType.RightFace)
            OnScrollUpRequested?.Invoke();
        else if (drumDataType == DrumDataType.LeftFace)
            OnScrollDownRequested?.Invoke();
        else if (drumDataType == DrumDataType.DobletFace)
            OnChoiceMadeRequested?.Invoke();
    }

    public void UpdateChoiceDisplay(List<MusicData> newChoiceData)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            ChoiceData uiSlot = choices[i];
            MusicData dataForSlot = newChoiceData[i];
            
            uiSlot.Text.text = dataForSlot.Title;
            uiSlot.ChoiceType = dataForSlot.ChoiceType;
        }

        foreach (var uiCard in choices)
        {
            InitAnimations(uiCard);
            
            uiCard.ActiveFrame.SetActive(false);
            uiCard.ArrowData.Arrow.SetActive(false);
        }

        int activeIndex = 3;
        ChoiceData activeCardUI = choices[activeIndex];
        
        activeCardUI.ActiveFrame.SetActive(true);

        ActiveFrameAnimation(activeCardUI);
        ArrowAnimation(activeCardUI);
    }

    public void PlayChoiceAnimation(Action onAnimationComplete)
    {
        if (_isAnimating) return;
        Single.System.AudioManager.PlaySFX(ChooseMusic);
        
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
            _isAnimating = true;
            _animator.SetBool(HasHIsChoice, true);
        });

        seq.AppendCallback(() =>
        {
            _isAnimating = false;
        });
        onAnimationComplete?.Invoke();
    }

    public void PlayScrollDownAnimation(Action onAnimationComplete)
    {
        if (_isAnimating)
            return;

        StopAllCoroutines();
        StartCoroutine(ScrollProcess(false, onAnimationComplete));
    }

    public void PlayScrollUpAnimation(Action onAnimationComplete)
    {
        if (_isAnimating)
            return;
        StopAllCoroutines();
        StartCoroutine(ScrollProcess(true, onAnimationComplete));
    }

    private IEnumerator ScrollProcess(bool isUp, Action onAnimationComplete)
    {
        _isAnimating = true;
        Single.System.AudioManager.PlaySFX(isUp ? UpScrollMusic : DownScrollMusic);
        
        Sequence sequence = DOTween.Sequence();
        int activeIndex = 3;
        
        choices[activeIndex].ArrowData.Arrow.SetActive(false);
        choices[activeIndex].ActiveFrame.SetActive(false);
        if (isUp)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                if (i == 4)
                    ApplyAnimation(sequence, choices[i], _settings.activeCard, 120f);
                else if (i == 3)
                    ApplyAnimation(sequence, choices[i], _settings.notActiveCard, 120f);
                else
                    sequence.Join(choices[i].CardTrans
                        .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y + _settings.moveDistance, _settings.moveDuration)
                        .SetEase(Ease.InOutQuad));
            }
        }
        else
        {
            for (int i = 0; i < choices.Count; i++)
            {
                
                if (i == 2)
                    ApplyAnimation(sequence, choices[i], _settings.activeCard, -120f);
                else if (i == 3)
                    ApplyAnimation(sequence, choices[i], _settings.notActiveCard, -120f);
                else
                    sequence.Join(choices[i].CardTrans
                        .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y - _settings.moveDistance, _settings.moveDuration)
                        .SetEase(Ease.InOutQuad));
            }
        }

        bool finished = false;
        sequence.OnComplete(() => finished = true);
        yield return new WaitUntil(() => finished);

        choices[4].ActiveFrame.SetActive(false);

        var bottomChoice = GetBottomChoice();
        var topChoice = GetTopChoice();
        if (isUp)
        {
            topChoice.CardTrans.anchoredPosition = new Vector2(topChoice.CardTrans.anchoredPosition.x, bottomChoice.CardTrans.anchoredPosition.y - _settings.moveDistance);
            choices.Remove(topChoice);
            choices.Add(topChoice);
        }
        else
        {
            bottomChoice.CardTrans.anchoredPosition = new Vector2(bottomChoice.CardTrans.anchoredPosition.x, topChoice.CardTrans.anchoredPosition.y + _settings.moveDistance); 
            choices.Remove(bottomChoice);
            choices.Insert(0, bottomChoice);
        }
        _isAnimating = false;
        onAnimationComplete?.Invoke();
    }
    
    private void ArrowAnimation(ChoiceData choice)
    {
        ArrowData arrowData = choice.ArrowData;
        activeSequences = DOTween.Sequence();
        activeSequences.Append(arrowData.DownArrow
            .DOFade(0.1f, 2f)
            .From(1f));
        activeSequences.Join(arrowData.DownArrowRect
            .DOAnchorPosY(arrowData.DownArrowRect.anchoredPosition.y - _settings.arrowDistance, 2f)
            .SetEase(Ease.Linear)
            .From(_startDownPos));
        activeSequences.Join(arrowData.UpArrow
            .DOFade(0.1f, 2f)
            .From(1f));
        activeSequences.Join(arrowData.UpArrowRect
            .DOAnchorPosY(arrowData.UpArrowRect.anchoredPosition.y + _settings.arrowDistance, 2f)
            .SetEase(Ease.Linear)
            .From(_startUpPos));
        activeSequences.SetLoops(-1);
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

    private void ApplyAnimation(Sequence sequence, ChoiceData choice, ChoiceCardSettings data, float distance)
    {
        sequence.Join(choice.CenterTrans
            .DOSizeDelta(new Vector2(choice.CenterTrans.sizeDelta.x, data.CenterHeight), _settings.moveDuration)
            .SetEase(Ease.InOutQuad));

        sequence.Join(choice.TopTrans
            .DOAnchorPosY(choice.TopTrans.anchoredPosition.y + data.TopPosY, _settings.moveDuration)
            .SetEase(Ease.InOutQuad));
        sequence.Join(choice.TopTrans
            .DOSizeDelta(new Vector2(choice.TopTrans.sizeDelta.x, data.TopHeight), _settings.moveDuration)
            .SetEase(Ease.InOutQuad));

        sequence.Join(choice.BottomTrans
            .DOSizeDelta(new Vector2(choice.BottomTrans.sizeDelta.x, data.BottomHeight), _settings.moveDuration)
            .SetEase(Ease.InOutQuad));
        sequence.Join(choice.BottomTrans
            .DOAnchorPosY(choice.BottomTrans.anchoredPosition.y + data.BottomPosY, _settings.moveDuration)
            .SetEase(Ease.InOutQuad));

        sequence.Join(choice.InTrans
            .DOSizeDelta(new Vector2(choice.InTrans.sizeDelta.x, data.InHeight), _settings.moveDuration)
            .SetEase(Ease.InOutQuad));

        sequence.Join(choice.CardTrans
            .DOAnchorPosY(choice.CardTrans.anchoredPosition.y + distance, _settings.moveDuration)
            .SetEase(Ease.InOutQuad));
        Vector2 Crown = new Vector2(data.CrownSizeX, data.CrownSizeY);
        Vector2 Badge = new Vector2(data.BadgeSizeX, data.BadgeSizeY);
        sequence.Join(choice.CrownTrans
            .DOSizeDelta(Crown, _settings.moveDuration)
            .SetEase(Ease.InOutQuad));
        sequence.Join(choice.BadgeTrans
            .DOSizeDelta(Badge, _settings.moveDuration)
            .SetEase(Ease.InOutQuad));
    }

    private ChoiceData GetTopChoice()
    {
        ChoiceData top = choices[0];

        // foreach (var choice in choices)
        // {
        //     if (choice.CardTrans.anchoredPosition.y > top.CardTrans.anchoredPosition.y)
        //         top = choice;
        // }

        return top;
    }

    private ChoiceData GetBottomChoice()
    {
        ChoiceData bottom = choices[^1];
        // foreach (var choice in choices)
        // {
        //     if (choice.CardTrans.anchoredPosition.y < bottom.CardTrans.anchoredPosition.y)
        //         bottom = choice;
        // }

        return bottom;
    }
}

