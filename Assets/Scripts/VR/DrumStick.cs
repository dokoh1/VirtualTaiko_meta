using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DrumStick : MonoBehaviour
{

    public AudioClip clip;
    private AudioSource source;
    private LayerMask Stick;

    public delegate void AudiovisualFunction();
    AudiovisualFunction Audio;

    public bool randomizePitch = true;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    [Range(0, 1)]
    [SerializeField]
    private float intensity = 0.5f;
    [Range(0, 1)]
    [SerializeField]
    private float duration = 0.2f;

    public HapticImpulsePlayer leftControll;
    public HapticImpulsePlayer rightControll;
    private bool canHit = true;

    private void Awake()
    {
        Stick = LayerMask.NameToLayer("StickBody");
        source = GetComponent<AudioSource>();

    }

    private void Start()
    {
        Audio += PlayAudio;
        Audio += ControllPitch;
        Audio += PlayVibration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canHit) return;

        if (other.gameObject.layer == Stick)
        {
            canHit = false;
            Audio();
            Invoke(nameof(ResetHit), 0.1f);
        }
    }

    private void PlayAudio()
    {
        source.PlayOneShot(clip, 1.0f);
    }

    private void ControllPitch()
    {
        if (randomizePitch)
        {
            source.pitch = Random.Range(minPitch, maxPitch);
        }
    }

    private void PlayVibration()
    {
        leftControll.SendHapticImpulse(intensity, duration);
        rightControll.SendHapticImpulse(intensity, duration);
    }

    private void ResetHit()
    {
        canHit = true;
    }
}
