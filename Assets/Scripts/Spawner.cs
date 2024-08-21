using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private Timer _timer;
    
    private Enemy _spawnedEnemy;
    private float _spawnDelay = 2;

    public Action<Transform> EnemySpawned;

    private void OnEnable()
    {
        Spawn();
        _timer.TimeEmpty += Spawn;
    }

    private void OnDisable()
    {
        _spawnedEnemy.Died -= OnEnemyDied;
        _timer.TimeEmpty -= Spawn;
    }

    private void Spawn()
    {
        _spawnedEnemy = _enemyPrefabs[GetRandomChance()];

        var enemy = Instantiate(_spawnedEnemy, _spawnPoint, transform);
        enemy.SetTarget(_target);

        EnemySpawned?.Invoke(enemy.transform);

        enemy.Died += OnEnemyDied;
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
        return percent;
    }

    private void OnEnemyDied()
    {
        _timer.StartWork(_spawnDelay);
    }
}
