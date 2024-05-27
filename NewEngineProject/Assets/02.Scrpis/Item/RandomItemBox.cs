using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("획득한 아이템: " + randomItem);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 여기서 랜덤으로 얻은 아이템을 처리할 수 있습니다.
        if (!string.IsNullOrEmpty(randomItem))
        {
            if (randomItem == "Boost")
            {
                
            }
            else if (randomItem == "Health")
            {
                HillPack hillPack = new HillPack();
                hillPack.OnHill();
            }
            else if (randomItem == "Shield")
            {
                
            }

            // 아이템을 사용한 후 초기화
            randomItem = null;
        }
    }
}
