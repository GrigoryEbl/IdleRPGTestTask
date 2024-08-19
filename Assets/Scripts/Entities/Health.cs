using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private int _health;
    private int _armor;
    private float _damageReductionPercentage;
    private Rigidbody2D _rigidbody2d;

    public Action<int> HealthChanged;
    public Action<int> ArmorChanged;
    public Action Died;

    public int MaxHealth => _stats.Health;

    private void Awake()
    {
        InitStats();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        HealthChanged?.Invoke(_health);
        ArmorChanged?.Invoke(_armor);
    }

    public void RestoreHealth()
    {
        _health = MaxHealth;
        HealthChanged?.Invoke(_health);
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
        _damageReductionPercentage = _stats.DamageReductionPercentage;
    }

    private void Die()
    {
        float destroyTime = 2f;
        Died?.Invoke();
        enabled = false;
        _rigidbody2d.isKinematic = false;
        _rigidbody2d.AddForce(transform.right * 10);
        Destroy(gameObject, destroyTime);
    }
}
