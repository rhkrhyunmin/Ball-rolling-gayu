using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : RandomItemBox
{
    public float boostPower = 7f;
    public ParticleSystem boostParticle;
    private BallMove ballMove;

    public void ONBoost()
    {
        ballMove = FindObjectOfType<BallMove>();
        boostParticle.Play();
        //ballMove.rigid.AddForce();
    }
}
