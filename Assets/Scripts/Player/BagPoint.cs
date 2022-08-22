using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPoint : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _deltaPosition;

    private void Start()
    {
        _deltaPosition = transform.position - _target.position;
    }

    private void Update()
    {
        transform.position = _target.position + _deltaPosition;
    }
}
