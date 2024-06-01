using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Slider speedSlider;
    public Slider hpSlider;

    public Image hpImage;
    public Image speedImage;

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
