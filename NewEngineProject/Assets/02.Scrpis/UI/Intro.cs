using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public void NextScene()
    {
        LoadingScence.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
