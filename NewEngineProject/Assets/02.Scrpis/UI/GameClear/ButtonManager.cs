using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject �˾Ƽ�����;

    private void Start()
    {
        �˾Ƽ�����.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Back()
    {
        �˾Ƽ�����.SetActive(true);
    }
}
