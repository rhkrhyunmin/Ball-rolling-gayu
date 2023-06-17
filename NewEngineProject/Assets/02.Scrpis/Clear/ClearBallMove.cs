using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBallMove : MonoBehaviour
{
    public Transform target;
    public float initialMovementSpeed = 2f; // 초기 움직이는 스피드
    public float maxMovementSpeed = 15f; // 최대 움직이는 스피드
    public float speedIncrement = 3f; // 스피드의 증가량
    public float stoppingDistance = 10f; // 도착할 거리
    public float bounceForce = 10f; // 튕김 힘
    
    private Rigidbody rb;

    private float currentMovementSpeed;

    private void Start()
    {
        currentMovementSpeed = initialMovementSpeed;
        rb = GetComponent<Rigidbody>();
    }

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
                transform.Translate(direction * currentMovementSpeed * Time.deltaTime);
                rb.velocity = Vector3.zero; // 튕길 때 이전의 속도를 초기화

                // 움직이는 스피드를 점점 증가시킴
                currentMovementSpeed = Mathf.Min(currentMovementSpeed + speedIncrement * Time.deltaTime, maxMovementSpeed);
            }
            else
            {
                transform.Translate(Vector3.zero);
                currentMovementSpeed = 0f; // 적과의 거리가 도달하면 speed를 0으로 설정
                stoppingDistance = 25;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체가 플레이어와 충돌한 경우에만 튕김 효과 적용
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            rb.AddForce(-direction * bounceForce, ForceMode.Impulse);
            currentMovementSpeed = 5;
            Destroy(gameObject, 3f);
        }
    }
}
