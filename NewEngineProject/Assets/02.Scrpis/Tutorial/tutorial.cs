using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class tutorial : MonoBehaviour
{
    public BoxCollider[] gameObjects; // 생성된 GameObject 배열
    public TextMeshProUGUI[] textMeshes; // 생성된 TextMeshProUGUI 배열
    private bool isGamePaused; // 게임 일시정지 여부

    private int currentIndex = 0;
    [SerializeField]
    private LayerMask layer;


    private void Update()
    {
        ObjCheck();

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
        if(check)
        {
            textMeshes[currentIndex].gameObject.SetActive(false);
            currentIndex++;
            textMeshes[currentIndex].gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObjects[currentIndex].transform.position + gameObjects[currentIndex].center, gameObjects[currentIndex].size);
    }
}
