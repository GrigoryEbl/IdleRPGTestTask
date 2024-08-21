using System;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.EnemySpawned += SetTarget;
    }

    private void OnDisable()
    {
        _spawner.EnemySpawned -= SetTarget;
    }

    public override void Die()
    {
        Died?.Invoke();
        base.Die();
        enabled = false;
    }
}
