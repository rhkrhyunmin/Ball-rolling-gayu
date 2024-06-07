using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public EnemySO enemyStats; // EnemySO는 EnemyStat으로 변경하여 의미를 명확하게 함
    private Animator animator;

    private NavMeshAgent agent;
    private GameObject player;
    private PlayerHp playerHp;

    private bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Ball");

        if (player == null)
            return;

        UpdateMoveAnimation();

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= enemyStats.DetectionDistance && !isAttacking)
        {
            MoveTowardsPlayer();
            if (distanceToPlayer <= enemyStats.AttackDistance)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private void UpdateMoveAnimation()
    {
        if (!isAttacking)
        {
            animator.SetBool("IsWalk", true);
            agent.speed = enemyStats.MovementSpeed;
        }
        else
        {
            animator.SetBool("IsWalk", false);
            agent.speed = 0;
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    IEnumerator AttackCoroutine()
    {
        // 공격 시작: 걷기 애니메이션을 멈추고 공격 애니메이션 시작
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsAttack", true); // Start attacking animation
        //transform.LookAt(player.transform.position);

        // 공격 중 움직이지 않도록 NavMeshAgent를 멈춤
        agent.isStopped = true;

        if (playerHp == null)
        {
            playerHp = FindObjectOfType<PlayerHp>();
        }

        if (playerHp != null)
        {
            isAttacking = true;
            playerHp.TakeDamage(enemyStats.AttackDamage);
            yield return new WaitForSeconds(enemyStats.AttackCoolDonw);
        }

        // 공격 끝: 공격 애니메이션을 멈추고 다시 움직일 수 있도록 설정
        animator.SetBool("IsAttack", false);
        isAttacking = false;
        agent.isStopped = false;
    }
}
