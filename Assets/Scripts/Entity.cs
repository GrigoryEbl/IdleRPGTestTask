using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private int _health;
    private int _armor;
    private int _damage;
    private float _delayAttcak;

    public int ChanceSpawn => _stats.ChanceSpawn;

    private void Awake()
    {
        InitStats();
    }

    private void InitStats()
    {
        _health = _stats.Health;
        _armor = _stats.Armor;
        _damage = _stats.Damage;
        _delayAttcak = _stats.DelayAttack;
    }
}
