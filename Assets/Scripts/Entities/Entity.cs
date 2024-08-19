
using System;
using UnityEngine;


public class Entity : MonoBehaviour
{
    [SerializeField] private Stats _stats;
    [SerializeField] private Timer _timer;
    [SerializeField] private Weapon _weapon;

    private Entity _target;    
    private int _damage;
   
    public Weapon CurrentWeapon => _weapon;
    public float DelayAttack => _weapon.DelayAttack;
    public int ChanceSpawn => _stats.ChanceSpawn;

    private void Awake()
    {
        InitStats();
    }

    private void Start()
    {
        _timer.TimeEmpty += Attack;
    }

    private void OnDisable()
    {
        _timer.TimeEmpty -= Attack;
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void SetTarget(Transform target)
    {
        _target = target.GetComponent<Entity>();
        _timer.StartWork(DelayAttack);
    }

    private void InitStats()
    {
        _damage = _stats.Damage;
    }

    private void Attack()
    {
        if (_target != null)
        {
            _timer.StartWork(DelayAttack);
            _target.GetComponent<Health>().ApplyDamage(_damage);
        }
    }
}