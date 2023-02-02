using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingZone : MonoBehaviour
{
    [SerializeField] private List<TargetWZ> _targets;
    [SerializeField] private TMP_Text _countCustomersText;
    [SerializeField] private TMP_Text _maxCountCustomersText;
    [SerializeField] private Car _car;

    private List<Customer> _customers;

    private const float MinDistanceCustomerMovement = 0.1f;
    private const int MaxCustomersCount = 4;

    public int MaxCountCustomers => MaxCustomersCount;
    public int CurrentCustomersCount => _customers.Count;

    private void Start()
    {
        _customers = new List<Customer>();
    }

    private void Update()
    {
        _maxCountCustomersText.text = MaxCustomersCount.ToString();
        _countCustomersText.text = CurrentCustomersCount.ToString();

        if (CurrentCustomersCount > 0)
        {
            int index = Random.Range(0, CurrentCustomersCount);

            if (_customers[index].IsReady && _car.IsFree == true)
            {
                GiveAwayCustomer(_car, index);
            }
        }
    }

    private void GiveAwayCustomer(Car car, int index)
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            if (_customers[index].CheckPosition(_targets[i].transform))
            {
                _targets[i].Unlock();
                car.AddCustomer(_customers[index]);
                _customers[index].RemoveTargetWZ();
                _customers.RemoveAt(index);
                break;
            }
        }
    }

    public void SetTarget(Customer customer)
    {
        customer.SetMinDistance(MinDistanceCustomerMovement);

        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i].IsFree == true)
            {
                customer.SetTarget(_targets[i].transform);
                _customers.Add(customer);
                _targets[i].Block();
                break;
            }
        }
    }

    public void SetTargetCustomer(Transform transform, Customer customer)
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i].transform == transform)
            {
                customer.SetTargetWZ(_targets[i]);
                break;
            }
        }
    }
}
