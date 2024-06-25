using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public BallSO ballSO;

    public ParticleSystem shieldParticle;
    public ParticleSystem boostParticle;

    private Rigidbody rigid;

    public LayerMask whatIsGround;

    public bool isBoost;

    private bool canMove = false;
    private bool isDashing = false;
    private bool canUseSpace = true;

    private float accel = 5f, deAccel = 10f, boundsForce = 10f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = false;  // 회전은 허용4wwwwwww
    }

    private void Update()
    {
        // 바닥에 닿아있는지 확인 (회전을 고려하여 여러 Raycast 사용)
        canMove = CheckGrounded();

        if (canMove)
        {
            // 방향키 입력 처리
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 dir = new Vector3(horizontalInput, 0, verticalInput);

            // 속도 가속 및 감속 처리
            if (verticalInput > 0)
            {
                ballSO.moveSpeed += Time.deltaTime * accel;
            }
            else if (verticalInput < 0)
            {
                ballSO.moveSpeed -= Time.deltaTime * accel; // 뒤로 갈 때는 속도를 감소시킴
            }

            ballSO.moveSpeed = Mathf.Clamp(ballSO.moveSpeed, 0, 15);

            // dir.normalized의 각 성분을 ballSO.moveSpeed로 곱하여 힘 계산
            Vector3 normalizedDir = dir.normalized;
            Vector3 force = normalizedDir * ballSO.moveSpeed;

            rigid.AddForce(force, ForceMode.Force);

            // Rigidbody의 속도를 이용해 슬라이더 값 업데이트
            float velocityMagnitude = rigid.velocity.magnitude;
            UIManager.Instance.speedSlider.fillAmount = velocityMagnitude;

            Debug.Log("Velocity magnitude: " + velocityMagnitude); // Debug log 추가
        }

        // 스페이스 사용 가능한 상태에서 스페이스를 누르면 행동 실행
        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceCooldown());
            StartDash();
        }
    }

    private bool CheckGrounded()
    {
        float rayLength = 0.5f; // Raycast 길이
        Vector3[] raycastOrigins = new Vector3[]
        {
            transform.position + Vector3.forward * 0.5f,
            transform.position - Vector3.forward * 0.5f,
            transform.position + Vector3.right * 0.5f,
            transform.position - Vector3.right * 0.5f,
            transform.position
        };

        foreach (var origin in raycastOrigins)
        {
            if (Physics.Raycast(origin, Vector3.down, rayLength, whatIsGround))
            {
                return true;
            }
        }

        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canMove = true;
        }

        // 돌진 중에 보스와 충돌하면 데미지 입히고 튕겨나감
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            UIManager.Instance.VictroyUI();
        }

        if (other.CompareTag("Key"))
        {
            GameManager.Instance.isKey = true;
        }
    }

    private void OnBoost()
    {
        if (isBoost)
        {
            boostParticle.Play();
            StartCoroutine(BoostCo(5f));
        }
    }

    IEnumerator BoostCo(float delay)
    {
        rigid.AddForce(Vector3.forward * ballSO.moveSpeed * 3);
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator SpaceCooldown()
    {
        canUseSpace = false;  // 스페이스 사용 불가능 상태로 설정
        yield return new WaitForSeconds(ballSO.dashCooldown);
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
            rigid.AddForce(dashDirection * ballSO.dashForce + Vector3.up * ballSO.dashForce, ForceMode.Impulse);
        }
    }
}
