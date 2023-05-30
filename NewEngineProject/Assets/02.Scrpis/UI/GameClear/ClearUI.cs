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
        // 패널 비활성화
        panelObject.SetActive(false);
    }

    private void Update()
    {
        if (panelObject.activeSelf)
        {
            displayTimer += Time.deltaTime;

            // 글 활성화 시간이 지났을 때
            if (displayTimer >= displayTime)
            {
                // 현재 글 비활성화
                enemy.panelTexts[currentTextIndex].gameObject.SetActive(false);

                // 다음 글 활성화
                currentTextIndex++;
                if (currentTextIndex < enemy.panelTexts.Length)
                {
                    enemy.panelTexts[currentTextIndex].gameObject.SetActive(true);
                }
                else
                {
                    // 모든 글이 나타났을 때 패널 비활성화
                    panelObject.SetActive(false);
                }

                displayTimer = 0f;
            }
        }
    }

    public void ShowPanel()
    {
        // 첫 번째 글 활성화
        currentTextIndex = 0;
        enemy.panelTexts[currentTextIndex].gameObject.SetActive(true);

        // 패널 활성화
        panelObject.SetActive(true);
    }
}
