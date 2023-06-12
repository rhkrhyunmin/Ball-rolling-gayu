using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float detectionDistance = 10f;
    public float movementSpeed = 2f;
    public Animator animator;

    [SerializeField]
    private BallMove playerController;
    private bool isPlayerDetected = false;

    private bool isIdle = true;

    private void Start()
    {
        // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� ������ �ִ� ������Ʈ���� �ش� ������Ʈ�� �����ɴϴ�.
       

        if (playerController == null)
        {
            
        }
    }

    public void FindPlayer(BallMove ball)
    {
        playerController = ball;
    }

    private void Update()
    {
        if (playerController == null)
        {
            return;
        }

        // �÷��̾���� �Ÿ��� ����մϴ�.
        float distanceToPlayer = Vector3.Distance(transform.position, playerController.transform.position);
        Debug.Log(distanceToPlayer);
        

        // �Ÿ��� ���� �ִϸ��̼��� �����ϰ� �̵��մϴ�.
        if (distanceToPlayer <= detectionDistance)
        {
            animator.SetBool("IsWalk", true);
            isPlayerDetected = true;
            isIdle = false;

        }
        else
        {
            animator.SetBool("IsWalk", false);
            isPlayerDetected = false;
            isIdle = true;
        }

        // �÷��̾ ���� �̵��մϴ�.
        if (isPlayerDetected)
        {
            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
            transform.LookAt(playerController.transform.position);
        }
    }
}
