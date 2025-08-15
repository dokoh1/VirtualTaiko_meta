using System;
using UnityEngine;

public class StartView : MonoBehaviour
{
    public event Action OnInputReceived;
    
    public AudioClip backgroundMusic;
    public AudioClip sfxMusic;

    void OnEnable()
    {
        Single.System.AudioManager.PlayBGM(backgroundMusic);
        Single.System.AudioManager.PlaySFX(sfxMusic);
    }
    
    void Update()
    {
        //Execute Code
        DrumDataType drumDataType = Single.System.InputManager.GetInput();
        if (drumDataType != DrumDataType.NotHit)
            OnInputReceived?.Invoke();
    }
}
