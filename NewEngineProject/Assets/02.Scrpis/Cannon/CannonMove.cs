using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMove : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float maxUpAngle = 45f; // 최대 위쪽 회전 각도 (degree)
    public float maxDownAngle = -10f; // 최대 아래쪽 회전 각도 (degree)

    private float currentAngle = 0f;

    void Update()
    {
        // 방향키 입력을 감지하여 대포의 회전 값을 조정합니다.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 현재 회전 각도를 업데이트합니다.
        currentAngle += verticalInput * rotationSpeed * Time.deltaTime;

        // 회전 각도를 일정 범위 내에서 제한합니다.
        currentAngle = Mathf.Clamp(currentAngle, maxDownAngle, maxUpAngle);

        // Cannon 게임 오브젝트의 transform을 사용하여 회전합니다.
        transform.GetChild(0).localRotation = Quaternion.Euler(currentAngle, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
    }
}
