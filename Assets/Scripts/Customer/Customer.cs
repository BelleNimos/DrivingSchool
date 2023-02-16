using UnityEngine;

[RequireComponent(typeof(CustomerMovement))]
public class Customer : MonoBehaviour
{
    private CustomerMovement _movement;
    private CustomerAnimator _animator;
    private StackDollars _stackDollars;
    private MoneyPoint _moneyPoint;
    private Transform _lastTarget;
    private Transform _firstTarget;
    private Transform _target;
    private Transform _exitTarget;
    private TargetWZ _targetWZ;
    private bool _isRunningTime;
    private int _moneyToWithdraw;
    private float _maxWaitingSeconds;
    private float _waitingSeconds;

    public bool IsExitReady { get; private set; }
    public bool IsFinish { get; private set; }
    public bool IsReady { get; private set; }

    private void Start()
    {
        _movement = GetComponent<CustomerMovement>();
        _animator = GetComponent<CustomerAnimator>();
        _moneyPoint = FindObjectOfType<MoneyPoint>();
        _lastTarget = FindObjectOfType<CustomerLastTarget>().transform;
        _exitTarget = FindObjectOfType<Exit>().transform;
        _target = _firstTarget;
        _movement.SetTarget(_target);
        _targetWZ = null;
        _isRunningTime = true;
        IsExitReady = false;
        IsFinish = false;
        IsReady = false;
        _waitingSeconds = 0f;
    }

    private void Update()
    {
        _waitingSeconds += Time.deltaTime;

        if (_waitingSeconds >= _maxWaitingSeconds)
            if (_isRunningTime == true)
                TakeMoney();

        if (_targetWZ == null)
            IsReady = false;

        if (IsFinish == true)
        {
            _target = _lastTarget;
            _movement.SetTarget(_lastTarget);
        }
    }

    private void TakeMoney()
    {
        GoToExit();
        _moneyPoint.SpendMoney(_moneyToWithdraw);
        _waitingSeconds = 0f;
    }

    public void InstantiateStackDollars()
    {
        Instantiate(_stackDollars, transform.position, Quaternion.identity);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _movement.SetTarget(_target);
    }

    public void SetFirstTarget(Transform transform)
    {
        _firstTarget = transform;
    }

    public void SetTargetWZ(TargetWZ targetWZ)
    {
        _targetWZ = targetWZ;
    }

    public void SetMoneyToWithdraw(int value)
    {
        _moneyToWithdraw = value;
    }

    public void SetMaxWaitingSeconds(float value)
    {
        _maxWaitingSeconds = value;
    }

    public void SetStackDollars(StackDollars stackDollars)
    {
        _stackDollars = stackDollars;
    }

    public void SetMinDistance(float minDistance)
    {
        _movement.SetMinDistance(minDistance);
    }

    public void RemoveTargetWZ()
    {
        _targetWZ = null;
    }

    public void StopTimer()
    {
        _isRunningTime = false;
    }

    public void GoToExit()
    {
        SetTarget(_exitTarget);
        IsExitReady = true;
    }

    public void SitDown()
    {
        _animator.SitDown();
    }

    public void StopMove()
    {
        _movement.RemoveTarget();
        _movement.DisableAgent();

        if (_target != null)
            transform.position = _target.position;
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

    public void IsFinishTrue()
    {
        IsFinish = true;
    }

    public void IsFinishFalse()
    {
        IsFinish = false;
    }

    public void IsReadyTrue()
    {
        IsReady = true;
    }
}
