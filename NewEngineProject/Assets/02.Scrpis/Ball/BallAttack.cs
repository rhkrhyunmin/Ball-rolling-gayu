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
    public float upwardForce = 7f;       // ���� ���� ��

    private Rigidbody playerRigidbody;  // �÷��̾� Rigidbody ������Ʈ

    private bool isDashing = false;        // ���� ������ ����
    private bool hasDamagedBoss = false;  // �̹� �������� �������� �־����� ��
    private float dashTimer = 2f;          // ���� Ÿ�̸�
    private float dashCooldownTimer = 1f;  // ���� ��ٿ� Ÿ�̸�

    private bool canUseSpace = true;          // �����̽� ��� �������� ����
    private float spaceCooldown = 3f;         // �����̽� ��ٿ� �ð�
    private float lastSpacePressTime = 0f;    // ������ �����̽� ���� �ð�
    private float boundsForce = 10f;

    private float currentMovementSpeed;

    BallMove ballMove;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        ballMove = GetComponent<BallMove>();

    }


    private void Update()
    {
        // �����̽� ��� ������ ���¿��� �����̽��� ������ �ൿ ����
        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            // �����̽� ��ٿ� ���� �� �ൿ ����
            lastSpacePressTime = Time.time;
            StartCoroutine(SpaceCooldown());

            // ���� ����
            StartDash();
        }
    }

    private IEnumerator SpaceCooldown()
    {
        canUseSpace = false;  // �����̽� ��� �Ұ��� ���·� ����

        yield return new WaitForSeconds(spaceCooldown);

        canUseSpace = true;  // �����̽� ��� ���� ���·� ����
    }

    private void StartDash()
    {
        GameObject bossObject = GameObject.FindWithTag("Boss");
        isDashing = true;
        dashTimer = 0f;

        // ���� ���� ���� (���� ��ġ�� ���ϵ���)
        Vector3 dashDirection = (bossObject.transform.position - transform.position).normalized;

        // ���� �� ����
        playerRigidbody.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        // ���� ���ϴ� �� ����
        playerRigidbody.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �߿� ���� �浹�ϸ� ������ ������ ƨ�ܳ���
        if (isDashing && other.CompareTag("Boss") && !hasDamagedBoss)
        {
            // �����̽��� 3�ʿ� 1���� ��� �����ϵ��� üũ
            if (Time.time - lastSpacePressTime >= spaceCooldown)
            {
                canUseSpace = true;
            }
            else
            {
                canUseSpace = false;
            }

            // ������ ������ ������
            BossHp bossHp = other.GetComponent<BossHp>();
            if (bossHp != null)
            {
                bossHp.TakeDamage(dashDamage);
                hasDamagedBoss = true;  // �������� �־����� ǥ��
                Debug.Log(bossHp.currentHp);
                // �÷��̾ �ڷ� ƨ�ܳ����� �� ����
                Vector3 direction = (other.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(-direction * boundsForce, ForceMode.Impulse);
                currentMovementSpeed = 5;
                Debug.Log("1");
            }
        }
    }
}
