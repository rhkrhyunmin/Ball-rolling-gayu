using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;

    private bool isMoving = false;
    private bool spacePressed = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Space 키를 누르면 플래그를 설정합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }

        // Space 키를 누르고, 공이 움직이지 않는 상태일 때 공을 움직입니다.
        if (spacePressed && !isMoving)
        {
            isMoving = true;
            rb.velocity = transform.forward * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Space 키를 누르고, 적에게 닿으면 데미지를 입힙니다.
        if (spacePressed && other.CompareTag("Boss"))
        {
            EnemyControl enemy = other.GetComponent<EnemyControl>();
            if (enemy != null)
            {
                //enemy.TakeDamage(damage); // 적에게 데미지를 입히는 함수 호출
            }

            // 공을 멈춥니다.
            rb.velocity = Vector3.zero;
            isMoving = false;

            // 공을 초기 위치로 되돌립니다. (필요에 따라 수정 가능)
            transform.position = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Space 키를 뗐을 때 플래그를 초기화합니다.
        if (other.CompareTag("Enemy"))
        {
            spacePressed = false;
        }
    }
}
