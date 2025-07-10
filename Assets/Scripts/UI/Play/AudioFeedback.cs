using UnityEngine;
using UnityEngine.Serialization;

public class AudioFeedback : MonoBehaviour, IAudioFeedback
{
    public AudioClip combo10;
    public AudioClip combo30;
    public AudioClip combo50;
    public AudioClip combo100;
    public AudioClip combo200;
    
    public void PlayComboSound(int combo)
    {
        AudioClip audioClip = null;
        switch (combo)
        {
            case 10 : audioClip = combo10; break;
            case 30 : audioClip = combo30; break;
            case 50 : audioClip = combo50; break;
            case 100 : audioClip = combo100; break;
            case 200 : audioClip = combo200; break;
        }
        if (audioClip != null)
            Single.System.AudioManager.PlaySFX(audioClip);
    }
    
}
