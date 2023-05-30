using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearUI : MonoBehaviour
{
    public GameObject panelObject;
    public TMP_Text[] texts;
    public float displayTime = 5f;
    private int currentTextIndex = 0;
    private float displayTimer = 0f;

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
                texts[currentTextIndex].gameObject.SetActive(false);

                // ���� �� Ȱ��ȭ
                currentTextIndex++;
                if (currentTextIndex < texts.Length)
                {
                    texts[currentTextIndex].gameObject.SetActive(true);
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
        texts[currentTextIndex].gameObject.SetActive(true);

        // �г� Ȱ��ȭ
        panelObject.SetActive(true);
    }
}
