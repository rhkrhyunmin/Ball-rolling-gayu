using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float minRotationTime = 1.0f; // �ּ� ȸ�� �ð�
    public float maxRotationTime = 5.0f; // �ִ� ȸ�� �ð�
    public float rotationAngle = 90.0f; // ȸ�� ����

    private float nextRotationTime; // ���� ȸ�������� �ð�
    private bool rotating; // ȸ�� ����

    private Quaternion initialRotation; // �ʱ� ȸ�� ����

    private void Start()
    {
        // �ʱ� ȸ�� ���� ����
        initialRotation = transform.rotation;

        // ù ȸ�������� �ð� ����
        nextRotationTime = Random.Range(minRotationTime, maxRotationTime);
        rotating = false;
    }

    private void Update()
    {
        // ȸ�� Ÿ�̸� ������Ʈ
        nextRotationTime -= Time.deltaTime;

        if (nextRotationTime <= 0)
        {
            // ȸ���� �ð��� �Ǹ� ȸ�� ���� ����
            rotating = !rotating;
            nextRotationTime = Random.Range(minRotationTime, maxRotationTime);
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
