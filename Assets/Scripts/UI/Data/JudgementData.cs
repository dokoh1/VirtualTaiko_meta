using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game/JudgementData")]
public class JudgementData : ScriptableObject
{
    public int rapidHitScore;
    public int greatScore;
    public int goodScore;
    public int badScore;
    
    //Combo 점수
    public int rapidHitComboScore;
    public int greatComboScore;
    public int goodComboScore;
    public int badComboScore;
}
