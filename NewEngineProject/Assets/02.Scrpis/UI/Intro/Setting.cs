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
        SettingPanel.SetActive(false); // ���� �ÿ��� �г��� ��Ȱ��ȭ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // ���� �г��� Ȱ��ȭ ���¿� ���� �ݴ�� ����
            isPanelActive = !isPanelActive;
            SettingPanel.SetActive(isPanelActive);
        }
    }
}
