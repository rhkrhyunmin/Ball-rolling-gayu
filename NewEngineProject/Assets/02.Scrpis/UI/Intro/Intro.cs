using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Intro : MonoSingleton<Intro>
{
    public GameObject stageUI;
    public RectTransform panel;
    public TextMeshProUGUI _startText;
    public float animationDuration = 0.5f;
    private Vector3 offScreenPosition;

    protected override void Awake()
    {
        base.Awake();

        // 패널 애니메이션 초기화
        offScreenPosition = new Vector3(Screen.width + 70, panel.localPosition.y, panel.localPosition.z);
        ResetPanelPosition(); // 패널 위치를 초기화
    }

    public void OpenPanel()
    {
        panel.DOLocalMove(Vector3.zero, animationDuration).SetEase(Ease.OutBack); // 패널을 화면 중앙으로 이동
    }

    public void ClosePanel()
    {
        panel.DOLocalMove(offScreenPosition, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            // 필요한 작업을 OnComplete에 추가할 수 있습니다.
        });
    }

    public void NextScene()
    {
        // 패널을 닫고 다음 씬으로 이동
        ClosePanel();
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        // 씬을 비동기적으로 로드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("NextSceneName"); // 실제 씬 이름으로 변경하세요.

        // 씬 로드가 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 씬 로드 완료 후 패널 초기화 및 애니메이션 실행
        OnSceneLoaded();
    }

    private void OnSceneLoaded()
    {
        ResetPanelPosition();
        OpenPanel();
    }

    private void ResetPanelPosition()
    {
        panel.localPosition = offScreenPosition;
    }
}
