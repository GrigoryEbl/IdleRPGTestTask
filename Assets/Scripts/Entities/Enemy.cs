using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Rigidbody2D _rigidbody2d;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public override void Die()
    {
        float destroyDelay = 2f;
        Died?.Invoke();
        enabled = false;
        _rigidbody2d.isKinematic = false;
        Destroy(gameObject, destroyDelay);
    }
}
