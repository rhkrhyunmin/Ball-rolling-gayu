using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public BossControl bossControl;
    public EnemySO bossSo;
    public BallHp ballHp;

    public void Update()
    {
        // ballHp�� null�� ��쿡�� ã���� ��
        if (ballHp == null)
        {
            ballHp = FindObjectOfType<BallHp>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���� �ݶ��̴��� ���� ������Ʈ�� �÷��̾����� Ȯ��
        Debug.Log(bossControl.isAttacking);
        if (other.CompareTag("Ball") && bossControl.isAttacking == true)
        {
            if (ballHp != null)
            {
                ballHp.TakeDamage(bossSo.AttackDamage);
            }
        }
    }
}
