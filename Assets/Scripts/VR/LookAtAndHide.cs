using UnityEngine;

public class LookAtAndHide : MonoBehaviour
{
    [SerializeField]
    private Transform target; // A 오브젝트 (바라볼 대상)

    [SerializeField, Range(0f, 20f)]
    private float hideDistance = 5f; // 특정 거리 이하일 때 숨기기

    private Renderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

   private void Update()
{
    if (target == null) return;

    Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

    transform.LookAt(targetPosition);

    float distance = Vector3.Distance(transform.position, target.position);

    if (myRenderer != null)
        myRenderer.enabled = distance > hideDistance;
}
}
