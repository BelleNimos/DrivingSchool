using UnityEngine;

[RequireComponent(typeof(CarMovement))]
public class Car : MonoBehaviour
{
    [SerializeField] private Seat _seat;
    [SerializeField] private Lane _lane;
    [SerializeField] private CarFirstPosition _firstPosition;

    private CarMovement _movement;
    private Customer _customer;

    private const float Delay = 2f;
    private const string StartMove = "StartMove";

    public bool IsFree { get; private set; }
    public bool IsFinish { get; private set; }

    private void Start()
    {
        _movement = GetComponent<CarMovement>();
        IsFinish = false;
    }

    private void Update()
    {
        if (_movement.IsReady == false)
        {
            transform.position = _firstPosition.transform.position;
            transform.rotation = _firstPosition.transform.rotation;
        }

        if (_customer != null)
        {
            IsFree = false;
            _customer.transform.position = _seat.transform.position;
            _customer.transform.rotation = _seat.transform.rotation;
            _customer.SitDown();
        }
        else
        {
            IsFree = true;
        }

        if (_lane.IsReady == true && IsFree == false)
            _movement.Invoke(StartMove, Delay);
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
                _customer.Finish();
                _customer = null;
            }
        }
    }

    public void AddCustomer(Customer customer)
    {
        _customer = customer;
    }
}
