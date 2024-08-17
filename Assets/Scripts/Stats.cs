using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private float _damageReductionPercentage;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayAttack;
    [SerializeField][Range(0f, 100f)] private int _chanceSpawn;

    public int Health => _health;
    public int Armor => _armor;
    public int Damage => _damage;
    public float DamageReductionPercentage => _damageReductionPercentage;
    public float DelayAttack => _delayAttack;
    public int ChanceSpawn => _chanceSpawn;
}
