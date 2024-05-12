using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public EnemySO enemyStats; // EnemySO는 EnemyStat으로 변경하여 의미를 명확하게 함
    public Animator animator;

    private NavMeshAgent agent;
    private GameObject player;
    private BallHp ballHp;

    private bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Ball");

        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= enemyStats.DetectionDistance)
        {
            MoveTowardsPlayer();
            if (!isAttacking && distanceToPlayer <= enemyStats.AttackDistance)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.transform.position);
        animator.SetBool("IsWalk", true);
        animator.SetBool("IsAttack", false); // Stop attacking animation while moving
    }

    IEnumerator AttackCoroutine()
    {
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsAttack", true); // Start attacking animation
        transform.LookAt(player.transform.position);

        if (ballHp == null)
        {
            ballHp = FindObjectOfType<BallHp>(); // GameObject.FindObjectOfType 대신 FindObjectOfType 사용
        }

        if (ballHp != null)
        {
            isAttacking = true;
            ballHp.TakeDamage(enemyStats.AttackDamage);
            yield return new WaitForSeconds(3f);
        }

        // 공격 딜레이 이후 공격 애니메이션 종료
        animator.SetBool("IsAttack", false);
        isAttacking = false;
    }
}
