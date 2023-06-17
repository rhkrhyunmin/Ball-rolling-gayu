using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public float dashForce = 10f;          // 돌진 힘
    public float dashDuration = 0.5f;      // 돌진 지속 시간
    public float dashCooldown = 1f;        // 돌진 쿨다운 시간
    public float dashDamage = 5;         // 돌진으로 입힐 데미지
    public float dashKnockbackForce = 5f;  // 돌진으로 적을 튕겨낼 힘
    public float detectionRadius = 5f;      // 보스 감지 반경
    public float upwardForce = 7f;       // 위로 가할 힘

    private Rigidbody playerRigidbody;  // 플레이어 Rigidbody 컴포넌트

    private bool isDashing = false;        // 돌진 중인지 여부
    private bool hasDamagedBoss = false;  // 이미 보스에게 데미지를 주었는지 여
    private float dashTimer = 2f;          // 돌진 타이머
    private float dashCooldownTimer = 1f;  // 돌진 쿨다운 타이머

    private bool canUseSpace = true;          // 스페이스 사용 가능한지 여부
    private float spaceCooldown = 3f;         // 스페이스 쿨다운 시간
    private float lastSpacePressTime = 0f;    // 마지막 스페이스 누른 시간
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
        // 스페이스 사용 가능한 상태에서 스페이스를 누르면 행동 실행
        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            // 스페이스 쿨다운 갱신 및 행동 실행
            lastSpacePressTime = Time.time;
            StartCoroutine(SpaceCooldown());

            // 돌진 시작
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
        isDashing = true;
        dashTimer = 0f;

        // 돌진 방향 설정 (보스 위치를 향하도록)
        Vector3 dashDirection = (bossObject.transform.position - transform.position).normalized;

        // 돌진 힘 적용
        playerRigidbody.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        // 위로 가하는 힘 적용
        playerRigidbody.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 돌진 중에 적과 충돌하면 데미지 입히고 튕겨나감
        if (isDashing && other.CompareTag("Boss") && !hasDamagedBoss)
        {
            // 스페이스를 3초에 1번만 사용 가능하도록 체크
            if (Time.time - lastSpacePressTime >= spaceCooldown)
            {
                canUseSpace = true;
            }
            else
            {
                canUseSpace = false;
            }

            // 적에게 데미지 입히기
            BossHp bossHp = other.GetComponent<BossHp>();
            if (bossHp != null)
            {
                bossHp.TakeDamage(dashDamage);
                hasDamagedBoss = true;  // 데미지를 주었음을 표시
                Debug.Log(bossHp.currentHp);
                // 플레이어를 뒤로 튕겨나가는 힘 적용
                Vector3 direction = (other.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(-direction * boundsForce, ForceMode.Impulse);
                currentMovementSpeed = 5;
                Debug.Log("1");
            }
        }
    }
}
