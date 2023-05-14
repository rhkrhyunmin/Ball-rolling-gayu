using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMove : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float maxUpAngle = 45f; // �ִ� ���� ȸ�� ���� (degree)
    public float maxDownAngle = -10f; // �ִ� �Ʒ��� ȸ�� ���� (degree)

    private float currentAngle = 0f;

    void Update()
    {
        // ����Ű �Է��� �����Ͽ� ������ ȸ�� ���� �����մϴ�.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ���� ȸ�� ������ ������Ʈ�մϴ�.
        currentAngle += verticalInput * rotationSpeed * Time.deltaTime;

        // ȸ�� ������ ���� ���� ������ �����մϴ�.
        currentAngle = Mathf.Clamp(currentAngle, maxDownAngle, maxUpAngle);

        // Cannon ���� ������Ʈ�� transform�� ����Ͽ� ȸ���մϴ�.
        transform.GetChild(0).localRotation = Quaternion.Euler(currentAngle, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
    }
}
