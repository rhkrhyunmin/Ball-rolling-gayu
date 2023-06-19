using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBall : MonoBehaviour
{
    public Transform target; // ��ǥ ������ Transform ������Ʈ
    public Transform plane; // ����� Transform ������Ʈ
    public Transform initialPosition; // ó�� ��ġ�� Transform ������Ʈ

    private Rigidbody rb;
    private Vector3 initialPositionVector; // ó�� ��ġ ���� ����

    private bool reachedTarget = false; // ��ǥ ���� ���� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPositionVector = initialPosition.position; // ó�� ��ġ ����
    }

    private void FixedUpdate()
    {
        if (!reachedTarget)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // ���� ��ǥ ���� ������ ���� ���� ���
        Vector3 direction = target.position - transform.position;

        // ����� ���� ���� ���
        Vector3 normal = plane.up;

        // ���� ���͸� ��鿡 ����
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, normal);

        // ������ �������� ���� ���������� ����
        rb.AddForce(projectedDirection.normalized * 10f);

        // ��ǥ ������ �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            reachedTarget = true;
            ResetToInitialPosition(); // ��ǥ ������ �����ϸ� ó�� ��ġ�� �ǵ��ư�
        }
    }

    private void ResetToInitialPosition()
    {
        // ���� ��ġ�� ó�� ��ġ�� ����
        transform.position = initialPositionVector;

        // ���� �ӵ��� �� �ʱ�ȭ
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep(); // ���� ���� �ùķ��̼� ����

        reachedTarget = false; // ��ǥ ���� ���� ���� �ʱ�ȭ
    }
}
