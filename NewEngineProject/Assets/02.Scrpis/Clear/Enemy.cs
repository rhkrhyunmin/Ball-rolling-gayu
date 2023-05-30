using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool hasCollided = false;
    public Animator animationComponent;

    private void Start()
    {
        // �ִϸ��̼� ������Ʈ �Ҵ�
        animationComponent = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ball�� �浹�� ��ü�� Ư�� ������ �����ϸ� �ִϸ��̼� ���
        if (collision.gameObject.CompareTag("Ball") && !hasCollided)
        {
            hasCollided = true;
            // �ִϸ��̼� ���
            animationComponent.SetTrigger("CharacterArmature|Death");
        }
    }
}
