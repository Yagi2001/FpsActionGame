using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( CharacterController ) )]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _playerCamera;
    [SerializeField]
    private float _walkSpeed = 6f;
    [SerializeField]
    private float _runSpeed = 12f;
    [SerializeField]
    private float _jumpPower = 7f;
    [SerializeField]
    private float _gravity = 10f;
    [SerializeField]
    private float _lookSpeed = 2f;
    [SerializeField]
    private float _lookXLimit = 45f;
    [SerializeField]
    private float _defaultHeight = 2f;
    [SerializeField]
    private float _crouchHeight = 1f;
    [SerializeField]
    private float _crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private void OnEnable()
    {
        GameController.firstMeetingWithGod += FreezePlayer;
    }

    private void OnDisable()
    {
        GameController.firstMeetingWithGod -= FreezePlayer;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection( Vector3.forward );
        Vector3 right = transform.TransformDirection( Vector3.right );

        bool isRunning = Input.GetKey( KeyCode.LeftShift );
        float curSpeedX = canMove ? (isRunning ? _runSpeed : _walkSpeed) * Input.GetAxis( "Vertical" ) : 0;
        float curSpeedY = canMove ? (isRunning ? _runSpeed : _walkSpeed) * Input.GetAxis( "Horizontal" ) : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton( "Jump" ) && canMove && characterController.isGrounded)
        {
            moveDirection.y = _jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= _gravity * Time.deltaTime;
        }

        if (Input.GetKey( KeyCode.C ) && canMove)
        {
            characterController.height = _crouchHeight;
            _walkSpeed = _crouchSpeed;
            _runSpeed = _crouchSpeed;

        }
        else
        {
            characterController.height = _defaultHeight;
            _walkSpeed = 6f;
            _runSpeed = 12f;
        }

        characterController.Move( moveDirection * Time.deltaTime );

        if (canMove)
        {
            rotationX += -Input.GetAxis( "Mouse Y" ) * _lookSpeed;
            rotationX = Mathf.Clamp( rotationX, -_lookXLimit, _lookXLimit );
            _playerCamera.transform.localRotation = Quaternion.Euler( rotationX, 0, 0 );
            transform.rotation *= Quaternion.Euler( 0, Input.GetAxis( "Mouse X" ) * _lookSpeed, 0 );
        }
    }

    private void FreezePlayer()
    {
        canMove = false;
    }
}