
using System;
using UnityEngine;


public class Entity : MonoBehaviour
{
    [SerializeField] private Stats _stats;
    [SerializeField] private Timer _timerAttack;
    [SerializeField] private Timer _timerPreparingAttack;
    [SerializeField] private Weapon _weapon;

    private Entity _target;    
    private int _damage;

    public Action<float> AttackPreparing;
    public Action<float> Attacked;
   
    public Weapon CurrentWeapon => _weapon;
    public float DelayAttack => _weapon.DelayAttack;
    public int ChanceSpawn => _stats.ChanceSpawn;
    public float PreparingAttackTime => _stats.PreparingAttackTime;

    private void Awake()
    {
        InitStats();
    }

    private void Start()
    {
        _timerPreparingAttack.TimeEmpty += PreparingAttack;
        _timerAttack.TimeEmpty += Attack;
    }

    private void OnDisable()
    {
        _timerPreparingAttack.TimeEmpty -= PreparingAttack;
        _timerAttack.TimeEmpty -= Attack;
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _timerPreparingAttack.StartWork(PreparingAttackTime);
    }

    public void SetTarget(Transform target)
    {
        _target = target.GetComponent<Entity>();
        _timerPreparingAttack.StartWork(PreparingAttackTime);
        AttackPreparing?.Invoke(PreparingAttackTime);
    }

    public void StopAttack()
    {
        _timerPreparingAttack.StopWork();
        _timerAttack.StopWork();
    }

    private void InitStats()
    {
        _damage = _stats.Damage;
    }

    private void PreparingAttack()
    {
        _timerAttack.StartWork(DelayAttack);
        Attacked?.Invoke(DelayAttack);
    }

    private void Attack()
    {
        if (_target != null)
        {
            _target.GetComponent<Health>().ApplyDamage(_damage);
            _timerPreparingAttack.StartWork(PreparingAttackTime);
            AttackPreparing?.Invoke(PreparingAttackTime);
        }
    }
}