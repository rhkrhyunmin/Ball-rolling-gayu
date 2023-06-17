using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBallMove : MonoBehaviour
{
    public Transform target;
    public float initialMovementSpeed = 2f; // �ʱ� �����̴� ���ǵ�
    public float maxMovementSpeed = 15f; // �ִ� �����̴� ���ǵ�
    public float speedIncrement = 3f; // ���ǵ��� ������
    public float stoppingDistance = 10f; // ������ �Ÿ�
    public float bounceForce = 10f; // ƨ�� ��
    
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
            // ���� ���� �̵��ϴ� ���� ���
            Vector3 direction = (target.position - transform.position).normalized;

            float distance = Vector3.Distance(transform.position, target.position);

            // ������ �Ÿ��� ������ �Ÿ����� ũ�� �̵�
            if (distance > stoppingDistance)
            {
                // ���� �������� �̵�
                transform.Translate(direction * currentMovementSpeed * Time.deltaTime);
                rb.velocity = Vector3.zero; // ƨ�� �� ������ �ӵ��� �ʱ�ȭ

                // �����̴� ���ǵ带 ���� ������Ŵ
                currentMovementSpeed = Mathf.Min(currentMovementSpeed + speedIncrement * Time.deltaTime, maxMovementSpeed);
            }
            else
            {
                transform.Translate(Vector3.zero);
                currentMovementSpeed = 0f; // ������ �Ÿ��� �����ϸ� speed�� 0���� ����
                stoppingDistance = 25;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �÷��̾�� �浹�� ��쿡�� ƨ�� ȿ�� ����
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            rb.AddForce(-direction * bounceForce, ForceMode.Impulse);
            currentMovementSpeed = 5;
            Destroy(gameObject, 3f);
        }
    }
}
