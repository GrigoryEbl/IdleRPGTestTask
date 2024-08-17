
using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Stats _stats;
    [SerializeField] private Timer _timer;

    private Rigidbody2D _rigidbody2d;
    private Entity _target;
    
    private int _health;
    private int _armor;
    private int _damage;
    private float _damageReductionPercentage;
    private float _delayAttcak;

    public Action<int> HealthChanged;
    public Action<int> ArmorChanged;
    public Action Died;

    public int MaxHealth => _stats.Health;
    public float DelayAttack => _delayAttcak;
    public int ChanceSpawn => _stats.ChanceSpawn;

    private void Awake()
    {
        InitStats();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _timer.TimeEmpty += Attack;
        HealthChanged?.Invoke(_health);
        ArmorChanged?.Invoke(_armor);
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
        ArmorChanged?.Invoke(_armor);

        float finalDamage = damage - (int)absorbedDamage;

        if (_armor < 0)
        {
            _armor = 0;
            finalDamage += Mathf.Abs(_armor);
        }

        _health -= (int)finalDamage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
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
        if (_target != null)
        {
            _target.ApplyDamage(_damage);
            _timer.StartWork(_delayAttcak);
        }
    }

    private void Die()
    {
        Died?.Invoke();
        _rigidbody2d.isKinematic = false;
        enabled = false;
        Destroy(gameObject,2);
    }
}