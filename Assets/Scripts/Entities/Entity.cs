
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
    [SerializeField] private Stats _stats;
    [SerializeField] private Timer _timer;
    [SerializeField] private Weapon _weapon;

    private Rigidbody2D _rigidbody2d;
    private Entity _target;
    
    private int _health;
    private int _armor;
    private int _damage;
    private float _damageReductionPercentage;

    public Action<int> HealthChanged;
    public Action<int> ArmorChanged;
    public Action Died;

    public int MaxHealth => _stats.Health;
    public float DelayAttack => _weapon.DelayAttack;
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
        _timer.StartWork(DelayAttack);
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
        
    }

    private void Attack()
    {
        if (_target != null)
        {
            _timer.StartWork(DelayAttack);
            _target.ApplyDamage(_damage);
        }
    }

    private void Die()
    {
        float destroyTime = 2f;
        Died?.Invoke();
        enabled = false;
        _rigidbody2d.isKinematic = false;
        _rigidbody2d.AddForce(transform.position - _target.transform.position * 10);
        Destroy(gameObject, destroyTime);
    }
}