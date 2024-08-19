using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownAttackView : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private Slider _slider;

    private Timer _timer;

    private void Start()
    {
        _timer = _entity.GetComponent<Timer>();
        _slider.maxValue = _entity.DelayAttack;
    }

    private void Update()
    {
        _slider.value = _timer.CurrentTime;
    }
}
