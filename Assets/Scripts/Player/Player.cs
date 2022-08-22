using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    if (_bag.CurrentCones <= 0)
    //    {
    //        _animator.SetBool("Carry", false);
    //    }
    //    else
    //    {
    //        _animator.SetBool("Carry", true);
    //        _animator.SetBool("Carry Idle", false);
    //        _animator.SetBool("Run", false);
    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Cone>(out Cone cone) && _bag.CurrentCones < _bag.MaxCones)
        {
            _bag.AddCone(cone);
        }
    }
}
