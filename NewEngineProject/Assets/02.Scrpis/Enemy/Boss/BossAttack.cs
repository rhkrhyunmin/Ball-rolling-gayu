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
        // Ʈ���� �ݶ��̴��� ���� ������Ʈ�� �÷��̾����� Ȯ��
       
        if (other.CompareTag("Ball") && bossControl.isAttacking == true)
        {
            ballHp.TakeDamage(bossSo.AttackDamage);
        }
    }
}
