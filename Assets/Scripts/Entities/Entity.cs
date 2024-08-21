using System;
using UnityEngine;
using SO;
using Weapons;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private Stats _stats;
        [SerializeField] private Timer _timerAttack;
        [SerializeField] private Timer _timerPreparingAttack;
        [SerializeField] private Weapon _weapon;

        private Entity _target;

        public Action<float> AttackPreparing;
        public Action AttackPrepared;
        public Action<float> Attacking;
        public Action AttackFinished;
        public Action Died;

        public bool IsDied { get; private set; }
        public float AttackTimeLeft => _timerAttack.CurrentTime;
        public Weapon CurrentWeapon => _weapon;
        public float DelayAttack => _weapon.DelayAttack;
        public int ChanceSpawn => _stats.ChanceSpawn;
        public float PreparingAttackTime => _stats.PreparingAttackTime;

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

        public virtual void Die()
        {
            IsDied = true;
        }

        public void ChangeWeapon(Weapon weapon)
        {
            _weapon = weapon;
            StopAttack();
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
        }

        private void PreparingAttack()
        {
            _timerAttack.StartWork(DelayAttack);
            Attacking?.Invoke(DelayAttack);
        }

        private void Attack()
        {
            if (_target != null)
            {
                _target.GetComponent<Health>().ApplyDamage(_stats.Damage);

                if (_target.IsDied == false)
                {
                    _timerPreparingAttack.StartWork(PreparingAttackTime);
                    AttackPreparing?.Invoke(PreparingAttackTime);
                }
            }
        }
    }
}