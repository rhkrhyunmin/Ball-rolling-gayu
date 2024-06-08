using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHp : MonoBehaviour
{
    public GameObject keyPrefab; // 열쇠 프리팹
    private BossControl BossControl;

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

        UIManager.Instance.BossHp.value = currentHp / maxHp; // 보스 체력바 업데이트
        UIManager.Instance.TextBossHp.text = currentHp + " / " + maxHp;

        if (BossControl.bossState.currentHp <= 0)
        {
            SpawnKey();
            Destroy(gameObject); // 보스를 삭제하거나 적절한 죽음 처리를 추가할 수 있습니다.
        }
    }

    private void SpawnKey()
    {
        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }
    }
}
