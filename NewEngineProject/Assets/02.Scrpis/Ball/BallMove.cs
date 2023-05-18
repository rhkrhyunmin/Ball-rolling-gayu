using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private bool canMove = true; // 이동 가능한지 여부를 나타내는 변수

    private void Update()
    {
        if (canMove)
        {
            // 방향키 입력 처리
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 움직임 처리
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            transform.Translate(movement * speed * Time.deltaTime);
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
