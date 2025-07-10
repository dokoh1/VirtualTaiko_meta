using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class VaneAnimation : MonoBehaviour
{
    public RectTransform rectTransform;
    [FormerlySerializedAs("Vanes")] public List<RectTransform> vanes = new();
    
    private readonly Vector2 _moveOffset = new(4000, 0);
    private readonly float _moveDuration = 15f;
    private void OnEnable()
    {
        RotateAnimation();
        MoveAnimation();
    }

    private void RotateAnimation()
    {
        foreach (var vane in vanes)
        {
            vane.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }

    private void MoveAnimation()
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(
            rectTransform.DOAnchorPos(rectTransform.anchoredPosition + _moveOffset, _moveDuration)
                .SetEase(Ease.Linear)
        );
        moveSequence.AppendInterval(5f);
        moveSequence.SetLoops(-1, LoopType.Restart);
    }
    
}
