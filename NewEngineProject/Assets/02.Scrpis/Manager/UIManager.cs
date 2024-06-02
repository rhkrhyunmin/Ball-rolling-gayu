using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject Navagation;
    public Slider BossHp;

    [Header("Speed")]
    public Slider speedSlider;
    public Image speedImage;
    [Header("Hp")]
    public Slider hpSlider;
    public Image hpImage;
    

    private void Awake()
    {
        // 싱글톤 인스턴스를 설정합니다.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴되지 않도록 설정합니다.
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로 생성된 객체를 파괴합니다.
        }
    }
}
