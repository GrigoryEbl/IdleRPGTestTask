using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefendView : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _armorText;

    private void OnEnable()
    {
        _entity.HealthChanged += OnHealthChanged;
        _entity.ArmorChanged += OnArmorChanged;
        _healthText.text = $"{_entity.MaxHealth}";
    }

    private void OnDisable()
    {
        _entity.HealthChanged -= OnHealthChanged;
        _entity.ArmorChanged -= OnArmorChanged;
    }

    private void OnHealthChanged(int value)
    {
        _healthText.text = $"Health: {value} / {_entity.MaxHealth}";
    }

    private void OnArmorChanged(int value)
    {
        _armorText.text =$"Armor: {value}";
    }
}
