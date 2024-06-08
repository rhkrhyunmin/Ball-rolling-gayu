using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHp : MonoBehaviour
{
    public GameObject keyPrefab; // ���� ������
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

        UIManager.Instance.BossHp.value = currentHp / maxHp; // ���� ü�¹� ������Ʈ
        UIManager.Instance.TextBossHp.text = currentHp + " / " + maxHp;

        if (BossControl.bossState.currentHp <= 0)
        {
            SpawnKey();
            Destroy(gameObject); // ������ �����ϰų� ������ ���� ó���� �߰��� �� �ֽ��ϴ�.
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
