using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickForMovement : JoystickHandler
{
    [SerializeField] private CharacterMovement _characterMovement;

    private void Update()
    {
        if (_inputVector.x != 0 || _inputVector.y != 0)
        {
            _characterMovement.Move(new Vector3(_inputVector.x, 0, _inputVector.y));
            _characterMovement.Rotate(new Vector3(_inputVector.x, 0, _inputVector.y));
        }
        else
        {
            _characterMovement.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            _characterMovement.Rotate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }
}
