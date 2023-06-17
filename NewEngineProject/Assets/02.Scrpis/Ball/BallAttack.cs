using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public float dashForce = 10f;          // ���� ��
    public float dashDuration = 0.5f;      // ���� ���� �ð�
    public float dashCooldown = 1f;        // ���� ��ٿ� �ð�
    public float dashDamage = 5;         // �������� ���� ������
    public float dashKnockbackForce = 5f;  // �������� ���� ƨ�ܳ� ��
    public float detectionRadius = 5f;      // ���� ���� �ݰ�
    public float upwardForce = 5f;       // ���� ���� ��

    private Rigidbody playerRigidbody;  // �÷��̾� Rigidbody ������Ʈ

    private bool isDashing = false;        // ���� ������ ����
    private bool hasDamagedBoss = false;  // �̹� �������� �������� �־����� ��
    private float dashTimer = 0f;          // ���� Ÿ�̸�
    private float dashCooldownTimer = 0f;  // ���� ��ٿ� Ÿ�̸�
    public GameObject bossObject;  // ���� ���� ������Ʈ


    private void Start()
    {
        // ������ ã�Ƽ� Transform�� ������
        bossObject = GameObject.FindGameObjectWithTag("Boss");

        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ���� ��ٿ� ����
        dashCooldownTimer -= Time.deltaTime;

        // �����̽� �ٸ� ������ ���� ��ٿ��� 0���� �۰ų� ���� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f)
        {
            // ���� ����
            StartDash();
        }

        // ���� ���� �� Ÿ�̸� ���� �� ���� ���� üũ
        if (isDashing)
        {
            dashTimer += Time.deltaTime;

            if (dashTimer >= dashDuration)
            {
                EndDash();
            }
        }
        else if (!isDashing && dashCooldownTimer <= 0f && Input.GetKey(KeyCode.Space))
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = 0f;

        // ���� ���� ���� (���� ��ġ�� ���ϵ���)
        Vector3 dashDirection = (bossObject.transform.position - transform.position).normalized;

        // ���� �� ����
        playerRigidbody.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        // ���� ���ϴ� �� ����
        playerRigidbody.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
    }

    private void EndDash()
    {
        // ���� ����
        isDashing = false;

        // �÷��̾ �ڷ� ���ĳ��� �� ����
        Vector3 knockbackDirection = -transform.forward;
        playerRigidbody.AddForce(knockbackDirection * dashKnockbackForce, ForceMode.Impulse);

        // ���� ��ٿ� ����
        dashCooldownTimer = dashCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �߿� ���� �浹�ϸ� ������ ������ ƨ�ܳ���
        if (isDashing && other.CompareTag("Boss") && !hasDamagedBoss)
        {
            // ������ ������ ������
            BossHp bossHp = other.GetComponent<BossHp>();
            if (bossHp != null)
            {
                bossHp.TakeDamage(dashDamage);
                hasDamagedBoss = true;  // �������� �־����� ǥ��
            }

            // �÷��̾ �ڷ� ƨ�ܳ����� �� ����
            Vector3 knockbackDirection = -transform.forward;
            playerRigidbody.AddForce(knockbackDirection * dashKnockbackForce, ForceMode.Impulse);
        }
    }
}
