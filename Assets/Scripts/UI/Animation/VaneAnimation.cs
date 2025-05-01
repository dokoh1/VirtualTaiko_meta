using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VaneAnimation : MonoBehaviour
{
    public RectTransform rectTransform;
    public List<RectTransform> Vanes = new();
    
    private Vector2 moveOffset = new(4000, 0);
    private float moveDuration = 15f;
    private void OnEnable()
    {
        foreach (var vane in Vanes)
        {
            vane.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        rectTransform.DOAnchorPos(rectTransform.anchoredPosition + moveOffset, moveDuration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);


    }
    
}
