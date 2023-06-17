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
    private float nextTextTimer = 0f; // ���� �ؽ�Ʈ ��ȯ������ Ÿ�̸�


    private void Update()
    {
        if (!isNextTextRunning)
        {
            nextTextTimer += Time.deltaTime;
            if (nextTextTimer >= timeInterval)
            {
                //NextText();
                nextTextTimer = 0f;
                ObjCheck();

            }
        }

        // "F" Ű�� ������ ���� �簳
        if (Input.GetKeyDown(KeyCode.F))
        {
            isGamePaused = false;
            Time.timeScale = 1f;
        }
    }

    private void NextText()
    {
        if (isNextTextRunning)
            return;

        int newIndex = GetNextIndex();
        int previousIndex = currentIndex;

        if (newIndex != -1)
        {
            FirstText[currentIndex].gameObject.SetActive(false);
            currentIndex = newIndex;
            FirstText[currentIndex].gameObject.SetActive(true);
        }

        isNextTextRunning = true;
        ObjCheck();
    }
    private int GetNextIndex()
    {
        int count = FirstText.Length;
        if (count <= 1)
            return -1;

        int attempts = 0;
        int randomIndex = currentIndex;
        while (attempts < count)
        {
            randomIndex = (randomIndex + 1) % count;
            if (randomIndex != currentIndex && randomIndex != previousIndex)
                return randomIndex;

            attempts++;
        }

        return -1;
    }

    private void ObjCheck()
    {
        bool check = Physics.CheckBox(gameObjects[currentIndex].transform.position + gameObjects[currentIndex].center, gameObjects[currentIndex].size, Quaternion.identity, layer);
        if (check)
        {
            textMeshes[currentIndex].gameObject.SetActive(false);
            //NextText();
            textMeshes[currentIndex].gameObject.SetActive(true);    
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObjects[currentIndex].transform.position + gameObjects[currentIndex].center, gameObjects[currentIndex].size);
    }
}
