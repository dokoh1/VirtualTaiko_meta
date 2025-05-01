using UnityEngine;

public class StartController : MonoBehaviour
{
    // public TestDrumInput Input;
    private bool isChanged;
    public AudioClip BackgroundMusic;
    public AudioClip SFXMusic;

    void OnEnable()
    {
        isChanged = false;
        dokoh.System.AudioManager.PlayBGM(BackgroundMusic);
        dokoh.System.AudioManager.PlaySFX(SFXMusic);
    }
    void Update()
    {
        //Execute Code
        DrumDataType drumDataType = dokoh.System.DrumManager.UseQueue();
        if (drumDataType == DrumDataType.RightFace)
        {
            isChanged = true;
            dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        }
        
        //Test Code
        // if (!isChanged && Input.testInputType == DrumDataType.RightFace)
        // {
        //     isChanged = true;
        //     dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        // }
    }
}
