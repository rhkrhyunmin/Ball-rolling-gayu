using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;  // ������ ���� ������
    public Transform spawnPoint;   // ���� ������ ��ġ (�ּ� x: 0, �ִ� x: 5.6)

    public float minX = 0f;  // �ּ� x ��ǥ
    public float maxX = 7f;  // �ִ� x ��ǥ


    private float timer = 0f;      // Ÿ�̸� ����

    void Update()
    {
        timer += Time.deltaTime;   // ������ �� �ð� ������Ʈ

        if (timer >= 10f)           // 10�ʸ��� ����
        {
            Spawn();            // �� ���� �Լ� ȣ��
            timer = 0f;             // Ÿ�̸� �ʱ�ȭ
        }
    }

    void Spawn()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX,maxX), spawnPoint.position.y, spawnPoint.position.z);
        Instantiate(ballPrefab, randomPosition, Quaternion.identity);
    }
}
