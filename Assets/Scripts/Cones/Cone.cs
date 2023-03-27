using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(MeshCollider))]
public abstract class Cone : MonoBehaviour
{
    [SerializeField] private AudioSource _addSound;
    [SerializeField] private AudioSource _fallSound;
    [SerializeField] private AudioSource _collisionSound;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private MeshCollider _collider;
    private MoneyPoint _moneyPoint;
    private float _waitingSeconds;

    private const string Fall = "Fall";
    private const float MaxWaitingSeconds = 50f;
    private const float MaxSpeedRb = 0.1f;
    private const int ConeLayer = 9;
    private const int ConeUsedLayer = 10;

    public abstract int CountDollars { get; }
    public abstract int Index { get; }
    public bool IsCollision { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<MeshCollider>();
        _waitingSeconds = 0f;
        IsCollision = false;
    }

    private void Update()
    {
        _waitingSeconds += Time.deltaTime;

        if (_waitingSeconds >= MaxWaitingSeconds)
            if (IsCollision == true)
                TakeMoney();

        if (_rigidbody.velocity.magnitude < MaxSpeedRb && IsCollision == true)
            BlockPhysics();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Car>(out Car car) || collision.gameObject.TryGetComponent<Cone>(out Cone cone))
        {
            _collisionSound.Play();
            IsCollision = true;
            _waitingSeconds = 0f;
        }
    }

    private void TakeMoney()
    {
        if (_moneyPoint.CurrentDollarsCount >= CountDollars)
            _moneyPoint.SpendMoney(CountDollars);
        
        Destroy(gameObject);
    }

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

    public void SetMoneyPoint(MoneyPoint moneyPoint)
    {
        _moneyPoint = moneyPoint;
    }

    public void StartFallAnimation()
    {
        _animator.Play(Fall);
    }

    public void PlayAddSound()
    {
        _addSound.Play();
    }

    public void PlayFallSound()
    {
        _fallSound.Play();
    }
}