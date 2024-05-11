using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public EnemySO enemyStats; // EnemySO는 EnemyStat으로 변경하여 의미를 명확하게 함
    public Animator animator;

    private Rigidbody rb;
    private BallMove playerController;
    private BallHp ballHp;

    private bool isPlayerDetected = false;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetPlayer(BallMove ball)
    {
        playerController = ball; // FindPlayer 함수명을 SetPlayer로 변경하고, 이름의 명확성을 높임
    }

    private void Update()
    {
        if (playerController == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerController.transform.position);

        if (!isPlayerDetected && distanceToPlayer <= enemyStats.DetectionDistance)
        {
            StartWalking(); // IDLE 애니메이션 실행 및 Walk로 전환
        }
        else if (isPlayerDetected && distanceToPlayer > enemyStats.DetectionDistance)
        {
            StopWalking(); // Walk 애니메이션 실행 중지 및 IDLE로 전환
        }

        if (isPlayerDetected)
        {
            MoveTowardsPlayer(distanceToPlayer);

            if (distanceToPlayer <= enemyStats.AttackDistance && !isAttacking)
            {
                Attack();
            }
        }

        if (isAttacking)
        {
            UpdateAttackTimer();
        }
    }

    private void StartWalking()
    {
        animator.SetBool("IsWalk", true);
        isPlayerDetected = true;
        
    }

    private void StopWalking()
    {
        animator.SetBool("IsWalk", false);
        isPlayerDetected = false;
    }

    private void MoveTowardsPlayer(float distanceToPlayer)
    {
        Vector3 direction = (playerController.transform.position - transform.position).normalized;
        transform.Translate(direction * enemyStats.MovementSpeed * Time.deltaTime);
        transform.LookAt(playerController.transform.position);
    }

    private void UpdateAttackTimer()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= enemyStats.AttackCoolDonw)
        {
            EndAttack();
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttack", true);
        animator.SetBool("IsWalk", false);
        transform.LookAt(playerController.transform.position);

        if (ballHp == null)
        {
            ballHp = FindObjectOfType<BallHp>(); // GameObject.FindObjectOfType 대신 FindObjectOfType 사용
        }

        if (ballHp != null)
        {
            ballHp.TakeDamage(enemyStats.AttackDamage); // Attack Damage를 EnemySO에 추가하여 사용
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("IsAttack", false);
        animator.SetBool("IsWalk", true);
        isPlayerDetected = true;
        attackTimer = 0f;
    }
}
