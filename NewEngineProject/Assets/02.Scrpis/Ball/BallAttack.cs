using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public float dashForce = 10f;          // ���� ��
    public float dashDuration = 0.5f;      // ���� ���� �ð�
    public float dashCooldown = 1f;        // ���� ��ٿ� �ð�
    public float dashDamage = 10f;         // �������� ���� ������
    public float dashKnockbackForce = 5f;  // �������� ���� ƨ�ܳ� ��

    private bool isDashing = false;        // ���� ������ ����
    private float dashTimer = 0f;          // ���� Ÿ�̸�
    private float dashCooldownTimer = 0f;  // ���� ��ٿ� Ÿ�̸�

    private void Update()
    {
        // ���� ��ٿ� ����
        dashCooldownTimer -= Time.deltaTime;

        // �����̽� �ٸ� ������ ���� ��ٿ��� 0���� �۰ų� ���� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f)
        {
            // ���� ����
            StartDash();
            Debug.Log("1");
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
    }

    private void StartDash()
    {
        // ���� ���·� ����
        isDashing = true;
        dashTimer = 0f;

        // ���� ���� ���� (���⼭�� �÷��̾ �ٶ󺸴� ����)
        Vector3 dashDirection = transform.forward;

        // ���� �� ����
        GetComponent<Rigidbody>().AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }

    private void EndDash()
    {
        // ���� ����
        isDashing = false;

        // ���� ��ٿ� ����
        dashCooldownTimer = dashCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �߿� ���� �浹�ϸ� ������ ������ ƨ�ܳ���
        if (isDashing && other.CompareTag("Boss"))
        {
            // ������ ������ ������
            other.GetComponent<BossHp>().TakeDamage(dashDamage);

            // �÷��̾ �ڷ� ƨ�ܳ����� �� ����
            Vector3 knockbackDirection = -transform.forward;
            GetComponent<Rigidbody>().AddForce(knockbackDirection * dashKnockbackForce, ForceMode.Impulse);
        }
    }
}
