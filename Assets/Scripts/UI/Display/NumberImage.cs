using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class NumberImage : MonoBehaviour
{
    public Sprite[] digitSprites;
    public GameObject digitPrefab;
    private List<NumberData> digits = new();
    private Sequence seq;
    public float sizeAmount;
    public float duration;
    private int saveScore;
    private float originalWidth;
    private float originalHeight;


    public void UpdateDisplay(int currentScore)
    {
        if (saveScore == currentScore) return;
        int value = currentScore;
        int needed = Mathf.Max(1, value == 0 ? 1 : (int)Mathf.Floor(Mathf.Log10(value)) + 1);
        
        while (digits.Count < needed)
        {
            var go = Instantiate(digitPrefab, transform);
            NumberData data = new();
            data.DigitImage = go.GetComponent<Image>();
            data.DigitTransform = go.GetComponent<RectTransform>();
            originalWidth = data.DigitTransform.sizeDelta.x;
            originalHeight = data.DigitTransform.sizeDelta.y;
            digits.Add(data);
        }
        
        while (digits.Count > needed)
        {
            Destroy(digits[digits.Count - 1].DigitImage.gameObject);
            digits.RemoveAt(digits.Count - 1);
        }
        Vector2 baseSize = new(originalWidth, originalHeight);
        if (seq != null && seq.IsActive())
        {
            seq.Kill();
        }

        for (int i = 0; i < digits.Count; i++)
        {
            digits[i].DigitTransform.sizeDelta = baseSize;
        }
        seq = DOTween.Sequence();
        for (int i = 0; i < digits.Count; i++)
        {
            var targetSize = new Vector2(baseSize.x, baseSize.y + sizeAmount);
            seq.Join(digits[i].DigitTransform.DOSizeDelta(targetSize, duration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutCubic));
        }
        for (int i = digits.Count - 1; i >= 0; i--)
        {
            int digit = value % 10;
            digits[i].DigitImage.sprite = digitSprites[digit];
            value /= 10;
        }

        saveScore = value;
    }
}
