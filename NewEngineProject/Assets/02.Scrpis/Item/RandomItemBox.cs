using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class RandomItemBox : MonoBehaviour
{
    public List<string> itemList;
    private string randomItem;

    private void Start()
    {
        itemList = new List<string> { "Boost", "Health", "Shield" };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            int randomIndex = Random.Range(0, itemList.Count);
            randomItem = itemList[randomIndex];

            OnBuff();
        }

        gameObject.SetActive(false);
    }

    public void OnBuff()
    {
        Player player = FindObjectOfType<Player>();
        PlayerHp ballHp = FindObjectOfType<PlayerHp>();

        if (randomItem == "Boost")
        {
            Debug.Log("B");
            player.isBoost = true;
        }
        else if (randomItem == "Health")
        {
            player.ballSO.currentHp += 3f;
        }
        else if (randomItem == "Shield")
        {
            ballHp.isShield = true;
        }
    }
}
