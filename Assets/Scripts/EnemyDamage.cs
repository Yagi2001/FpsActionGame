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
    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
            Die();
    }

    private void Die()
    {
        _rb.isKinematic = true;   //This is a solution for weird flying dead bug. Later can be changed to an better alternative
        if(_anim != null)
        {
            _anim.SetTrigger( "DeathTrigger" );
        }
        Destroy( gameObject,5f );
    }
}
