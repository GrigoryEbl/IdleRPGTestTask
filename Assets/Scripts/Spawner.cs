using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;

    private bool _canSpawn = true;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        var enemy = Instantiate(_enemyPrefab, _spawnPoint, transform);
        _canSpawn = false;
    }
}
