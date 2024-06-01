using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallSpawn : MonoBehaviour
{
    public string poolTag = "Trap";
    private float spawnRandomTime;
    public List<Transform> spawnPoints; // TrapBall�� ������ ��ġ���� �����ϴ� ����Ʈ

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
            // �������� ���� ��ġ�� �����մϴ�.
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            trapBall.transform.position = spawnPoint.position;
            trapBall.transform.rotation = spawnPoint.rotation;
            StartCoroutine(DisableTrapBall(trapBall));
        }
    }
}
