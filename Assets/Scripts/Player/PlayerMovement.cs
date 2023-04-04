using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerAnimator), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private PlayerAnimator _playerAnimator;
    private CharacterController _characterController;
    private BoxCollider _boxCollider;
    private float _moveSpeed;
    private float _slowDownMoveSpeed = 6f;
    private float _rotateSpeed = 1f;
    private float _gravityForce = 10f;
    private float _currentGravity = 0f;
    private bool _isSlowDown = false;

    private const float SurplusFactorSpeed = 0.2f;
    private const float SurplusFactorRadius = 0.1f;

    public float MoveSpeed => _moveSpeed;
    public Vector3 Radius => _boxCollider.size;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _characterController = GetComponent<CharacterController>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        GravityHandling();
    }

    private void GravityHandling()
    {
        if (!_characterController.isGrounded)
            _currentGravity -= _gravityForce * Time.deltaTime;
        else
            _currentGravity = 0;
    }

    private void IncreaseRadius()
    {
        Vector3 newSize = new(_boxCollider.size.x + SurplusFactorRadius, _boxCollider.size.y, _boxCollider.size.z + SurplusFactorRadius);
        _boxCollider.size = newSize;
    }

    public void Move(Vector3 moveDirection)
    {
        if (_isSlowDown == true)
            moveDirection = moveDirection.normalized * _slowDownMoveSpeed;
        else
            moveDirection = moveDirection.normalized * _moveSpeed;

        if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            if (_bag.CurrentConesCount > 0)
                _playerAnimator.StartCarryIdleAnimation();
            else if (_bag.CurrentConesCount <= 0)
                _playerAnimator.StartIdleAnimation();

            _bag.StopAnimationRocking();
        }
        else if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            if (_bag.CurrentConesCount > 0)
            {
                _playerAnimator.StartCarryAnimation();
                _bag.StartAnimationRocking();
            }
            else if (_bag.CurrentConesCount <= 0)
            {
                _playerAnimator.StartRunAnimation();
                _bag.StopAnimationRocking();
            }
        }

        moveDirection.y = _currentGravity;
        _characterController.Move(moveDirection * Time.deltaTime);
    }

    public void Rotate(Vector3 moveDirection)
    {
        if (_characterController.isGrounded)
        {
            if (Vector3.Angle(transform.forward, moveDirection) > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }

    public void SetDefaultValues()
    {
        _moveSpeed = 8f;

        float sizeX = 0.5f;
        float sizeY = 1.5f;
        float sizeZ = 0.5f;

        Vector3 radius = new Vector3(sizeX, sizeY, sizeZ);

        _boxCollider.size = radius;
    }

    public void SetStartValues(float value, Vector3 radius)
    {
        _moveSpeed = value;
        _boxCollider.size = radius;
    }

    public void IncreaseSpeed()
    {
        _moveSpeed += SurplusFactorSpeed;
        IncreaseRadius();
        _playerAnimator.IncreaseSpeedAnimator();
        _bag.IncreaseSpeedAnimation();
    }

    public void SlowDownSpeed()
    {
        _isSlowDown = true;
    }

    public void ResetSpeed()
    {
        _isSlowDown = false;
    }
}