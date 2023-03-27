using UnityEngine;

[RequireComponent(typeof(CustomerMovement))]
public class Customer : MonoBehaviour
{
    private Transform _target;
    private CustomerFirstTarget _firstTarget;
    private CustomerLastTarget _lastTarget;
    private TargetWZ _targetWZ;
    private Exit _exitTarget;

    private CustomerMovement _movement;
    private CustomerAnimator _animator;
    private StackDollars _stackDollars;
    private MoneyPoint _moneyPoint;
    private int _moneyToWithdraw;
    private float _maxWaitingSeconds;
    private float _waitingSeconds;
    private bool _isRunningTime;

    public bool IsReadyDrive { get; private set; }
    public bool IsReadyExit { get; private set; }
    public bool IsFinish { get; private set; }

    private void Start()
    {
        _movement = GetComponent<CustomerMovement>();
        _animator = GetComponent<CustomerAnimator>();

        _target = _firstTarget.transform;
        _movement.SetTarget(_target);

        _targetWZ = null;
        _isRunningTime = true;
        IsReadyExit = false;
        IsFinish = false;
        IsReadyDrive = false;
        _waitingSeconds = 0f;
    }

    private void Update()
    {
        _waitingSeconds += Time.deltaTime;

        if (_waitingSeconds >= _maxWaitingSeconds)
            if (_isRunningTime == true)
                TakeMoney();

        if (_targetWZ == null)
            IsReadyDrive = false;

        if (IsFinish == true)
        {
            _target = _lastTarget.transform;
            _movement.SetTarget(_target);
        }
    }

    private void TakeMoney()
    {
        if (_moneyPoint.CurrentDollarsCount >= _moneyToWithdraw)
            _moneyPoint.SpendMoney(_moneyToWithdraw);

        GoToExit();
        _waitingSeconds = 0f;
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

    public void SetMoneyPoint(MoneyPoint moneyPoint)
    {
        _moneyPoint = moneyPoint;
    }

    public void SetLastTarget(CustomerLastTarget lastTarget)
    {
        _lastTarget = lastTarget;
    }

    public void SetFirstTarget(CustomerFirstTarget firstTarget)
    {
        _firstTarget = firstTarget;
    }

    public void SetTargetWZ(TargetWZ targetWZ)
    {
        _targetWZ = targetWZ;
    }

    public void SetExitTarget(Exit exitTarget)
    {
        _exitTarget = exitTarget;
    }

    public void InstantiateStackDollars()
    {
        StackDollars stackDollars = Instantiate(_stackDollars, transform.position, Quaternion.Euler(0f, 20f, 0f));
        stackDollars.SetMoneyPoint(_moneyPoint);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _movement.SetTarget(_target);
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
        SetTarget(_exitTarget.transform);
        IsReadyExit = true;
        IsFinish = false;
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

    public void ReadyDrive()
    {
        IsReadyDrive = true;
    }

    public void Finished()
    {
        IsFinish = true;
    }
}
