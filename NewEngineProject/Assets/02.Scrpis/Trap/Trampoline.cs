using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 1000f;

    private bool isJumping = false;
    private Rigidbody rb;
    public Animator animator;

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip jumpSound;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

            audioSource.PlayOneShot(jumpSound);
        }
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f); 

        isJumping = false;
        animator.SetBool("jump", false);
    }

}
