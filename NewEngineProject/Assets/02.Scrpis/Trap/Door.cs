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

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ball") && GameManager.Instance.isKey)
        {
            rotating = true;
            GameManager.Instance.isKey = false;
        }
    }

    private void Update()
    {

        if (rotating)
        {
            Debug.Log("99");
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
