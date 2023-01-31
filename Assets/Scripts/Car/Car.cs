using UnityEngine;
using TMPro;

[RequireComponent(typeof(CarMovement))]
public class Car : MonoBehaviour
{
    [SerializeField] private TMP_Text _drivesLeftCountText;
    [SerializeField] private TMP_Text _maxDrivesLeftCountText;
    [SerializeField] private Seat _seat;
    [SerializeField] private Lane _lane;
    [SerializeField] private CarFirstPosition _firstPosition;

    private CarMovement _movement;
    private Customer _customer;
    private int _drivesLeftCount;
    private bool _isCollision;
    private bool _isSuccessfully;

    private const int MaxDrivesLeftCount = 2;

    public bool IsFree { get; private set; }
    public bool IsFinish { get; private set; }

    private void Start()
    {
        _movement = GetComponent<CarMovement>();
        _drivesLeftCount = 0;
        IsFinish = false;
    }

    private void Update()
    {
        _drivesLeftCountText.text = _drivesLeftCount.ToString();
        _maxDrivesLeftCountText.text = MaxDrivesLeftCount.ToString();

        if (_movement.IsReady == false)
        {
            transform.position = _firstPosition.transform.position;
            transform.rotation = _firstPosition.transform.rotation;
        }

        if (_drivesLeftCount < MaxDrivesLeftCount)
        {
            if (IsFree == false && _lane.IsReady == true)
                _movement.StartMove();

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
        }
        else
        {
            IsFree = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Cone>(out Cone cone))
            _isCollision = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<FinishLane>(out FinishLane finish))
        {
            if (_customer != null)
                IsFinish = true;

            if (_isCollision == true)
                _isSuccessfully = false;
            else
                _isSuccessfully = true;
        }

        if (collision.TryGetComponent<CarFirstPosition>(out CarFirstPosition firstPosition))
        {
            _movement.StopMove();

            if (_isSuccessfully == true && _drivesLeftCount < MaxDrivesLeftCount)
                _drivesLeftCount++;

            if (IsFinish == true)
            {
                _lane.ResetState();

                IsFinish = false;
                _isCollision = false;

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
