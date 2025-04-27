using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Animation : MonoBehaviour
{
    [SerializeField]
    private RectTransform _backgrounds;

    private const float BgMove = 300;

    void OnEnable()
    {
        _backgrounds.DOAnchorPosX(BgMove, 5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
