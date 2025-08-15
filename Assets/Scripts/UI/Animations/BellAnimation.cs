using DG.Tweening;
using UnityEngine;

public class BellAnimation : MonoBehaviour
{
    [Header("애니메이션 타겟")]
    [SerializeField]
    private GameObject scene;

    [Header("애니메이션 설정")]
    [SerializeField] private float bellDestinationX = 435f;
    [SerializeField] private float bellDuration = 3f;

    public void DoAnimation(GameObject bell)
    {
        var go = Instantiate(bell, scene.transform);
        var goRectTransform = go.GetComponent<RectTransform>();
        Sequence seq = DOTween.Sequence();

        seq.Join(goRectTransform.DOAnchorPosX(bellDestinationX, bellDuration)
                .SetEase(Ease.Linear))
            .SetId("Bell");
        seq.AppendCallback(() => { Destroy(go); });
    }

    private void OnDisable()
    {
        DOTween.Kill("Bell");
    }
}

