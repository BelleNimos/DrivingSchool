using UnityEngine;

[RequireComponent(typeof(CustomerMovement))]
public class Customer : MonoBehaviour
{
    [SerializeField] private Transform _lastTarget;
    [SerializeField] private StackDollars _stackDollarsPrefab;

    private CustomerMovement _movement;
    private Transform _firstTarget;
    private Transform _target;
    private TargetWZ _targetWZ;
    private bool _isPaid;
    private bool _isFree;

    public bool IsFinish { get; private set; }
    public bool IsReady { get; private set; }

    private void Start()
    {
        _movement = GetComponent<CustomerMovement>();

        _target = _firstTarget;
        _movement.SetTarget(_target);

        _targetWZ = null;
        _isPaid = false;
        _isFree = true;
        IsFinish = false;
        IsReady = false;
    }

    private void Update()
    {
        if (_targetWZ == null)
            IsReady = false;
        else
            transform.position = _targetWZ.transform.position;

        if (IsFinish == true)
        {
            _target = _lastTarget;
            _movement.SetTarget(_lastTarget);
        }

        if (_isPaid == true)
        {
            _target = _firstTarget;
            _movement.SetTarget(_target);
            IsFinish = false;
        }

        if (_isFree == false)
        {
            transform.position = _firstTarget.transform.position;
            _target = null;
            _movement.RemoveTarget();
        }
    }

    public void InstantiateStackDollars()
    {
        Instantiate(_stackDollarsPrefab, transform.position, Quaternion.identity);
    }

    public void StopMove()
    {
        _movement.RemoveTarget();
        _movement.DisableAgent();
    }

    public void SetFirstTarget(Transform transform)
    {
        _firstTarget = transform;
    }

    public void SetTarget(Transform target)
    {
        _target = target;

        if (_isPaid == false)
            _movement.SetTarget(_target);

        _isFree = true;
    }

    public void SetTargetWZ(TargetWZ targetWZ)
    {
        _targetWZ = targetWZ;
    }

    public bool CheckPosition(Transform transform)
    {
        bool value;

        if (_target == transform)
            value = true;
        else
            value = false;

        return value;
    }

    public void RemoveTargetWZ()
    {
        _targetWZ = null;
    }

    public void SetMinDistance(float minDistance)
    {
        _movement.SetMinDistance(minDistance);
    }

    public void SitDown()
    {
        _movement.SitDown();
    }

    public void Finish()
    {
        IsFinish = true;
    }

    public void IsFinishFalse()
    {
        IsFinish = false;
    }

    public void IsFreeFalse()
    {
        _isFree = false;
    }

    public void IsPaidTrue()
    {
        _isPaid = true;
    }

    public void IsReadyTrue()
    {
        IsReady = true;
    }
}
