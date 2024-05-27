using System.Collections;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public BallSO ballSO;
    private Rigidbody playerRigidbody;  // �÷��̾� Rigidbody ������Ʈ

    private bool isDashing = false;       // ���� ������ ����
    private bool canUseSpace = true;     // �����̽� ��� �������� ����

    private float spaceCooldown = 3f;    // �����̽� ��ٿ� �ð�
    private float boundsForce = 10f;     // ƨ�ܳ��� ���� ��

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �����̽� ��� ������ ���¿��� �����̽��� ������ �ൿ ����
        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceCooldown());
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
        if (bossObject != null)
        {
            isDashing = true;

            // ���� ���� ���� (���� ��ġ�� ���ϵ���)
            Vector3 dashDirection = (bossObject.transform.position - transform.position).normalized;

            // ���� �� ����
            playerRigidbody.AddForce(dashDirection * ballSO.dashForce + Vector3.up * ballSO.dashForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // ���� �߿� ������ �浹�ϸ� ������ ������ ƨ�ܳ���
        if (isDashing && other.collider.CompareTag("Boss"))
        {
            isDashing = false;

            // ������ ������ ������
            BossControl bossHp = other.collider.GetComponent<BossControl>();
            if (bossHp != null)
            {
                bossHp.TakeDamage(ballSO.dashDamage);

                Vector3 direction = (other.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(-direction * boundsForce, ForceMode.Impulse);
            }
        }
    }
}
