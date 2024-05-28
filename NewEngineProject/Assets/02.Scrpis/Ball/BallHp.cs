using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHp : MonoBehaviour
{
    public bool isShield;

    private Player player;

    public GameObject gameOverPanel;
    public ParticleSystem shieldParticle;
    public Slider healthSlider; 

    private void Start()
    {
        player = GetComponent<Player>();
        healthSlider.maxValue = player.ballSO.maxHp;
        player.ballSO.currentHp = player.ballSO.maxHp;
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
            Onshield();
            isShield = false;
        }
    }

    public void Onshield()
    {
        isShield = true;

        if(isShield == true)
        {
            shieldParticle.Play();
        }
        else
        {
            shieldParticle.Stop();
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
