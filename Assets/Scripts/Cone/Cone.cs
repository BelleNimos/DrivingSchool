using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]
public class Cone : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private BoxCollider _collider;

    private const string Fall = "Fall";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }

    public void StartFallAnimation()
    {
        _animator.Play(Fall);
    }

    public void OffKinematic()
    {
        _rigidbody.isKinematic = false;
    }

    public void OffTrigger()
    {
        _collider.isTrigger = false;
    }
}