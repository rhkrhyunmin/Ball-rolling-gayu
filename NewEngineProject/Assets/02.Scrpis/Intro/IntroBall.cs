using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBall : MonoBehaviour
{
    public string targetTag = "Goal"; // 타겟 오브젝트의 태그
    public float speed = 5f;
    public GameObject prefabSpawner; // SpawnBall 프리팹

    private Transform target; // 타겟 오브젝트
    private bool reachedTarget = false; // 목표 지점 도달 여부

    private float timer = 0f; // 타이머 변수
    private bool shouldDestroy = false; // 삭제 여부

    private void Start()
    {
        // 타겟 오브젝트 찾기
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

        // 공과 목표 지점 사이의 방향 벡터 계산
        Vector3 direction = target.position - transform.position;

        // 목표 지점까지의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < 0.5f)
        {
            reachedTarget = true;
            shouldDestroy = true;
            SpawnPrefab();
        }
        else
        {
            // 이동 속도와 방향을 곱하여 이동 벡터 계산
            Vector3 movement = direction.normalized * speed * Time.fixedDeltaTime;

            // 현재 위치에 이동 벡터를 더하여 이동
            transform.position += movement;
        }
    }

    private void SpawnPrefab()
    {
        Instantiate(prefabSpawner, transform.position, Quaternion.identity);
    }
}
