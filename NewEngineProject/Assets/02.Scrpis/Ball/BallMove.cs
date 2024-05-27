using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public LayerMask whatIsGround;
    private Player player;

    Camera cam;

    private bool canMove = true;
    private float accel = 5f,deAccel = 5f;

    private void Start()
    {
        cam = Camera.main;
        player = GetComponent<Player>();
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
            if(verticalInput != 0) player.ballSO.moveSpeed += Time.deltaTime * accel;
            else player.ballSO.moveSpeed -= Time.deltaTime * deAccel;
            player.ballSO.moveSpeed = Mathf.Clamp(player.ballSO.moveSpeed, 0, 5);

            player.rigid.AddForce(Vector3.ProjectOnPlane(dir, hit.normal).normalized * player.ballSO.moveSpeed, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canMove = true; 
        }
    }
}
