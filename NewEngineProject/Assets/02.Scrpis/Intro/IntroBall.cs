using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBall : MonoBehaviour
{
    public Transform target; // 목표 지점의 Transform 컴포넌트
    public Transform plane; // 평면의 Transform 컴포넌트

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 공과 목표 지점 사이의 방향 벡터 계산
        Vector3 direction = target.position - transform.position;

        // 평면의 법선 벡터 계산
        Vector3 normal = plane.up;

        // 방향 벡터를 평면에 투영
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, normal);

        // 투영된 방향으로 공을 굴러가도록 설정
        rb.AddForce(projectedDirection.normalized * 10f);
    }
}
