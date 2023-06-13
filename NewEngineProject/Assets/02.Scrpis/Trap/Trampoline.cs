using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 1000f;
    public GameObject targetObject; // 이동할 대상 GameObject

    private bool isJumping = false;
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isJumping)
        {
            isJumping = true;
            Jump();
            animator.SetBool("jump", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            MoveToTarget();
            animator.SetBool("jump", false);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void MoveToTarget()
    {
        if (targetObject != null)
        {
            transform.position = targetObject.transform.position;
        }
    }
}
