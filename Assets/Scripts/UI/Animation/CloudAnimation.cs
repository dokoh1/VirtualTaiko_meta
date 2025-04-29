using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CloudAnimation : BGAnimation
{
    [SerializeField] 
    private float yMoveAmount;

    [SerializeField] 
    private float cloudsduration;
    

    [SerializeField] 
    private RectTransform clouds;

    protected override void OnEnable()
    {
        base.OnEnable();
        CloudMove();
    }

    public void CloudMove()
    {
        clouds.DOAnchorPosY(clouds.anchoredPosition.y + yMoveAmount, cloudsduration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId("clouds");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DOTween.Kill(clouds);
    }
}
