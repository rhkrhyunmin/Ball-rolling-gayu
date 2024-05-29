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
        // ballHp�� null�� ��쿡�� ã���� ��
        if (playerHp == null)
        {
            playerHp = FindObjectOfType<PlayerHp>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���� �ݶ��̴��� ���� ������Ʈ�� �÷��̾����� Ȯ��
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
