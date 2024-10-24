using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _player;
    public float speed;
    [SerializeField] private Animator _anim;
    private Rigidbody _rb; 

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag( "Player" );
        if (playerObject != null)
        {
            _player = playerObject.transform;
        }
        else
        {
            Debug.LogError( "Player not found!" );
        }

        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        _anim.SetBool( "isRunning", true );
        Vector3 direction = (_player.position - transform.position).normalized;
        _rb.MovePosition( transform.position + direction * speed * Time.deltaTime );

        if (direction != Vector3.zero) 
        {
            Quaternion lookRotation = Quaternion.LookRotation( direction );
            transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, Time.deltaTime * 1000 );
        }
    }
}
