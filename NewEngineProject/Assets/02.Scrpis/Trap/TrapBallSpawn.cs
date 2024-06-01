using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallSpawn : MonoBehaviour
{
    public string poolTag = "Trap";
    private float spawnRandomTime;
    public List<Transform> spawnPoints; // TrapBall이 스폰될 위치들을 저장하는 리스트

    private void Start()
    {
        spawnRandomTime = Random.Range(3, 10);
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
            // 무작위로 스폰 위치를 선택합니다.
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            trapBall.transform.position = spawnPoint.position;
            trapBall.transform.rotation = spawnPoint.rotation;
            StartCoroutine(DisableTrapBall(trapBall));
        }
    }
}
