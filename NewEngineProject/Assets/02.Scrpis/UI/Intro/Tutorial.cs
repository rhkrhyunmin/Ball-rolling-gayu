using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public Image[] images; // 이미지들을 담는 배열
    public TextMeshProUGUI[] texts; // 텍스트들을 담는 배열
    private int currentIndex = 0; // 현재 활성화된 이미지와 텍스트의 인덱스

    void Start()
    {
        // 모든 이미지와 텍스트를 비활성화하고 첫 번째 이미지를 활성화
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
            texts[i].gameObject.SetActive(false);
        }
        if (images.Length > 0 && texts.Length > 0)
        {
            images[0].gameObject.SetActive(true);
            texts[0].gameObject.SetActive(true);
        }
    }

    public void ShowNextItem()
    {
        int nextIndex = (currentIndex + 1) % images.Length; // 다음 인덱스 계산
        ShowItem(nextIndex);
    }

    public void ShowPreviousItem()
    {
        int prevIndex = (currentIndex - 1 + images.Length) % images.Length; // 이전 인덱스 계산
        ShowItem(prevIndex);
    }

    private void ShowItem(int index)
    {
        if (index < 0 || index >= images.Length || index >= texts.Length)
            return;

        // 현재 이미지와 텍스트를 페이드 아웃
        Sequence fadeOutSequence = DOTween.Sequence();
        fadeOutSequence.Append(images[currentIndex].DOFade(0, 0.5f));
        fadeOutSequence.Join(texts[currentIndex].DOFade(0, 0.5f));
        fadeOutSequence.OnComplete(() =>
        {
            images[currentIndex].gameObject.SetActive(false); // 페이드 아웃 후 비활성화
            texts[currentIndex].gameObject.SetActive(false); // 페이드 아웃 후 비활성화

            currentIndex = index;
            images[currentIndex].gameObject.SetActive(true); // 새 이미지 활성화
            texts[currentIndex].gameObject.SetActive(true); // 새 텍스트 활성화

            // 새 이미지와 텍스트를 페이드 인
            images[currentIndex].color = new Color(1, 1, 1, 0); // 알파값 0으로 설정
            Sequence fadeInSequence = DOTween.Sequence();
            fadeInSequence.Append(images[currentIndex].DOFade(1, 0.5f));
            fadeInSequence.Join(texts[currentIndex].DOFade(1, 0.5f));
        });
    }
}
