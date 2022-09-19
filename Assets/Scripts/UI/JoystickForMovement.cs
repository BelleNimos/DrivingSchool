using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickForMovement : JoystickHandler
{
    [SerializeField] private CharacterMovement _CharacterMovement;

    private void Update()
    {
        if (_inputVector.x != 0 || _inputVector.y != 0)
        {
            _CharacterMovement.Move(new Vector3(_inputVector.x, 0, _inputVector.y));
            _CharacterMovement.Rotate(new Vector3(_inputVector.x, 0, _inputVector.y));
        }
        else
        {
            _CharacterMovement.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            _CharacterMovement.Rotate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }
}
