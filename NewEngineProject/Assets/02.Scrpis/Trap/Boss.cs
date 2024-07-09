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
        transform.position = startPoint;
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
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        if (transform.position == player.transform.position)
        {
            GameManager.Instance.isShrinking = false;
        }
    }
}
