using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class CloudAnimation : BGAnimation
{
    [SerializeField] 
    private float yMoveAmount;

    [SerializeField] 
    private float cloudsduration;

    [SerializeField] private Sprite cloudImage;
    [SerializeField] private Sprite backgroundImage;
    
    [SerializeField] private List<Image> cloudSprites;
    [SerializeField] private List<Image> backgroundSprites;
    
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

    public void ChangeImage()
    {
        foreach (var image in cloudSprites)
        {
            image.sprite = cloudImage;
        }

        foreach (var image in backgroundSprites)
        {
            image.sprite = backgroundImage;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DOTween.Kill(clouds);
    }
}
