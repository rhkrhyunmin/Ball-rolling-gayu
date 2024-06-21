using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostPower = 7f;
    public float boostGage = 100;

    public bool isBoost = false;

    public ParticleSystem boostParticle;
    private Player ballMove;

    private void Start()
    {
        ballMove = FindObjectOfType<Player>();
    }

    public void CollectBoost()
    {
        if (ballMove.ballSO.moveSpeed <= 2)
        {
            boostGage -= ballMove.ballSO.moveSpeed * 2;
        }
        else
        {
            boostGage += ballMove.ballSO.moveSpeed / 1.5f;
        }
    }
    
    public IEnumerator OnBoost()
    {
        boostParticle.Play();
        if(boostGage >= 100 && !isBoost)
        {
            ballMove.ballSO.moveSpeed += boostPower;
        }

        yield return new WaitForSeconds(3f);

        boostParticle.Stop();
        ballMove.ballSO.moveSpeed -= boostPower;
        boostGage = 0;
    }
}
