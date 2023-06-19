using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;  // 생성할 공의 프리팹
    public Transform spawnPoint;   // 공을 생성할 위치 (최소 x: 0, 최대 x: 5.6)

    public float minX = 0f;  // 최소 x 좌표
    public float maxX = 7f;  // 최대 x 좌표

    public float SpawnTime = 15;


    private float timer = 0f;      // 타이머 변수

    void Update()
    {
        timer += Time.deltaTime;   // 프레임 간 시간 업데이트

        if (timer >= SpawnTime)           // 10초마다 실행
        {
            Spawn();            // 공 생성 함수 호출
            timer = 0f;             // 타이머 초기화
        }
    }

    void Spawn()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX,maxX), spawnPoint.position.y, spawnPoint.position.z);
        Instantiate(ballPrefab, randomPosition, Quaternion.identity);
    }
}
