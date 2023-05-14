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
    public Transform cannon;              // ȸ����ų ������Ʈ

    private float currentAngle;            // ���� ����

    public static Transform FindDeepChild(Transform parent, string name)
    {
        Transform child = parent.Find(name);
        if (child != null)
            return child;
        foreach (Transform tr in parent)
        {
            child = FindDeepChild(tr, name);
            if (child != null)
                return child;
        }
        return null;
    }

    private void Awake()
    {
        cannon = FindDeepChild(transform, "cannon");
        cannon.localRotation = Quaternion.Euler(-90, 0, 0);
    }

    void Start()
    {
        currentAngle = -90f;
        //cannon.localRotation = Quaternion.Euler(-90, 0, 0);
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

        cannon.localRotation = Quaternion.Euler(-targetAngle, 0, 0);
        currentAngle = targetAngle;

        // �߻�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(cannon.forward * fireForce);
        }
    }
}
