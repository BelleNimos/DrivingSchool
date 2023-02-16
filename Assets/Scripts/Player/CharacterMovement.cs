using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerAnimator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private PlayerAnimator _playerAnimator;
    private CharacterController _characterController;
    private float _moveSpeed;
    private float _rotateSpeed;
    private float _gravityForce;
    private float _currentGravity;

    private const float SurplusFactor = 0.5f;

    public float MoveSpeed => _moveSpeed;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _characterController = GetComponent<CharacterController>();
        _rotateSpeed = 1f;
        _gravityForce = 10;
        _currentGravity = 0;

        if (SceneData.MoveSpeedPlayer > 0)
            _moveSpeed = SceneData.MoveSpeedPlayer;
        else
            _moveSpeed = 8;
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
    }
}