using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 15f; // 총알 속도

    private Rigidbody bulletRigidbody;
    public GameObject ballObject; // Ball 오브젝트 참조
    private Vector3 initialDirection;
    private float startTime;
    private float chaseDuration = 1f; // Ball 추적 시간
    private float chaseEndTime; // Ball 추적 종료 시간
    //public Vector3 muzzle; // 총구(GameObj) 변수 추가


    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        if (bulletRigidbody == null)
        {

        }

        ballObject = GameObject.FindWithTag("Ball");
        if (ballObject == null)
        {
        }

        startTime = Time.time;
        initialDirection = transform.forward;
        chaseEndTime = Time.time + chaseDuration;

    }

    private void FixedUpdate()
    {
        if (Time.time < chaseEndTime)
        {
            ChaseBall();
        }
        else
        {
            MoveForward();
        }
    }

    public void ChaseBall()
    {
        if (ballObject == null)
        {
            MoveForward();
            return;
        }

        Vector3 ballDirection = (ballObject.transform.position - transform.position).normalized;
        Vector3 newDirection = Vector3.Lerp(initialDirection, ballDirection, (Time.time - startTime) / chaseDuration).normalized;
        bulletRigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
        MoveForward();

        if (Time.time >= chaseEndTime)
        {
            bulletRigidbody.MoveRotation(Quaternion.LookRotation(initialDirection));
        }
    }

    public void MoveForward()
    {
        Vector3 moveAmount = transform.forward * bulletSpeed * Time.deltaTime;
        bulletRigidbody.MovePosition(transform.position + moveAmount);
        Destroy(gameObject, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
