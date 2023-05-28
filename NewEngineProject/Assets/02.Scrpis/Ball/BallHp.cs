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

        // ������ �ν��Ͻ��� HP ��ũ��Ʈ�� healthSlider�� �����մϴ�.
        // ������ ���ο� �ִ� Slider UI ��Ҹ� �Ҵ��ؾ� �մϴ�.
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
