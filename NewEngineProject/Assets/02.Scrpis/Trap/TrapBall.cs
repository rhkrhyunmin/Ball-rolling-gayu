using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBall : MonoBehaviour
{
    private Vector3 originalScale;   // 원래 크기

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale; // 초기 크기 저장
    }

    // 충돌 감지
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 충돌한 객체가 플레이어인지 확인
        {
            // 객체 크기 증가
            transform.localScale = originalScale * 2.5f;

            // 플레이어에게 데미지 주기
            PlayerHp playerHealth = other.GetComponent<PlayerHp>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(5);
            }
        }
    }
}
