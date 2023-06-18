using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainLoadingScene : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Main Loading");
        Debug.Log("1");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcsee());
    }

    IEnumerator LoadSceneProcsee()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime; // Time.unscaledTime ��� Time.unscaledDeltaTime�� ���
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true; // allowSceneActivation�� true�� �����Ͽ� �� Ȱ��ȭ
                    yield break;
                }
            }
        }
    }
}
