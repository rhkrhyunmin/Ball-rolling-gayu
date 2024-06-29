using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public bool isShield;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        player.ballSO.currentHp = player.ballSO.maxHp;
    }

    private void Update()
    {
        Onshield();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EndLine"))
        {
            player.ballSO.currentHp = 0;
            ShowGameOver();
        }
    }

    public void Onshield()
    {
        if (isShield == true)
        {
            player.shieldParticle.Play();
        }
        else
        {
            player.shieldParticle.Stop();
        }
    }

    private void ShowGameOver()
    {
        UIManager.Instance.ActiveUI(UIManager.Instance.UIObjects.Find(obj => obj.name == "GameOverUI"));
        Time.timeScale = 0f;
    }
}
