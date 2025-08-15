using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class VaneAnimation : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField] private VaneAnimationSettings _settings;
    [FormerlySerializedAs("Vanes")] public List<RectTransform> vanes = new();
    
    private void OnEnable()
    {
        RotateAnimation();
        MoveAnimation();
    }

    private void RotateAnimation()
    {
        foreach (var vane in vanes)
        {
            vane.DORotate(_settings.roationAngle, _settings.rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }

    private void MoveAnimation()
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(
            rectTransform.DOAnchorPos(rectTransform.anchoredPosition + _settings.moveOffset, _settings.moveDuration)
                .SetEase(Ease.Linear)
        );
        moveSequence.AppendInterval(_settings.moveAppendInterval);
        moveSequence.SetLoops(-1, LoopType.Restart);
    }
    
}
