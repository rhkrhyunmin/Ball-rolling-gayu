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
        // Space Ű�� ������ �÷��׸� �����մϴ�.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }

        // Space Ű�� ������, ���� �������� �ʴ� ������ �� ���� �����Դϴ�.
        if (spacePressed && !isMoving)
        {
            isMoving = true;
            rb.velocity = transform.forward * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Space Ű�� ������, ������ ������ �������� �����ϴ�.
        if (spacePressed && other.CompareTag("Boss"))
        {
            EnemyControl enemy = other.GetComponent<EnemyControl>();
            if (enemy != null)
            {
                //enemy.TakeDamage(damage); // ������ �������� ������ �Լ� ȣ��
            }

            // ���� ����ϴ�.
            rb.velocity = Vector3.zero;
            isMoving = false;

            // ���� �ʱ� ��ġ�� �ǵ����ϴ�. (�ʿ信 ���� ���� ����)
            transform.position = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Space Ű�� ���� �� �÷��׸� �ʱ�ȭ�մϴ�.
        if (other.CompareTag("Enemy"))
        {
            spacePressed = false;
        }
    }
}
