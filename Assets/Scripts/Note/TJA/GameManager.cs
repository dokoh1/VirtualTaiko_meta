using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TjaParser parser;
    public NoteSpawner spawner;
    public AudioSource musicSource;

    void Start()
    {
        // 파싱이 끝난 이후에 Spawner 초기화
        spawner.Initialize(parser.notes);

        // 음악 시작과 동시에 노트 스폰 시작
        musicSource.Play();
        spawner.StartMusic();
    }
}