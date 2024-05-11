using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public EnemySO enemyStats; // EnemySO�� EnemyStat���� �����Ͽ� �ǹ̸� ��Ȯ�ϰ� ��
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
        playerController = ball; // FindPlayer �Լ����� SetPlayer�� �����ϰ�, �̸��� ��Ȯ���� ����
    }

    private void Update()
    {
        if (playerController == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerController.transform.position);

        if (!isPlayerDetected && distanceToPlayer <= enemyStats.DetectionDistance)
        {
            StartWalking(); // IDLE �ִϸ��̼� ���� �� Walk�� ��ȯ
        }
        else if (isPlayerDetected && distanceToPlayer > enemyStats.DetectionDistance)
        {
            StopWalking(); // Walk �ִϸ��̼� ���� ���� �� IDLE�� ��ȯ
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
            ballHp = FindObjectOfType<BallHp>(); // GameObject.FindObjectOfType ��� FindObjectOfType ���
        }

        if (ballHp != null)
        {
            ballHp.TakeDamage(enemyStats.AttackDamage); // Attack Damage�� EnemySO�� �߰��Ͽ� ���
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
