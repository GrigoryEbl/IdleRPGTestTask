using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Transform _spawnPoint;

    public Action<Transform> EnemySpawned;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        var enemy = Instantiate(_enemyPrefabs[GetRandomChance()], _spawnPoint, transform);
        EnemySpawned?.Invoke(enemy.transform);
    }

    private int GetRandomChance()
    {
        int percent = CalculateChance();

        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            if (percent <= _enemyPrefabs[i].ChanceSpawn)
            {
                return i;
            }
        }

        return 0;
    }

    private int CalculateChance()
    {
        int maxPercent = 100;

        int percent = UnityEngine.Random.Range(0, maxPercent);
        print("Percent spawn: " + percent);
        return percent;
    }
}
