using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearUI : MonoBehaviour
{
    public GameObject panelObject;
    public TMP_Text[] texts;
    public float displayTime = 2f;
    private int currentTextIndex = 0;
    private float displayTimer = 0f;

    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        // �г� ��Ȱ��ȭ
        panelObject.SetActive(false);
    }

    private void Update()
    {
        if (panelObject.activeSelf)
        {
            displayTimer += Time.deltaTime;

            // �� Ȱ��ȭ �ð��� ������ ��
            if (displayTimer >= displayTime)
            {
                // ���� �� ��Ȱ��ȭ
                enemy.panelTexts[currentTextIndex].gameObject.SetActive(false);

                // ���� �� Ȱ��ȭ
                currentTextIndex++;
                if (currentTextIndex < enemy.panelTexts.Length)
                {
                    enemy.panelTexts[currentTextIndex].gameObject.SetActive(true);
                }
                else
                {
                    // ��� ���� ��Ÿ���� �� �г� ��Ȱ��ȭ
                    panelObject.SetActive(false);
                }

                displayTimer = 0f;
            }
        }
    }

    public void ShowPanel()
    {
        // ù ��° �� Ȱ��ȭ
        currentTextIndex = 0;
        enemy.panelTexts[currentTextIndex].gameObject.SetActive(true);

        // �г� Ȱ��ȭ
        panelObject.SetActive(true);
    }
}
