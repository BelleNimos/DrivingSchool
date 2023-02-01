using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Cone : MonoBehaviour
{
    [SerializeField] private Dollar _dollarPrefab;

    private Dollar _dollar;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private BoxCollider _collider;

    protected WaitForSeconds WaitForSeconds;
    protected int CountDollars;

    private const string Fall = "Fall";

    private const float PowerFallDollar = 1f;
    private const float DurationFallDollar = 0.6f;
    private const int NumsFallsDollar = 1;

    private const float PowerFlightDollar = 1f;
    private const float DurationFlightDollar = 1f;
    private const int NumFlightsDollar = 1;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
        WaitForSeconds = new WaitForSeconds(0.6f);
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

    public void OffKinematic()
    {
        _rigidbody.isKinematic = false;
    }

    public void OffTrigger()
    {
        _collider.isTrigger = false;
    }
}