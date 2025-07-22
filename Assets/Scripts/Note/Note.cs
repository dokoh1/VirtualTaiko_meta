using UnityEngine;

public class Note : MonoBehaviour
{
    public HitResult currentZone = HitResult.Bad;
    public NoteType noteType;
    
    public float notespeed = 300f;

    private void Update()
    {
        transform.localPosition -= Vector3.right * (notespeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var zone = other.GetComponent<JudgementZone>();
        if (zone != null)
            currentZone = zone.zoneType;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var zone = other.GetComponent<JudgementZone>();
        if (zone != null && zone.zoneType == currentZone)
            currentZone = HitResult.Bad;
    }
}