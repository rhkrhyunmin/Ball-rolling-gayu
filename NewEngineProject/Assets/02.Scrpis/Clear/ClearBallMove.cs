using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBallMove : MonoBehaviour
{
    public Transform target;
    public float movementSpeed = 5f;
    public float stoppingDistance = 1f; // 도착할 거리

    private void Update()
    {
        if (target != null)
        {
            // 적을 향해 이동하는 방향 계산
            Vector3 direction = (target.position - transform.position).normalized;

            float distance = Vector3.Distance(transform.position, target.position);

            // 적과의 거리가 도착할 거리보다 크면 이동
            if (distance > stoppingDistance)
            {
                // 계산된 방향으로 이동
                transform.Translate(direction * movementSpeed * Time.deltaTime);
            }
        }
    }
}
