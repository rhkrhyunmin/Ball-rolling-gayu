using System.Collections;
using UnityEngine;

public class TrapCannon : MonoBehaviour
{
    private string poolTag = "Bullet";
    public Transform spawnPoint; 
    public Player player; 
    private float fireDelay = 5f;

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
}
