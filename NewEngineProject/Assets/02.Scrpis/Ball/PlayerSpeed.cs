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
        // 목표 위치 계산
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1f, 0.8f, 0));

        // UI 위치 부드럽게 이동
        speedUI.transform.position = Vector3.SmoothDamp(speedUI.transform.position, targetPosition, ref velocity, 0.08f);
    }

    
}
