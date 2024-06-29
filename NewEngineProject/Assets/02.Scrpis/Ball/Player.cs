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
        rigid.freezeRotation = false;  // ȸ���� ���4wwwwwww
    }

    private void Update()
    {
        // �ٴڿ� ����ִ��� Ȯ�� (ȸ���� ����Ͽ� ���� Raycast ���)
        canMove = CheckGrounded();

        if (canMove)
        {
            // ����Ű �Է� ó��
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 dir = new Vector3(horizontalInput, 0, verticalInput);

            // �ӵ� ���� �� ���� ó��
            if (verticalInput > 0)
            {
                ballSO.moveSpeed += Time.deltaTime;
            }
            else if (verticalInput < 0)
            {
                ballSO.moveSpeed -= Time.deltaTime; // �ڷ� �� ���� �ӵ��� ���ҽ�Ŵ
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
        canUseSpace = false;  // �����̽� ��� �Ұ��� ���·� ����
        yield return new WaitForSeconds(ballSO.dashCooldown);
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
            rigid.AddForce(dashDirection * ballSO.dashForce + Vector3.up * ballSO.dashForce, ForceMode.Impulse);
        }
    }
}
