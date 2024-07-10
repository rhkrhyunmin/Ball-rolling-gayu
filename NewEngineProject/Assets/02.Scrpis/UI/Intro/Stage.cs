using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public string sceneName; // �̵��� �� �̸�
    public int stageIndex; // �������� �ε���
    private Button button;
    private Image buttonImage;

    [Header("Loading")]
    public GameObject LoadingUI;
    public TextMeshProUGUI loadingText;
    public Slider loadingSlider;

    public Image unlockedImage; // �رݵ� �������� �̹���
    public Image lockedImage; // ��� �������� �̹���

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // ���������� �رݵǾ� �ִ��� Ȯ��
        if (IsStageUnlocked())
        {
            button.onClick.AddListener(OnButtonClicked);
            unlockedImage.gameObject.SetActive(true);
            lockedImage.gameObject.SetActive(false);
        }
        else
        {
            unlockedImage.gameObject.SetActive(false);
            lockedImage.gameObject.SetActive(true);
        }
    }

    bool IsStageUnlocked()
    {
        bool isUnlocked = PlayerPrefs.GetInt("Stage" + stageIndex.ToString() + "Unlocked", 0) == 1;
        return isUnlocked;
    }

    void OnButtonClicked()
    {
        LoadingUI.SetActive(true);
        LoadLevel(sceneName);
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync(string levelName)
    {
        LoadingUI.SetActive(true); // Ensure the loading UI is active before loading

        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            loadingSlider.value = progress;
            Debug.Log($"Loading progress: {progress * 100f}%");
            loadingText.text = (progress * 100f).ToString("F2") + "%";
            yield return null;
        }

        // �ε� �Ϸ�
        loadingSlider.value = 1f;
        loadingText.text = "100%";

        yield return new WaitForSeconds(0.5f);
        op.allowSceneActivation = true;
    }
}
