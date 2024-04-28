using UnityEngine;

public class MoveSkyBox : MonoBehaviour
{
    public Skybox _Skybox;
    public float rotationSpeed = 1.0f;

    void Update()
    {
        // 현재 스카이박스 회전값을 저장합니다.
        Quaternion currentRotation = _Skybox.transform.rotation;

        // 스카이박스를 새로운 회전값으로 설정합니다.
        _Skybox.transform.rotation = currentRotation * Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
    }
}
