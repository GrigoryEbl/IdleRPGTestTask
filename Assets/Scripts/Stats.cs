using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private int _damage;

    public int Health => _health;
    public int Armor => _armor;
    public int Damage => _damage;
}
