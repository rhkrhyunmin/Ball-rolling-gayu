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
            Debug.Log("ȹ���� ������: " + randomItem);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // ���⼭ �������� ���� �������� ó���� �� �ֽ��ϴ�.
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

            // �������� ����� �� �ʱ�ȭ
            randomItem = null;
        }
    }
}
