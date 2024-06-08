using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    public GameObject keyPrefab; // 열쇠 프리팹
    public Transform[] spawnZones; // 열쇠가 생성될 구역들

    private void Start()
    {
        SpawnKeyInRandomZone();
    }

    private void SpawnKeyInRandomZone()
    {
        // 랜덤하게 구역 선택
        int randomIndex = Random.Range(0, spawnZones.Length);
        Transform selectedZone = spawnZones[randomIndex];

        // 열쇠 생성
        Instantiate(keyPrefab, selectedZone.position, Quaternion.identity);
    }
}
