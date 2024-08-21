using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _time;
    private bool _isWork;

    public Action TimeEmpty;

    public float CurrentTime => _time;
    public bool IsWork => _isWork;

    private void Update()
    {
        if (_isWork)
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
            {
                _isWork = false;
                TimeEmpty?.Invoke();
            }
        }
    }

    public void StartWork(float startTime)
    {
        if (_isWork)
            return;

        _time = startTime;
        _isWork = true;
    }

    public void StopWork()
    {
        _isWork = false;
    }
}
