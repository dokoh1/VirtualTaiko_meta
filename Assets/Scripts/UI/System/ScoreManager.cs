using UnityEngine;

namespace dokoh
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; set; } = 0;
        public int Hit { get; set; } = 0;
        public int Combo { get; set; } = 0;
        public int Perfect { get; set; } = 0;
        public int Good { get; set; } = 0;
        public int Bad { get; set; } = 0;
    }
}
