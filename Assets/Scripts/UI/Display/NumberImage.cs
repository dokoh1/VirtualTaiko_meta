using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberImage : MonoBehaviour
{
    public Sprite[] digitSprites;
    public GameObject digitPrefab;
    private List<Image> digits = new List<Image>();

    public void UpdateDisplay(int currentScore)
    {
        int value = currentScore;
        int needed = Mathf.Max(1, value == 0 ? 1 : (int)Mathf.Floor(Mathf.Log10(value)) + 1);
        
        while (digits.Count < needed)
        {
            var go = Instantiate(digitPrefab, transform);
            digits.Add(go.GetComponent<Image>());
        }

        while (digits.Count < needed)
        {
            Destroy(digits[digits.Count - 1].gameObject);
            digits.RemoveAt(digits.Count - 1);
        }

        for (int i = digits.Count - 1; i >= 0; i--)
        {
            int digit = value % 10;
            digits[i].sprite = digitSprites[digit];
            value /= 10;
        }
    }
}
