using UnityEngine;

[RequireComponent(typeof(CarMovement), typeof(CarSound))]
public class Car : MonoBehaviour
{
    [SerializeField] private Seat _seat;
    [SerializeField] private Lane _lane;
    [SerializeField] private CarFirstPosition _firstPosition;

    private CarSound _carSound;
    private CarMovement _movement;
    private Customer _customer;

    private const float Delay = 2f;
    private const string StartMoveText = "StartMove";

    public bool IsFree { get; private set; }
    public bool IsFinish { get; private set; }

    private void Start()
    {
        _movement = GetComponent<CarMovement>();
        _carSound = GetComponent<CarSound>();
        IsFinish = false;
    }

    private void Update()
    {
        if (_customer != null)
        {
            IsFree = false;
            _customer.transform.position = _seat.transform.position;
            _customer.transform.rotation = _seat.transform.rotation;
            _customer.SitDown();
        }
        else
        {
            _carSound.Stop();
            IsFree = true;
        }

        if (_lane.IsReady == true && IsFree == false && IsFinish == false)
            _movement.Invoke(StartMoveText, Delay);

        if (_movement.IsReady == false && _customer != null)
            _carSound.PlayIdle();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<FinishLane>(out FinishLane finish))
        {
            if (_customer != null)
                IsFinish = true;
        }

        if (collision.TryGetComponent<CarFirstPosition>(out CarFirstPosition firstPosition))
        {
            _movement.StopMove();

            if (IsFinish == true)
            {
                IsFinish = false;
                _customer.Finished();
                _customer = null;
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<CarFirstPosition>(out CarFirstPosition firstPosition))
        {
            if (_movement.IsReady == false)
            {
                transform.position = _firstPosition.transform.position;
                transform.rotation = _firstPosition.transform.rotation;
            }
        }
    }

    public void AddCustomer(Customer customer)
    {
        _customer = customer;
    }
}
