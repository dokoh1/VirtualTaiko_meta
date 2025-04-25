using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private float targetTime;
    private float musicStartTime;
    private float speed;

    public void Initialize(float noteTime, float musicStart, float scrollSpeed)
    {
        targetTime = noteTime;
        musicStartTime = musicStart;
        speed = scrollSpeed;
    }

    void Update()
    {
        float currentTime = Time.time - musicStartTime;
        float timeToHit = targetTime - currentTime;

        // 앞으로 이동 (판정라인 기준으로)
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
