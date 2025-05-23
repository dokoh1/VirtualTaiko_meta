using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using System.Collections.Generic;

public class Drums : MonoBehaviour
{

    public delegate void AudioFunction();
    VisualFunction Audio;
    public delegate void VisualFunction();
    AudioFunction Visual;
    private LayerMask leftStick;
    private LayerMask rightStick;

    // Audio
    public AudioClip clip;
    public bool randomizePitch = true;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
    private AudioSource source;
    private float volum;
    
    // Velocity 
    public VelocityEstimator estimator;
    public bool useVelocity = true;
    public float minVelocity = 0f;
    public float maxVelocity = 2f;

    // vibration
    public HapticImpulsePlayer leftControll;
    public HapticImpulsePlayer rightControll;
    [Range(0, 1)]
    [SerializeField]
    private float intensity = 0.5f;
    [Range(0, 1)]
    [SerializeField]
    private float duration = 0.2f;

    // 판정 보정
    [Range(0, 1f)]
    [SerializeField]
    private float delay = 0.2f;
    
    private bool leftHit = false;
    private bool rightHit = false;
    
    // 코루틴 리셋
    private Coroutine resetCoroutine = null;

    // 파티클시스템
    [SerializeField]
    private VisualEffect RedWave;
    [SerializeField]
    private VisualEffect lightFace;



    private void Awake()
    {
        leftStick = LayerMask.NameToLayer("LeftStick");
        rightStick = LayerMask.NameToLayer("RightStick");
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Audio += UseStickVelocity;
        Audio += PlayAudio;
        Audio += ControllPitch;
        Audio += PlayWaveParticle;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("true");
        estimator = other.GetComponent<VelocityEstimator>();


        //  데이터 저장
        if (other.gameObject.layer == leftStick)
        {
            leftHit = true;
            dokoh.System.DrumManager.AddQueue(DrumDataType.LeftFace);
            //print(dataSet);
            PlayLeftVibration();
            Audio();
        }


        if (other.gameObject.layer == rightStick)
        {
            rightHit = true;
            dokoh.System.DrumManager.AddQueue(DrumDataType.RightFace);
            //print(dataSet);
            PlayRightVibration();
            Audio();
        }


        if (rightHit && leftHit)
        {
            dokoh.System.DrumManager.AddQueue(DrumDataType.DobletFace);
            // Audio();
        }

        // 코루틴 재시작
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }
        resetCoroutine = StartCoroutine(NotHit());
    }

    IEnumerator NotHit()
    {
        yield return new WaitForSeconds(delay);
        leftHit = false;
        rightHit = false;
        //print(dataSet);
    }


    private void PlayAudio()
    {
        source.PlayOneShot(clip, volum * 1.5f);
    }

    private void UseStickVelocity()
    {
        if (estimator && useVelocity)
        {
            float v = estimator.GetVelocityEstimate().magnitude;
            volum = Mathf.InverseLerp(minVelocity, maxVelocity, v);
        }
        else
        {
            volum = 1.0f;
        }
    }

    private void ControllPitch()
    {
        if (randomizePitch)
        {
            source.pitch = Random.Range(minPitch, maxPitch);
        }
    }

    private void PlayLeftVibration()
    {
        leftControll.SendHapticImpulse(intensity * volum, duration);
    }

    private void PlayRightVibration()
    {
        rightControll.SendHapticImpulse(intensity * volum, duration);
    }

    private void PlayWaveParticle()
    {
      RedWave.Play();
      lightFace.Play();
    }

}
