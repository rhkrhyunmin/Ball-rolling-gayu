using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class IntroBall : MonoBehaviour
{
    public float speed = 5f; // ���� �̵� �ӵ�
    public float checkInterval = 1f; // ��ġ�� üũ�ϴ� ����

    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(MoveBall());
    }

    IEnumerator MoveBall()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x + 10, startPosition.y, startPosition.z); // ���������� 10 ���� �̵�

        float distance = Vector3.Distance(startPosition, endPosition);
        float elapsedTime = 0;

        while (elapsedTime < distance / speed)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / (distance / speed));
            elapsedTime += Time.deltaTime;

            // ��ġ üũ �� ����
            if (elapsedTime % checkInterval < Time.deltaTime)
            {
                positions.Add(transform.position);
            }

            yield return null;
        }

        // ������ ��ġ üũ
        positions.Add(endPosition);
    }

}
