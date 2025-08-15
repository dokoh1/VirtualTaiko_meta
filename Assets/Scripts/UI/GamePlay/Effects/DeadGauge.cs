using UnityEngine;
using DG.Tweening;

public class DeadGauge : MonoBehaviour
{
    [SerializeField]
    private RectTransform firstGauge;
    [SerializeField]
    private RectTransform secondGauge;

    [SerializeField] 
    private DeadGaugeSettings _settings;

    private float _savedDeadGauge;

    void OnEnable()
    {
        _savedDeadGauge = _settings.maxGauge;
        SetGaugeSize(firstGauge, _settings.initFirstGauge);
        SetGaugeSize(secondGauge, _settings.initSecondGauge);
        GameEvents.OnDeadGauge += HandleDeadGaugeUpdated;
    }

    void OnDisable()
    {
        GameEvents.OnDeadGauge -= HandleDeadGaugeUpdated;
    }

    public void HandleDeadGaugeUpdated(float currentDeadGauge)
    {
        if (Mathf.Approximately(_savedDeadGauge, currentDeadGauge))
            return;

        if (currentDeadGauge > _settings.maxGauge)
            return;

        float delta = _savedDeadGauge - currentDeadGauge;
        RectTransform targetGauge = (currentDeadGauge >= _settings.initFirstGauge) ? secondGauge : firstGauge;
        
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
        gauge.DOSizeDelta(new Vector2(width, gauge.sizeDelta.y), _settings.animationDuration);
    }
}
