using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField]
    private AudioSource _hitSound1;
    [SerializeField]
    private AudioSource _hitSound2;
    [SerializeField]
    private AudioSource _deadSound; // Later on it can be better to put them those audios together instead of adding them one by one. Currently fine for test purposes
    [SerializeField]
    private NavMeshAgent _enemy;
    private bool _isDying;
    private float originalSpeed;
    private Coroutine hitCoroutine;  // Store the currently running hit coroutine

    private void Start()
    {
        _isDying = false;
        originalSpeed = _enemy.speed;
    }

    public void TakeDamage( float damage )
    {
        _health -= damage;
        if (_health <= 0f)
        {
            Die();
        }
        else
        {
            if (hitCoroutine == null) 
            {
                hitCoroutine = StartCoroutine( HandleHit() );
            }
            else
            {
                StopCoroutine( hitCoroutine ); 
                hitCoroutine = StartCoroutine( HandleHit() );
            }
        }
    }

    private void Die()
    {
        if (_anim != null && !_isDying)
        {
            _enemy.enabled = false;
            _isDying = true;
            _rb.isKinematic = true;  // Solution for weird flying dead bug
            _anim.SetTrigger( "DeathTrigger" );
            _deadSound.Play();
            Destroy( gameObject, 5f );
        }
    }

    private IEnumerator HandleHit()
    {
        int randomHitSound = Random.Range( 0, 2 );
        if (randomHitSound == 0)
            _hitSound1.Play();
        else
            _hitSound2.Play();
        _enemy.speed = 0f;
        _anim.SetTrigger( "Hit1Trigger" );
        yield return new WaitForSeconds( _hitReactTime );
        _enemy.speed = originalSpeed;
        hitCoroutine = null;
    }
}
