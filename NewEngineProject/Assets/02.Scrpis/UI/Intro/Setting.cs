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
        // alarm ��ü�� Ȱ��ȭ�ϰ� �ʱ� �������� �����մϴ�.
        alarm.gameObject.SetActive(true);
        alarm.transform.localScale = Vector3.zero; // �������� �ʱ� ���·� ����

        // ���ο� �������� �����մϴ�.
        Sequence sequence = DOTween.Sequence();

        // Ʈ�� �ִϸ��̼��� �������� �߰��մϴ�.
        sequence.Append(alarm.transform.DOScale(1f, 1f).SetEase(Ease.InOutQuad));

        // 3���� �����̸� �߰��մϴ�.
        sequence.AppendInterval(3f);


        // ������ �� alarm ��ü�� ��Ȱ��ȭ�ϴ� �ݹ��� �߰��մϴ�.
        sequence.AppendCallback(() => alarm.gameObject.SetActive(false));

        // �������� ����մϴ�.
        sequence.Play();
    }


    public void OpenURLOtherGame()
    {
        // ���⿡ ���� ���� URL�� �Է��ϼ���.
        Application.OpenURL("https://nickel-tracker-e45.notion.site/b4651968780847b9b754f6cae152fc23?v=bc55addce8d3429b87332b117f1576b6");
    }

    public void OpenURLAbout()
    {
        Application.OpenURL("https://ggm.gondr.net/user/profile/60");
    }

    #region �׷��� ����
    private void SetGraphicsQuality(int qualityIndex)
    {
        // ����Ƽ ǰ�� ���� ����
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    public void SetHighQuality()
    {
        highGrapicButton.enabled = true;
        SetGraphicsQuality(5);
        MediumGrapicButton.enabled = false;
        LowGrapicButton.enabled = false;
    }

    // �߰� �׷��� ǰ���� ����
    public void SetMediumQuality()
    {
        MediumGrapicButton.enabled = true;
        SetGraphicsQuality(3);
        LowGrapicButton.enabled = false;
        highGrapicButton.enabled = false;
    }

    // ���� �׷��� ǰ���� ����
    public void SetLowQuality()
    {
        LowGrapicButton.enabled = true;
        SetGraphicsQuality(1);
        highGrapicButton.enabled = false;
        MediumGrapicButton.enabled = false;
    }

    
    #endregion
}
