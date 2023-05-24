using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float minRotationTime = 1.0f; // 최소 회전 시간
    public float maxRotationTime = 5.0f; // 최대 회전 시간
    public float rotationAngle = 90.0f; // 회전 각도

    private float nextRotationTime; // 다음 회전까지의 시간
    private bool rotating; // 회전 여부

    private Quaternion initialRotation; // 초기 회전 상태

    private void Start()
    {
        // 초기 회전 상태 저장
        initialRotation = transform.rotation;

        // 첫 회전까지의 시간 설정
        nextRotationTime = Random.Range(minRotationTime, maxRotationTime);
        rotating = false;
    }

    private void Update()
    {
        // 회전 타이머 업데이트
        nextRotationTime -= Time.deltaTime;

        if (nextRotationTime <= 0)
        {
            // 회전할 시간이 되면 회전 상태 변경
            rotating = !rotating;
            nextRotationTime = Random.Range(minRotationTime, maxRotationTime);
        }

        if (rotating)
        {
            // 회전 중이면 회전 실행
            transform.rotation = initialRotation * Quaternion.Euler(0, 0, rotationAngle);
        }
        else
        {
            // 회전 중이 아니면 회전을 중지
            transform.rotation = initialRotation;
        }
    }
}
