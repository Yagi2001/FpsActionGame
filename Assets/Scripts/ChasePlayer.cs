using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _enemy;
    [SerializeField]
    private Animator _anim;
    private Transform _player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag( "Player" );
        _player = playerObject.transform;
    }

    private void Update()
    {
        if(_enemy != null)
        {
            _anim.SetBool( "isRunning", true );
            _enemy.SetDestination( _player.position );
        }
    }
}
