using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallSpawn : MonoBehaviour
{
    public string poolTag = "Trap";

    private void Start()
    {
        StartCoroutine(SpawnTrapBalls());
    }

    private IEnumerator SpawnTrapBalls()
    {
        while (true)
        {
            SpawnTrapBall();
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator DisableTrapBall(GameObject trapBall)
    {
        yield return new WaitForSeconds(10f);
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
