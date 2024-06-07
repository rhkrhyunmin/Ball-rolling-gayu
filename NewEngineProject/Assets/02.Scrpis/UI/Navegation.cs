using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public Transform destination; // ������ ��ġ
    public GameObject indicatorPrefab; // ������ ������ ������
    public float amplitude = 0.5f; // �������� ���Ʒ� ������ ����
    public float speed = 1.0f; // �������� ������ �ӵ�
    public float initialYOffset = 30.0f; // �ʱ� y�� ������

    private GameObject indicatorInstance; // ������ ������ �ν��Ͻ�
    private float startY; // �������� �ʱ� y ��ġ

    void Start()
    {
        // ������ ������ ����
        indicatorInstance = Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity);
        startY = indicatorInstance.transform.position.y + initialYOffset; // �ʱ� y ��ġ ����

    }

    void Update()
    {
        // ������ �������� ��ġ�� ������ ��ġ�� �̵�
        indicatorInstance.transform.position = destination.position;

        // �������� y ��ġ�� ���Ʒ��� ��ȭ���� ������ ȿ���� ��
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        indicatorInstance.transform.position = new Vector3(indicatorInstance.transform.position.x, newY, indicatorInstance.transform.position.z);

        // ȭ�� ������Ʈ ȸ��
        indicatorInstance.transform.rotation = Quaternion.Euler(0f, -90f, 90f);
    }
}
