using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    //public new Animation animation;
    Animator anim;
    private Rigidbody ballRigidbody;
    public GameObject Panel;
    public ClearUI panelManager;
    public TMP_Text[] panelTexts;

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

        else if (collision.gameObject.CompareTag("OpenUI"))
        {
            Panel.SetActive(true);
        }
    }
}
