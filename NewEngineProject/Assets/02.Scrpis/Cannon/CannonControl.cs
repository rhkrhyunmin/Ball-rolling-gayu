using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    public float force = 10f;  // 스페이스바 누른 힘
    public float trajectoryDuration = 2f;  // 포물선이 그려지는 시간
    public float dotInterval = 0.1f;  // 점선을 그릴 간격
    //public GameObject dotPrefab;  // 점선에 사용될 프리팹

    private Rigidbody rb;  // 이 오브젝트의 Rigidbody 컴포넌트
    private LineRenderer lineRenderer;  // 라인 렌더러 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShowTrajectory());
        }
    }

    IEnumerator ShowTrajectory()
    {
        Vector3 initialVelocity = CalculateInitialVelocity();
        float timeStep = dotInterval / initialVelocity.magnitude;
        float remainingTime = trajectoryDuration;

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        while (remainingTime > 0)
        {
            Vector3 displacement = initialVelocity * timeStep + 0.5f * Physics.gravity * timeStep * timeStep;
            initialVelocity += Physics.gravity * timeStep;
            remainingTime -= timeStep;

            Vector3 nextPos = transform.position + displacement;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, nextPos);

            yield return new WaitForSeconds(timeStep);
        }

        lineRenderer.enabled = false;
    }

    Vector3 CalculateInitialVelocity()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z - transform.position.z;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = worldMousePos - transform.position;
        direction.Normalize();

        return direction * force;
    }

    void OnCollisionEnter(Collision collision)
    {
        lineRenderer.enabled = false;
    }
}
