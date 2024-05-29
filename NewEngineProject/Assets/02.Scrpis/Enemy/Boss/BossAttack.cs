using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public BossControl bossControl;
    public EnemySO bossSo;
    public PlayerHp playerHp;

    public void Update()
    {
        // ballHp가 null인 경우에만 찾도록 함
        if (playerHp == null)
        {
            playerHp = FindObjectOfType<PlayerHp>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 트리거 콜라이더에 닿은 오브젝트가 플레이어인지 확인
        Debug.Log(bossControl.isAttacking);
        if (other.CompareTag("Ball") && bossControl.isAttacking == true)
        {
            if (playerHp != null)
            {
                playerHp.TakeDamage(bossSo.AttackDamage);
            }
        }
    }
}
