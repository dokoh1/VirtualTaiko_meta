using System.Collections;
using JetBrains.Annotations;
using Unity.XR.Oculus.Input;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Drums : MonoBehaviour
{
    public delegate void AudiovisualFunction();
    AudiovisualFunction audioVisual;
    private LayerMask leftStick;
    private LayerMask rightStick;

    // Audio
    public AudioClip clip;
    private AudioSource source;
    private float volum;
    public bool randomizePitch = true;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    // Velocity 
    public bool useVelocity = true;
    public float minVelocity = 0f;
    public float maxVelocity = 2f;
    public VelocityEstimator estimator;

    // vibration
    [Range(0, 1)]
    [SerializeField]
    private float intensity = 0.5f;
    [Range(0, 1)]
    [SerializeField]
    private float duration = 0.2f;

    public HapticImpulsePlayer leftControll;
    public HapticImpulsePlayer rightControll;

   
    // 판정 보정
    [Range(0, 0.2f)]
    [SerializeField]
    private float delay = 0.1f;    

    private string dateSet = "notHit";
    private bool leftHit = false;
    private bool rightHit = false;

    private Coroutine resetCoroutine;
    
   
    private void Awake()
    {
        leftStick = LayerMask.NameToLayer("LeftStick");
        rightStick = LayerMask.NameToLayer("RightStick");
        source = GetComponent<AudioSource>();     
    }

    private void Start()
    {
        audioVisual += UseStickVelocity;
        audioVisual += PlayAudio;
        audioVisual += ControllPitch;
        audioVisual += PlayVibration;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        print("true");
        if (other.gameObject.layer == leftStick || other.gameObject.layer == rightStick )
        {
            estimator = other.GetComponent<VelocityEstimator>();
            audioVisual();
        }

        //  데이터 저장
        if(other.gameObject.layer == leftStick)
        {
           leftHit = true;
           dateSet = "leftHit";
        }

    
        if(other.gameObject.layer == rightStick)
        {
           rightHit = true;
           dateSet = "rightHit";
        }


        if(rightHit && leftHit)
        {
           dateSet = "dobletHit";
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
        dateSet = "notHit";
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

     private void PlayVibration()
    {
        leftControll.SendHapticImpulse(intensity *  volum, duration);
        rightControll.SendHapticImpulse(intensity *  volum, duration);
    }        

}
