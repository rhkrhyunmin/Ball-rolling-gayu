using System.Collections;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public BallSO ballSO;
    private Rigidbody playerRigidbody;  // 플레이어 Rigidbody 컴포넌트

    private bool isDashing = false;       // 돌진 중인지 여부
    private bool canUseSpace = true;     // 스페이스 사용 가능한지 여부

    private float spaceCooldown = 3f;    // 스페이스 쿨다운 시간
    private float boundsForce = 10f;     // 튕겨나갈 때의 힘

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 스페이스 사용 가능한 상태에서 스페이스를 누르면 행동 실행
        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceCooldown());
            StartDash();
        }
    }

    private IEnumerator SpaceCooldown()
    {
        canUseSpace = false;  // 스페이스 사용 불가능 상태로 설정

        yield return new WaitForSeconds(spaceCooldown);

        canUseSpace = true;  // 스페이스 사용 가능 상태로 설정
    }

    private void StartDash()
    {
        GameObject bossObject = GameObject.FindWithTag("Boss");
        if (bossObject != null)
        {
            isDashing = true;

            // 돌진 방향 설정 (보스 위치를 향하도록)
            Vector3 dashDirection = (bossObject.transform.position - transform.position).normalized;

            // 돌진 힘 적용
            playerRigidbody.AddForce(dashDirection * ballSO.dashForce + Vector3.up * ballSO.dashForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // 돌진 중에 보스와 충돌하면 데미지 입히고 튕겨나감
        if (isDashing && other.collider.CompareTag("Boss"))
        {
            isDashing = false;

            // 적에게 데미지 입히기
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
