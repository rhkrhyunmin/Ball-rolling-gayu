using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BallSO ballSO;

    public ParticleSystem shieldParticle;
    public ParticleSystem boostParticle;

    private Rigidbody rigid;

    public LayerMask whatIsGround;

    Camera cam;

    public bool isBoost;

    private bool canMove = true;
    private bool isDashing = false;       // ���� ������ ����
    private bool canUseSpace = true;     // �����̽� ��� �������� ����

    private float accel = 5f, deAccel = 5f , boundsForce = 10f;

    private void Start()
    {
        cam = Camera.main;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.2f, whatIsGround);

        if (canMove)
        {
            // ����Ű �Է� ó��
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 dir = cam.transform.forward;
            dir.y = 0;
            dir *= verticalInput;
            if (verticalInput != 0) ballSO.moveSpeed += Time.deltaTime * accel;
            else ballSO.moveSpeed -= Time.deltaTime * deAccel;
            ballSO.moveSpeed = Mathf.Clamp(ballSO.moveSpeed, 0, 10);

            rigid.AddForce(Vector3.ProjectOnPlane(dir, hit.normal).normalized * ballSO.moveSpeed, ForceMode.Force);
            OnBoost();
        }

        // �����̽� ��� ������ ���¿��� �����̽��� ������ �ൿ ����
        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceCooldown());
            StartDash();
        }
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

    private void OnBoost()
    {
        if (isBoost)
        {
            boostParticle.Play();
            StartCoroutine(BoostCo(5f));
        }
        else
        {
            //player.boostParticle.Stop();
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
