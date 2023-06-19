using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBall : MonoBehaviour
{
    public Transform target; // 목표 지점의 Transform 컴포넌트
    public Transform plane; // 평면의 Transform 컴포넌트
    public Transform initialPosition; // 처음 위치의 Transform 컴포넌트

    private Rigidbody rb;
    private Vector3 initialPositionVector; // 처음 위치 저장 변수

    private bool reachedTarget = false; // 목표 지점 도달 여부

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPositionVector = initialPosition.position; // 처음 위치 저장
    }

    private void FixedUpdate()
    {
        if (!reachedTarget)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // 공과 목표 지점 사이의 방향 벡터 계산
        Vector3 direction = target.position - transform.position;

        // 평면의 법선 벡터 계산
        Vector3 normal = plane.up;

        // 방향 벡터를 평면에 투영
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, normal);

        // 투영된 방향으로 공을 굴러가도록 설정
        rb.AddForce(projectedDirection.normalized * 10f);

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            reachedTarget = true;
            ResetToInitialPosition(); // 목표 지점에 도달하면 처음 위치로 되돌아감
        }
    }

    private void ResetToInitialPosition()
    {
        // 공의 위치를 처음 위치로 설정
        transform.position = initialPositionVector;

        // 공의 속도와 힘 초기화
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep(); // 공의 물리 시뮬레이션 정지

        reachedTarget = false; // 목표 지점 도달 여부 초기화
    }
}
