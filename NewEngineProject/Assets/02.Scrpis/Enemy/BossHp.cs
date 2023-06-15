using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHp : MonoBehaviour
{
    public float currentHp = 0;
    public float maxHp = 10;

    private BossHpBar hpSlider;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        hpSlider = FindObjectOfType<BossHpBar>();
        if (hpSlider != null)
        {
            hpSlider.SetMaxHp(maxHp);
            hpSlider.SetHp(currentHp);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log(currentHp);
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (hpSlider != null)
        {
            hpSlider.SetHp(currentHp);
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsDie",false);
        //SceneManager.LoadScene(4);
    }
}
