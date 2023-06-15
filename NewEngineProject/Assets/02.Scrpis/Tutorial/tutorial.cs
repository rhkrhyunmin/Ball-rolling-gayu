using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tutorial : MonoBehaviour
{
    public GameObject textPrefab; // ǥ���� TextMeshProUGUI ������
    public Transform[] spawnPositions; // ���� ��ġ �迭
    public string[] textContents; // ǥ���� �ؽ�Ʈ �迭

    private GameObject[] gameObjects; // ������ GameObject �迭
    private TextMeshProUGUI[] textMeshes; // ������ TextMeshProUGUI �迭
    private bool isGamePaused; // ���� �Ͻ����� ����

    private void Start()
    {
        gameObjects = new GameObject[spawnPositions.Length]; // GameObject �迭 �ʱ�ȭ
        textMeshes = new TextMeshProUGUI[spawnPositions.Length]; // TextMeshProUGUI �迭 �ʱ�ȭ

        // GameObject ���� �� ��ġ ����
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            gameObjects[i] = Instantiate(textPrefab, spawnPositions[i].position, Quaternion.identity);
            textMeshes[i] = gameObjects[i].GetComponent<TextMeshProUGUI>();
        }

        isGamePaused = true; // ���� �Ͻ����� ���·� ����
        Time.timeScale = 0f; // ���� �ð� ����
    }

    private void Update()
    {
        // "F" Ű�� ������ ���� �簳
        if (Input.GetKeyDown(KeyCode.F))
        {
            isGamePaused = false;
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� GameObject�� �ε��� ��������
        int index = System.Array.IndexOf(gameObjects, other.gameObject);

        // �ε����� �ش��ϴ� GameObject�� TextMeshProUGUI�� �ؽ�Ʈ ����
        if (index >= 0 && index < textMeshes.Length)
        {
            textMeshes[index].text = textContents[index];
            isGamePaused = true;
            Time.timeScale = 0f;
        }
    }
}
