using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(MeshCollider))]
public abstract class Cone : MonoBehaviour
{
    [SerializeField] private Dollar _dollarPrefab;
    [SerializeField] private WarningPoint _warningPoint;

    private Dollar _dollar;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private MeshCollider _collider;
    private MoneyPoint _moneyPoint;
    private float _maxWaitingSeconds;
    private float _waitingSeconds;
    private int _moneyToWithdraw;

    protected WaitForSeconds WaitForSeconds;
    protected int CountDollars;

    private const string Fall = "Fall";
    private const string BlockPhysicsText = "BlockPhysics";

    private const float PowerFallDollar = 1f;
    private const float DurationFallDollar = 0.6f;
    private const int NumsFallsDollar = 1;

    private const float PowerFlightDollar = 1f;
    private const float DurationFlightDollar = 1f;
    private const int NumFlightsDollar = 1;

    private const float Delay = 6f;

    public bool IsCollision { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<MeshCollider>();
        _moneyPoint = FindObjectOfType<MoneyPoint>();
        WaitForSeconds = new WaitForSeconds(0.6f);
        _maxWaitingSeconds = 50f;
        _waitingSeconds = 0f;
        _moneyToWithdraw = 1;
        IsCollision = false;
    }

    private void Update()
    {
        _waitingSeconds += Time.deltaTime;

        if (_waitingSeconds >= _maxWaitingSeconds)
            if (IsCollision == true)
                Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Car>(out Car car) || collision.gameObject.TryGetComponent<Cone>(out Cone cone))
        {
            IsCollision = true;
            _waitingSeconds = 0f;
            Invoke(BlockPhysicsText, Delay);
        }
    }

    private void TakeMoney()
    {
        _moneyPoint.SpendMoney(_moneyToWithdraw);
    }

    private void BlockPhysics()
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }

    protected void InstantiateDollar(MoneyPoint moneyPoint, Transform transform)
    {
        _dollar = Instantiate(_dollarPrefab, transform.position, Quaternion.identity);
        _dollar.StartMoveHorizontalAnimation();

        _dollar.transform.DOJump(_dollar.GetTarget(), PowerFallDollar, NumsFallsDollar, DurationFallDollar)
            .OnComplete(() =>
            {
                _dollar.StartMoveVerticalAnimation();

                _dollar.transform.DOJump(moneyPoint.transform.position, PowerFlightDollar, NumFlightsDollar, DurationFlightDollar)
                    .OnComplete(() =>
                    {
                        moneyPoint.AddDollar(_dollar);
                    }
                    );
            }
            );
    }

    public abstract void CreateDollar(MoneyPoint moneyPoint, Transform transform);

    public void StartFallAnimation()
    {
        _animator.Play(Fall);
    }

    public void UnlockPhysics()
    {
        _rigidbody.isKinematic = false;
        _collider.isTrigger = false;
    }

    public void ResetState()
    {
        IsCollision = false;
    }
}