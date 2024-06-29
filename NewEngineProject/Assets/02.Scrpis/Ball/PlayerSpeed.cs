using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeed : MonoBehaviour
{
    private Player player;
    public GameObject speedUI;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void LateUpdate()
    {
        // ��ǥ ��ġ ���
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1f, 0.8f, 0));

        // UI ��ġ �ε巴�� �̵�
        speedUI.transform.position = Vector3.SmoothDamp(speedUI.transform.position, targetPosition, ref velocity, 0.08f);
    }

    
}
