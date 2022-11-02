using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomerMovement))]
public class Customer : MonoBehaviour
{
    [SerializeField] private Transform _firstTarget;
    [SerializeField] private Transform _lastTarget;
    [SerializeField] private StackDollars _stackDollarsPrefab;

    private CustomerMovement _movement;
    private StackDollars _stackDollars;
    private bool _isFinish;
    private bool _isPaid;
    private bool _isFree;

    public Transform Target => _movement.Target;

    private void Start()
    {
        _movement = GetComponent<CustomerMovement>();
        _movement.SetTarget(_firstTarget);
        _isFinish = false;
        _isPaid = false;
        _isFree = true;
    }

    private void Update()
    {
        if (_isPaid == true)
        {
            _isFinish = false;
            _movement.SetTarget(_firstTarget);
        }

        if (_isFinish == true)
            _movement.SetTarget(_lastTarget);

        if (_isFree == false)
        {
            transform.position = _firstTarget.transform.position;
            StopMove();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<TargetWZ>(out TargetWZ targetWZ))
            if (targetWZ.transform == _movement.Target)
                StopMove();

        if (collision.TryGetComponent<FirstTarget>(out FirstTarget firstTarget))
        {
            if (firstTarget.transform == _movement.Target)
                _isFree = false;
        }

        if (collision.TryGetComponent<LastTarget>(out LastTarget lastTarget))
        {
            if (_isFinish == true)
            {
                InstantiateStackDollars();

                _isPaid = true;
                _isFinish = false;
            }
        }
    }

    private void InstantiateStackDollars()
    {
        _stackDollars = Instantiate(_stackDollarsPrefab, transform.position, Quaternion.identity);
    }

    private void StopMove()
    {
        _movement.RemoveTarget();
        _movement.DisableAgent();
    }

    public void Finished()
    {
        _isFinish = true;
    }

    public void SitDown()
    {
        _movement.SitDown();
    }

    public void SetTarget(Transform target)
    {
        if (_isPaid == false)
            _movement.SetTarget(target);

        _isFree = true;
    }

    public void SetMinDistance(float minDistance)
    {
        _movement.SetMinDistance(minDistance);
    }
}
