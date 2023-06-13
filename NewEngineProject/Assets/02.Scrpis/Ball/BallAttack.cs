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
        // Space Ű�� ���� �� ���� �����̰� �մϴ�.
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving = true;
            rb.velocity = transform.forward * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ������ Ȯ���ϰ� �������� �����ϴ�.
        if (other.CompareTag("Enemy"))
        {
            EnemyControl enemy = other.GetComponent<EnemyControl>();
            BeeControl bee = other.GetComponent<BeeControl>();
            if (enemy != null)
            {
                //���߿� �Ұ� (�ð�������)
                //enemy.TakeDamage(damage);
            }

            // ���� ����ϴ�.
            rb.velocity = Vector3.zero;
            isMoving = false;

            // ���� �ʱ� ��ġ�� �ǵ����ϴ�. (�ʿ信 ���� ���� ����)
            transform.position = Vector3.zero;
        }
    }
}
