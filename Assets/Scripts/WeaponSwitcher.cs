using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Weapon _sword;
    [SerializeField] private Weapon _bow;

    private Player _player;
    private float _delay = 2f;

    public Action<float> WeaponSwitch;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void SwitchWeapon()
    {
        _player.StopAttack();
        
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(_player.AttackTimeLeft);
        WeaponSwitch?.Invoke(_delay);

        yield return new WaitForSeconds(_delay);

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