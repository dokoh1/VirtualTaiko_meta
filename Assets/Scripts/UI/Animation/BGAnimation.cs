using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class BGAnimation : MonoBehaviour
{
    [SerializeField]
    protected RectTransform backgrounds;
    protected DOTween Tween;

    [SerializeField]
    protected float duration;

    [SerializeField]
    protected float moveAmount;


    protected virtual void OnEnable()
    {
        BackGroundMove();
    }

    protected void BackGroundMove()
    {
        backgrounds.DOAnchorPosX(backgrounds.anchoredPosition.x + moveAmount, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId(backgrounds);
    }

    protected virtual void OnDisable()
    {
        DOTween.Kill(backgrounds);
    }
}
