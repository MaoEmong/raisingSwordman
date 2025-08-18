using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SpawnPosition;

    public GameObject EnemyPrefab;

    public BackgroundScroll backScroll;

    public GameObject SpawnEnemy()
    {
        GameObject enemy = Managers.Pool.Pop(EnemyPrefab);
        enemy.transform.position = SpawnPosition.transform.position;

        int val = Random.Range(1, 20);

        enemy.GetComponent<EnemyScript>().Init(val, backScroll);

        return enemy;
    }

}
