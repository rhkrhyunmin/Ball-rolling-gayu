using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HillPack : RandomItemBox
{
    public int plusHp = 3;
    private Player player;

    public void OnHill()
    {
        player = FindObjectOfType<Player>();
        player.ballSO.currentHp += plusHp;
    }
}
