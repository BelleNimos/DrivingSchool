using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Bag))]
public class PhysicCharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Bag _bag;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _bag = GetComponent<Bag>();
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        Vector3 offset = moveDirection * _moveSpeed * Time.deltaTime;

        if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            if (_bag.CurrentConesCount > 0)
            {
                _animator.SetBool("Carry Idle", true);
                _animator.SetBool("Carry", false);
                _animator.SetBool("Run", false);
                _animator.SetBool("Idle", false);
            }
            else if (_bag.CurrentConesCount <= 0)
            {
                _animator.SetBool("Idle", true);
                _animator.SetBool("Run", false);
                _animator.SetBool("Carry", false);
                _animator.SetBool("Carry Idle", false);
            }
        }
        else if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            if (_bag.CurrentConesCount > 0)
            {
                _animator.SetBool("Carry", true);
                _animator.SetBool("Run", false);
                _animator.SetBool("Carry Idle", false);
                _animator.SetBool("Idle", false);
            }
            else if (_bag.CurrentConesCount <= 0)
            {
                _animator.SetBool("Run", true);
                _animator.SetBool("Carry", false);
                _animator.SetBool("Carry Idle", false);
                _animator.SetBool("Idle", false);
            }
        }

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    public void RotateCharacter(Vector3 moveDirection)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
