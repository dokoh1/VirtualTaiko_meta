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
    public AudioClip chooseClip;
    public AudioClip musicUpClip;
    public AudioClip musicDownClip;
    
    // 사용자 입력을 Presenter에게 알리기 위한 이벤트
    public event Action OnScrollUpRequested;
    public event Action OnScrollDownRequested;
    public event Action<ChoiceType> OnChoiceMadeRequested;
    
    // UI
    [FormerlySerializedAs("choiceList")] [SerializeField]
    private List<ChoiceData> choices = new();
    
    public ChoiceAnimationSettings _settings;

    public Animator _animator;
    private readonly int HasHIsChoice = Animator.StringToHash("IsChoice");
    
    private bool _isScrolling;
    private bool _isChanged;
    
    private ChoiceData _activeChoice;
    private Vector2 _startDownPos;
    private Vector2 _startUpPos;

    private Dictionary<Image, Tween> activeTweens = new Dictionary<Image, Tween>();

    private Sequence activeSequences;

    // private Sequence Sequence;
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

    void Start()
    {
        _activeChoice = choices[3];
    }

    void Update()
    {
        _activeChoice = choices[3];
        DrumDataType drumDataType = InputManager.Instance.GetInput();

        if (drumDataType == DrumDataType.RightFace)
            ScrollUpAnimation();
        else if (drumDataType == DrumDataType.LeftFace)
            ScrollDownAnimation();
        else if (drumDataType == DrumDataType.DobletFace)
            DoChoice();
    }

    private void DoChoice()
    {
        _isChanged = true;
        InitAnimations(_activeChoice);
        Single.System.AudioManager.PlaySFX(chooseClip);
        Sequence seq = DOTween.Sequence();
        Image[] ActiveImages = choices[3].ActiveImages;
        foreach (var ActiveImage in ActiveImages)
        {
            seq.Join(ActiveImage.DOFade(1f, 0.1f)
                .From(0f)
                .SetLoops(4, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad));
        }

        seq.InsertCallback(0f, () => { _animator.SetBool(HasHIsChoice, true); });

        seq.AppendCallback(() =>
        {
            if (_activeChoice.ChoiceType == ChoiceType.Music1)
                Single.System.SceneManager.LoadScene(SceneDataType.Music1);
            else if (_activeChoice.ChoiceType == ChoiceType.Music2)
                Single.System.SceneManager.LoadScene(SceneDataType.Music1);
            else if (_activeChoice.ChoiceType == ChoiceType.Music3)
                Single.System.SceneManager.LoadScene(SceneDataType.Music1);
            else if (_activeChoice.ChoiceType == ChoiceType.BackToMenu)
                Single.System.SceneManager.LoadScene(SceneDataType.Start);
            else if (_activeChoice.ChoiceType == ChoiceType.RandomMusic)
            {
                int rand = Random.Range(0, 3);
                if (rand == 0)
                    Single.System.SceneManager.LoadScene(SceneDataType.Music1);
                else if (rand == 1)
                    Single.System.SceneManager.LoadScene(SceneDataType.Music1);
                else if (rand == 2)
                    Single.System.SceneManager.LoadScene(SceneDataType.Music1);
            }
        });
    }

    public void ScrollDownAnimation()
    {
        if (_isScrolling)
            return;

        StopAllCoroutines();
        StartCoroutine(ScrollDownProcess());
    }

    public void ScrollUpAnimation()
    {
        if (_isScrolling)
            return;
        StopAllCoroutines();
        StartCoroutine(ScrollUpProcess());
    }

    private IEnumerator ScrollUpProcess()
    {
        _isScrolling = true;
        Single.System.AudioManager.PlaySFX(musicUpClip);
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < choices.Count; i++)
        {
            float activeDistance = 120f;
            if (i == 4)
                ApplyAnimation(sequence, choices[i], _settings.activeCard, activeDistance);
            else if (i == 3)
            {
                InitAnimations(choices[i]);
                choices[i].ArrowData.Arrow.SetActive(false);
                choices[i].ActiveFrame.SetActive(false);
                ApplyAnimation(sequence, choices[i], _settings.notActiveCard, activeDistance);
            }
            else
            {
                sequence.Join(choices[i].CardTrans
                    .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y + _settings.moveDistance, _settings.moveDuration)
                    .SetEase(Ease.InOutQuad));
            }
        }

        bool finished = false;
        sequence.OnComplete(() => finished = true);
        yield return new WaitUntil(() => finished);

        // choices[4].ActiveFrame.SetActive(false);

        var bottomChoice = GetBottomChoice();
        var topChoice = GetTopChoice();

        float bottomChoiceRectY = bottomChoice.CardTrans.anchoredPosition.y - _settings.moveDistance;

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
        Single.System.AudioManager.PlaySFX(musicDownClip);
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < choices.Count; i++)
        {
            float activeDistance = -120f;
            if (i == 2)
            {
                ApplyAnimation(sequence, choices[i], _settings.activeCard, activeDistance);
            }
            else if (i == 3)
            {
                InitAnimations(choices[i]);
                choices[i].ArrowData.Arrow.SetActive(false);
                choices[i].ActiveFrame.SetActive(false);
                ApplyAnimation(sequence, choices[i], _settings.notActiveCard, activeDistance);
            }
            else
            {
                sequence.Join(choices[i].CardTrans
                    .DOAnchorPosY(choices[i].CardTrans.anchoredPosition.y - _settings.moveDistance, _settings.moveDuration)
                    .SetEase(Ease.InOutQuad));
            }
        }
        
        bool finished = false;
        sequence.OnComplete(() => finished = true);
        yield return new WaitUntil(() => finished);


        var bottomChoice = GetBottomChoice();
        var topChoice = GetTopChoice();
        float topChoiceRectY = topChoice.CardTrans.anchoredPosition.y + _settings.moveDistance;
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

