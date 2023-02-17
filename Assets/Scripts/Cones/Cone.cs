using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(MeshCollider))]
public abstract class Cone : MonoBehaviour
{
    [SerializeField] private Dollar _dollarPrefab;

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
    private const float MaxSpeedRb = 0.1f;
    private const int ConeLayer = 9;
    private const int ConeUsedLayer = 10;

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
        IsCollision = false;
    }

    private void Update()
    {
        _waitingSeconds += Time.deltaTime;

        if (_rigidbody.velocity.magnitude < MaxSpeedRb && IsCollision == true)
            BlockPhysics();

        if (_waitingSeconds >= _maxWaitingSeconds)
            if (IsCollision == true)
                TakeMoney();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Car>(out Car car) || collision.gameObject.TryGetComponent<Cone>(out Cone cone))
        {
            IsCollision = true;
            _waitingSeconds = 0f;
        }
    }

    private void TakeMoney()
    {
        _moneyPoint.SpendMoney(_moneyToWithdraw);
        Destroy(this.gameObject);
    }

    protected void InstantiateDollar(Transform transform)
    {
        Dollar dollar = Instantiate(_dollarPrefab, transform.position, Quaternion.identity);
        dollar.SetMoneyPoint(_moneyPoint);
        dollar.StartMove();
    }

    public abstract void CreateDollar(Transform transform);

    public void UnlockPhysics()
    {
        gameObject.layer = ConeLayer;
        _rigidbody.isKinematic = false;
        _collider.isTrigger = false;
    }

    public void BlockPhysics()
    {
        gameObject.layer = ConeUsedLayer;
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }

    public void ResetState()
    {
        IsCollision = false;
    }

    public void SetMoneyToWithdraw(int value)
    {
        _moneyToWithdraw = value;
    }

    public void StartFallAnimation()
    {
        _animator.Play(Fall);
    }
}