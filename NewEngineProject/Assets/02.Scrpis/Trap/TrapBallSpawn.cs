using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallSpawn : MonoBehaviour
{
    public string poolTag = "Trap";
    private float spawnRandomTime;

    public Vector3 minSpawnRange; // �ּ� ���� ���� (X, Y, Z)
    public Vector3 maxSpawnRange; // �ִ� ���� ���� (X, Y, Z)

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
            // �������� ���� ��ġ�� �����մϴ�.
            Vector3 randomPosition = new Vector3(
                Random.Range(minSpawnRange.x, maxSpawnRange.x),
                Random.Range(minSpawnRange.y, maxSpawnRange.y),
                Random.Range(minSpawnRange.z, maxSpawnRange.z)
            );

            trapBall.transform.position = randomPosition;
            trapBall.transform.rotation = Quaternion.identity; // �Ǵ� �ٸ� ���ϴ� ȸ���� ����
            StartCoroutine(DisableTrapBall(trapBall));
        }
    }
}
