using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class tutorial : MonoBehaviour
{
    public BoxCollider[] gameObjects; // ������ GameObject �迭
    public TextMeshProUGUI[] textMeshes; // ������ TextMeshProUGUI �迭
    public TextMeshProUGUI[] FirstText;
    private bool isGamePaused; // ���� �Ͻ����� ����

    private int currentIndex = 0;
    private int previousIndex = -1;
    [SerializeField]
    private LayerMask layer;

    public float timeInterval = 5f; // 5�ʸ��� ���� �ؽ�Ʈ�� ��ȯ

    private float timer = 0f;
    private bool isNextTextRunning = false; // NextText() �Լ� ���� ����

    private void Update()
    {
        ObjCheck();

        if (!isGamePaused)
        {
            timer += Time.deltaTime;
            if (timer >= timeInterval)
            {
                timer = 0f;
                if (!isNextTextRunning)
                {
                    NextText();
                }
            }
        }

        // "F" Ű�� ������ ���� �簳
        if (Input.GetKeyDown(KeyCode.F))
        {
            isGamePaused = false;
            Time.timeScale = 1f;
        }
    }

    private void ObjCheck()
    {
        bool check = Physics.CheckBox(gameObjects[currentIndex].transform.position + gameObjects[currentIndex].center, gameObjects[currentIndex].size, Quaternion.identity, layer);
        if (check)
        {
            textMeshes[currentIndex].gameObject.SetActive(false);
            currentIndex++;
            if (currentIndex >= textMeshes.Length)
            {
                currentIndex = 0;
            }
            textMeshes[currentIndex].gameObject.SetActive(true);
        }
    }

    private void NextText()
    {
        int newIndex = GetNextIndex();

        if (newIndex != 0)
        {
            FirstText[currentIndex].gameObject.SetActive(false);
            currentIndex = (currentIndex + 1) % FirstText.Length;
            FirstText[currentIndex].gameObject.SetActive(true);
        }
        else
        {
            FirstText[currentIndex].gameObject.SetActive(false); // ���� �ؽ�Ʈ�� ��Ȱ��ȭ
        }
    }

    private int GetNextIndex()
    {
        int count = FirstText.Length;
        if (count <= 1)
        {
            return 0; // �迭�� ũ�Ⱑ 1������ ��쿡�� �׻� 0��° �ε����� ��ȯ
        }

        int attempts = 0;
        int randomIndex = UnityEngine.Random.Range(0, count); // ������ �ε������� ����
        while (attempts < count)
        {
            if (randomIndex != currentIndex && randomIndex != previousIndex)
            {
                return randomIndex; // ���� �ε����� ���� �ε����� �ƴ� ���, �ش� �ε��� ��ȯ
            }
            else
            {
                randomIndex = (randomIndex + 1) % count; // ���� �ε����� �̵�
                attempts++;
            }
        }

        return -1; // ������ �ε����� ã�� ���� ��� -1 ��ȯ
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObjects[currentIndex].transform.position + gameObjects[currentIndex].center, gameObjects[currentIndex].size);
    }
}
