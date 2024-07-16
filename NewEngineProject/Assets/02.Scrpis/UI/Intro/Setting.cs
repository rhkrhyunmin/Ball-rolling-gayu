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

    [Header("GrapicButton")]
    public Image highGrapicButton;
    public Image MediumGrapicButton;
    public Image LowGrapicButton;

    private void Awake()
    {
        DOTween.Init();
    }

    private void Update()
    {
        SoundVolum();
        BGMVolum();
    }

    public void SoundVolum()
    {
        foreach(var source in SoundManager.Instance.SFXSource)
        {
            source.volume = SoundManager.Instance.SFXSlider.value;
        }
    }

    public void BGMVolum()
    {
        SoundManager.Instance.BGMSource.volume = SoundManager.Instance.BGMSlider.value;
    }

    public void OpenAlarm()
    {
        // alarm 객체를 활성화하고 초기 스케일을 설정합니다.
        alarm.gameObject.SetActive(true);
        alarm.transform.localScale = Vector3.zero; // 스케일을 초기 상태로 설정

        // 새로운 시퀀스를 생성합니다.
        Sequence sequence = DOTween.Sequence();

        // 트윈 애니메이션을 시퀀스에 추가합니다.
        sequence.Append(alarm.transform.DOScale(1f, 1f).SetEase(Ease.InOutQuad));

        // 3초의 딜레이를 추가합니다.
        sequence.AppendInterval(3f);


        // 딜레이 후 alarm 객체를 비활성화하는 콜백을 추가합니다.
        sequence.AppendCallback(() => alarm.gameObject.SetActive(false));

        // 시퀀스를 재생합니다.
        sequence.Play();
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
