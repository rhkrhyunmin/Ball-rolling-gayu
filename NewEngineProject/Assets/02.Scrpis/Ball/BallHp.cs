using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHp : MonoBehaviour
{
    public float maxHp = 20;
    public float currentHp = 0;

    GameOver gameOver;

    private void Start()
    {
        gameOver = gameObject.GetComponent<GameOver>();
    }

    public void OnDamage()
    {
        if(currentHp < maxHp)
        {
            gameOver.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
