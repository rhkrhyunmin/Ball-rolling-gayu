using System.Collections;
using UnityEngine;

public class TrapCannon : MonoBehaviour
{
    private string poolTag = "Bullet";
    public Player player;
    private bool isCheck = true;
    private float fireDelay = 5f;

    private void Start()
    {
        // Player�� ���� ���� �� �� ���� ã���� �մϴ�.
        player = FindObjectOfType<Player>();
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        // �� �����Ӹ��� �÷��̾� ������ ȸ���մϴ�.
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // ���⼭�� �ܼ��� �ٷ� ȸ����Ű����, ���� ���ӿ����� �ε巴�� ȸ���ϵ��� Slerp �Լ� ���� ����� �� �ֽ��ϴ�.
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }
    }

    IEnumerator FireRoutine()
    {
        // ���� ������ ����Ͽ� �ڷ�ƾ�� �ݺ� �����մϴ�.
        while (true)
        {
            // �߻� ���� �˻�
            if (isCheck)
            {
                // ���⼭ PoolManager�� ���� �Ѿ��� �����ϰ� �߻� ������ �����մϴ�.
                PoolManager.Instance.GetObject(poolTag);
                Debug.Log("Fire!");

                // ������ �����̸�ŭ ���
                yield return new WaitForSeconds(fireDelay);
            }
            else
            {
                // isCheck�� false�� ��� �ڷ�ƾ ������ �ߴ����� �ʰ�, ���� �������� ��ٸ��ϴ�.
                yield return null;
            }
        }
    }
}
