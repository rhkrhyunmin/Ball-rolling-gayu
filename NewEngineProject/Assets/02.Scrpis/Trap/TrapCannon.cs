using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCannon : MonoBehaviour
{
    private string poolTag = "Bullet";

    public Player player;

    private bool isCheck = false;

    private void Update()
    {
        player = FindObjectOfType<Player>();
        Fire();
    }

    private void Fire()
    {
        if(isCheck)
        {
            StartCoroutine(FireDelay(5f));
        }
    }

    IEnumerator FireDelay(float delayTime)
    {
        if(delayTime < 0)
        {
            PoolManager.Instance.GetObject(poolTag);
        }
        else
        {
            delayTime -= Time.deltaTime;
        }

        yield return new WaitForSeconds(delayTime);
    }
}
