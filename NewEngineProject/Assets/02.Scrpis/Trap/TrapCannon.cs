using System.Collections;
using UnityEngine;

public class TrapCannon : MonoBehaviour
{
    private string poolTag = "Bullet";
    public Transform spawnPoint; 
    public Player player; 
    private float fireDelay = 5f;

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            // �÷��̾��� ��ġ�� �������� ���� ��ü�� y�� ���� ����Ͽ� ��ǥ ��ġ�� �����մϴ�.
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            // ���� ��ü�� ��ǥ ��ġ�� �ٶ󺸵��� �մϴ�.
            transform.LookAt(targetPosition);

            // �̹� 90�� ȸ���� ���¸� ����Ͽ� �߰����� ȸ���� �����մϴ�.
            transform.rotation *= Quaternion.Euler(0, 90, 0);
        }

    }



    IEnumerator FireRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.isBoss)
            {
                GameObject bullet = PoolManager.Instance.GetObject(poolTag);
                if (bullet != null)
                {
                    bullet.transform.position = spawnPoint.position;
                    bullet.transform.rotation = spawnPoint.rotation;
                    bullet.SetActive(true);
                }

                yield return new WaitForSeconds(fireDelay);
            }
            else
            {
                yield return null;
            }
        }
    }
}
