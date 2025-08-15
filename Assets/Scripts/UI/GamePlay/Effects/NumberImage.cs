using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NumberImage : MonoBehaviour
{
    [SerializeField] private NumberImageSettings _settings;
    [SerializeField] private Sprite[] digitSprites;
    [SerializeField] private NumberImagePool pool;

    private List<NumberData> _digits = new();
    private Sequence _seq;
    private int _saveScore = -1;
    private Vector2 _digitBaseSize;
    public DisplayType displayType;
    
    private void Awake()
    {
        if (pool.digitPrefab != null)
        {
            RectTransform rt = pool.digitPrefab.GetComponent<RectTransform>();
            _digitBaseSize = rt != null ? rt.sizeDelta : _settings.baseDigitSize; // fallback size
        }
    }

    private void OnEnable()
    {
        if (displayType == DisplayType.Score)
        {
            GameEvents.OnScoreUpdated += HandleScoreUpdated;
        }
        else if (displayType == DisplayType.Combo)
        {
            GameEvents.OnComboUpdated += HandleComboUpdated;
        }
        ClearDigits();
    }

    private void OnDestroy()
    {
        ClearDigits();
    }

    private void ClearDigits()
    {
        foreach (var digitData in _digits)
        {
            pool.Release(digitData.DigitImage.gameObject);
        }
        _digits.Clear();
    }

    private void HandleScoreUpdated(int score)
    {
        UpdateDisplay(score);
    }
    
    private void HandleComboUpdated(int combo)
    {
        UpdateDisplay(combo);
    }
    public void UpdateDisplay(int currentScore)
    {
        if (_saveScore == currentScore) return;
        int digitCount = GetDigitCount(currentScore);
        AdjustDigitList(digitCount);
        AnimateDigits();
        UpdateDigitSprites(currentScore);

        _saveScore = currentScore;
    }
    
    private int GetDigitCount(int value)
    {
        return Mathf.Max(1, value == 0 ? 1 : (int)Mathf.Floor(Mathf.Log10(value)) + 1);
    }

    private void AdjustDigitList(int count)
    {
        while (_digits.Count < count)
        {
            var go = pool.Get();
            go.transform.SetParent(transform, false);
            var data = new NumberData
            {
                DigitImage = go.GetComponent<Image>(),
                DigitTransform = go.GetComponent<RectTransform>()
            };
            _digits.Add(data);
        }

        while (_digits.Count > count)
        {
            Destroy(_digits[^1].DigitImage.gameObject);
            _digits.RemoveAt(_digits.Count - 1);
        }

        foreach (var digit in _digits)
        {
            digit.DigitTransform.sizeDelta = _digitBaseSize;
        }
    }

    private void AnimateDigits()
    {
        if (_seq != null && _seq.IsActive())
            _seq.Kill();

        _seq = DOTween.Sequence();

        Vector2 targetSize = _digitBaseSize + new Vector2(0, _settings.sizeAmount);
        foreach (var digit in _digits)
        {
            _seq.Join(digit.DigitTransform
                .DOSizeDelta(targetSize, _settings.duration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutCubic));
        }
    }

    private void UpdateDigitSprites(int value)
    {
        for (int i = _digits.Count - 1; i >= 0; i--)
        {
            int digit = value % 10;

            if (_digits[i] == null)
                Debug.LogError($"_digits[{i}] is null!");
            if (_digits[i].DigitImage == null)
                Debug.LogError($"_digits[{i}].DigitImage is null!");
            _digits[i].DigitImage.sprite = digitSprites[digit];
            value /= 10;
        }
    }
}