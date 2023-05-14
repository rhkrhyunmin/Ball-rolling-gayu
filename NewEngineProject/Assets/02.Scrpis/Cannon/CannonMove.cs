using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMove : MonoBehaviour
{
    public GameObject projectilePrefab;    // �߻�ü ������
    public Transform spawnPoint;           // �߻� ��ġ
    public float fireForce = 500.0f;       // �߻� �ӵ�
    public float rotateSpeed = 50.0f;      // ȸ�� �ӵ�
    public float minAngle = -80.0f;        // �ּ� ����
    public float maxAngle = 80.0f;         // �ִ� ����

    private float currentAngle;            // ���� ����

    void Start()
    {
        currentAngle = 0.0f;    // ���� ������ 0
    }

    void Update()
    {
        // �¿� ȸ��
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontal * rotateSpeed * Time.deltaTime);

        // ���� ȸ��
        float vertical = Input.GetAxis("Vertical");
        float targetAngle = currentAngle + vertical * rotateSpeed * Time.deltaTime;
        targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(-targetAngle, transform.localRotation.eulerAngles.y, 0);
        currentAngle = targetAngle;

        // �߻�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * fireForce);
        }
    }
}
