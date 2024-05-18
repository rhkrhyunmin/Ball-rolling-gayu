using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public BossControl bossControl;
    public EnemySO bossSo;
    private BallHp ballHp;

    private void Awake()
    {
        bossControl = GetComponent<BossControl>();
    }

    public void Update()
    {
        ballHp.GetComponent<BallHp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 트리거 콜라이더에 닿은 오브젝트가 플레이어인지 확인
       
        if (other.CompareTag("Ball") && bossControl.isAttacking == true)
        {
            ballHp.TakeDamage(bossSo.AttackDamage);
        }
    }
}
