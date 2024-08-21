using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float _delayAttack;

        public float DelayAttack => _delayAttack;
    }
}