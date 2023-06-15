using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // 총알 속도
    public float chaseDuration = 1f; // Ball 추적 시간

    private Rigidbody bulletRigidbody;
    public GameObject ballObject; // Ball 오브젝트 참조
    private Vector3 initialDirection;
    private float startTime;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        if (bulletRigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on the bullet!");
        }

        ballObject = GameObject.FindWithTag("Ball");
        if (ballObject == null)
        {
        }

        startTime = Time.time;
        initialDirection = transform.forward;
    }

    private void FixedUpdate()
    {
        if (Time.time - startTime <= chaseDuration)
        {
            ChaseBall();
        }
        else
        {
            MoveForward();
        }
    }

    private void ChaseBall()
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
    }

    private void MoveForward()
    {
        Vector3 moveAmount = transform.forward * bulletSpeed * Time.fixedDeltaTime;
        bulletRigidbody.MovePosition(transform.position + moveAmount);

        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
    }
}
