using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private float time = 0;

    private void ActionTime()
    {
        if(time > 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.8f);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
    }
}
