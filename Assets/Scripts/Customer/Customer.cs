using UnityEngine;

[RequireComponent(typeof(CustomerMovement))]
public class Customer : MonoBehaviour
{
    [SerializeField] private Transform _firstTarget;
    [SerializeField] private Transform _lastTarget;
    [SerializeField] private StackDollars _stackDollarsPrefab;

    private CustomerMovement _movement;
    private Transform _target;
    private TargetWZ _targetWZ;
    private bool _isFinish;
    private bool _isPaid;
    private bool _isFree;

    public bool IsReady { get; private set; }

    private void Start()
    {
        _movement = GetComponent<CustomerMovement>();
        
        _target = _firstTarget;
        _movement.SetTarget(_target);

        _targetWZ = null;
        _isFinish = false;
        _isPaid = false;
        _isFree = true;
        IsReady = false;

    }

    private void Update()
    {
        if (_targetWZ == null)
            IsReady = false;
        else
            transform.position = _targetWZ.transform.position;

        if (_isFinish == true)
        {
            _target = _lastTarget;
            _movement.SetTarget(_lastTarget);
        }

        if (_isPaid == true)
        {
            _target = _firstTarget;
            _movement.SetTarget(_target);
            _isFinish = false;
        }

        if (_isFree == false)
        {
            transform.position = _firstTarget.transform.position;
            _target = null;
            _movement.RemoveTarget();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<CustomerFirstTarget>(out CustomerFirstTarget firstTarget))
        {
            if (firstTarget.transform == _target.transform)
            {
                StopMove();
                _isFree = false;
            }
        }

        if (collision.TryGetComponent<TargetWZ>(out TargetWZ targetWZ))
        {
            if (targetWZ.transform == _target.transform)
            {
                StopMove();

                _targetWZ = targetWZ;
                IsReady = true;
            }
        }

        if (collision.TryGetComponent<CustomerLastTarget>(out CustomerLastTarget lastTarget))
        {
            if (lastTarget.transform == _target.transform)
            {
                if (_isFinish == true)
                {
                    InstantiateStackDollars();

                    _isPaid = true;
                    _isFinish = false;
                }
            }
        }
    }

    private void InstantiateStackDollars()
    {
        Instantiate(_stackDollarsPrefab, transform.position, Quaternion.identity);
    }

    private void StopMove()
    {
        _movement.RemoveTarget();
        _movement.DisableAgent();
    }

    public void SetTarget(Transform target)
    {
        _target = target;

        if (_isPaid == false)
            _movement.SetTarget(_target);

        _isFree = true;
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
        _isFinish = true;
    }
}
