using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : Entity
    {
        private Rigidbody2D _rigidbody2d;

        private void Awake()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public override void Die()
        {
            base.Die();
            float destroyDelay = 2f;
            Died?.Invoke();
            enabled = false;
            _rigidbody2d.isKinematic = false;
            Destroy(gameObject, destroyDelay);
        }
    }
}