using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBallMove : MonoBehaviour
{
    public Transform target;
    public float movementSpeed = 5f;
    public float stoppingDistance = 1f; // ������ �Ÿ�

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
                transform.Translate(direction * movementSpeed * Time.deltaTime);
            }
        }
    }
}
