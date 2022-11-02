 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;
    private float _distance;
    private float _minDistance;
    private bool _isReady;

    private const float MaxDistance = 100;

    private void Start()
    {
        _isReady = false;
        _minDistance = 1;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isReady == true)
            Move();
    }

    private void Move()
    {
        _distance = Vector3.Distance(transform.position, _target.transform.position);

        if (_distance <= _minDistance)
        {
            _agent.enabled = false;
        }
        else if (_distance <= MaxDistance)
        {
            _agent.enabled = true;
            _agent.SetDestination(_target.transform.position);
        }
    }

    public void StartMove()
    {
        _isReady = true;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
