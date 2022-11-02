using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class CustomerMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _target;
    private float _distance;
    private float _minDistance;
    private bool _isSit;

    private const string Idle = "Idle";
    private const string Run = "Run";
    private const string Sit = "Sit";
    private const float MaxDistance = 100;

    public Transform Target => _target;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _minDistance = 0.1f;
    }

    private void Update()
    {
        if (_target != null)
        {
            _isSit = false;
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

        if (_agent.enabled == false)
        {
            if (_isSit == true)
            {
                _animator.SetBool(Sit, true);
                _animator.SetBool(Run, false);
                _animator.SetBool(Idle, false);
            }
            else
            {
                _animator.SetBool(Idle, true);
                _animator.SetBool(Run, false);
                _animator.SetBool(Sit, false);
            }
        }
        else if (_agent.enabled == true)
        {
            _animator.SetBool(Run, true);
            _animator.SetBool(Idle, false);
            _animator.SetBool(Sit, false);
        }
    }

    public void SitDown()
    {
        _isSit = true;
    }

    public void DisableAgent()
    {
        _agent.enabled = false;
    }

    public void RemoveTarget()
    {
        _target = null;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetMinDistance(float minDistance)
    {
        _minDistance = minDistance;
    }
}
