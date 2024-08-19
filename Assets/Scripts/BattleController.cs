using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [SerializeField] private Button _ButtonStartBattle;
    [SerializeField] private Button _ButtonLeaveBattle;
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _ButtonStartBattle.onClick.AddListener(StartBattle);
        _ButtonLeaveBattle.onClick.AddListener(LeaveBattle);
    }

    private void OnDisable()
    {
        _ButtonStartBattle.onClick.RemoveListener(StartBattle);
        _ButtonLeaveBattle.onClick.RemoveListener(LeaveBattle);
    }

    private void StartBattle()
    {
        _player.enabled = true;
        _spawner.enabled = true;
    }

    private void LeaveBattle()
    {
        _player.enabled = false;
        _spawner.enabled = false;
        Destroy(_spawner.GetComponentInChildren<Entity>().gameObject);
    }
}
