using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float minRotationTime = 1.0f; // 최소 회전 시간
    public float maxRotationTime = 5.0f; // 최대 회전 시간
    public float rotationAngle = 90.0f; // 회전 각도

    private bool rotating; // 회전 여부

    private Quaternion initialRotation; // 초기 회전 상태

    private void Start()
    {
        // 초기 회전 상태 저장
        initialRotation = transform.rotation;

        rotating = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ball") && GameManager.Instance.isKey)
        {
            rotating = true;
            GameManager.Instance.isKey = false;
        }
    }

    private void Update()
    {

        if (rotating)
        {
            Debug.Log("99");
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
