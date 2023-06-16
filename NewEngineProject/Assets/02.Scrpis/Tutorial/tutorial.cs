using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class tutorial : MonoBehaviour
{
    public BoxCollider[] gameObjects; // ������ GameObject �迭
    public TextMeshProUGUI[] textMeshes; // ������ TextMeshProUGUI �迭
    private bool isGamePaused; // ���� �Ͻ����� ����

    private int currentIndex = 0;
    [SerializeField]
    private LayerMask layer;


    private void Update()
    {
        ObjCheck();

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
