using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject SettingPanel;

    public void OnPanel()
    {
        SettingPanel.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SettingPanel.SetActive(false); // 패널을 비활성화하여 숨깁니다.
        }
    }
}
