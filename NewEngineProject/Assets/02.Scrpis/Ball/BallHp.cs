using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHp : MonoBehaviour
{
    public bool isShield;
    private Player player;

    public GameObject gameOverPanel;
    public Slider healthSlider; 

    private void Start()
    {
        player = GetComponent<Player>();
        healthSlider.maxValue = player.ballSO.maxHp;
        player.ballSO.currentHp = player.ballSO.maxHp;
        UpdateHealthBar();
    }

    private void Update()
    {
        Onshield();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(player.ballSO.damageAmount);
        }

        if (collision.gameObject.CompareTag("EndLine"))
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        if(isShield == false)
        {
            player.ballSO.currentHp -= damage;
            UpdateHealthBar();

            if (player.ballSO.currentHp <= 0f)
            {
                ShowGameOver();
            }
        }
        else
        {
            isShield = false;
        }
    }

    public void Onshield()
    {
        if(isShield == true)
        {
            player.shieldParticle.Play();
        }
        else
        {
            player.shieldParticle.Stop();
        }
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = player.ballSO.currentHp; // ���� ü�� ���� ü�� �ٿ� �Ҵ�
    }
}
