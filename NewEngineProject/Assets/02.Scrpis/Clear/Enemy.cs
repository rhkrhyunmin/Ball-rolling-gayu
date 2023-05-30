using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{   
    Animator anim;
    private Rigidbody ballRigidbody;
    public GameObject openUIPanelObject;
    private float panelDelay = 5f;
    private bool openUIPanelActivated = false;

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
            // 애니메이션 실행
            anim.enabled = true;
            ballRigidbody.useGravity = true;
        }

        if (collision.gameObject.CompareTag("OpenUI"))
        {
            Invoke("ActivateOpenUIPanel", panelDelay);
        }
    }

    private void ActivateOpenUIPanel()
    {
        openUIPanelObject.SetActive(true);
    }
}
