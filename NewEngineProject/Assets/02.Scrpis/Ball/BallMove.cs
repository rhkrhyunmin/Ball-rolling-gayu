using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] LayerMask whatIsGround;
    Rigidbody rigid;
    private bool canMove = true; // 이동 가능한지 여부를 나타내는 변수
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
            // 방향키 입력 처리
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 dir = cam.transform.forward;
            dir.y = 0;
            dir *= verticalInput;
            if(verticalInput != 0)curSpeed += Time.deltaTime * accel;
            else curSpeed -= Time.deltaTime * deAccel;
            curSpeed = Mathf.Clamp(curSpeed, 0, speed);

            // 움직임 처리

            rigid.AddForce(Vector3.ProjectOnPlane(dir, hit.normal).normalized*curSpeed, ForceMode.Force);
        }
    }

    private IEnumerator FireSequence()
    {
        // 대포 발사 시퀀스 실행

        // 공이 땅에 닿을 때까지 대기
        yield return new WaitForSeconds(1f); // 원하는 대기 시간

        canMove = true; // 이동 허용
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 땅에 닿았을 때의 처리
            canMove = true; // 이동 허용
        }
    }
}
