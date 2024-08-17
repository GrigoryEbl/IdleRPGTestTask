using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Stats _stats;
    [SerializeField] private Entity _target;
    [SerializeField] private Spawner _spawner;

    private Timer _timer;

    [Header("Stats")]
    private int _health;
    private int _armor;
    private int _damage;
    private float _damageReductionPercentage;
    private float _delayAttcak;

    public int ChanceSpawn => _stats.ChanceSpawn;

    private void Awake()
    {
        InitStats();
        _timer = GetComponent<Timer>();
        _timer.StartWork(_delayAttcak);
    }

    private void OnEnable() => _timer.TimeEmpty += Attack;

    private void OnDisable() => _timer.TimeEmpty -= Attack;

    public void ApplyDamage(int damage)
    {
        float absorbedDamage = damage * _damageReductionPercentage;

        _armor -= (int)absorbedDamage;

        float finalDamage = damage - (int)absorbedDamage;

        if (_armor < 0)
        {
            _armor = 0;
            finalDamage += Mathf.Abs(_armor);
        }

        _health -= (int)finalDamage;

        if (_health < 0)
        {
            _health = 0;
            Die();
        }
    }

    private void InitStats()
    {
        _health = _stats.Health;
        _armor = _stats.Armor;
        _damage = _stats.Damage;
        _damageReductionPercentage = _stats.DamageReductionPercentage;
        _delayAttcak = _stats.DelayAttack;
    }

    private void Attack()
    {
        _target.ApplyDamage(_damage);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
