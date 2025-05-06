using System.Collections;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;

    [Header("Light Control")]
    public float lightActivationDistance = 20f;
    public float shadowEnableDistance = 10f;
    public float updateInterval = 0.5f;
    public float maxLightRange = 30f;   // 라이트 최대 범위
    public float maxLightIntensity = 1f; // 라이트 최대 세기

    private float yaw = 0f;
    private float pitch = 0f;
    private float timer = 0f;
    private Light[] allLights;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(InitializeLights());
    }

    IEnumerator InitializeLights()
    {
        yield return null; // 도메인 백업 에러 방지

        allLights = Object.FindObjectsByType<Light>(FindObjectsSortMode.None);

        // 라이트 컴포넌트 비활성화
        foreach (var light in allLights)
        {
            if (light && light.type != LightType.Directional)
            {
                light.gameObject.SetActive(false);  // 라이트 컴포넌트를 비활성화
            }
        }

        UpdateLights(); // 처음에 근처 라이트만 활성화
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();

        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;
            UpdateLights();
        }
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); // A, D
        float v = Input.GetAxis("Vertical");   // W, S
        float upDown = 0f;

        if (Input.GetKey(KeyCode.E)) upDown += 1f;
        if (Input.GetKey(KeyCode.Q)) upDown -= 1f;

        Vector3 direction = (transform.forward * v + transform.right * h + transform.up * upDown).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void HandleMouseLook()
    {
        yaw += Input.GetAxis("Mouse X") * lookSpeed;
        pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void UpdateLights()
    {
        foreach (var light in allLights)
        {
            if (!light || light.type == LightType.Directional)
                continue;

            float dist = Vector3.Distance(transform.position, light.transform.position);
            bool inRange = dist < lightActivationDistance;

            // 라이트가 범위 내에 있으면 활성화
            if (inRange)
            {
                light.gameObject.SetActive(true); // 라이트 컴포넌트를 활성화

                light.shadows = (dist < shadowEnableDistance) ? LightShadows.Soft : LightShadows.None;

                // 라이트의 범위와 세기 조정
                float rangeFactor = Mathf.InverseLerp(0f, lightActivationDistance, dist);
                light.range = Mathf.Lerp(0f, maxLightRange, 1f - rangeFactor);
                light.intensity = Mathf.Lerp(0f, maxLightIntensity, 1f - rangeFactor);
            }
            else
            {
                light.gameObject.SetActive(false); // 라이트 컴포넌트를 비활성화
            }
        }
    }
}
