using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHp : MonoBehaviour
{
   BossControl BossControl;

    private void Start()
    {
        BossControl = GetComponent<BossControl>();
    }

    private void Update()
    {
        UpdateHpUI();
    }

    private void UpdateHpUI()
    {
        UIManager.Instance.BossHp.value += BossControl.bossState.currentHp;
        if(BossControl.bossState.currentHp <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
