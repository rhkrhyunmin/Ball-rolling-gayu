using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private bool isJumping = false;
    private Rigidbody rb;
    private Animator animator;

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
            Vector3 jumpDirection = Vector3.up * 75 + Vector3.forward * 75;
            rb.AddForce(jumpDirection, ForceMode.Impulse);
            animator.SetBool("jump", true);
            StartCoroutine(ResetJump());

            //audioSource.PlayOneShot(jumpSound);
        }
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f);

        isJumping = false;
        animator.SetBool("jump", false);
    }
}
