using DG.Tweening;
using UnityEngine;

public class BellAnimation : MonoBehaviour
{
    public GameObject Scene;
    private readonly float destination = 435f;
    private readonly float duration = 3f;

    public void CreateBell(GameObject Bell)
    {
        var go = Instantiate(Bell, Scene.transform);
        var goRectTransform = go.GetComponent<RectTransform>();
        Sequence seq  = DOTween.Sequence();
        seq.Join(goRectTransform.DOAnchorPosX(destination, duration)
            .SetEase(Ease.Linear));
        seq.AppendCallback(() =>
        {
            Destroy(go);
        });
    }
}
