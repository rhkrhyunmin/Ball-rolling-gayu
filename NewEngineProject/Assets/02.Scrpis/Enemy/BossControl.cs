using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossControl : MonoBehaviour
{
    public EnemySO bossEnemySo;

    public Slider healthSlider;
    public Animator animator;

    private NavMeshAgent agent;
    public Transform bulletSpawn;
    public GameObject bullet;

    private BallMove playerController;

    private bool isAttacking = false;
    private bool canMove = true;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        bossEnemySo.currentHp = bossEnemySo.MaxHp;
        UpdateHealthBar();
    }

    public void FindPlayer(BallMove ball)
    {
        playerController = ball;
    }

    private void Update()
    {
        if (playerController == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerController.transform.position);

        if (distanceToPlayer <= bossEnemySo.DetectionDistance)
        {
            MoveTowardsPlayer(playerController.transform.position);
            if (!isAttacking && distanceToPlayer <= bossEnemySo.AttackDistance)
            {
                Attack();
            }
        }
    }

    private void MoveTowardsPlayer(Vector3 targetPosition)
    {
        if (canMove)
        {
            agent.SetDestination(targetPosition);
            animator.SetBool("IsWalk", true);
            animator.SetBool("IsAttack", false); // Stop attacking animation while moving
        }
    }

    private void Attack()
    {
        canMove = false;
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsAttack", true); // Start attacking animation
        StartCoroutine(ResetMovementAfterAttack());
    }

    private IEnumerator ResetMovementAfterAttack()
    {
        yield return new WaitForSeconds(bossEnemySo.AttackCoolDonw);
        canMove = true; // 공격 후 일정 시간이 지나면 움직임 가능하도록 변수 변경
        animator.SetBool("IsAttack", false);
        animator.SetBool("IsWalk", true);
    }

    public void Fire()
    {
        GameObject Bullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(bossEnemySo.AttackDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        bossEnemySo.currentHp -= damage;
        animator.SetBool("IsWalk", false);
        animator.SetTrigger("IsHit");

        UpdateHealthBar(); // 체력 바 업데이트

        if (bossEnemySo.currentHp <= 0f)
        {
            SceneManager.LoadScene("Clear");
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = bossEnemySo.currentHp;
        healthSlider.maxValue = bossEnemySo.MaxHp;
    }
}
