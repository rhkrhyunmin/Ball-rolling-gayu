using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BallSO ballSO;
    public ParticleSystem boostParticle;
    private Rigidbody rigid;
    public LayerMask whatIsGround;

    public bool isBoost;
    private bool canMove = false;
    private bool canUseSpace = true;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = false;
    }

    private void Update()
    {
        canMove = CheckGrounded();

        if (canMove)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 dir = new Vector3(horizontalInput, 0, verticalInput);

            // ������ �ִ� �ӷ�
            float maxMoveSpeed = 15f;

            if (verticalInput > 0)
            {
                // �ӷ� ����
                ballSO.moveSpeed += Time.deltaTime;
            }
            else if (verticalInput < 0)
            {
                // �ӷ� ����
                ballSO.moveSpeed -= Time.deltaTime;
            }

            // �ִ� �ӷ� ����
            ballSO.moveSpeed = Mathf.Clamp(ballSO.moveSpeed, 0, maxMoveSpeed);

            Vector3 normalizedDir = dir.normalized;
            Vector3 force = normalizedDir * ballSO.moveSpeed;
            rigid.AddForce(force, ForceMode.Force);

            float velocityMagnitude = rigid.velocity.magnitude;

            // UI�� ǥ���� �ӷ� ��
            float displaySpeed = Mathf.Clamp(velocityMagnitude, 0, maxMoveSpeed);

            // UI ������Ʈ
            UIManager.Instance.speedGage.fillAmount = displaySpeed / maxMoveSpeed;

            if (velocityMagnitude > 10)
            {
                UIManager.Instance.boostPack.fillAmount += 0.001f;
            }

            OnBoost();

            if (isBoost == true)
            {
                UIManager.Instance.boostPack.fillAmount -= 0.01f;
            }
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
            UIManager.Instance.ActiveUI(UIManager.Instance.UIObjects.Find(obj => obj.name == "VictoryUI"));
            GameManager.Instance.isGoal = true;
        }
    }

    private void OnBoost()
    {
        if (UIManager.Instance.boostPack.fillAmount == 1 && Input.GetKeyDown(KeyCode.F))
        {
            isBoost = true;
            boostParticle.transform.rotation = Quaternion.identity;
            boostParticle.Play();
            StartCoroutine(BoostCo(2f));
        }
    }

    private IEnumerator BoostCo(float delay)
    {
        rigid.AddForce(Vector3.forward * ballSO.moveSpeed * 3);
        yield return new WaitForSeconds(delay);
        boostParticle.Stop();
        rigid.AddForce(Vector3.forward * ballSO.moveSpeed / 3);
    }
}
