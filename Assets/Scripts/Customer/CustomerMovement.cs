using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CustomerMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;
    private float _distance;
    private float _minDistance;

    private const float MaxDistance = 100;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _minDistance = 0.1f;
    }

    private void Update()
    {
        if (_target != null)
        {
            _distance = Vector3.Distance(transform.position, _target.position);

            if (_distance <= _minDistance)
            {
                _agent.enabled = false;
            }
            else if (_distance <= MaxDistance)
            {
                _agent.enabled = true;
                _agent.SetDestination(_target.position);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetMinDistance(float minDistance)
    {
        _minDistance = minDistance;
    }

    public void DisableAgent()
    {
        _agent.enabled = false;
    }

    public void RemoveTarget()
    {
        _target = null;
    }
}
