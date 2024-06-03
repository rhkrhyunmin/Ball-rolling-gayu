using System.Collections;
using UnityEngine;

public class TrapCannon : MonoBehaviour
{
    private string poolTag = "Bullet";
    public Player player;
    private bool isCheck = true;
    private float fireDelay = 5f;

    private void Start()
    {
        // Player는 게임 시작 시 한 번만 찾도록 합니다.
        player = FindObjectOfType<Player>();
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        // 매 프레임마다 플레이어 쪽으로 회전합니다.
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // 여기서는 단순히 바로 회전시키지만, 실제 게임에서는 부드럽게 회전하도록 Slerp 함수 등을 사용할 수 있습니다.
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }
    }

    IEnumerator FireRoutine()
    {
        // 무한 루프를 사용하여 코루틴을 반복 실행합니다.
        while (true)
        {
            // 발사 조건 검사
            if (isCheck)
            {
                // 여기서 PoolManager를 통해 총알을 생성하고 발사 로직을 구현합니다.
                PoolManager.Instance.GetObject(poolTag);
                Debug.Log("Fire!");

                // 지정된 딜레이만큼 대기
                yield return new WaitForSeconds(fireDelay);
            }
            else
            {
                // isCheck가 false일 경우 코루틴 실행을 중단하지 않고, 다음 프레임을 기다립니다.
                yield return null;
            }
        }
    }
}
