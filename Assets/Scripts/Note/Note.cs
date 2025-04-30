using UnityEngine;

public class Note : MonoBehaviour
{
    public float notespeed = 300f;
    void Update()
    {
        transform.localPosition -= Vector3.right * (notespeed * Time.deltaTime);
    }
    public HitResult currentZone = HitResult.Bad;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var zone = other.GetComponent<JudgementZone>();
        if (zone != null)
        {
            currentZone = zone.zoneType;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var zone = other.GetComponent<JudgementZone>();
        if (zone != null && zone.zoneType == currentZone)
        {
            currentZone = HitResult.Bad;  // 빠져나오면 리셋
        }
    }
}