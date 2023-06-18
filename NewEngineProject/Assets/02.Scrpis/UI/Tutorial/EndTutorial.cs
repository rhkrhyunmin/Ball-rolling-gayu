using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndTutorial : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // 콜라이더에 "Player" 태그가 있는 오브젝트가 닿으면
        {
            MainLoadingScene.LoadScene("Main"); // 지정한 씬을 로드합니다.
        }
    }
}
