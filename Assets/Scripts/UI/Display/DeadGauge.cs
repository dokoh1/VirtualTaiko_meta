using UnityEngine;
using DG.Tweening;

public class DeadGauge : MonoBehaviour
{
    [SerializeField]
    private RectTransform firstGauge;
    [SerializeField]
    private RectTransform secondGauge;

    private const float MaxGauge = 350f;
    private const float InitFirstGauge = 200f;
    private const float InitSecondGauge = 150f;
    private const float AnimationDuration = 0.1f;

    private float _savedDeadGauge = MaxGauge;

    void OnEnable()
    {
        SetGaugeSize(firstGauge, InitFirstGauge);
        SetGaugeSize(secondGauge, InitSecondGauge);
    }

    public void DeadGaugeUpdate(float currentDeadGauge)
    {
        if (Mathf.Approximately(_savedDeadGauge, currentDeadGauge))
            return;

        if (currentDeadGauge > MaxGauge)
            return;

        float delta = _savedDeadGauge - currentDeadGauge;
        RectTransform targetGauge = (currentDeadGauge >= InitFirstGauge) ? secondGauge : firstGauge;
        
        float newWidth = targetGauge.sizeDelta.x - delta;
        SetGaugeSizeAnimated(targetGauge, newWidth);

        _savedDeadGauge = currentDeadGauge;
    }

    private void SetGaugeSize(RectTransform gauge, float width)
    {
        gauge.sizeDelta = new Vector2(width, gauge.sizeDelta.y);
    }

    private void SetGaugeSizeAnimated(RectTransform gauge, float width)
    {
        gauge.DOSizeDelta(new Vector2(width, gauge.sizeDelta.y), AnimationDuration);
    }
}
