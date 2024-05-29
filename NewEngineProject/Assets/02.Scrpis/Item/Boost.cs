using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : RandomItemBox
{
    public float boostPower = 7f;
    public ParticleSystem boostParticle;
    private Player ballMove;

    public void ONBoost()
    {
        ballMove = FindObjectOfType<Player>();
        boostParticle.Play();
        //player.rigid.AddForce();
    }
}
