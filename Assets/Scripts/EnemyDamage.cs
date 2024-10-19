using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private float _health;
    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
            Die();
    }

    private void Die()
    {
        Destroy( gameObject );
    }
}
