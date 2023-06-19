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
            // ��ǥ ������ �������� �� �̺�Ʈ ȣ��
            OnGoalReached?.Invoke();
            Destroy(other.gameObject); // ������ ���� ����
        }
    }
}
