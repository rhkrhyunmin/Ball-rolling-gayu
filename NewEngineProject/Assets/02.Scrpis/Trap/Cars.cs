using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Cars : MonoBehaviour
{
    public TrapSo carsSo;
    private float speed = 5f;

    public void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true) // ���� ����
        {
            // Move the car forward in its local space
            transform.position += transform.forward * speed * Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // ��ǥ ��ġ�� ��Ȯ�� ����
    }
}
