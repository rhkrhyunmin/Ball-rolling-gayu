using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [Header("Alarm")]
    public Image alarm;

    [Header("Music")]
    public Slider SFXSlider;
    public AudioSource[] SFXSoure;
    public Slider BGMSlider;
    public AudioSource BGMSSoure;

    [Header("GrapicButton")]
    public Image highGrapicButton;
    public Image MediumGrapicButton;
    public Image LowGrapicButton;

    private void Update()
    {
        SoundVolum();
    }

    public void Exit()
    {
        UIManager.Instance.settingUI.gameObject.SetActive(false);
        UIManager.Instance.pauseUI.gameObject.SetActive(true);
    }

    public void SoundVolum()
    {
       foreach(var source in SFXSoure)
        {
            source.volume = SFXSlider.value;
        }
        
    }

    public void OpenAlarm()
    {

    }

    public void OpenURLOtherGame()
    {
        // 여기에 열고 싶은 URL을 입력하세요.
        Application.OpenURL("https://nickel-tracker-e45.notion.site/b4651968780847b9b754f6cae152fc23?v=bc55addce8d3429b87332b117f1576b6");
    }

    public void OpenURLAbout()
    {
        Application.OpenURL("https://ggm.gondr.net/user/profile/60");
    }

    #region 그래픽 설정
    private void SetGraphicsQuality(int qualityIndex)
    {
        // 유니티 품질 레벨 설정
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    public void SetHighQuality()
    {
        highGrapicButton.enabled = true;
        SetGraphicsQuality(5);
        MediumGrapicButton.enabled = false;
        LowGrapicButton.enabled = false;
    }

    // 중간 그래픽 품질로 설정
    public void SetMediumQuality()
    {
        MediumGrapicButton.enabled = true;
        SetGraphicsQuality(3);
        LowGrapicButton.enabled = false;
        highGrapicButton.enabled = false;
    }

    // 낮은 그래픽 품질로 설정
    public void SetLowQuality()
    {
        LowGrapicButton.enabled = true;
        SetGraphicsQuality(1);
        highGrapicButton.enabled = false;
        MediumGrapicButton.enabled = false;
    }

    
    #endregion
}
