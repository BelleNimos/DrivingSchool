using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerAnimator), typeof(BoxCollider))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private PlayerAnimator _playerAnimator;
    private CharacterController _characterController;
    private BoxCollider _boxCollider;
    private float _moveSpeed;
    private float _rotateSpeed;
    private float _gravityForce;
    private float _currentGravity;

    private const float SurplusFactor = 0.2f;
    private const float SurplusFactorRadius = 0.05f;

    public float MoveSpeed => _moveSpeed;
    public Vector3 Radius => _boxCollider.size;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _characterController = GetComponent<CharacterController>();
        _boxCollider = GetComponent<BoxCollider>();
        _rotateSpeed = 1f;
        _gravityForce = 10;
        _currentGravity = 0;

        if (SceneData.MoveSpeedPlayer > 0)
        {
            _moveSpeed = SceneData.MoveSpeedPlayer;
            _boxCollider.size = SceneData.RadiusPlayer;
        }
        else
        {
            _moveSpeed = 8;
            _boxCollider.size = new Vector3(0.5f, 1.8f, 0.5f);
        }
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
        Vector3 newSize = new Vector3(_boxCollider.size.x + SurplusFactorRadius, _boxCollider.size.y, _boxCollider.size.z + SurplusFactorRadius);
        _boxCollider.size = newSize;
    }

    public void Move(Vector3 moveDirection)
    {
        moveDirection = moveDirection * _moveSpeed;

        if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            if (_bag.CurrentConesCount > 0)
            {
                _playerAnimator.StartCarryIdleAnimation();
                _bag.StopAnimationRocking();
            }
            else if (_bag.CurrentConesCount <= 0)
            {
                _playerAnimator.StartIdleAnimation();
                _bag.StopAnimationRocking();
            }
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

    public void IncreaseSpeed()
    {
        _moveSpeed += SurplusFactor;
        IncreaseRadius();
        _playerAnimator.IncreaseSpeedAnimation();
        _bag.IncreaseSpeedAnimation();
    }
}