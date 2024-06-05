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
        float currentHp = BossControl.bossState.currentHp;
        float maxHp = BossControl.bossState.MaxHp;

        UIManager.Instance.BossHp.value += currentHp;
        UIManager.Instance.TextBossHp.text = currentHp + " / " + maxHp;

        if(BossControl.bossState.currentHp <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
