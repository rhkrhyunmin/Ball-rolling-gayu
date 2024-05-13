using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossControl : MonoBehaviour
{
    public EnemySO bossState; // EnemySO는 EnemyStat으로 변경하여 의미를 명확하게 함
    private Animator animator;
    private BossAnimator bossAnimator;

    private NavMeshAgent agent;
    private GameObject player;
    private BallHp ballHp;

    private bool isAttacking = false;

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
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        bossAnimator.SetAnimationState(AnimationState.Run,true);
    }

    IEnumerator AttackCoroutine()
    {
        bossAnimator.SetAnimationState(AnimationState.Attack,true);
        agent.isStopped = true;
        transform.LookAt(player.transform.position);

        if (ballHp == null)
        {
            ballHp = FindObjectOfType<BallHp>();
        }

        else
        {
            isAttacking = true;
            yield return new WaitForSeconds(bossState.AttackCoolDonw);
        }
        /*bossAnimator.SetAnimationState(AnimationState.Run,true);
        bossAnimator.SetAnimationState(AnimationState.Attack, false);*/
        isAttacking = false;
    }

    public void GiveDamage()
    {
        ballHp.TakeDamage(bossState.AttackDamage);
    }

    public void TakeDamage(float damage)
    {
        bossState.currentHp -= damage;
        bossAnimator.SetAnimationState(AnimationState.Hit,true);
        Debug.Log(bossState.currentHp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            TakeDamage(5);
        }
    }
}
