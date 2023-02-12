using UnityEngine;

[RequireComponent(typeof(CustomerMovement))]
public class Customer : MonoBehaviour
{
    [SerializeField] private StackDollars _stackDollarsPrefab;

    private CustomerMovement _movement;
    private CustomerAnimator _animator;
    private MoneyPoint _moneyPoint;
    private Transform _lastTarget;
    private Transform _firstTarget;
    private Transform _target;
    private Transform _exitTarget;
    private TargetWZ _targetWZ;
    private bool _isRunningTime;
    private float _maxWaitingSeconds;
    private float _waitingSeconds;
    private int _moneyToWithdraw;
    
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
        _maxWaitingSeconds = 80f;
        _waitingSeconds = 0f;
        _moneyToWithdraw = 10;
    }

    private void Update()
    {
        _waitingSeconds += Time.deltaTime;

        if (_waitingSeconds >= _maxWaitingSeconds)
            if (_isRunningTime == true)
                TakeMoney();

        if (_targetWZ == null)
            IsReady = false;
        else
            transform.position = _targetWZ.transform.position;

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

    public void StopTimer()
    {
        _isRunningTime = false;
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

    public void StopMove()
    {
        _movement.RemoveTarget();
        _movement.DisableAgent();

        if (_target != null)
            transform.position = _target.position;
    }

    public void InstantiateStackDollars()
    {
        Instantiate(_stackDollarsPrefab, transform.position, Quaternion.identity);
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

    public void GoToExit()
    {
        SetTarget(_exitTarget);
        IsExitReady = true;
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
        _animator.SitDown();
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
