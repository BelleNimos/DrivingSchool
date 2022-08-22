using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private Bag _bag;

    private Animator _animator;
    private CharacterController _characterController;
    private float _currentGravity = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GravityHandling();
    }

    public void Move(Vector3 moveDirection)
    {
        moveDirection = moveDirection * _moveSpeed;

        if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            if (_bag.CurrentCones > 0)
            {
                _animator.SetBool("Carry Idle", true);
                _animator.SetBool("Carry", false);
                _animator.SetBool("Run", false);
                _animator.SetBool("Idle", false);
            }
            else if (_bag.CurrentCones <= 0)
            {
                _animator.SetBool("Idle", true);
                _animator.SetBool("Run", false);
                _animator.SetBool("Carry", false);
                _animator.SetBool("Carry Idle", false);
            }
        }
        else if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            if (_bag.CurrentCones > 0)
            {
                _animator.SetBool("Carry", true);
                _animator.SetBool("Run", false);
                _animator.SetBool("Carry Idle", false);
                _animator.SetBool("Idle", false);
            }
            else if (_bag.CurrentCones <= 0)
            {
                _animator.SetBool("Run", true);
                _animator.SetBool("Carry", false);
                _animator.SetBool("Carry Idle", false);
                _animator.SetBool("Idle", false);
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

    private void GravityHandling()
    {
        if (!_characterController.isGrounded)
            _currentGravity -= _gravityForce * Time.deltaTime;
        else
            _currentGravity = 0;
    }
}