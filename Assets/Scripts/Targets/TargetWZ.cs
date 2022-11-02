using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWZ : MonoBehaviour
{
    private Customer _customer;

    public bool IsFree { get; private set; }

    private void Update()
    {
        if (_customer != null)
        {
            IsFree = false;
            _customer.transform.position = transform.position;
        }
        else
        {
            IsFree = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Customer>(out Customer customer))
        {
            if (gameObject.transform == customer.Target)
                _customer = customer;
        }
    }

    public Customer GetCustomer()
    {
        return _customer;
    }

    public void RemoveCustomer()
    {
        _customer = null;
    }
}
