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
    [SerializeField]
    private float _hitReactTime;
    private bool _isDying;
    private bool _isHit;
    private Enemy Enemy;
    private float originalSpeed;

    private void Start()
    {
        _isDying = false;
        Enemy = GetComponent<Enemy>();
        originalSpeed = Enemy.speed;

    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
            Die();
        else
        {
            if (!_isHit)
            {
                StartCoroutine( HandleHit() );
            }
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

    private IEnumerator HandleHit()
    {
        _isHit = true;
        Enemy.speed = 0f;
        _anim.SetTrigger( "Hit1Trigger" );
        yield return new WaitForSeconds( _hitReactTime );
        Enemy.speed = originalSpeed;
        _isHit = false;
    }
}
