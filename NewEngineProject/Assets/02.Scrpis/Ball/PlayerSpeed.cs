using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSpeed : MonoBehaviour
{
    public Slider speedSlider;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        speedSlider = FindObjectOfType<Slider>();
    }

    private void Update()
    {
        OnSpeedGage();
    }

    private void OnSpeedGage()
    {
        speedSlider.value = player.ballSO.moveSpeed * 2;
    }
}
