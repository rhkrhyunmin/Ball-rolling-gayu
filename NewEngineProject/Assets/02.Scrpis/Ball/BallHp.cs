using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHp : MonoBehaviour
{
    public float maxHp = 20f;
    public float currentHp = 0f;
    public float damageAmount = 5f;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Slider healthSlider; 

    private void Start()
    {
        currentHp = maxHp;
       /* GameObject instance = Instantiate(gameOverPanel);
        BallHp hpScript = instance.GetComponent<BallHp>();

        // 프리팹 인스턴스의 HP 스크립트에 healthSlider를 설정합니다.
        // 프리팹 내부에 있는 Slider UI 요소를 할당해야 합니다.
        Slider slider = instance.GetComponentInChildren<Slider>();*/
        UpdateHealthBar(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(damageAmount);
            
        }
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
