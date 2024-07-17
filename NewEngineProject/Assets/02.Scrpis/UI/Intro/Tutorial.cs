using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public Image[] images; // �̹������� ��� �迭
    public TextMeshProUGUI[] texts; // �ؽ�Ʈ���� ��� �迭
    private int currentIndex = 0; // ���� Ȱ��ȭ�� �̹����� �ؽ�Ʈ�� �ε���

    void Start()
    {
        // ��� �̹����� �ؽ�Ʈ�� ��Ȱ��ȭ�ϰ� ù ��° �̹����� Ȱ��ȭ
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
        int nextIndex = (currentIndex + 1) % images.Length; // ���� �ε��� ���
        ShowItem(nextIndex);
    }

    public void ShowPreviousItem()
    {
        int prevIndex = (currentIndex - 1 + images.Length) % images.Length; // ���� �ε��� ���
        ShowItem(prevIndex);
    }

    private void ShowItem(int index)
    {
        if (index < 0 || index >= images.Length || index >= texts.Length)
            return;

        // ���� �̹����� �ؽ�Ʈ�� ���̵� �ƿ�
        Sequence fadeOutSequence = DOTween.Sequence();
        fadeOutSequence.Append(images[currentIndex].DOFade(0, 0.5f));
        fadeOutSequence.Join(texts[currentIndex].DOFade(0, 0.5f));
        fadeOutSequence.OnComplete(() =>
        {
            images[currentIndex].gameObject.SetActive(false); // ���̵� �ƿ� �� ��Ȱ��ȭ
            texts[currentIndex].gameObject.SetActive(false); // ���̵� �ƿ� �� ��Ȱ��ȭ

            currentIndex = index;
            images[currentIndex].gameObject.SetActive(true); // �� �̹��� Ȱ��ȭ
            texts[currentIndex].gameObject.SetActive(true); // �� �ؽ�Ʈ Ȱ��ȭ

            // �� �̹����� �ؽ�Ʈ�� ���̵� ��
            images[currentIndex].color = new Color(1, 1, 1, 0); // ���İ� 0���� ����
            Sequence fadeInSequence = DOTween.Sequence();
            fadeInSequence.Append(images[currentIndex].DOFade(1, 0.5f));
            fadeInSequence.Join(texts[currentIndex].DOFade(1, 0.5f));
        });
    }
}
