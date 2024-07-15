using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour
{
    public Transform startPoint; // ���� ����
    public Transform endPoint;   // ��ǥ ����
    public float speed = 7f;   // �̵� �ӵ�

    private bool movingToEnd = true; // ��ǥ �������� �̵� ������ ����

    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (movingToEnd)
            {
                // ��ǥ �������� �̵�
                yield return StartCoroutine(MoveToPosition(endPoint.position));
                movingToEnd = false;
                // ��ǥ ������ ���� �� 0�� ȸ�� (������� ȸ���� �ƴ�)
                transform.rotation = Quaternion.Euler(0,-90,0);
            }
            else
            {
                yield return StartCoroutine(MoveToPosition(startPoint.position));
                movingToEnd = true;
                // ��ǥ ������ ���� �� 180�� ȸ�� (������� ȸ���� �ƴ�)
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
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
