using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public EnemySO enemyStats; // EnemySO�� EnemyStat���� �����Ͽ� �ǹ̸� ��Ȯ�ϰ� ��
    private Animator animator;

    private NavMeshAgent agent;
    private GameObject player;
    private BallHp ballHp;

    private bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        Debug.Log(isAttacking);
        player = GameObject.FindGameObjectWithTag("Ball");

        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= enemyStats.DetectionDistance && !isAttacking)
        {
            MoveTowardsPlayer();
        }

        if (distanceToPlayer <= enemyStats.AttackDistance)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.transform.position);
        if(isAttacking == true)
        {
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
        }
    }

    IEnumerator AttackCoroutine()
    {
        //animator.SetBool("IsWalk", false);
        animator.SetBool("IsAttack", true); // Start attacking animation
        transform.LookAt(player.transform.position);

        if (ballHp == null)
        {
            ballHp = FindObjectOfType<BallHp>(); 
        }

        if (ballHp != null)
        {
            isAttacking = true;
            ballHp.TakeDamage(enemyStats.AttackDamage);
            yield return new WaitForSeconds(enemyStats.AttackCoolDonw);
        }

        // ���� ������ ���� ���� �ִϸ��̼� ����
        animator.SetBool("IsAttack", false);
        isAttacking = false; 
    }
}
