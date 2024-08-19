using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private float _damageReductionPercentage;
    [SerializeField] private int _damage;
    [SerializeField][Range(0f, 100f)] private int _chanceSpawn;
    [SerializeField] private float _preparingAttackTime;

    public int Health => _health;
    public int Armor => _armor;
    public int Damage => _damage;
    public float DamageReductionPercentage => _damageReductionPercentage;
    public int ChanceSpawn => _chanceSpawn;
    public float PreparingAttackTime => _preparingAttackTime;
}
