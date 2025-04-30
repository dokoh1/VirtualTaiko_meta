using DG.Tweening;
using UnityEngine;

public class DeadGauge : MonoBehaviour
{
    [SerializeField]
    private RectTransform FirstGauge;
    [SerializeField]
    private RectTransform SecondGauge;
    
    private readonly float _initFirstGauge = 200;
    private readonly float _initSecondGauge = 150;
    private readonly float _duration = 0.3f;
    
    private float _saveDeadGauge = 350;
    void OnEnable()
    {
        FirstGauge.sizeDelta = new Vector2(_initFirstGauge, FirstGauge.sizeDelta.y);
        SecondGauge.sizeDelta = new Vector2(_initSecondGauge, SecondGauge.sizeDelta.y);
    }

    public void DeadGaugeUpdate(float currentDeadGuage)
    {
        if (Mathf.Approximately(_saveDeadGauge, currentDeadGuage))
            return;
        
        float value = currentDeadGuage;
        float amount = _saveDeadGauge - currentDeadGuage;
        
        // DeadGauge 증가
        if (amount < 0)
        {
            if (currentDeadGuage > 350)
                return;
            if (value > _initFirstGauge)
                SecondGauge.DOSizeDelta(new Vector2(SecondGauge.sizeDelta.x - amount, SecondGauge.sizeDelta.y), _duration);
            else
                FirstGauge.DOSizeDelta(new Vector2(FirstGauge.sizeDelta.x - amount, FirstGauge.sizeDelta.y), _duration);
        }
        //DeadGauge 감소
        else
        {
            if (value >= _initFirstGauge)
                SecondGauge.DOSizeDelta(new Vector2(SecondGauge.sizeDelta.x - amount, SecondGauge.sizeDelta.y), _duration);
            else
                FirstGauge.DOSizeDelta(new Vector2(FirstGauge.sizeDelta.x - amount, FirstGauge.sizeDelta.y), _duration);
        }
        _saveDeadGauge = currentDeadGuage;
    }
}
