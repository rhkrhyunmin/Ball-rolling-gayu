using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossControl : MonoBehaviour
{
    public EnemySO bossState; // EnemySO�� EnemyStat���� �����Ͽ� �ǹ̸� ��Ȯ�ϰ� ��
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

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= bossState.DetectionDistance)
        {
            MoveTowardsPlayer();
            if (!isAttacking && distanceToPlayer <= bossState.AttackDistance)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.transform.position);
        animator.SetBool("IsWalk", true);
    }

    IEnumerator AttackCoroutine()
    {
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsAttack", true); // Start attacking animation
        transform.LookAt(player.transform.position);

        if (ballHp == null)
        {
            ballHp = FindObjectOfType<BallHp>();
        }

        if (ballHp != null)
        {
            isAttacking = true;
            ballHp.TakeDamage(bossState.AttackDamage);
            yield return new WaitForSeconds(bossState.AttackCoolDonw);
        }

        // ���� ������ ���� ���� �ִϸ��̼� ����
        animator.SetBool("IsAttack", false);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        bossState.currentHp -= damage;
        Debug.Log(bossState.currentHp);
    }

    /*    private void UpdateHealthBar()
        {
            healthSlider.value = bossEnemySo.currentHp;
            healthSlider.maxValue = bossEnemySo.MaxHp;
        }*/
}
