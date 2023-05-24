using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Restart : MonoBehaviour
{
    private float time = 0;

    public void Update()
    {
        GampakGampak();
    }

    private void GampakGampak()
    {
        if(time < 0.7f)
        {
            GetComponent<TextMeshProUGUI>().color = new Color(1,1,1,1 - time);
        }
        else
        {
            GetComponent<TextMeshProUGUI>().color = new Color(1,1,1,time);
            if(time > 1.5f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }
}
