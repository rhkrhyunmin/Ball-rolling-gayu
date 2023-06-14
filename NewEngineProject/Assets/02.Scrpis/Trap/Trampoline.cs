using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 1000f;
    public GameObject targetObject;

    private bool isJumping = false;
    private Rigidbody rb;
    public Animator animator;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("jump", true);
            StartCoroutine(ResetJump());
            Debug.Log("1");
        }
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f); // 점프 후 대기할 시간 설정

        isJumping = false;
        animator.SetBool("jump", false);
    }

    private void MoveToTarget()
    {
        if (targetObject != null)
        {
            transform.position = targetObject.transform.position;
        }
    }
}
