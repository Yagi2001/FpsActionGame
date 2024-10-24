using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Rigidbody _rb;
    private bool _isDying;

    private void Start()
    {
        _isDying = false;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
            Die();
        else
        {
            _anim.SetTrigger( "Hit1Trigger" );
        }
    }

    private void Die()
    {
        if(_anim != null && !_isDying)
        {
            _isDying = true;
            _rb.isKinematic = true;   //This is a solution for weird flying dead bug. Later can be changed to an better alternative
            _anim.SetTrigger( "DeathTrigger" );
            Destroy( gameObject, 5f );
        }
    }
}
