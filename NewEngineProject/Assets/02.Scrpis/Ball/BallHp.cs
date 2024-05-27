using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHp : MonoBehaviour
{
    public BallSO ballSO;
    public GameObject gameOverPanel;
    public Slider healthSlider; 

    private void Start()
    {
        healthSlider.maxValue = ballSO.maxHp; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(ballSO.damageAmount);
        }

        if (collision.gameObject.CompareTag("EndLine"))
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        ballSO.currentHp -= damage;
        UpdateHealthBar(); 

        if (ballSO.currentHp <= 0f)
        {
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
        healthSlider.value = ballSO.currentHp; // 현재 체력 값을 체력 바에 할당
    }
}
