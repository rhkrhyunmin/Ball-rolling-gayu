using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public BallSO ballSO;
    public Image speedSlider;

    public ParticleSystem shieldParticle;
    public ParticleSystem boostParticle;

    private Rigidbody rigid;

    public LayerMask whatIsGround;

    public bool isBoost;

    private bool canMove = false;
    private bool isDashing = false;
    private bool canUseSpace = true;

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
                ballSO.moveSpeed += Time.deltaTime;
            }
            else if (verticalInput < 0)
            {
                ballSO.moveSpeed -= Time.deltaTime; // 뒤로 갈 때는 속도를 감소시킴
            }

            ballSO.moveSpeed = Mathf.Clamp(ballSO.moveSpeed, 0, 15);

            Vector3 normalizedDir = dir.normalized;
            Vector3 force = normalizedDir * ballSO.moveSpeed;

            rigid.AddForce(force, ForceMode.Force);

            float velocityMagnitude = rigid.velocity.magnitude;
            speedSlider.fillAmount = velocityMagnitude / 100;
        }

        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceCooldown());
            StartDash();
        }
    }

    private bool CheckGrounded()
    {
        float rayLength = 0.5f;
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
