using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HillPack : MonoBehaviour
{
    public int plusHp = 3;
    private BallHp ballHp;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if (ballHp == null)
            {
                ballHp = FindObjectOfType<BallHp>();
            }

            if (ballHp != null)
            {
                ballHp.ballSO.currentHp += plusHp;
            }
        }
    }
}
