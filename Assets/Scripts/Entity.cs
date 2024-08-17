
using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Stats _stats;
    [SerializeField] private Timer _timer;

    private Entity _target;


    private int _health;
    private int _armor;
    private int _damage;
    private float _damageReductionPercentage;
    private float _delayAttcak;

    public Action Died;

    public int ChanceSpawn => _stats.ChanceSpawn;

    private void Awake()
    {
        InitStats();
    }

    private void Start()
    {
        _timer.TimeEmpty += Attack;
    }

    private void OnDisable()
    {
        _timer.TimeEmpty -= Attack;
    }

    public void SetTarget(Transform target)
    {
        _target = target.GetComponent<Entity>();
        _timer.StartWork(_delayAttcak);
    }

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

        print(name + " apply damage: " + finalDamage);
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
        if (_target != null)
        {
            _target.ApplyDamage(_damage);
            _timer.StartWork(_delayAttcak);
            print(name + " Attacked: " + _target.name);
        }
    }

    private void Die()
    {
        Died?.Invoke();
        print(name + " is died");
        Destroy(gameObject);
    }
}