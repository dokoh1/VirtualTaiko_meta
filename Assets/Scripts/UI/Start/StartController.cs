using UnityEngine;
using UnityEngine.Serialization;

public class StartController : MonoBehaviour
{
    // public TestDrumInput Input;
    private bool _isChanged;
    public AudioClip backgroundMusic;
    public AudioClip sfxMusic;

    void OnEnable()
    {
        _isChanged = false;
        Single.System.AudioManager.PlayBGM(backgroundMusic);
        Single.System.AudioManager.PlaySFX(sfxMusic);
    }
    void Update()
    {
        //Execute Code
        DrumDataType drumDataType = Single.System.DrumManager.UseQueue();
        if (drumDataType == DrumDataType.RightFace || drumDataType == DrumDataType.LeftFace)
        {
            _isChanged = true;
            Single.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        }
    }
}
