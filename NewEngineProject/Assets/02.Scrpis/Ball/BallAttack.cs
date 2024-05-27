using System.Collections;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    private Player player;

    private bool isDashing = false;       // ���� ������ ����
    private bool canUseSpace = true;     // �����̽� ��� �������� ����

    private float boundsForce = 10f;     // ƨ�ܳ��� ���� ��

    private void Start()
    {
        player = GetComponent<Player>();
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

        yield return new WaitForSeconds(player.ballSO.dashCooldown);

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
            player.rigid.AddForce(dashDirection * player.ballSO.dashForce + Vector3.up * player.ballSO.dashForce, ForceMode.Impulse);
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
                bossHp.TakeDamage(player.ballSO.dashDamage);

                Vector3 direction = (other.transform.position - transform.position).normalized;
                player.rigid.AddForce(-direction * boundsForce, ForceMode.Impulse);
            }
        }
    }
}
