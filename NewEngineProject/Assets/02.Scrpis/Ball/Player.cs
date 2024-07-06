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

            if (verticalInput > 0)
            {
                ballSO.moveSpeed += Time.deltaTime;
            }
            else if (verticalInput < 0)
            {
                ballSO.moveSpeed -= Time.deltaTime;
            }

            ballSO.moveSpeed = Mathf.Clamp(ballSO.moveSpeed, 0, 15);
            Vector3 normalizedDir = dir.normalized;
            Vector3 force = normalizedDir * ballSO.moveSpeed;
            rigid.AddForce(force, ForceMode.Force);

            float velocityMagnitude = rigid.velocity.magnitude;
            UIManager.Instance.speedGage.fillAmount = velocityMagnitude / 100;

            if(ballSO.moveSpeed > 10)
            {
                UIManager.Instance.boostPack.fillAmount++;
            }

            if(UIManager.Instance.boostPack.fillAmount == 1)
            {
                isBoost = true;
            }
        }

        if (canUseSpace && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceCooldown());
            OnBoost();
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
        }
    }

    private void OnBoost()
    {
        if (isBoost)
        {
            //boostParticle.Play();
            StartCoroutine(BoostCo(3f));
        }
    }

    private IEnumerator BoostCo(float delay)
    {
        rigid.AddForce(Vector3.forward * ballSO.moveSpeed * 3);
        yield return new WaitForSeconds(delay);
        boostParticle.Stop();
    }

    private IEnumerator SpaceCooldown()
    {
        canUseSpace = false;
        yield return new WaitForSeconds(ballSO.dashCooldown);
        canUseSpace = true;
    }
}
