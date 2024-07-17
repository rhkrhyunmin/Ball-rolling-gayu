using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed = 10f;
    public Vector3 startPoint;

    private GameObject player;
    private Animator animator;

    void Start()
    {
        transform.position = new Vector3(startPoint.x, startPoint.y, startPoint.z);
        animator = GetComponent<Animator>();
        // Player 태그를 가진 게임 오브젝트를 찾기
    }

    void Update()
    {
        if (GameManager.Instance.isShrinking)
        {
            player = GameObject.FindGameObjectWithTag("Ball");
            MoveZone();
        }
    }

    void MoveZone()
    {
        if (player != null)
        {
            // Target position with y value decreased by 1
            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
            Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                GameManager.Instance.isShrinking = false;
            }
        }
    }
}
