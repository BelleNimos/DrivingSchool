using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CarMovement))]
public class Car : MonoBehaviour
{
    [SerializeField] private TMP_Text _drivesLeftCountText;
    [SerializeField] private TMP_Text _maxDrivesLeftCountText;
    [SerializeField] private Seat _seat;
    [SerializeField] private Lane _lane;
    [SerializeField] private Transform _firstPosition;
    [SerializeField] private Transform _finishTarget;

    private CarMovement _movement;
    private Customer _customer;
    private Transform _target;
    private int _drivesLeftCount;
    private bool _isCollision;
    private bool _isSuccessfully;

    private const int _maxDrivesLeftCount = 2;

    public bool IsFree { get; private set; }
    public bool IsFinish { get; private set; }
    public Transform Target => _target;

    private void Start()
    {
        _movement = GetComponent<CarMovement>();
        IsFinish = false;
        _target = _finishTarget;
        _drivesLeftCount = 0;
    }

    private void Update()
    {
        _drivesLeftCountText.text = _drivesLeftCount.ToString();
        _maxDrivesLeftCountText.text = _maxDrivesLeftCount.ToString();

        if (_drivesLeftCount < _maxDrivesLeftCount)
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
                IsFree = true;
            }

            if (IsFree == false && _lane.IsReady == true)
            {
                _movement.SetTarget(_target);
                _movement.StartMove();
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
        {
            _isCollision = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Finish>(out Finish finish))
        {
            if (_customer != null)
            {
                _target = _firstPosition;
                IsFinish = true;
            }

            if (_isCollision == true)
                _isSuccessfully = false;
            else
                _isSuccessfully = true;
        }

        if (collision.TryGetComponent<FirstPosition>(out FirstPosition firstPosition))
        {
            transform.position = firstPosition.transform.position;
            transform.rotation = firstPosition.transform.rotation;

            if (_isSuccessfully == true && _drivesLeftCount < _maxDrivesLeftCount)
            {
                _drivesLeftCount++;
            }

            if (IsFinish == true)
            {
                _target = _finishTarget;

                _lane.ResetState();
                
                IsFinish = false;
                _isCollision = false;

                _customer.Finished();
                _customer = null;

            }
        }
    }

    public void AddCustomer(Customer customer)
    {
        _customer = customer;
    }
}
