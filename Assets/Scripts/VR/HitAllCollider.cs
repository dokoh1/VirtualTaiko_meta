using UnityEngine;
using UnityEngine.VFX;

public class HitAllCollider : MonoBehaviour
{
    [SerializeField]
    private VisualEffect hitParticle;
    [SerializeField]
    private Transform spawnPoint;
    // private Rigidbody rb;

    private void Awake()
    {
        // rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"충돌 발생! 상대 오브젝트: {collision.gameObject.name}");

        // rb.linearVelocity = Vector3.zero;    // 이동 속도 0
        // rb.angularVelocity = Vector3.zero; // 회전 속도 0

        VisualEffect vfxInstance = Instantiate(hitParticle, spawnPoint.transform.position, transform.rotation);

        vfxInstance.Play();
        Destroy(vfxInstance.gameObject, 1f); 
    }
}
