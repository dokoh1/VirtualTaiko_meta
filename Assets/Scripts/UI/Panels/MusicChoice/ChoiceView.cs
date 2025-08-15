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
    [SerializeField] private CardScrollAnimator scrollAnimator;
    
    public ChoiceAnimationSettings _settings;
    private bool _isChoiceAnimating = false;
    public Animator _animator;
    private readonly int HasHIsChoice = Animator.StringToHash("IsChoice");
    private ChoiceData _activeChoice;
    private Vector2 _startDownPos;
    private Vector2 _startUpPos;

    private Dictionary<Image, Tween> activeTweens = new();

    private Sequence activeSequences;

    void Awake()
    {
        scrollAnimator.Initialize(choices, _settings);
    }
    void Update()
    {
        if (scrollAnimator.IsAnimating || _isChoiceAnimating) return;
        
        DrumDataType drumDataType = Single.System.InputManager.GetInput();

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
        if (_isChoiceAnimating) return;
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
            _isChoiceAnimating = true;
            _animator.SetBool(HasHIsChoice, true);
        });

        seq.AppendCallback(() =>
        {
            _isChoiceAnimating = false;
        });
        onAnimationComplete?.Invoke();
    }

    public void PlayScrollDownAnimation(Action onAnimationComplete)
    {
        Single.System.AudioManager.PlaySFX(DownScrollMusic);
        scrollAnimator.PlayScrollAnimation(false, onAnimationComplete);
    }

    public void PlayScrollUpAnimation(Action onAnimationComplete)
    {
        Single.System.AudioManager.PlaySFX(UpScrollMusic);
        scrollAnimator.PlayScrollAnimation(true, onAnimationComplete);
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
}

