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
        transform.position = new Vector3(startPoint.x, -2, startPoint.z);
        animator = GetComponent<Animator>();
        // Player �±׸� ���� ���� ������Ʈ�� ã��
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
        Vector3 targetPosition = new Vector3(player.transform.position.x, -2, player.transform.position.z);
        Vector3 currentPosition = new Vector3(transform.position.x, -2, transform.position.z);
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            GameManager.Instance.isShrinking = false;
        }
    }
}