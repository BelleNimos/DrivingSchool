using UnityEngine;

public class JoystickMovement : JoystickHandler
{
    [SerializeField] private PlayerMovement _playerMovement;

    private float _horizontal;
    private float _vertical;

    private void Update()
    {
        

        if (InputVector.x != 0 || InputVector.y != 0)
        {
            _playerMovement.Move(new Vector3(InputVector.x, 0, InputVector.y));
            _playerMovement.Rotate(new Vector3(InputVector.x, 0, InputVector.y));
        }
        else
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            _playerMovement.Move(new Vector3(_horizontal, 0, _vertical));
            _playerMovement.Rotate(new Vector3(_horizontal, 0, _vertical));
        }
    }
}
