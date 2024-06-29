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
            // 플레이어의 위치를 가져오고 현재 객체의 y축 값을 사용하여 목표 위치를 설정합니다.
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            // 현재 객체가 목표 위치를 바라보도록 합니다.
            transform.LookAt(targetPosition);

            // 이미 90도 회전된 상태를 고려하여 추가적인 회전을 적용합니다.
            transform.rotation *= Quaternion.Euler(0, 90, 0);
        }

    }
}
