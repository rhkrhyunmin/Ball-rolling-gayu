using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float detectionDistance = 10f;
    public float attackDistance = 2f;
    public float movementSpeed = 2f;
    public float currentHp = 0;
    public float MaxHp = 10;
    public Animator animator;

    Rigidbody rb;
    BallHp ballHp;

    [SerializeField]
    private BallMove playerController;
    private bool isPlayerDetected = false;
    private bool isIdle = true;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    public float attackCooldown = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FindPlayer(BallMove ball)
    {
        playerController = ball;
    }

    private void Update()
    {
        if (playerController == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerController.transform.position);

        if (isIdle)
        {
            // IDLE �ִϸ��̼��� ���� ���� ���
            if (distanceToPlayer <= detectionDistance)
            {
                // ���ѵ� �Ÿ� �ȿ� �÷��̾ ������ Walk �ִϸ��̼� ����
                animator.SetBool("IsWalk", true);
                isPlayerDetected = true;
                isIdle = false;
            }
        }
        else
        {
            // Walk �ִϸ��̼� ���� ���� ���
            if (distanceToPlayer > detectionDistance)
            {
                // ���ѵ� �Ÿ� ������ ������ IDLE �ִϸ��̼� ����
                animator.SetBool("IsWalk", false);
                isPlayerDetected = false;
                isIdle = true;
            }
        }

        if (isPlayerDetected)
        {
            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
            transform.LookAt(playerController.transform.position);

            if (distanceToPlayer <= attackDistance && !isAttacking)
            {
                // ���� ���� �ȿ� �÷��̾ �ְ�, ���� ���� �ƴ� ��� ���� �ִϸ��̼� ����
                Attack();
            }
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false;
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsWalk", true);
                isPlayerDetected = true;
                attackTimer = 0f;
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttack", true);
        animator.SetBool("IsWalk", false);
        isPlayerDetected = false;
        transform.LookAt(playerController.transform.position);

        if (ballHp == null)
        {
            ballHp = GameObject.Find("Ball")?.GetComponent<BallHp>();
        }

        if (ballHp != null)
        {
            ballHp.TakeDamage(5);
            Debug.Log("1");
        }
    }
}
