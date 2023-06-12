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
        // 플레이어 컨트롤러 스크립트를 가지고 있는 오브젝트에서 해당 컴포넌트를 가져옵니다.
       

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

        // 플레이어와의 거리를 계산합니다.
        float distanceToPlayer = Vector3.Distance(transform.position, playerController.transform.position);
        Debug.Log(distanceToPlayer);
        

        // 거리에 따라 애니메이션을 변경하고 이동합니다.
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

        // 플레이어를 따라 이동합니다.
        if (isPlayerDetected)
        {
            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
            transform.LookAt(playerController.transform.position);
        }
    }
}
