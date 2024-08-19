using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefendView : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _armorText;

    private Health _health;

    private void Awake()
    {
        _health = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _health.ArmorChanged += OnArmorChanged;
        _healthText.text = $"{_health.MaxHealth}";
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        _health.ArmorChanged -= OnArmorChanged;
    }

    private void OnHealthChanged(int value)
    {
        _healthText.text = $"Health: {value} / {_health.MaxHealth}";
    }

    private void OnArmorChanged(int value)
    {
        _armorText.text = $"Armor: {value}";
    }
}
