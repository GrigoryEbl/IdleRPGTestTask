using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private int _health;
    private int _armor;
    private int _damage;

    private void Awake()
    {
        InitStats();
    }

    private void InitStats()
    {
        _health = _stats.Health;
        _armor = _stats.Armor;
        _damage = _stats.Damage;
    }
}
