using UnityEngine;

public class Note : MonoBehaviour
{
    public float notespeed = 300f;
    public HitResult currentZone = HitResult.Bad;
    public NoteType noteType; // 🔸 노트 타입 추가
    private bool isHit = false;

    void Update()
    {
        transform.localPosition -= Vector3.right * (notespeed * Time.deltaTime);
    }

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

    // 🔹 입력 판정 처리 함수
    public void TryHit(KeyCode inputKey, TimingManager timingManager)
    {
        if (isHit || currentZone == HitResult.Bad)
            return;

        bool matched = false;

        switch (noteType)
        {
            case NoteType.smallRed:
                if (inputKey == KeyCode.S || inputKey == KeyCode.K)
                    matched = true;
                break;

            case NoteType.smallBlue:
                if (inputKey == KeyCode.A || inputKey == KeyCode.L)
                    matched = true;
                break;

            case NoteType.bigRed:
                if ((inputKey == KeyCode.S && inputKey == KeyCode.K)) // 둘 중 하나만 쳐도 판정
                    matched = true;
                break;

            case NoteType.bigBlue:
                if ((inputKey == KeyCode.A && inputKey == KeyCode.L))
                    matched = true;
                break;
        }

        if (matched)
        {
            HitResult result = currentZone;
            Debug.Log($"{noteType} Hit! Zone: {currentZone} → Result: {result}");

            timingManager.ProcessResult(result);
            isHit = true;
            Destroy(gameObject);
        }
    }
}