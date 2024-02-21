using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _playerPosition;

    private void OnEnable()
    {
        GameController.firstMeetingWithGod += CharacterAppear;
    }

    private void OnDisable()
    {
        GameController.firstMeetingWithGod -= CharacterAppear;
    }


    public void CharacterAppear()
    {
        transform.position = new Vector3( _playerPosition.position.x, transform.position.y, _playerPosition.position.z );
    }
}
