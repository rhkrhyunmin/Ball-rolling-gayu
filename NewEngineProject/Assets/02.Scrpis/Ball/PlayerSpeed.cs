using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSpeed : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        OnSpeedGage();
    }

    private void OnSpeedGage()
    {
        UIManager.Instance.speedSlider.value = player.ballSO.moveSpeed;
    }
}
