using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossControl : MonoBehaviour
{
    public float detectionDistance = 10f;
    public float attackDistance = 3f;
    public float movementSpeed = 2f;

    public float maxHp = 20f;
    public float currentHp = 0f;
    public float damageAmount = 3f;

    public Slider healthSlider;


    public Animator animator;

    Rigidbody rb;

    public Transform bulletSpawn;
    public GameObject bullet;

    [SerializeField]
    private BallMove playerController;
    private bool isPlayerDetected = false;
    private bool isIdle = true;
    private bool isAttacking = false;
    private bool canMove = true;

    private float attackTimer = 0f;
    public float attackCooldown = 2f;

    BallAttack attack;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        attack = GetComponent<BallAttack>();

        currentHp = maxHp;
        UpdateHealthBar();
        healthSlider.value = maxHp;
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
            // IDLE 애니메이션이 실행 중인 경우
            if (distanceToPlayer <= detectionDistance)
            {
                // 제한된 거리 안에 플레이어가 들어오면 Walk 애니메이션 실행
                animator.SetBool("IsWalk", true);
                isPlayerDetected = true;
                isIdle = false;
            }
        }
        else
        {
            // Walk 애니메이션 실행 중인 경우
            if (distanceToPlayer > detectionDistance)
            {
                // 제한된 거리 밖으로 나가면 IDLE 애니메이션 실행
                animator.SetBool("IsWalk", false);
                animator.ResetTrigger("IsAttack");
                animator.ResetTrigger("IsHit");
                isPlayerDetected = false;
                isIdle = true;
            }
        }

        if (isPlayerDetected && canMove)
        {
            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
            transform.LookAt(playerController.transform.position);
            Debug.Log("1");

            if (distanceToPlayer <= attackDistance && !isAttacking)
            {
                Attack();
            }
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                movementSpeed = 0;
                isAttacking = true;
                animator.ResetTrigger("IsHit");
                animator.SetTrigger("IsAttack");
                animator.SetBool("IsWalk", false);
                isPlayerDetected = false;
                attackTimer = 0f;
            }
        }
    }

    private void Attack()
    {
        transform.LookAt(playerController.transform.position);
        animator.SetBool("IsWalk", false);
        animator.SetTrigger("IsAttack");

    }
    public void Fire()
    {
        GameObject Bullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        animator.SetBool("IsWalk", false);
        animator.SetTrigger("IsHit");
        animator.ResetTrigger("IsAttack");

        UpdateHealthBar(); // 체력 바 업데이트
        Debug.Log(currentHp);

        if (currentHp <= 5f)
        {
            SceneManager.LoadScene("Clear");
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHp; 
        healthSlider.maxValue = maxHp; 
    }
}
