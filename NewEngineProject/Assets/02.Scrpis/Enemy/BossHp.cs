using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHp : MonoBehaviour
{
    public float maxHp = 20f;
    public float currentHp = 0f;
    public float damageAmount = 3f;

    public Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        currentHp = maxHp;
        UpdateHealthBar();
        healthSlider.value = maxHp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar(); // 체력 바 업데이트
        Debug.Log(currentHp);

        if (currentHp <= 5f)
        {
            SceneManager.LoadScene("Clear");
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHp; // 현재 체력 값을 체력 바에 할당
        healthSlider.maxValue = maxHp; // 최대 체력 값을 체력 바에 할당
    }
}
