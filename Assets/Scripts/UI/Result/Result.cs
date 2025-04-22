using System;
using UnityEngine;

namespace dokoh
{
    public class Result  : MonoBehaviour
    {
        [SerializeField] private NumberImage Perfect;
        [SerializeField] private NumberImage Good;
        [SerializeField] private NumberImage Bad;
        [SerializeField] private NumberImage Hit;
        [SerializeField] private NumberImage Combo;
        [SerializeField] private NumberImage Score;

        private void OnEnable()
        {
            Perfect.UpdateDisplay(dokoh.System.ScoreManager.Perfect);
            Good.UpdateDisplay(dokoh.System.ScoreManager.Good);
            Bad.UpdateDisplay(dokoh.System.ScoreManager.Bad);
            Hit.UpdateDisplay(dokoh.System.ScoreManager.Hit);
            Combo.UpdateDisplay(dokoh.System.ScoreManager.Combo);
            Score.UpdateDisplay(dokoh.System.ScoreManager.Score);
        }
    }
}
