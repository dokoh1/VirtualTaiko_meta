using UnityEngine;

public class Note : MonoBehaviour
{
    public float notespeed = 300f;
    void Update()
    {
        transform.localPosition -= Vector3.right * notespeed * Time.deltaTime;
    }
}
