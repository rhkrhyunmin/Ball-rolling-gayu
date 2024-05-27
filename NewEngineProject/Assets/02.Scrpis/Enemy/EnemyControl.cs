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
    private BallHp ballHp;

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

        UpdateMoveAnmation();

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

    private void UpdateMoveAnmation()
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

        // 공격 딜레이 이후 공격 애니메이션 종료
        animator.SetBool("IsAttack", false);
        isAttacking = false; 
    }
}
