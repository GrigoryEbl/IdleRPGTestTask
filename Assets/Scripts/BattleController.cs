using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [SerializeField] private Button _ButtonStartBattle;
    [SerializeField] private Button _ButtonLeaveBattle;
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _panelBattle;
    [SerializeField] private GameObject _panelMenu;


    private void OnEnable()
    {
        _ButtonStartBattle.onClick.AddListener(StartBattle);
        _ButtonLeaveBattle.onClick.AddListener(LeaveBattle);
        _player.Died += LeaveBattle;
    }

    private void OnDisable()
    {
        _ButtonStartBattle.onClick.RemoveListener(StartBattle);
        _ButtonLeaveBattle.onClick.RemoveListener(LeaveBattle);
        _player.Died -= LeaveBattle;
    }

    private void StartBattle()
    {
        _player.enabled = true;
        _spawner.enabled = true;
        _panelBattle.SetActive(true);
        _panelMenu.SetActive(false);
    }

    private void LeaveBattle()
    {
        _panelBattle.SetActive(false);
        _panelMenu.SetActive(true);
        _player.StopAttack();
        _player.enabled = false;
        _spawner.enabled = false;
        Destroy(_spawner.GetComponentInChildren<Entity>().gameObject);
    }
}
