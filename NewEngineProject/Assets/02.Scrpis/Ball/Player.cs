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

    private Camera cam;

    public bool isBoost;

    private bool canMove = false;
    private bool isDashing = false;       
    private bool canUseSpace = true;

    private float accel = 5f, deAccel = 10f, boundsForce = 10f;

    private void Start()
    {
        cam = Camera.main;
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = false;  // ȸ���� ���
    }

    private void Update()
    {
        // �ٴڿ� ����ִ��� Ȯ�� (ȸ���� ����Ͽ� ���� Raycast ���)
        canMove = CheckGrounded();

        if (canMove)
        {
            // ����Ű �Է� ó��
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 dir = cam.transform.forward;
            dir.y = 0;
            dir *= verticalInput;

            // �ӵ� ���� �� ���� ó��
            if (verticalInput > 0)
            {
                ballSO.moveSpeed += Time.deltaTime * accel;
            }
            else if (verticalInput < 0)
            {
                ballSO.moveSpeed -= Time.deltaTime * accel; // �ڷ� �� ���� �ӵ��� ���ҽ�Ŵ
            }

            ballSO.moveSpeed = Mathf.Clamp(ballSO.moveSpeed, 0, 15);

            rigid.AddForce(dir.normalized * ballSO.moveSpeed, ForceMode.Force);
        }

        // �����̽� ��� ������ ���¿��� �����̽��� ������ �ൿ ����
        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceCooldown());
            StartDash();
        }
    }

    private void FixedUpdate()
    {
        // �߰� �߷� ����
        //if (!canMove)
        //{
        //    rigid.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
        //}
    }

    private bool CheckGrounded()
    {
        float rayLength = 0.5f; // Raycast ����
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
                rigid.AddForce(-direction * boundsForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OnBoss"))
        {
            StartCoroutine(BossUI(3f));
            GameManager.Instance.isBoss = true;
        }

        if (other.CompareTag("Goal"))
        {
            UIManager.Instance.VictroyUI();
        }

        if(other.CompareTag("Key"))
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

    IEnumerator BossUI(float duration)
    {
        UIManager.Instance.bossWarningImage.SetActive(true);
        yield return new WaitForSeconds(duration);

        UIManager.Instance.BossHp.gameObject.SetActive(true);
        UIManager.Instance.bossWarningImage.SetActive(false);
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
