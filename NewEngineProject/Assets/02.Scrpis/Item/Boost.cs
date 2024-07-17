using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Boost : MonoBehaviour
{
    public Volume volume;
    public bool isBoost = false;

    public ParticleSystem boostParticle;
    private Player ballMove;

    private void Update()
    {
        ballMove = FindObjectOfType<Player>();
    }

   public void Play()
    {
        volume.weight = 1.0f;
        boostParticle.transform.position = ballMove.transform.position;
        boostParticle.Play();
    }

    public void Stop()
    {
        volume.weight = 0f;
        boostParticle.Stop();
    }
}
