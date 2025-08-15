using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CloudAnimation : BGAnimation
{
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
        GameEvents.OnNoteHit += HandleNoteHit;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DOTween.Kill(clouds);
        GameEvents.OnNoteHit -= HandleNoteHit;
    }

    private void HandleNoteHit(HitResult result, ScoreData scoreData)
    {
        if (scoreData.hit == 60)
        {
            ChangeImage();
        }
    }
    private void CloudMove()
    {
        clouds.DOAnchorPosY(clouds.anchoredPosition.y + moveAmount, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId("clouds");
    }

    public void ChangeImage()
    {
        foreach (var image in cloudSprites)
            image.sprite = cloudImage;

        foreach (var image in backgroundSprites)
            image.sprite = backgroundImage;
    }
}

