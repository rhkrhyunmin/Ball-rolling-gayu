using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public new Animation animation;
    Animator anim;
    public Rigidbody ballRigidbody;

    private void Awake()
    {
        anim = GetComponent<Animator>();   
        anim.enabled = false;
        ballRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // �ִϸ��̼� ����
            anim.enabled = true;
            ballRigidbody.useGravity = true;
        }
    }
}
