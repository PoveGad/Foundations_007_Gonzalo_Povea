using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController _enemy))
        {
            _enemy.SetStunned();
            Destroy(gameObject);
        }
    }
}
