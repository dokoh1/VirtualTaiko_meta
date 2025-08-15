

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardScrollAnimator : MonoBehaviour
{
    private List<ChoiceData> _choices;
    private ChoiceAnimationSettings _settings;
    private bool _isAnimating = false;

    public bool IsAnimating => _isAnimating;
    public void Initialize(List<ChoiceData> choices, ChoiceAnimationSettings settings)
    {
        _choices = choices;
        _settings = settings;
    }

    public void PlayScrollAnimation(bool isUp, Action onAnimationComplete)
    {
        if (_isAnimating)
            return;
        StartCoroutine(ScrollProcess(isUp, onAnimationComplete));
    }

    private IEnumerator ScrollProcess(bool isUp, Action onAnimationComplete)
    {
        _isAnimating = true;
        
        Sequence sequence = DOTween.Sequence();
        int activeIndex = 3;
        
        _choices[activeIndex].ArrowData.Arrow.SetActive(false);
        _choices[activeIndex].ActiveFrame.SetActive(false);
        if (isUp)
        {
            for (int i = 0; i < _choices.Count; i++)
            {
                if (i == 4)
                    ApplyAnimation(sequence, _choices[i], _settings.activeCard, 120f);
                else if (i == 3)
                    ApplyAnimation(sequence, _choices[i], _settings.notActiveCard, 120f);
                else
                    sequence.Join(_choices[i].CardTrans
                        .DOAnchorPosY(_choices[i].CardTrans.anchoredPosition.y + _settings.moveDistance, _settings.moveDuration)
                        .SetEase(Ease.InOutQuad));
            }
        }
        else
        {
            for (int i = 0; i < _choices.Count; i++)
            {
                
                if (i == 2)
                    ApplyAnimation(sequence, _choices[i], _settings.activeCard, -120f);
                else if (i == 3)
                    ApplyAnimation(sequence, _choices[i], _settings.notActiveCard, -120f);
                else
                    sequence.Join(_choices[i].CardTrans
                        .DOAnchorPosY(_choices[i].CardTrans.anchoredPosition.y - _settings.moveDistance, _settings.moveDuration)
                        .SetEase(Ease.InOutQuad));
            }
        }

        bool finished = false;
        sequence.OnComplete(() => finished = true);
        yield return new WaitUntil(() => finished);

        _choices[4].ActiveFrame.SetActive(false);

        var bottomChoice = GetBottomChoice();
        var topChoice = GetTopChoice();
        if (isUp)
        {
            topChoice.CardTrans.anchoredPosition = new Vector2(topChoice.CardTrans.anchoredPosition.x, bottomChoice.CardTrans.anchoredPosition.y - _settings.moveDistance);
            _choices.Remove(topChoice);
            _choices.Add(topChoice);
        }
        else
        {
            bottomChoice.CardTrans.anchoredPosition = new Vector2(bottomChoice.CardTrans.anchoredPosition.x, topChoice.CardTrans.anchoredPosition.y + _settings.moveDistance); 
            _choices.Remove(bottomChoice);
            _choices.Insert(0, bottomChoice);
        }
        _isAnimating = false;
        onAnimationComplete?.Invoke();
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
        ChoiceData top = _choices[0];
        return top;
    }

    private ChoiceData GetBottomChoice()
    {
        ChoiceData bottom = _choices[^1];
        return bottom;
    }
}