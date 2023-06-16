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

        // "F" 키를 누르면 게임 재개
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
            FirstText[currentIndex].gameObject.SetActive(false); // 현재 텍스트를 비활성화
        }
    }

    private int GetNextIndex()
    {
        int count = FirstText.Length;
        if (count <= 1)
        {
            return 0; // 배열의 크기가 1이하인 경우에는 항상 0번째 인덱스를 반환
        }

        int attempts = 0;
        int randomIndex = UnityEngine.Random.Range(0, count); // 랜덤한 인덱스에서 시작
        while (attempts < count)
        {
            if (randomIndex != currentIndex && randomIndex != previousIndex)
            {
                return randomIndex; // 이전 인덱스와 현재 인덱스가 아닌 경우, 해당 인덱스 반환
            }
            else
            {
                randomIndex = (randomIndex + 1) % count; // 다음 인덱스로 이동
                attempts++;
            }
        }

        return -1; // 적절한 인덱스를 찾지 못한 경우 -1 반환
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObjects[currentIndex].transform.position + gameObjects[currentIndex].center, gameObjects[currentIndex].size);
    }
}
