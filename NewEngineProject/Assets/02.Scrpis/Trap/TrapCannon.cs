using System.Collections;
using UnityEngine;

public class TrapCannon : MonoBehaviour
{
    private string poolTag = "Bullet";
    public Transform spawnPoint; 
    public Player player; 
    private bool isCheck = true;
    private float fireDelay = 5f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            if (isCheck)
            {
                GameObject bullet = PoolManager.Instance.GetObject(poolTag);
                if (bullet != null)
                {
                    bullet.transform.position = spawnPoint.position;
                    bullet.transform.rotation = spawnPoint.rotation;
                    bullet.SetActive(true);
                }
                Debug.Log("Fire!");

                yield return new WaitForSeconds(fireDelay);
            }
            else
            {
                yield return null;
            }
        }
    }
}
