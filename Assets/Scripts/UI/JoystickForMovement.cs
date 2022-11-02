using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickForMovement : JoystickHandler
{
    [SerializeField] private CharacterMovement _characterMovement;

    private void Update()
    {
        if (InputVector.x != 0 || InputVector.y != 0)
        {
            _characterMovement.Move(new Vector3(InputVector.x, 0, InputVector.y));
            _characterMovement.Rotate(new Vector3(InputVector.x, 0, InputVector.y));
        }
        else
        {
            _characterMovement.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            _characterMovement.Rotate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }
}
