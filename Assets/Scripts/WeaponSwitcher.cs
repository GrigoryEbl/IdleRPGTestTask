using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Weapon _sword;
    [SerializeField] private Weapon _bow;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _sword.gameObject.SetActive(true);
        _player.ChangeWeapon(_sword);
    }

    public void Switch()
    {
        if (_player.CurrentWeapon == _sword)
        {
            _sword.gameObject.SetActive(false);
            _bow.gameObject.SetActive(true);
            _player.ChangeWeapon(_bow);
        }
        else
        {
            _bow.gameObject.SetActive(false);
            _sword.gameObject.SetActive(true);
            _player.ChangeWeapon(_sword);
        }
    }
}
