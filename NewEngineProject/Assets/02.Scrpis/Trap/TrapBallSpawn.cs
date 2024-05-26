using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallSpawn : MonoBehaviour
{
    public string poolTag = "Trap";
    private float spawnRandomTime;

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
            trapBall.transform.position = transform.position;
            trapBall.transform.rotation = transform.rotation;
            StartCoroutine(DisableTrapBall(trapBall));
        }
    }
}
