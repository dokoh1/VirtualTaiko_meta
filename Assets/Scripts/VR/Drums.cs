using System.Collections;
using JetBrains.Annotations;
using Unity.XR.Oculus.Input;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
//  나중에 함수별로 코드 분리할 예정
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

    public DrumDataType dataSet = DrumDataType.NotHit;
    private bool leftHit = false;
    private bool rightHit = false;
    
    // 코루틴 리셋
    private Coroutine resetCoroutine = null;

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("true");
        estimator = other.GetComponent<VelocityEstimator>();

        //  데이터 저장
        if (other.gameObject.layer == leftStick)
        {
            leftHit = true;
            dataSet = DrumDataType.LeftFace;
            //print(dataSet);
            PlayLeftVibration();
            Audio();
        }


        if (other.gameObject.layer == rightStick)
        {
            rightHit = true;
            dataSet = DrumDataType.RightFace;
            //print(dataSet);
            PlayRightVibration();
            Audio();
        }


        if (rightHit && leftHit)
        {
            dataSet = DrumDataType.DobletFace;
            //print(dataSet);
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
        dataSet = DrumDataType.NotHit;
        //print(dataSet);
    }


    private void PlayAudio()
    {
        source.PlayOneShot(clip, volum);
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

}
