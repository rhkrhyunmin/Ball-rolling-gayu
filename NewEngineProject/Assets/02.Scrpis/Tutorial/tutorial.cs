using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class tutorial : MonoBehaviour
{
    public BoxCollider[] gameObjects; // 생성된 GameObject 배열
    public TextMeshProUGUI[] textMeshes; // 생성된 TextMeshProUGUI 배열
    public TextMeshProUGUI[] FirstText;
    private bool isGamePaused; // 게임 일시정지 여부

    private int currentIndex = 0;
    private int previousIndex = -1;
    [SerializeField]
    private LayerMask layer;

    public float timeInterval = 5f; // 5초마다 다음 텍스트로 전환

    private float timer = 0f;
    private bool isNextTextRunning = false; // NextText() 함수 실행 여부
    private float nextTextTimer = 0f; // 다음 텍스트 전환까지의 타이머


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

        // "F" 키를 누르면 게임 재개
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
