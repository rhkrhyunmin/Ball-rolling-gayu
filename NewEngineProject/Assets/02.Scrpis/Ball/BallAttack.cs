using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;

    private bool isMoving = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Space 키를 누를 때 공을 움직이게 합니다.
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving = true;
            rb.velocity = transform.forward * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 적인지 확인하고 데미지를 입힙니다.
        if (other.CompareTag("Enemy"))
        {
            EnemyControl enemy = other.GetComponent<EnemyControl>();
            BeeControl bee = other.GetComponent<BeeControl>();
            if (enemy != null)
            {
                //나중에 할거 (시간남으면)
                //enemy.TakeDamage(damage);
            }

            // 공을 멈춥니다.
            rb.velocity = Vector3.zero;
            isMoving = false;

            // 공을 초기 위치로 되돌립니다. (필요에 따라 수정 가능)
            transform.position = Vector3.zero;
        }
    }
}
