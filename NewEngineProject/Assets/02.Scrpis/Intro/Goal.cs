using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public delegate void GoalReachedDelegate();
    public static event GoalReachedDelegate OnGoalReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IntroBall"))
        {
            // 목표 지점에 도달했을 때 이벤트 호출
            OnGoalReached?.Invoke();
            Destroy(other.gameObject); // 도달한 공을 삭제
        }
    }
}
