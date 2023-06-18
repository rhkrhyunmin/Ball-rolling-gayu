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
    BallHp ballHp;

    public Transform bulletSpawn;
    public GameObject bullet;

    [SerializeField]
    private BallMove playerController;
    private bool isPlayerDetected = false;
    private bool isIdle = true;
    private bool isAttacking = false;
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
        ballHp = GetComponent<BallHp>();
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
                animator.SetBool("IsHit", false);
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
        animator.SetBool("IsHit", false);
        isPlayerDetected = false;
        transform.LookAt(playerController.transform.position);

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
        animator.SetBool("IsHit", true);
        animator.SetBool("IsAttack", false);
        currentHp -= damage;
        
        UpdateHealthBar(); // ü�� �� ������Ʈ
        Debug.Log(currentHp);

        if (currentHp <= 5f)
        {
            SceneManager.LoadScene("Clear");
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHp; // ���� ü�� ���� ü�� �ٿ� �Ҵ�
        healthSlider.maxValue = maxHp; // �ִ� ü�� ���� ü�� �ٿ� �Ҵ�
    }
}
