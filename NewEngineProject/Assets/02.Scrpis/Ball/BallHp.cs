using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHp : MonoBehaviour
{
    public float maxHp = 20f;
    public float currentHp = 0f;
    public float damageAmount = 3f;

    public GameObject gameOverPanel;
    public Slider healthSlider; 

    private void Start()
    {
        currentHp = maxHp;
        UpdateHealthBar(); 
        healthSlider.value = maxHp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(damageAmount);
        }

        if (collision.gameObject.CompareTag("EndLine"))
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        /*if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(damageAmount);
        }*/
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar(); // 체력 바 업데이트

        if (currentHp <= 0f)
        {
            // 추가: 데미지로 인해 체력이 0 이하가 되면 게임 오버 처리 등을 수행할 수 있습니다.
            ShowGameOver();
        }
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHp; // 현재 체력 값을 체력 바에 할당
        healthSlider.maxValue = maxHp; // 최대 체력 값을 체력 바에 할당
    }
}
