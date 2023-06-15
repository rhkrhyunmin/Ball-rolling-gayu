using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public float dashForce = 10f;          // 돌진 힘
    public float dashDuration = 0.5f;      // 돌진 지속 시간
    public float dashCooldown = 1f;        // 돌진 쿨다운 시간
    public float dashDamage = 10f;         // 돌진으로 입힐 데미지
    public float dashKnockbackForce = 5f;  // 돌진으로 적을 튕겨낼 힘

    private bool isDashing = false;        // 돌진 중인지 여부
    private float dashTimer = 0f;          // 돌진 타이머
    private float dashCooldownTimer = 0f;  // 돌진 쿨다운 타이머

    private void Update()
    {
        // 돌진 쿨다운 갱신
        dashCooldownTimer -= Time.deltaTime;

        // 스페이스 바를 누르고 돌진 쿨다운이 0보다 작거나 같을 때만 돌진 가능
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f)
        {
            // 돌진 시작
            StartDash();
            Debug.Log("1");
        }

        // 돌진 중일 때 타이머 갱신 및 돌진 종료 체크
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
        // 돌진 상태로 변경
        isDashing = true;
        dashTimer = 0f;

        // 돌진 방향 설정 (여기서는 플레이어가 바라보는 방향)
        Vector3 dashDirection = transform.forward;

        // 돌진 힘 적용
        GetComponent<Rigidbody>().AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }

    private void EndDash()
    {
        // 돌진 종료
        isDashing = false;

        // 돌진 쿨다운 설정
        dashCooldownTimer = dashCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 돌진 중에 적과 충돌하면 데미지 입히고 튕겨나감
        if (isDashing && other.CompareTag("Boss"))
        {
            // 적에게 데미지 입히기
            other.GetComponent<BossHp>().TakeDamage(dashDamage);

            // 플레이어를 뒤로 튕겨나가는 힘 적용
            Vector3 knockbackDirection = -transform.forward;
            GetComponent<Rigidbody>().AddForce(knockbackDirection * dashKnockbackForce, ForceMode.Impulse);
        }
    }
}
