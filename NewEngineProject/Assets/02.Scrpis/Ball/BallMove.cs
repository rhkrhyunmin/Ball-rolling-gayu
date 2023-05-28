using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] LayerMask whatIsGround;
    Rigidbody rigid;
    private bool canMove = true; // �̵� �������� ���θ� ��Ÿ���� ����

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.2f, whatIsGround);
        if (canMove)
        {
            // ����Ű �Է� ó��
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // ������ ó��
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            rigid.AddForce(Vector3.ProjectOnPlane(movement, hit.normal).normalized*speed, ForceMode.Force);
        }
    }

    private IEnumerator FireSequence()
    {
        // ���� �߻� ������ ����

        // ���� ���� ���� ������ ���
        yield return new WaitForSeconds(1f); // ���ϴ� ��� �ð�

        canMove = true; // �̵� ���
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ���� ����� ���� ó��
            canMove = true; // �̵� ���
        }
    }
}
