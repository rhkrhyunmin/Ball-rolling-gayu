using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public float speed = 5f;              // �ڱ����� �̵��ϴ� �ӵ�
    public Vector3 startPoint;            // �ڱ����� ���� ����
    public Vector3 endPoint;              // �ڱ����� ��ǥ ����


    void Start()
    {
        transform.position = startPoint;
    }

    void Update()
    {
        if (GameManager.Instance.isShrinking)
        {
            MoveZone();
        }
    }

    void MoveZone()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

        // ��ǥ ������ �����ϸ� �̵� ����
        if (transform.position == endPoint)
        {
            GameManager.Instance.isShrinking = false;
        }
    }
}
