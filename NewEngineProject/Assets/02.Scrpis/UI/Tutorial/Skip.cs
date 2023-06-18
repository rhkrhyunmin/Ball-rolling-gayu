using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{

    public  void LoadMainScene()
    {
        MainLoadingScene.LoadScene("Main");
    }
}
