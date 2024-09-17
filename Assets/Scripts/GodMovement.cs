using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GodMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _playerPosition;
    public static Action ForestTalkFinished;
    private float _testTimer;
    private void OnEnable()
    {
        GameController.firstMeetingWithGod += CharacterAppear;
    }

    private void OnDisable()
    {
        GameController.firstMeetingWithGod -= CharacterAppear;
    }

    private void Update()
    {
        _testTimer += Time.deltaTime;
        if (_testTimer >= 17f)
        {
            SceneManager.LoadScene( "PortalScene" );
        }
    }

    public void CharacterAppear()
    {
        transform.position = new Vector3( _playerPosition.position.x, transform.position.y, _playerPosition.position.z );
    }
}
