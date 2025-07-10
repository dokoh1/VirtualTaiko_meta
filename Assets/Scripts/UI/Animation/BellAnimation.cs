using DG.Tweening;
using UnityEngine;

public class BellAnimation : MonoBehaviour
{
    public GameObject scene;

    private readonly float _bellDestination = 435f;
    private readonly float _bellDuration = 3f;

    public void DoAnimation(GameObject bell)
    {
        var go = Instantiate(bell, scene.transform);
        var goRectTransform = go.GetComponent<RectTransform>();
        Sequence seq = DOTween.Sequence();

        seq.Join(goRectTransform.DOAnchorPosX(_bellDestination, _bellDuration)
                .SetEase(Ease.Linear))
            .SetId("Bell");
        seq.AppendCallback(() => { Destroy(go); });
    }

    private void OnDisable()
    {
        DOTween.Kill("Bell");
    }
}

