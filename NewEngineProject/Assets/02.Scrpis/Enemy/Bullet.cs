using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // ÃÑ¾Ë ¼Óµµ

    private Rigidbody bulletRigidbody;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        Vector3 forwardDirection = transform.forward;
        Vector3 moveAmount = forwardDirection * bulletSpeed * Time.fixedDeltaTime;

        bulletRigidbody.MovePosition(transform.position + moveAmount);
    }
}
