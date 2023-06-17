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
        UpdateHealthBar(); // ü�� �� ������Ʈ

        if (currentHp <= 0f)
        {
            // �߰�: �������� ���� ü���� 0 ���ϰ� �Ǹ� ���� ���� ó�� ���� ������ �� �ֽ��ϴ�.
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
        healthSlider.value = currentHp; // ���� ü�� ���� ü�� �ٿ� �Ҵ�
        healthSlider.maxValue = maxHp; // �ִ� ü�� ���� ü�� �ٿ� �Ҵ�
    }
}
