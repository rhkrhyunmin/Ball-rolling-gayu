using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool hasCollided = false;
    public Animator animationComponent;

    private void Start()
    {
        // 애니메이션 컴포넌트 할당
        animationComponent = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ball과 충돌한 객체가 특정 조건을 만족하면 애니메이션 재생
        if (collision.gameObject.CompareTag("Ball") && !hasCollided)
        {
            hasCollided = true;
            // 애니메이션 재생
            animationComponent.SetTrigger("CharacterArmature|Death");
        }
    }
}
