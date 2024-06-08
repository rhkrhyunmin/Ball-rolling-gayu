using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    public GameObject keyPrefab; // ���� ������
    public Transform[] spawnZones; // ���谡 ������ ������

    private void Start()
    {
        SpawnKeyInRandomZone();
    }

    private void SpawnKeyInRandomZone()
    {
        // �����ϰ� ���� ����
        int randomIndex = Random.Range(0, spawnZones.Length);
        Transform selectedZone = spawnZones[randomIndex];

        // ���� ����
        Instantiate(keyPrefab, selectedZone.position, Quaternion.identity);
    }
}
