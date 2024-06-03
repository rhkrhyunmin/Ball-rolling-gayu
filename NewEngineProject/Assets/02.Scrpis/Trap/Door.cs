using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float minRotationTime = 1.0f; // �ּ� ȸ�� �ð�
    public float maxRotationTime = 5.0f; // �ִ� ȸ�� �ð�
    public float rotationAngle = 90.0f; // ȸ�� ����

    private bool rotating; // ȸ�� ����

    private Quaternion initialRotation; // �ʱ� ȸ�� ����

    private void Start()
    {
        // �ʱ� ȸ�� ���� ����
        initialRotation = transform.rotation;

        rotating = false;
    }

    private void Update()
    {

        if (GameManager.Instance.isBoss)
        {
            rotating = !rotating;
        }

        if (rotating)
        {
            // ȸ�� ���̸� ȸ�� ����
            transform.rotation = initialRotation * Quaternion.Euler(0, 0, rotationAngle);
        }
        else
        {
            // ȸ�� ���� �ƴϸ� ȸ���� ����
            transform.rotation = initialRotation;
        }
    }
}
