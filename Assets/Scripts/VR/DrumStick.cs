using UnityEngine;

public class DrumStick : MonoBehaviour
{

    public AudioClip clip;
    private AudioSource source;

    public delegate void AudiovisualFunction();
    AudiovisualFunction Audiovisual;

    private void Awake()
    {
        // Stick = LayerMask.NameToLayer("Stick");
        source = GetComponent<AudioSource>();

    }

    private void Start()
    {
        Audiovisual += PlayAudio;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stick")
        {
            Audiovisual();
        }
    }

    private void PlayAudio()
    {
        source.PlayOneShot(clip, 1.0f);
    }
}
