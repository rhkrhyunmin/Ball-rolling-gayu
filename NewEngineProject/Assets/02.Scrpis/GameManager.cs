using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private OtherCannonShoot cannon;

    private void Start()
    {
        cannon = FindObjectOfType<OtherCannonShoot>();
    }

    private void Update()
    {
        cannon.Update();
    }
}
