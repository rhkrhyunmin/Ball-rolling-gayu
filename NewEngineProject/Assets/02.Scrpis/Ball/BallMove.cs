using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    Rigidbody rigid;
    private bool canMove = true; // �̵� �������� ���θ� ��Ÿ���� ����
    Camera cam;
    private float curSpeed = 0;
    [SerializeField] private float accel = 5f,deAccel = 5f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        cam = Camera.main;
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
            if(verticalInput != 0) curSpeed += Time.deltaTime * accel;
            else curSpeed -= Time.deltaTime * deAccel;
            curSpeed = Mathf.Clamp(curSpeed, 0, 5);

            // ������ ó��

            rigid.AddForce(Vector3.ProjectOnPlane(dir, hit.normal).normalized*curSpeed, ForceMode.Force);
        }
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
