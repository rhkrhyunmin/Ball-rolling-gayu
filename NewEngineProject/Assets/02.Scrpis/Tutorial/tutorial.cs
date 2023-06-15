using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tutorial : MonoBehaviour
{
    public GameObject textPrefab; // 표시할 TextMeshProUGUI 프리팹
    public Transform[] spawnPositions; // 생성 위치 배열
    public string[] textContents; // 표시할 텍스트 배열

    private GameObject[] gameObjects; // 생성된 GameObject 배열
    private TextMeshProUGUI[] textMeshes; // 생성된 TextMeshProUGUI 배열
    private bool isGamePaused; // 게임 일시정지 여부

    private void Start()
    {
        gameObjects = new GameObject[spawnPositions.Length]; // GameObject 배열 초기화
        textMeshes = new TextMeshProUGUI[spawnPositions.Length]; // TextMeshProUGUI 배열 초기화

        // GameObject 생성 및 위치 설정
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            gameObjects[i] = Instantiate(textPrefab, spawnPositions[i].position, Quaternion.identity);
            textMeshes[i] = gameObjects[i].GetComponent<TextMeshProUGUI>();
        }

        isGamePaused = true; // 게임 일시정지 상태로 시작
        Time.timeScale = 0f; // 게임 시간 정지
    }

    private void Update()
    {
        // "F" 키를 누르면 게임 재개
        if (Input.GetKeyDown(KeyCode.F))
        {
            isGamePaused = false;
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 GameObject의 인덱스 가져오기
        int index = System.Array.IndexOf(gameObjects, other.gameObject);

        // 인덱스에 해당하는 GameObject의 TextMeshProUGUI에 텍스트 설정
        if (index >= 0 && index < textMeshes.Length)
        {
            textMeshes[index].text = textContents[index];
            isGamePaused = true;
            Time.timeScale = 0f;
        }
    }
}
