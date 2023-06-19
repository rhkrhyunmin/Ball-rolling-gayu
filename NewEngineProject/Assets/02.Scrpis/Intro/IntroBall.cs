using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBall : MonoBehaviour
{
    public string targetTag = "Goal"; // Ÿ�� ������Ʈ�� �±�
    public float speed = 5f;
    public GameObject prefabSpawner; // SpawnBall ������

    private Transform target; // Ÿ�� ������Ʈ
    private bool reachedTarget = false; // ��ǥ ���� ���� ����

    private float timer = 0f; // Ÿ�̸� ����
    private bool shouldDestroy = false; // ���� ����

    private void Start()
    {
        // Ÿ�� ������Ʈ ã��
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
    }

    private void FixedUpdate()
    {
        if (!reachedTarget)
        {
            MoveTowardsTarget();
        }
        else if (shouldDestroy)
        {
            timer += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Destroy(gameObject);
        }
    }

    private void MoveTowardsTarget()
    {
        if (target == null)
        {
            return;
        }

        // ���� ��ǥ ���� ������ ���� ���� ���
        Vector3 direction = target.position - transform.position;

        // ��ǥ ���������� �Ÿ� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < 0.5f)
        {
            reachedTarget = true;
            shouldDestroy = true;
            SpawnPrefab();
        }
        else
        {
            // �̵� �ӵ��� ������ ���Ͽ� �̵� ���� ���
            Vector3 movement = direction.normalized * speed * Time.fixedDeltaTime;

            // ���� ��ġ�� �̵� ���͸� ���Ͽ� �̵�
            transform.position += movement;
        }
    }

    private void SpawnPrefab()
    {
        Instantiate(prefabSpawner, transform.position, Quaternion.identity);
    }
}
