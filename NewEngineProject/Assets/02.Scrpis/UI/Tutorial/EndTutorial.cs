using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndTutorial : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // �ݶ��̴��� "Player" �±װ� �ִ� ������Ʈ�� ������
        {
            MainLoadingScene.LoadScene("Main"); // ������ ���� �ε��մϴ�.
        }
    }
}
