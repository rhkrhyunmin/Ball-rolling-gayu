using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallSpawn : MonoBehaviour
{
    public string poolTag = "Trap";
    private float spawnRandomTime;

    public Vector3 minSpawnRange; // 최소 스폰 범위 (X, Y, Z)
    public Vector3 maxSpawnRange; // 최대 스폰 범위 (X, Y, Z)

    private void Start()
    {
        spawnRandomTime = Random.Range(1, 7);
        StartCoroutine(SpawnTrapBalls());
    }

    private IEnumerator SpawnTrapBalls()
    {
        while (true)
        {
            SpawnTrapBall();
            yield return new WaitForSeconds(spawnRandomTime);
        }
    }

    private IEnumerator DisableTrapBall(GameObject trapBall)
    {
        yield return new WaitForSeconds(28f);
        PoolManager.Instance.ReturnObject(trapBall);
    }

    private void SpawnTrapBall()
    {
        GameObject trapBall = PoolManager.Instance.GetObject(poolTag);
        if (trapBall != null)
        {
            // 무작위로 스폰 위치를 생성합니다.
            Vector3 randomPosition = new Vector3(
                Random.Range(minSpawnRange.x, maxSpawnRange.x),
                Random.Range(minSpawnRange.y, maxSpawnRange.y),
                Random.Range(minSpawnRange.z, maxSpawnRange.z)
            );

            trapBall.transform.position = randomPosition;
            trapBall.transform.rotation = Quaternion.identity; // 또는 다른 원하는 회전값 설정
            StartCoroutine(DisableTrapBall(trapBall));
        }
    }
}
