using System;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Health : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private Entity _entity;
    private int _health;
    private int _armor;
    private float _damageReductionPercentage;

    public Action<int> HealthChanged;
    public Action<int> ArmorChanged;

    public int MaxHealth => _stats.Health;

    private void Awake()
    {
        InitStats();
        _entity = GetComponent<Entity>();
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

        float finalDamage = damage - (int)absorbedDamage;
        _armor = Mathf.Clamp(_armor, 0, _stats.Armor);

        //finalDamage += Mathf.Abs(_armor);
        ArmorChanged?.Invoke(_armor);

        _health -= (int)finalDamage;
        _health = Mathf.Clamp(_health, 0, MaxHealth);
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            _entity.Die();
    }

    private void InitStats()
    {
        _health = _stats.Health;
        _armor = _stats.Armor;
        _damageReductionPercentage = _stats.DamageReductionPercentage;
    }
}
