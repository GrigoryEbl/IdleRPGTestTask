using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private int _health;
    private int _armor;
    private int _damage;

    private void Awake()
    {
        InitStats();
    }

    private void InitStats()
    {
        _health = _stats.Health;
        _armor = _stats.Armor;
        _damage = _stats.Damage;
    }
}
