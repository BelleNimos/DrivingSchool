using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class CustomerAnimator : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isSit;

    private const string Idle = "Idle";
    private const string Run = "Run";
    private const string Sit = "Sit";

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
        else
        {
            _animator.SetBool(Run, true);
            _animator.SetBool(Idle, false);
            _animator.SetBool(Sit, false);
            _isSit = false;
        }
    }

    public void SitDown()
    {
        _isSit = true;
    }
}
