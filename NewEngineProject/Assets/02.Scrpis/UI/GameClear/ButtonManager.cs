using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject 알아서쓰기;

    private void Start()
    {
        알아서쓰기.SetActive(true);
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
        알아서쓰기.SetActive(true);
    }
}
