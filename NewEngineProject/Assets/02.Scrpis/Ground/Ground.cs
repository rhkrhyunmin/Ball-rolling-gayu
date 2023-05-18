using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool _isGround = false;
    
    public bool IsGround()
    {
        return _isGround;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            _isGround = true;
        else
            _isGround = false;
    }
}
