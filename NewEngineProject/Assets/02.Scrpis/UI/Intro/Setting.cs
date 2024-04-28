using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject SettingPanel;
    private bool isPanelActive = false;

    void Start()
    {
        SettingPanel.SetActive(false); // 시작 시에는 패널을 비활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 현재 패널의 활성화 상태에 따라 반대로 설정
            isPanelActive = !isPanelActive;
            SettingPanel.SetActive(isPanelActive);
        }
    }
}
