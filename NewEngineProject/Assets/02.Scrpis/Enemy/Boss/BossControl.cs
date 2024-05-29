using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BossControl : MonoBehaviour
{
    public EnemySO bossState; // EnemySO는 EnemyStat으로 변경하여 의미를 명확하게 함
    private Animator animator;
    private BossAnimator bossAnimator;

    private NavMeshAgent agent;
    private GameObject player;
    private PlayerHp playerHp;

    public bool isAttacking = false;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        bossAnimator = GetComponent<BossAnimator>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Ball");

        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= bossState.DetectionDistance && !isAttacking)
        {
            MoveTowardsPlayer();
        }

        if (!isAttacking && distanceToPlayer <= bossState.AttackDistance)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void MoveTowardsPlayer()
    {
        //agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        agent.speed += bossState.MovementSpeed;
        bossAnimator.SetAnimationState(AnimationState.Run,true);
    }

    IEnumerator AttackCoroutine()
    {
        bossAnimator.SetAnimationState(AnimationState.Attack,true);
        agent.speed = 0;
        //agent.isStopped = true;
        transform.LookAt(player.transform.position);

        if (playerHp == null)
        {
            playerHp = FindObjectOfType<PlayerHp>();
        }

        else
        {
            isAttacking = true;
            yield return new WaitForSeconds(bossState.AttackCoolDonw);
        }
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        bossState.currentHp -= damage;
        bossAnimator.SetAnimationState(AnimationState.Hit,true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            TakeDamage(5);
        }
    }
}
