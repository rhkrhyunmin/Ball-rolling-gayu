using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBall : MonoBehaviour
{
    public Transform target; // ��ǥ ������ Transform ������Ʈ
    public Transform plane; // ����� Transform ������Ʈ

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // ���� ��ǥ ���� ������ ���� ���� ���
        Vector3 direction = target.position - transform.position;

        // ����� ���� ���� ���
        Vector3 normal = plane.up;

        // ���� ���͸� ��鿡 ����
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, normal);

        // ������ �������� ���� ���������� ����
        rb.AddForce(projectedDirection.normalized * 10f);
    }
}
