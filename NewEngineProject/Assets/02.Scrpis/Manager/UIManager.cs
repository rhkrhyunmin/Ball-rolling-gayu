using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject Navagation;

    [Header("Boss")]
    public GameObject bossWarningImage;
    public TextMeshProUGUI TextBossHp;
    public Slider BossHp;

    [Header("Speed")]
    public Slider speedSlider;
    public Image speedImage;
    [Header("Hp")]
    public Slider hpSlider;
    public Image hpImage;
    

    private void Awake()
    {
        // �̱��� �ν��Ͻ��� �����մϴ�.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����Ǿ �ı����� �ʵ��� �����մϴ�.
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������ ��ü�� �ı��մϴ�.
        }
    }
}
